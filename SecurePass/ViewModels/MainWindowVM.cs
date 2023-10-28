using data_access.Data;
using data_access.Entities;
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
        private bool isMainWindowEnabled, isFirstStart, isSelected, isAddEditCategoryWindowEnabled;
        private readonly SecurePassDBContext dbContext;
        private UserVM? currentUser;
        private string findString;
        private CategoryVM? selectedCategory;
        private List<SecureObjectVM> secureObjects = new();
        private SecureObjectVM? selectedSecureObject;
        private List<int> categoriesId = new();
        private BaseEntityVM createdObject;

        private void clearData()
        {
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
        }

        private async Task setEntityVMToDataBase(object vm)
        {
            switch (vm)
            {
                case UserVM userVM:
                    if (userVM.Id == 0)
                    {
                        User? user = new();
                        userVM.CopyToEntity(user);
                        await dbContext.Users.AddAsync(user);
                        await dbContext.SaveChangesAsync();
                        user = dbContext.Users.First(x => x.NikName == userVM.NikName);
                        CurrentUser = new(user);
                    }
                    else
                    {
                        User? user = dbContext.Users.Find(userVM.Id);
                        userVM.CopyToEntity(user);
                    }
                    break;
                case CategoryVM categoryVM:
                    if (categoryVM.Id == 0)
                    {
                        var category = new Category();
                        categoryVM.CopyToEntity(category);
                        await dbContext.Categories.AddAsync(category);
                        await dbContext.SaveChangesAsync();
                        var newCategory = dbContext.Categories.Where(x => x.UserId == CurrentUser.Id && !categoriesId.Any(y => y == x.Id)).First();
                        categoriesId.Add(newCategory.Id);
                        UserCategories.Add(new(newCategory));
                    }
                    else
                    {
                        Category? category = dbContext.Categories.Find(categoryVM.Id);
                        categoryVM.CopyToEntity(category);
                    }
                    break;
                case WiFiVM wifiVM:
                    if (wifiVM.Id == 0)
                    {

                    }
                    else
                    {
                        WiFi? wifi = dbContext.WiFis.Find(wifiVM.Id);
                        wifiVM.CopyToEntity(wifi);
                    }
                    break;
                case UniversalVM universalVM:
                    if (universalVM.Id == 0)
                    {

                    }
                    else
                    {
                        Universal? universal = dbContext.Universals.Find(universalVM.Id);
                        universalVM.CopyToEntity(universal);
                    }
                    break;
                case ServerVM serverlVM:
                    if (serverlVM.Id == 0)
                    {

                    }
                    else
                    {
                        Server? server = dbContext.Servers.Find(serverlVM.Id);
                        serverlVM.CopyToEntity(server);
                    }
                    break;
                case EmailVM emailVM:
                    if (emailVM.Id == 0)
                    {

                    }
                    else
                    {
                        Email? email = dbContext.Emails.Find(emailVM.Id);
                        emailVM.CopyToEntity(email);
                    }
                    break;
                case DataBaseVM dataBaseVM:
                    if (dataBaseVM.Id == 0)
                    {

                    }
                    else
                    {
                        DataBase? dataBase = dbContext.DataBases.Find(dataBaseVM.Id);
                        dataBaseVM.CopyToEntity(dataBase);
                    }
                    break;
                case CreditCardVM creditCardVM:
                    if (creditCardVM.Id == 0)
                    {

                    }
                    else
                    {
                        CreditCard? dataBase = dbContext.CreditCards.Find(creditCardVM.Id);
                        creditCardVM.CopyToEntity(dataBase);
                    }
                    break;
                case ContactVM contactVM:
                    if (contactVM.Id == 0)
                    {

                    }
                    else
                    {
                        Contact? contact = dbContext.Contacts.Find(contactVM.Id);
                        contactVM.CopyToEntity(contact);
                    }
                    break;
                case BankAccountVM bankAccountVM:
                    if (bankAccountVM.Id == 0)
                    {

                    }
                    else
                    {
                        BankAccount? bankAccount = dbContext.BankAccounts.Find(bankAccountVM.Id);
                        bankAccountVM.CopyToEntity(bankAccount);
                    }
                    break;
            }

            await dbContext.SaveChangesAsync();
        }

        private CategoryVM[] staticCategoryButtons =
        {
            new(new(){ Name = "AllElements",Id = -1}){ IsSelected = true  },
            new(new(){ Name = "Favorit",Id = -2})
        };

        private void secureObjectSelected(object o)
        {
            if (o is SecureObjectVM secureObject)
            {
                if (SelectedSecureObject != null)
                    SelectedSecureObject.IsSelected = false;
                SelectedSecureObject = secureObject;
                SelectedSecureObject.IsSelected = true;
            }
        }

        private async Task deleteObjectFromDataBase(object o)
        {
            switch (o)
            {
                case UserVM userVM:
                    dbContext.Users.Remove(await dbContext.Users.FindAsync(userVM.Id));
                    RegistryUtility.DeleteInfoFromRegistry();
                    clearData();
                    break;
                case CategoryVM categoryVM:
                    dbContext.Categories.Remove(await dbContext.Categories.FindAsync(categoryVM.Id));
                    var toDelete = secureObjects.Where(x => x.CategoryId == categoryVM.Id).ToArray();
                    foreach (var item in toDelete)
                        secureObjects.Remove(item);
                    UserCategories.Remove(categoryVM);
                    break;
                case WiFiVM wifiVM:
                    dbContext.WiFis.Remove(await dbContext.WiFis.FindAsync(wifiVM.Id));
                    break;
                case UniversalVM universalVM:
                    dbContext.Universals.Remove(await dbContext.Universals.FindAsync(universalVM.Id));
                    break;
                case ServerVM serverlVM:
                    dbContext.Servers.Remove(await dbContext.Servers.FindAsync(serverlVM.Id));
                    break;
                case EmailVM emailVM:
                    dbContext.Emails.Remove(await dbContext.Emails.FindAsync(emailVM.Id));
                    break;
                case DataBaseVM dataBaseVM:
                    dbContext.DataBases.Remove(await dbContext.DataBases.FindAsync(dataBaseVM.Id));
                    break;
                case CreditCardVM creditCardVM:
                    dbContext.CreditCards.Remove(await dbContext.CreditCards.FindAsync(creditCardVM.Id));
                    break;
                case ContactVM contacVM:
                    dbContext.Contacts.Remove(await dbContext.Contacts.FindAsync(contacVM.Id));
                    break;
                case BankAccountVM bankAccountVM:
                    dbContext.BankAccounts.Remove(await dbContext.BankAccounts.FindAsync(bankAccountVM.Id));
                    break;
            }
            dbContext.SaveChanges();
            OnPropertyChanged(nameof(SecureObjects));
        }

        private void createEditObject(object? o)
        {
            switch (o)
            {
                case UserVM userVM:

                    break;
                case CategoryVM categoryVM:
                    if (categoryVM.Id == 0)
                    {
                        categoryVM.UserId = CurrentUser.Id;
                        NewEditObject = categoryVM;
                    }
                    else NewEditObject = categoryVM.Clone() as CategoryVM;
                    IsAddEditCategoryWindowEnabled = true;
                    break;
                case WiFiVM wifiVM:
                    if (wifiVM.Id == 0)
                        NewEditObject = wifiVM;
                    else NewEditObject = wifiVM.Clone() as WiFiVM;
                    break;
                case UniversalVM universalVM:
                    if (universalVM.Id == 0)
                        NewEditObject = universalVM;
                    else NewEditObject = universalVM.Clone() as UniversalVM;
                    break;
                case ServerVM serverlVM:
                    if (serverlVM.Id == 0)
                        NewEditObject = serverlVM;
                    else NewEditObject = serverlVM.Clone() as ServerVM;
                    break;
                case EmailVM emailVM:
                    if (emailVM.Id == 0)
                        NewEditObject = emailVM;
                    else NewEditObject = emailVM.Clone() as EmailVM;
                    break;
                case DataBaseVM dataBaseVM:
                    if (dataBaseVM.Id == 0)
                        NewEditObject = dataBaseVM;
                    else NewEditObject = dataBaseVM.Clone() as DataBaseVM;
                    break;
                case CreditCardVM crediCardVM:
                    if (crediCardVM.Id == 0)
                        NewEditObject = crediCardVM;
                    else NewEditObject = crediCardVM.Clone() as CreditCardVM;
                    break;
                case ContactVM contacVM:
                    if (contacVM.Id == 0)
                        NewEditObject = contacVM;
                    else NewEditObject = contacVM.Clone() as ContactVM;
                    break;
                case BankAccountVM bankAccountVM:
                    if (bankAccountVM.Id == 0)
                        NewEditObject = bankAccountVM;
                    else NewEditObject = bankAccountVM.Clone() as BankAccountVM;
                    break;
            }
        }

        private void saveObject()
        {
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
                case WiFiVM wifiVM:

                    break;
                case UniversalVM universalVM:

                    break;
                case ServerVM serverlVM:

                    break;
                case EmailVM emailVM:

                    break;
                case DataBaseVM dataBaseVM:

                    break;
                case CreditCardVM crediCardVM:

                    break;
                case ContactVM contacVM:

                    break;
                case BankAccountVM bankAccountVM:

                    break;
            }
            setEntityVMToDataBase(NewEditObject);
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
                case WiFiVM wifiVM:

                    break;
                case UniversalVM universalVM:

                    break;
                case ServerVM serverlVM:

                    break;
                case EmailVM emailVM:

                    break;
                case DataBaseVM dataBaseVM:

                    break;
                case CreditCardVM crediCardVM:

                    break;
                case ContactVM contacVM:

                    break;
                case BankAccountVM bankAccountVM:

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
            }
        }

        private bool secureElementFilter(SecureObjectVM so)
        {
            bool strFindCondition = string.IsNullOrWhiteSpace(FindString) || so.Title.ToLower().Contains(FindString.ToLower());
            if (SelectedCategory?.Id == -1 && strFindCondition) return true;
            else return (so.CategoryId == SelectedCategory?.Id) && strFindCondition;
        }

        private bool isUserLoginInfoExist() => !string.IsNullOrWhiteSpace(UserLogin) && !string.IsNullOrWhiteSpace(UserPassword);

        // Create new account logic
        private async Task CreateNewAccClick()
        {
            if (dbContext.Users.Any(x => x.NikName == UserLogin))
                MessageBox.Show("This login is already in use...", "Server information");
            else
            {
                CurrentUser = new()
                {
                    NikName = UserLogin,
                    PasswordHash = Utility.GetHash(UserPassword)
                };
                await setEntityVMToDataBase(CurrentUser);
                dbContext.SaveChanges();
                RegistryUtility.SetLoginToRegistry(UserLogin);
                IsMainWindowEnabled = true;
                var test = Resource.ResourceManager.GetResourceSet(System.Globalization.CultureInfo.CurrentUICulture, true, true);

            }
        }

        // Login logic
        private async Task LoginClick()
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(x => x.NikName == UserLogin);
            if (user != null) CurrentUser = new(user);
            if (CurrentUser == null)
                MessageBox.Show("User with this login is not registered...", "Server information");
            else
            {
                if (Utility.GetHash(UserPassword) != CurrentUser.PasswordHash)
                    MessageBox.Show("Invalid password...", "Server information");
                else
                {
                    var categories = dbContext.Categories.Where(x => x.UserId == CurrentUser.Id).ToArray();
                    foreach (var item in categories)
                        UserCategories.Add(new(item));
                    categoriesId.AddRange(UserCategories.Select(x => x.Id));
                    secureObjects.AddRange(dbContext.Emails.Where(x => categoriesId.Any(y => y == x.CategoryId)).Select(x => new EmailVM(x)));
                    secureObjects.AddRange(dbContext.WiFis.Where(x => categoriesId.Any(y => y == x.CategoryId)).Select(x => new WiFiVM(x)));
                    secureObjects.AddRange(dbContext.Universals.Where(x => categoriesId.Any(y => y == x.CategoryId)).Select(x => new UniversalVM(x)));
                    secureObjects.AddRange(dbContext.Servers.Where(x => categoriesId.Any(y => y == x.CategoryId)).Select(x => new ServerVM(x)));
                    secureObjects.AddRange(dbContext.CreditCards.Where(x => categoriesId.Any(y => y == x.CategoryId)).Select(x => new CreditCardVM(x)));
                    secureObjects.AddRange(dbContext.Contacts.Where(x => categoriesId.Any(y => y == x.CategoryId)).Select(x => new ContactVM(x)));
                    secureObjects.AddRange(dbContext.BankAccounts.Where(x => categoriesId.Any(y => y == x.CategoryId)).Select(x => new BankAccountVM(x)));
                    secureObjects.AddRange(dbContext.DataBases.Where(x => categoriesId.Any(y => y == x.CategoryId)).Select(x => new DataBaseVM(x)));
                    SelectedCategory = staticCategoryButtons[0];
                    SelectedCategory.ElementsCount = secureObjects.Count;
                    RegistryUtility.SetLoginToRegistry(UserLogin);
                    IsMainWindowEnabled = true;
                }
            }
        }

        public MainWindowVM()
        {
            dbContext = new();
            UserLogin = RegistryUtility.TryGetLogin();
            IsFirstStart = UserLogin == string.Empty;
        }

        // Filter string
        public string FindString
        {
            get => findString;
            set
            {
                findString = value;
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

        public bool IsSelected
        {
            get => isSelected;
            set
            {
                isSelected = value;
                OnPropertyChanged();
            }
        }

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
        public RelayCommand SaveObject => new((o) => saveObject(), (o) => !string.IsNullOrWhiteSpace((NewEditObject as CategoryVM)?.Name));
        public RelayCommand AddEditObject => new((o) => createEditObject(o));
        public RelayCommand DeleteObject => new(async (o) => await deleteObjectFromDataBase(o));
    }
}
