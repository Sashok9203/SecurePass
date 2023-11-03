using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurePass.Common
{
    internal static class RegistryUtility
    {
        private const string keyLoginRegistryPath = @"Software\SecurePass\";
        private const string userLoginValueName = "SecurePassUserLogin";

        public static string TryGetLogin()
        {
            RegistryKey? registryKey = Registry.CurrentUser.OpenSubKey(keyLoginRegistryPath);
            if (registryKey == null)
                return string.Empty;
            else
                return registryKey?.GetValue(userLoginValueName)?.ToString() ?? string.Empty;
        }

        public static void SetLoginToRegistry(string login)
        {
            RegistryKey? registryKey = Registry.CurrentUser.CreateSubKey(keyLoginRegistryPath);
            registryKey.SetValue(userLoginValueName, login);
        }

        public static void DeleteInfoFromRegistry()
        {
            RegistryKey? registryKey = Registry.CurrentUser.OpenSubKey(keyLoginRegistryPath);
            if (registryKey == null) return;
            Registry.CurrentUser.DeleteSubKeyTree(keyLoginRegistryPath);
        }
        public static void SetInfoToRegistry(string value)
        {
            using RegistryKey? registryKey = Registry.CurrentUser.OpenSubKey(keyLoginRegistryPath, true);
            if (registryKey == null) return;

            registryKey.SetValue(userLoginValueName, value);
        }
    }
}
