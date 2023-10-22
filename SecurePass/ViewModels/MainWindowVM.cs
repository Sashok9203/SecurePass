using data_access.Data;
using data_access.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using SecurePass.Common;
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
        private bool isMainWindowEnabled, isFirstStart;
        private readonly SecurePassDBContext dbContext;
        private User? currentUser;

        private string tryGetLogin()
        {
            RegistryKey? registryKey = Registry.CurrentUser.OpenSubKey(keyLoginRegistryPath);
            if (registryKey == null) return string.Empty;
            else
                return registryKey?.GetValue(userLoginValueName)?.ToString() ?? string.Empty;
        }

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
                    List<SecureObject> temp = new();
                    temp.AddRange(dbContext.Emails.Include(x => x.Category));
                    temp.AddRange(dbContext.CreditCards.Include(x => x.Category));
                    temp.AddRange(dbContext.Universals.Include(x => x.Category));
                    temp.AddRange(dbContext.Servers.Include(x => x.Category));
                    foreach (var item in temp)
                        SecureObjects.Add(item);
                    foreach (var item in temp.Select(x => x.Category).Distinct())
                        UserCategories.Add(new() { Category = item, IsSelected = false });
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
        
        // First program start flag
        public bool IsFirstStart
        {
            get => isFirstStart;
            set
            {
                isFirstStart = value;
                OnPropertyChanged();
            }
        }

        // Switch main window - registration/authorization window
        public bool IsMainWindowEnabled
        {
            get => isMainWindowEnabled;
            set
            {
                isMainWindowEnabled = value;
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

        // Colection of the user SecureOblects
        public ObservableCollection<SecureObject> SecureObjects { get; set; } = new();

        // Colection of the user Categories
        public ObservableCollection<CategoryVM> UserCategories { get; set; } = new();

        // User login value 
        public string UserLogin { get; set; } = string.Empty;

        // User password value 
        public string UserPassword { get; set; } = string.Empty;

        public RelayCommand LoginButtonClick => new(async(o) => await LoginClick());
        public RelayCommand CreateNewAccButtonClick => new(async(o) => await CreateNewAccClick());
    }
}
