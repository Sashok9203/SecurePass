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

        public WiFiVM() : base(0, -1, 0, "", "", false,12)
        {
            baseStation = string.Empty;
            password = string.Empty;
            ip = string.Empty;
            airPortId = string.Empty;
            networkName = string.Empty;
            wirelessSecurity = string.Empty;
            wirelessPassword = string.Empty;
            connectedStoragePasswords = string.Empty;
        }

        public WiFiVM(WiFi wifi) : base(wifi.Id,wifi.ImageId,wifi.CategoryId,wifi.Title,wifi.NetworkName,wifi.IsFavorit,12)
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

        public override void CopyToEntity(BaseEntity entity)
        {
            base.CopyToEntity(entity);
            var temp = (WiFi)entity;
            temp.BaseStation = baseStation;
            temp.Password = password;
            temp.IP = ip;
            temp.AirPortId = airPortId;
            temp.NetworkName = networkName; ;
            temp.WirelessSecurity = wirelessSecurity;
            temp.WirelessPassword = wirelessPassword;
            temp.ConnectedStoragePasswords = connectedStoragePasswords;
        }

    }
}
