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
        private const string keyLoginRegistryPath = @"Software\SecurePass\";
        private const string userLoginValueName = "SecurePassUserLogin";
        private bool isMainWindowEnabled, isFirstStart, isSelected,isAddEditCategoryWindowEnabled;
        private readonly SecurePassDBContext dbContext;
        private User? currentUser;
        private string findString, newCategoryName;
        private CategoryVM? selectedCategory,createdCategory;
        private List<SecureObjectVM> secureObjects = new();
        private SecureObjectVM? selectedSecureObject;
        private List<int> categoriesId = new();
        private object? objectForEdit;

        private async Task setEntityVMToDataBase(BaseEntityVM vm,object? objectForEdit)
        {
            switch (vm)
            {
                case UserVM userVM:
                    break;
                case CategoryVM categoryVM:
                    if (objectForEdit == null)
                    {
                        await  dbContext.Categories.AddAsync( new Category()
                        {
                            ImageId = categoryVM.Id,
                            Name = categoryVM.Name,
                            UserId = CurrentUser.Id
                        });
                        await dbContext.SaveChangesAsync();
                        var newCategory = dbContext.Categories.Where(x =>x.UserId == CurrentUser.Id && !categoriesId.Any(y => y == x.Id)).First();
                        categoriesId.Add(newCategory.Id);
                        UserCategories.Add(new(newCategory));
                    }
                    else 
                    {
                        CategoryVM editable = objectForEdit as CategoryVM;
                        Category? category = dbContext.Categories.Find(editable.Id);
                        category.Name = categoryVM.Name;
                        category.ImageId = categoryVM.Id;
                        editable.Name = categoryVM.Name;
                        editable.ImageId = categoryVM.ImageId;
                    }
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
                case ContactVM crediCardVM:
                    break;
                case BankAccountVM bankAccountVM:
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
                case CategoryVM categoryVM:
                    dbContext.Categories.Remove(await dbContext.Categories.FindAsync(categoryVM.Id));
                    var toDelete = secureObjects.Where(x => x.CategoryId == categoryVM.Id).ToArray();
                    foreach (var item in toDelete)
                        secureObjects.Remove(item);
                    UserCategories.Remove(categoryVM);
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
                case ContactVM crediCardVM:
                    break;
                case BankAccountVM bankAccountVM:
                    break;
            }
            dbContext.SaveChanges();
            OnPropertyChanged(nameof(SecureObjects));
        }

        private void newCategory(object? o)
        {
            objectForEdit = o;
            NewCategory = new()
            {
                Name = (o as CategoryVM)?.Name ?? string.Empty,
                ImageId = (o as CategoryVM)?.ImageId ?? 0
            };
            IsAddEditCategoryWindowEnabled = true;
        }

        private async Task saveNewCategory()
        {
            await setEntityVMToDataBase(NewCategory, objectForEdit);
            IsAddEditCategoryWindowEnabled = false;
            NewCategory = null;
            objectForEdit = null;
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

        private string tryGetLogin()
        {
            RegistryKey? registryKey = Registry.CurrentUser.OpenSubKey(keyLoginRegistryPath);
            if (registryKey == null) return string.Empty;
            else
                return registryKey?.GetValue(userLoginValueName)?.ToString() ?? string.Empty;
        }

        private bool isUserLoginInfoExist() => !string.IsNullOrWhiteSpace(UserLogin) && !string.IsNullOrWhiteSpace(UserPassword);

        private void addRegistryKey()
        {
            RegistryKey? registryKey = Registry.CurrentUser.CreateSubKey(keyLoginRegistryPath);
            registryKey.SetValue(userLoginValueName, UserLogin);
        }

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
                await dbContext.Users.AddAsync(CurrentUser);
                dbContext.SaveChanges();
                addRegistryKey();
                IsMainWindowEnabled = true;
                var test = Resource.ResourceManager.GetResourceSet(System.Globalization.CultureInfo.CurrentUICulture, true, true);

            }
        }

        // Login logic
        private async Task LoginClick()
        {
            CurrentUser = await dbContext.Users.FirstOrDefaultAsync(x => x.NikName == UserLogin);
            if (CurrentUser == null)
                MessageBox.Show("User with this login is not registered...", "Server information");
            else
            {
                if (Utility.GetHash(UserPassword) != CurrentUser.PasswordHash)
                    MessageBox.Show("Invalid password...", "Server information");
                else
                {
                    var categories = dbContext.Categories.Where(x =>x.UserId == CurrentUser.Id).ToArray();
                    foreach (var item in categories)
                        UserCategories.Add(new(item));
                    categoriesId.AddRange(UserCategories.Select(x => x.Id));
                    secureObjects.AddRange(dbContext.Emails.Where(x => categoriesId.Any(y => y == x.CategoryId)).Select(x => new EmailVM(x)));
                    secureObjects.AddRange(dbContext.WiFis.Where(x => categoriesId.Any(y => y == x.CategoryId)).Select(x => new WiFiVM(x)));
                    secureObjects.AddRange(dbContext.Universals.Where(x => categoriesId.Any(y => y == x.CategoryId)).Select(x => new UniversalVM(x)));
                    secureObjects.AddRange(dbContext.Servers.Where(x => categoriesId.Any(y => y == x.CategoryId)).Select(x => new ServerVM(x)));
                    secureObjects.AddRange(dbContext.CreditCards.Where(x => categoriesId.Any(y => y == x.CategoryId)).Select(x => new CreditCardVM(x)));
                    secureObjects.AddRange(dbContext.Contacts.Where(x => categoriesId.Any(y => y == x.CategoryId)).Select(x => new ContactVM(x)));
                    secureObjects.AddRange(dbContext.BankAccount.Where(x => categoriesId.Any(y => y == x.CategoryId)).Select(x => new BankAccountVM(x)));
                    secureObjects.AddRange(dbContext.DataBases.Where(x => categoriesId.Any(y => y == x.CategoryId)).Select(x => new DataBaseVM(x)));
                    SelectedCategory = staticCategoryButtons[0];
                    SelectedCategory.ElementsCount = secureObjects.Count;
                    addRegistryKey();
                    IsMainWindowEnabled = true;
                }
            }
        }

        public MainWindowVM()
        {
            dbContext = new();
            UserLogin = tryGetLogin();
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
        public CategoryVM? NewCategory
        {
            get => createdCategory;
            set
            {
                createdCategory = value;
                OnPropertyChanged();
            }
        }

        // Authorized user
        public User? CurrentUser
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
        public IEnumerable<SecureObjectVM> SecureObjects  => secureObjects.Where(x=> secureElementFilter(x)).ToArray();

        // Сollection of user Categories
        public ObservableCollection<CategoryVM> UserCategories { get; set; } = new();

        // User login value 
        public string UserLogin { get; set; } = string.Empty;

        // User password value 
        public string UserPassword { get; set; } = string.Empty;

        public RelayCommand LoginButtonClick => new(async(o) => await LoginClick(), (o) => isUserLoginInfoExist());
        public RelayCommand CreateNewAccButtonClick => new(async(o) => await CreateNewAccClick(),(o)=>isUserLoginInfoExist());
        public RelayCommand CategorySelected => new((o) => categorySelected(o));
        public RelayCommand SecureObjectSelected => new((o) => secureObjectSelected(o));
        public RelayCommand Cancle => new((o) => IsAddEditCategoryWindowEnabled = false);
        public RelayCommand SaveCategory => new(async (o) => await saveNewCategory(),(o) => !string.IsNullOrWhiteSpace(NewCategory?.Name));
        public RelayCommand AddNewCategory => new((o) => newCategory(o));
        public RelayCommand DeleteObject => new(async(o) => await deleteObjectFromDataBase(o));
    }
}
