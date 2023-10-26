using data_access.Data;
using data_access.Entities;
using Microsoft.EntityFrameworkCore;
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
        private bool isMainWindowEnabled, isFirstStart,isSelected;
        private readonly SecurePassDBContext dbContext;
        private User? currentUser;
        private string findString;
        private CategoryVM? selectedCategory;
        private List<SecureObjectVM> secureObjects = new();

        private CategoryVM[] staticCategoryButtons =
        {
            new(new(){ Name = "AllElements",Id = -1}){ IsSelected = true  },
            new(new(){ Name = "Favorit",Id = -2})
        };


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
                    await dbContext.Emails.Where(p => p.Id == CurrentUser.Id).LoadAsync();
                    await dbContext.CreditCards.Where(p => p.Id == CurrentUser.Id).LoadAsync();
                    await dbContext.Universals.Where(p => p.Id == CurrentUser.Id).LoadAsync();
                    await dbContext.Servers.Where(p => p.Id == CurrentUser.Id).LoadAsync();
                    secureObjects.AddRange(dbContext.Emails.Include(x => x.Category).Select(x => new EmailVM(x)));
                    secureObjects.AddRange(dbContext.CreditCards.Include(x => x.Category).Select(x => new CreditCardVM(x)));
                    secureObjects.AddRange(dbContext.Universals.Include(x => x.Category).Select(x => new UniversalVM(x)));
                    secureObjects.AddRange(dbContext.Servers.Include(x => x.Category).Select(x => new ServerVM(x)));
                    SelectedCategory = staticCategoryButtons[0];
                    SelectedCategory.ElementsCount = secureObjects.Count;
                    var categoriesId = secureObjects.Select(x => x.CategoryId).Distinct();
                    var categories = dbContext.Categories.Where(x => categoriesId.Any(y => y == x.Id));
                    foreach (var item in categories)
                        UserCategories.Add(new(item));
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
    }
}
