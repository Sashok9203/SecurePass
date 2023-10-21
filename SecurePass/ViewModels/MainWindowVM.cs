using data_access.Data;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
        private bool isMainWindowEnabled, isFirstProgramStart;
        private readonly SecurePassDBContext dbContex;
        private string userLogin = string.Empty;
        private string tryGetLogin()
        {
            RegistryKey? registryKey = Registry.CurrentUser.OpenSubKey(keyLoginRegistryPath);
            if (registryKey == null) return string.Empty;
            else
                return registryKey?.GetValue(userLoginValueName)?.ToString() ?? string.Empty;
        }

        private void programStart()
        {
            userLogin = tryGetLogin();
            IsFirstProgramStart = userLogin == string.Empty;
            if (IsFirstProgramStart)
            {
                //Create new account 
               
                //Create key in registry
                RegistryKey? registryKey = Registry.CurrentUser.CreateSubKey(keyLoginRegistryPath);
                registryKey.SetValue(userLoginValueName, userLogin);
            }
            else 
            {
                // Authorization logic
               
            }
        }
        private void EnterLoginClick()
        {

        }
        private void CreateNewAccClick()
        {

        }

        private void LoginClick()
        {

        }

        public MainWindowVM()
        {
            dbContex = new();
            programStart();
        }
        
        public bool IsFirstProgramStart
        {
            get => isFirstProgramStart;
            set
            {
                isFirstProgramStart = value;
                OnPropertyChanged();
            }
        }


        public bool IsMainWindowEnabled
        {
            get => isMainWindowEnabled;
            set
            {
                isMainWindowEnabled = value;
                OnPropertyChanged();
            }
        }
        //Login
        public RelayCommand EnterLoginButtonClick => new((o) => EnterLoginClick());
        //Authorization
        public RelayCommand LoginButtonClick => new((o) => LoginClick());
        public RelayCommand CreateNewAccButtonClick => new((o) => CreateNewAccClick());
    }
}
