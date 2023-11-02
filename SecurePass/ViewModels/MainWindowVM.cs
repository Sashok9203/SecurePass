using data_access.Data;
using data_access.Entities;
using data_access.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Win32;
using SecurePass.Common;
using SecurePass.ViewModels.EntitiesVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SecurePass.ViewModels
{
    internal class MainWindowVM : BaseViewModel
    {
        public int SelectedIndex;
        private string[] filterCategories = { "All", "Bank Accounts", "Contacts", "Credit Cards", "Databases", "Servers", "WiFies", "Emails", "Passwords","Logins","Notes" };
        public IEnumerable<string> FilterTypes => filterCategories;
        private bool isMainWindowEnabled, 
            isFirstStart, 
            isSelected, 
            isAddEditCategoryWindowEnabled,
            isAddObjectWindowEnabled;
        private readonly UnitOfWork repository;
        private UserVM? currentUser;
        private string? findString;
        private CategoryVM? selectedCategory,categoryInObjectView;
        private List<SecureObjectVM> secureObjects = new();
        private SecureObjectVM? selectedSecureObject, secureObjectEdit;
        private BaseEntityVM? createdObject;

        private void setCategoryElementsCount(CategoryVM categoryVM)
        {
            if (categoryVM.Id == -1) categoryVM.ElementsCount = secureObjects.Count;
            else if (categoryVM.Id == -2) categoryVM.ElementsCount = secureObjects.Where(x => x.IsFavorit).Count();
            else categoryVM.ElementsCount = secureObjects.Where(x=>x.CategoryId == categoryVM.Id).Count();
        }

        private bool saveButtonEnabler()
        {
            if (NewEditObject is CategoryVM) return !string.IsNullOrWhiteSpace((NewEditObject as CategoryVM)?.Name);
            if (NewEditObject is SecureObjectVM)
                return !string.IsNullOrWhiteSpace((NewEditObject as SecureObjectVM).Title) &&
                NewEditObject switch 
                { 
                    ContactVM contactVM => !string.IsNullOrWhiteSpace(contactVM?.Name),
                    BankAccountVM bankAccountVM => !string.IsNullOrWhiteSpace(bankAccountVM?.Name) && !string.IsNullOrWhiteSpace(bankAccountVM?.OwnerName),
                    CreditCardVM creditCardVM => !string.IsNullOrWhiteSpace(creditCardVM?.Type) && !string.IsNullOrWhiteSpace(creditCardVM?.OwnerName),
                    DataBaseVM dataBaseVM => !string.IsNullOrWhiteSpace(dataBaseVM?.Name),
                    EmailVM emailVM => !string.IsNullOrWhiteSpace(emailVM?.Name),
                    ServerVM serverVM => !string.IsNullOrWhiteSpace(serverVM?.Name) && !string.IsNullOrWhiteSpace(serverVM?.URL) && !string.IsNullOrWhiteSpace(serverVM?.Password),
                    UniversalVM universalVM => !string.IsNullOrWhiteSpace(universalVM?.Value) && !string.IsNullOrWhiteSpace(universalVM?.Label),
                    WiFiVM wiFiVM => !string.IsNullOrWhiteSpace(wiFiVM?.NetworkName)
                };
            return false;
        }

        private void clearData()
        {
            createdObject = null;
            isMainWindowEnabled = false;
            isFirstStart = false;
            secureObjects.Clear();
            UserCategories.Clear();
            UserLogin = string.Empty;
            UserPassword = string.Empty;
            CurrentUser = null;
            SelectedCategory = null;
            SelectedSecureObject = null;
            findString = string.Empty;
            RegistryUtility.DeleteInfoFromRegistry();
        }

        private async Task setObjectToDataBase(BaseEntityVM? vm)
        {
            if (vm == null) return; 
            switch (vm)
            {
                case UserVM userVM:
                    if (userVM.Id == 0)
                    {
                        User? user = new();
                        userVM.CopyToEntity(user);
                        await repository.Users.InsertAsync(user);
                        await repository.SaveAsync();
                        user = repository.Users.FirstOrDefault(x => x.NikName == userVM.NikName);
                        if(user != null)
                             userVM = new(user);
                    }
                    else
                    {
                        User? user = await repository.Users.GetByIDAsync(userVM.Id);
                        userVM.CopyToEntity(user);
                    }
                    break;
                case CategoryVM categoryVM:
                    if (categoryVM.Id == 0)
                    {
                        var category = new Category();
                        categoryVM.CopyToEntity(category);
                        await repository.Categories.InsertAsync(category);
                        await repository.SaveAsync();
                        var newCategory = repository.Categories.Get(x => x.UserId == CurrentUser.Id && !UserCategories.Any(y => y.Id == x.Id)).First();
                        UserCategories.Add(new(newCategory));
                    }
                    else
                    {
                        Category? category = await repository.Categories.GetByIDAsync(categoryVM.Id);
                        categoryVM.CopyToEntity(category);
                    }
                    break;
                case WiFiVM wifiVM:
                    if (wifiVM.Id == 0)
                    {
                        var wifi = new WiFi();
                        wifiVM.CopyToEntity(wifi);
                        await repository.WiFis.InsertAsync(wifi);
                        await repository.SaveAsync();
                        var localWifisId = secureObjects.OfType<WiFiVM>()
                                                                       .Where(x => x.CategoryId == wifiVM.CategoryId)
                                                                       .Select(x => x.Id);
                        wifiVM.Id = repository.WiFis.Get(x => x.CategoryId == wifiVM.CategoryId).First(y => !localWifisId.Any(x => x == y.Id)).Id;
                    }
                    else
                    {
                        WiFi? wifi = await repository.WiFis.GetByIDAsync(wifiVM.Id);
                        wifiVM.CopyToEntity(wifi);
                    }
                    break;
                case UniversalVM universalVM:
                    if (universalVM.Id == 0)
                    {
                        var universal = new Universal();
                        universalVM.CopyToEntity(universal);
                        await repository.Universals.InsertAsync(universal);
                        await repository.SaveAsync();
                        var localUniversalsId = secureObjects.OfType<UniversalVM>()
                                                                       .Where(x => x.CategoryId == universalVM.CategoryId)
                                                                       .Select(x => x.Id);
                        universalVM.Id = repository.Universals.Get(x => x.CategoryId == universalVM.CategoryId).First(y => !localUniversalsId.Any(x => x == y.Id)).Id;
                    }
                    else
                    {
                        Universal? universal = await repository.Universals.GetByIDAsync(universalVM.Id);
                        universalVM.CopyToEntity(universal);
                    }
                    break;
                case ServerVM serverVM:
                    if (serverVM.Id == 0)
                    {
                        var server = new Server();
                        serverVM.CopyToEntity(server);
                        await repository.Servers.InsertAsync(server);
                        await repository.SaveAsync();
                        var localServersId = secureObjects.OfType<ServerVM>()
                                                                       .Where(x => x.CategoryId == serverVM.CategoryId)
                                                                       .Select(x => x.Id);
                        serverVM.Id = repository.Servers.Get(x => x.CategoryId == serverVM.CategoryId).First(y => !localServersId.Any(x => x == y.Id)).Id;
                    }
                    else
                    {
                        Server? server = await repository.Servers.GetByIDAsync(serverVM.Id);
                        serverVM.CopyToEntity(server);
                    }
                    break;
                case EmailVM emailVM:
                    if (emailVM.Id == 0)
                    {
                        var email = new Email();
                        emailVM.CopyToEntity(email);
                        await repository.Emails.InsertAsync(email);
                        await repository.SaveAsync();
                        var localEmailsId = secureObjects.OfType<EmailVM>()
                                                                       .Where(x => x.CategoryId == emailVM.CategoryId)
                                                                       .Select(x=>x.Id);
                        emailVM.Id = repository.Emails.Get(x => x.CategoryId == emailVM.CategoryId).First(y => !localEmailsId.Any(x => x == y.Id)).Id;
                    }
                    else
                    {
                        Email? email = await repository.Emails.GetByIDAsync(emailVM.Id);
                        emailVM.CopyToEntity(email);
                    }
                    break;
                case DataBaseVM dataBaseVM:
                    if (dataBaseVM.Id == 0)
                    {
                        var dataBase = new DataBase();
                        dataBaseVM.CopyToEntity(dataBase);
                        await repository.DataBases.InsertAsync(dataBase);
                        await repository.SaveAsync();
                        var localDataBasesId = secureObjects.OfType<DataBaseVM>()
                                                                       .Where(x => x.CategoryId == dataBaseVM.CategoryId)
                                                                       .Select(x => x.Id);
                        dataBaseVM.Id = repository.DataBases.Get(x => x.CategoryId == dataBaseVM.CategoryId)
                                                            .First(y => !localDataBasesId
                                                            .Any(x => x == y.Id)).Id;
                    }
                    else
                    {
                        DataBase? dataBase = await repository.DataBases.GetByIDAsync(dataBaseVM.Id);
                        dataBaseVM.CopyToEntity(dataBase);
                    }
                    break;
                case CreditCardVM creditCardVM:
                    if (creditCardVM.Id == 0)
                    {
                        var creditCard = new CreditCard();
                        creditCardVM.CopyToEntity(creditCard);
                        await repository.CreditCards.InsertAsync(creditCard);
                        await repository.SaveAsync();
                        var localCreditCardsId = secureObjects.OfType<CreditCardVM>()
                                                                       .Where(x => x.CategoryId == creditCardVM.CategoryId)
                                                                       .Select(x => x.Id);
                        creditCardVM.Id = repository.CreditCards.Get(x => x.CategoryId == creditCardVM.CategoryId)
                                                            .First(y => !localCreditCardsId
                                                            .Any(x => x == y.Id)).Id;
                    }
                    else
                    {
                        CreditCard? dataBase = await repository.CreditCards.GetByIDAsync(creditCardVM.Id);
                        creditCardVM.CopyToEntity(dataBase);
                    }
                    break;
                case ContactVM contactVM:
                    if (contactVM.Id == 0)
                    {
                        var contact = new Contact();
                        contactVM.CopyToEntity(contact);
                        await repository.Contacts.InsertAsync(contact);
                        await repository.SaveAsync();
                        var localContactsId = secureObjects.OfType<ContactVM>()
                                                                       .Where(x => x.CategoryId == contactVM.CategoryId)
                                                                       .Select(x => x.Id);
                        contactVM.Id = repository.Contacts.Get(x => x.CategoryId == contactVM.CategoryId)
                                                            .First(y => !localContactsId
                                                            .Any(x => x == y.Id)).Id;
                    }
                    else
                    {
                        Contact? contact = await repository.Contacts.GetByIDAsync(contactVM.Id);
                        contactVM.CopyToEntity(contact);
                    }
                    break;
                case BankAccountVM bankAccountVM:
                    if (bankAccountVM.Id == 0)
                    {
                        var bankAccount = new BankAccount();
                        bankAccountVM.CopyToEntity(bankAccount);
                        await repository.BankAccounts.InsertAsync(bankAccount);
                        await repository.SaveAsync();
                        var localBankAccountsId = secureObjects.OfType<BankAccountVM>()
                                                                       .Where(x => x.CategoryId == bankAccountVM.CategoryId)
                                                                       .Select(x => x.Id);
                        bankAccountVM.Id = repository.BankAccounts.Get(x => x.CategoryId == bankAccountVM.CategoryId)
                                                            .First(y => !localBankAccountsId
                                                            .Any(x => x == y.Id)).Id;
                    }
                    else
                    {
                        BankAccount? bankAccount = await repository.BankAccounts.GetByIDAsync(bankAccountVM.Id);
                        bankAccountVM.CopyToEntity(bankAccount);
                    }
                    break;
            }
            await repository.SaveAsync();
        }

        private CategoryVM[] staticCategoryButtons =
        {
            new(new(){ Name = "AllElements",Id = -1}){ IsSelected = true  },
            new(new(){ Name = "Favorit",Id = -2})
        };

        private async Task deleteObjectFromDataBase(BaseEntityVM? o)
        {
            if (o == null) return;
            switch (o)
            {
                case UserVM userVM:
                    repository.Users.Delete(userVM.Id);
                    RegistryUtility.DeleteInfoFromRegistry();
                    clearData();
                    break;
                case CategoryVM categoryVM:
                    repository.Categories.Delete(categoryVM.Id);
                    var toDelete = secureObjects.Where(x => x.CategoryId == categoryVM.Id).ToArray();
                    foreach (var item in toDelete)
                        secureObjects.Remove(item);
                    UserCategories.Remove(categoryVM);
                    setCategoryElementsCount(staticCategoryButtons[0]);
                    setCategoryElementsCount(staticCategoryButtons[1]);
                    break;
                case WiFiVM wifiVM:
                    repository.WiFis.Delete(wifiVM.Id);
                    break;
                case UniversalVM universalVM:
                    repository.Universals.Delete(universalVM.Id);
                    break;
                case ServerVM serverlVM:
                    repository.Servers.Delete(serverlVM.Id);
                    break;
                case EmailVM emailVM:
                    repository.Emails.Delete(emailVM.Id);
                    break;
                case DataBaseVM dataBaseVM:
                    repository.DataBases.Delete(dataBaseVM.Id);
                    break;
                case CreditCardVM creditCardVM:
                    repository.CreditCards.Delete(creditCardVM.Id);
                    break;
                case ContactVM contacVM:
                    repository.Contacts.Delete(contacVM.Id);
                    break;
                case BankAccountVM bankAccountVM:
                    repository.BankAccounts.Delete(bankAccountVM.Id);
                    break;
            }
            await repository.SaveAsync();
            if (o is SecureObjectVM secureObject)
            {
                if (SelectedSecureObject == secureObject)
                    secureObjectSelectionClear();
                secureObjects.Remove(secureObject);
                UserCategories.First(x=>x.Id == secureObject.CategoryId).ElementsCount--;
                staticCategoryButtons[0].ElementsCount--;
                if (secureObject.IsFavorit) staticCategoryButtons[1].ElementsCount--;
            }
            OnPropertyChanged(nameof(SecureObjects));
        }

        private void createEditObject(BaseEntityVM? o)
        {
            if (o == null) return;
            switch (o)
            {
                case UserVM userVM:

                    break;
                case CategoryVM categoryVM:
                    if (categoryVM.Id == 0)
                        NewEditObject = new CategoryVM() {UserId = CurrentUser?.Id ?? 0 ,Name = categoryVM.Name};
                    else NewEditObject = categoryVM.Clone() as CategoryVM;
                    secureObjectSelectionClear();
                    IsAddEditCategoryWindowEnabled = true;
                    break;
                case WiFiVM wifiVM:
                    if (wifiVM.Id == 0)
                        NewEditObject = new WiFiVM() { Title = wifiVM.Title};
                    else NewEditObject = wifiVM.Clone() as WiFiVM;
                    break;
                case UniversalVM universalVM:
                    if (universalVM.Id == 0)
                        NewEditObject = new UniversalVM() { Title = universalVM.Title,TypeId = universalVM.TypeId };
                    else NewEditObject = universalVM.Clone() as UniversalVM;
                    break;
                case ServerVM serverVM:
                    if (serverVM.Id == 0)
                        NewEditObject = new ServerVM() { Title = serverVM.Title }; 
                    else NewEditObject = serverVM.Clone() as ServerVM;
                    break;
                case EmailVM emailVM:
                    if (emailVM.Id == 0)
                        NewEditObject = new EmailVM() { Title = emailVM.Title };
                    else NewEditObject = emailVM.Clone() as EmailVM;
                    break;
                case DataBaseVM dataBaseVM:
                    if (dataBaseVM.Id == 0)
                        NewEditObject = new DataBaseVM() { Title = dataBaseVM.Title };
                    else NewEditObject = dataBaseVM.Clone() as DataBaseVM;
                    break;
                case CreditCardVM creditCardVM:
                    if (creditCardVM.Id == 0)
                        NewEditObject =new CreditCardVM() { Title = creditCardVM.Title };
                    else NewEditObject = creditCardVM.Clone() as CreditCardVM;
                    break;
                case ContactVM contactVM:
                    if (contactVM.Id == 0)
                        NewEditObject = new ContactVM() { Title = contactVM.Title };
                    else NewEditObject = contactVM.Clone() as ContactVM;
                    break;
                case BankAccountVM bankAccountVM:
                    if (bankAccountVM.Id == 0)
                        NewEditObject = new BankAccountVM() { Title = bankAccountVM.Title };
                    else NewEditObject = bankAccountVM.Clone() as BankAccountVM;
                    break;
            }
            if (o is SecureObjectVM secureObject)
            {
                if (SelectedSecureObject != secureObject)
                    secureObjectSelected(secureObject);
                SecureObjectEdit = NewEditObject as SecureObjectVM;
                SecureObjectEdit.IsEditable = true;
                if (SecureObjectEdit.Id == 0)
                {
                    SecureObjectEdit.CategoryId = 1;
                    CategoryInObjectView = UserCategories[0];
                    IsAddObjectWindowEnabled = false;
                }
            }
        }

        private async Task saveObject()
        {
            await setObjectToDataBase(NewEditObject);
            switch (NewEditObject)
            {
                case UserVM userVM:
                    CurrentUser = userVM;
                    break;
                case CategoryVM categoryVM:
                    for (int i = 0; i < UserCategories.Count; i++)
                    {
                        if (UserCategories[i].Id == categoryVM.Id)
                        {
                            UserCategories[i] = categoryVM;
                            break;
                        }
                    }
                    IsAddEditCategoryWindowEnabled = false;
                    break;
                case SecureObjectVM secureObjectVM:
                    secureObjectVM.IsEditable = false;
                    var newObject = secureObjects.FirstOrDefault(x => x.GetType() == secureObjectVM.GetType() && x.Id == secureObjectVM.Id);
                    if (newObject != null)
                    {
                        for (int i = 0; i < secureObjects.Count; i++)
                        {
                            if (secureObjects[i] == newObject)
                            {
                                int currentId = secureObjects[i].CategoryId;
                                secureObjects[i] = secureObjectVM;
                                if (currentId != secureObjectVM.CategoryId)
                                {
                                    setCategoryElementsCount(UserCategories.First(x => x.Id == currentId));
                                    setCategoryElementsCount(UserCategories.First(x => x.Id == secureObjectVM.CategoryId));
                                }
                                SelectedSecureObject = secureObjectVM;
                                break;
                            }
                        }
                    }
                    else
                    {
                        secureObjects.Add(secureObjectVM);
                        UserCategories.First(x=>x.Id == secureObjectVM.CategoryId).ElementsCount++;
                        staticCategoryButtons[0].ElementsCount++;
                        secureObjectSelected(secureObjectVM);
                    }
                    OnPropertyChanged(nameof(SecureObjects));
                    break;
            }
            NewEditObject = null;
        }

        private void cancel()
        {
            switch (NewEditObject)
            {
                case UserVM userVM:

                    break;
                case CategoryVM categoryVM:
                    IsAddEditCategoryWindowEnabled = false;
                    break;
                case SecureObjectVM secureObjectVM:
                    if (secureObjectVM.Id != 0)
                    {
                        secureObjectVM.IsEditable = false;
                        SecureObjectEdit = SelectedSecureObject;
                        CategoryInObjectView = UserCategories.First(x => x.Id == SelectedSecureObject?.CategoryId);
                    }
                    else SecureObjectEdit = null;
                    break;
            }
            NewEditObject = null;
        }

        private void categorySelected(object o)
        {
            if (o is CategoryVM category)
            {
                if (SelectedCategory != null)
                    SelectedCategory.IsSelected = false;
                SelectedCategory = category;
                SelectedCategory.IsSelected = true;
                secureObjectSelectionClear();
            }
        }

        private void secureObjectSelectionClear()
        {
            if (SelectedSecureObject != null)
            {
                SelectedSecureObject.IsSelected = false;
                SelectedSecureObject = null;
                SecureObjectEdit = null;
            }
        }

        private void secureObjectSelected(object o)
        {
            if (o is SecureObjectVM secureObject)
            {
                if (SelectedSecureObject != null )
                    SelectedSecureObject.IsSelected = false;
                SelectedSecureObject = secureObject;
                SelectedSecureObject.IsSelected = true;
                SecureObjectEdit = SelectedSecureObject;
                CategoryInObjectView = UserCategories.FirstOrDefault(x => x.Id == SecureObjectEdit.CategoryId);
                CategoryInObjectView ??= UserCategories[0];
            }
        }
        
        private bool secureElementFilter(SecureObjectVM so)
        {
            bool condition = SelectedIndex switch
            {
                0 => true,
                1 => so is BankAccountVM,
                2 => so is ContactVM,
                3 => so is CreditCardVM,
                4 => so is DataBaseVM,
                5 => so is ServerVM,
                6 => so is WiFiVM,
                7 => so is EmailVM,
                8 => so is UniversalVM universal && universal.TypeId == 1,
                9 => so is UniversalVM universal && universal.TypeId == 2,
                10 => so is UniversalVM universal && universal.TypeId == 3,
                _ => throw new NotImplementedException()
            };
            bool strFindCondition = string.IsNullOrWhiteSpace(FindString) || so.Title.ToLower().Contains(FindString.ToLower());
            if (SelectedCategory?.Id == -1 && strFindCondition && condition) return true;
            else return (so.CategoryId == SelectedCategory?.Id) && strFindCondition && condition;
        }

        private bool isUserLoginInfoExist() => !string.IsNullOrWhiteSpace(UserLogin) && !string.IsNullOrWhiteSpace(UserPassword);

        // Create new account logic
        private async Task CreateNewAccClick()
        {
            if (repository.Users.Any(x => x.NikName == UserLogin))
                MessageBox.Show("This login is already in use...", "Server information");
            else
            {
                CurrentUser = new()
                {
                    NikName = UserLogin,
                    PasswordHash = Utility.GetHash(UserPassword)
                };
                await setObjectToDataBase(CurrentUser);
                await repository.SaveAsync();
                RegistryUtility.SetLoginToRegistry(UserLogin);
                IsMainWindowEnabled = true;
                var test = Resource.ResourceManager.GetResourceSet(System.Globalization.CultureInfo.CurrentUICulture, true, true);

            }
        }

        // Login logic
        private async Task LoginClick()
        {
            var user = await repository.Users.FirstOrDefaultAsync(x => x.NikName == UserLogin);
            if (user != null) CurrentUser = new(user);
            if (CurrentUser == null)
                MessageBox.Show("User with this login is not registered...", "Server information");
            else
            {
                if (Utility.GetHash(UserPassword) != CurrentUser.PasswordHash)
                    MessageBox.Show("Invalid password...", "Server information");
                else
                {
                    var categories = repository.Categories.Get(x => x.UserId == CurrentUser.Id,includeProperties: "Universals,CreditCards,Emails,Servers,DataBases,BankAccounts,WiFis,Contacts");
                    foreach (var item in categories)
                    {
                        UserCategories.Add(new(item));
                        secureObjects.AddRange(item.Emails.Select(x => new EmailVM(x)));
                        secureObjects.AddRange(item.WiFis.Select(x => new WiFiVM(x)));
                        secureObjects.AddRange(item.Universals.Select(x => new UniversalVM(x)));
                        secureObjects.AddRange(item.Servers.Select(x => new ServerVM(x)));
                        secureObjects.AddRange(item.CreditCards.Select(x => new CreditCardVM(x)));
                        secureObjects.AddRange(item.Contacts.Select(x => new ContactVM(x)));
                        secureObjects.AddRange(item.BankAccounts.Select(x => new BankAccountVM(x)));
                        secureObjects.AddRange(item.DataBases.Select(x => new DataBaseVM(x)));
                    }
                    SelectedCategory = staticCategoryButtons[0];
                    setCategoryElementsCount(staticCategoryButtons[1]);
                    setCategoryElementsCount(staticCategoryButtons[0]);
                    RegistryUtility.SetLoginToRegistry(UserLogin);
                    IsMainWindowEnabled = true;
                }
            }
        }

        public MainWindowVM()
        {
            repository = new();
            UserLogin = RegistryUtility.TryGetLogin();
            IsFirstStart = UserLogin == string.Empty;
        }
        //Selected Filter
        public int SelectedFilterIndex
        {
            get => SelectedIndex;
            set
            {
                SelectedIndex = value;
                OnPropertyChanged(nameof(SecureObjects));
            }
        }
        // Filter string
        public string FindString
        {
            get => findString;
            set
            {
                findString = value;
                secureObjectSelectionClear();
                OnPropertyChanged(nameof(SecureObjects));
            }
        }

        // First start program  flag
        public bool IsFirstStart
        {
            get => isFirstStart;
            set
            {
                isFirstStart = value;
                OnPropertyChanged();
            }
        }

        // Show/Hide add/edit category window
        public bool IsAddEditCategoryWindowEnabled
        {
            get => isAddEditCategoryWindowEnabled;
            set
            {
                isAddEditCategoryWindowEnabled = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(MainWindowBlocked));
            }
        }

        // Switch main window <-> registration/authorization window
        public bool IsMainWindowEnabled
        {
            get => isMainWindowEnabled;
            set
            {
                isMainWindowEnabled = value;
                OnPropertyChanged();
            }
        }

        public bool IsAddObjectWindowEnabled
        {
            get => isAddObjectWindowEnabled;
            set
            {
                isAddObjectWindowEnabled = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(MainWindowBlocked));
            }
        }

        // Object was user select
        public SecureObjectVM? SelectedSecureObject
        {
            get => selectedSecureObject;
            set
            {
                selectedSecureObject = value;
                OnPropertyChanged();
            }
        }

        public SecureObjectVM? SecureObjectEdit
        {
            get => secureObjectEdit;
            set
            {
                secureObjectEdit = value;
                OnPropertyChanged();
            }
        }

        public bool IsSelected
        {
            get => isSelected;
            set
            {
                isSelected = value;
                OnPropertyChanged();
            }
        }

        public bool MainWindowBlocked => IsAddEditCategoryWindowEnabled || IsAddObjectWindowEnabled;

        // Category was user select
        public CategoryVM? SelectedCategory
        {
            get => selectedCategory;
            set
            {
                selectedCategory = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(SecureObjects));
            }
        }

        public CategoryVM? CategoryInObjectView
        {
            get => categoryInObjectView;
            set
            {
                categoryInObjectView = value;
                OnPropertyChanged();
                if (SecureObjectEdit?.IsEditable == true)
                {
                    SecureObjectEdit.CategoryId = value?.Id ?? 0;
                    OnPropertyChanged(nameof(SecureObjects));
                }
            }
        }
        
        // Created category 
        public BaseEntityVM? NewEditObject
        {
            get => createdObject;
            set
            {
                createdObject = value;
                OnPropertyChanged();
            }
        }

        // Authorized user
        public UserVM? CurrentUser
        {
            get => currentUser;
            set
            {
                currentUser = value;
                OnPropertyChanged();
            }
        }

        // Static category buttons
        public IEnumerable<CategoryVM> StaticCategoryButtons => staticCategoryButtons;

        // Сollection of user  SecureOblects
        public IEnumerable<SecureObjectVM> SecureObjects => secureObjects.Where(x => secureElementFilter(x)).ToArray();

        // Сollection of user Categories
        public ObservableCollection<CategoryVM> UserCategories { get; set; } = new();

        // User login value 
        public string UserLogin { get; set; } = string.Empty;

        // User password value 
        public string UserPassword { get; set; } = string.Empty;

        public RelayCommand LoginButtonClick => new(async (o) => await LoginClick(), (o) => isUserLoginInfoExist());
        public RelayCommand CreateNewAccButtonClick => new(async (o) => await CreateNewAccClick(), (o) => isUserLoginInfoExist());
        public RelayCommand CategorySelected => new((o) => categorySelected(o));
        public RelayCommand SecureObjectSelected => new((o) => secureObjectSelected(o));
        public RelayCommand Cancel => new((o) => cancel());
        public RelayCommand SaveObject => new(async (o) => await saveObject(), (o) => saveButtonEnabler());
        public RelayCommand AddEditObject => new((o) => createEditObject(o as BaseEntityVM));
        public RelayCommand DeleteObject => new(async (o) => await deleteObjectFromDataBase(o as BaseEntityVM));
        public RelayCommand AddNewObject => new( (o) => IsAddObjectWindowEnabled = !IsAddObjectWindowEnabled);

    }
}
