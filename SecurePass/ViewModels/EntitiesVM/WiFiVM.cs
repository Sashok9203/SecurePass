using data_access.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurePass.ViewModels.EntitiesVM
{
    internal class WiFiVM : SecureObjectVM
    {
        private string baseStation;
        private string password;
        private string ip;
        private string airPortId;
        private string networkName;
        private string wirelessSecurity;
        private string wirelessPassword;
        private string connectedStoragePasswords;

        public WiFiVM(WiFi wifi) : base(wifi.Id,wifi.ImageId,wifi.CategoryId,wifi.Title,wifi.NetworkName,wifi.IsFavorit)
        {
            baseStation = wifi.BaseStation;
            password = wifi.Password;
            ip = wifi.IP;
            airPortId = wifi.AirPortId;
            networkName = wifi.NetworkName;
            wirelessSecurity = wifi.WirelessSecurity;
            wirelessPassword = wifi.WirelessPassword;
            connectedStoragePasswords = wifi.ConnectedStoragePasswords;
        }

        public string BaseStation
        {
            get => baseStation;
            set
            {
                baseStation = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged();
            }
        }

        public string IP
        {
            get => ip;
            set
            {
                ip = value;
                OnPropertyChanged();
            }
        }

        public string AirPortId
        {
            get => airPortId;
            set
            {
                airPortId = value;
                OnPropertyChanged();
            }
        }

        public string NetworkName
        {
            get => networkName;
            set
            {
                networkName = value;
                OnPropertyChanged();
            }
        }

        public string WirelessSecurity
        {
            get => wirelessSecurity;
            set
            {
                wirelessSecurity = value;
                OnPropertyChanged();
            }
        }

        public string WirelessPassword
        {
            get => wirelessPassword;
            set
            {
                wirelessPassword = value;
                OnPropertyChanged();
            }
        }

        public string ConnectedStoragePasswords
        {
            get => connectedStoragePasswords;
            set
            {
                connectedStoragePasswords = value;
                OnPropertyChanged();
            }
        }

    }
}
