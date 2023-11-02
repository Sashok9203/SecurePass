using data_access.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SecurePass.ViewModels.EntitiesVM
{
    internal class DataBaseVM : SecureObjectVM
    {

        private string type;
        private string server;
        private int port;
        private string database;
        private string name;
        private string password;
        private string sid;
        private string pseudonym;
        private string connectionSettings;

        public DataBaseVM() : base(0, -1, 0, "", "", false,15)
        {
            type = string.Empty;
            server = string.Empty;
            database = string.Empty;
            name = string.Empty;
            password = string.Empty;
            sid = string.Empty;
            pseudonym = string.Empty;
            connectionSettings = string.Empty;
        }

        public DataBaseVM( DataBase dataBase) : base(dataBase.Id,dataBase.ImageId,dataBase.CategoryId,dataBase.Title,dataBase.Name,dataBase.IsFavorit,15)
        {
            type = dataBase.Type;
            server = dataBase.Server;
            port = dataBase.Port;
            database = dataBase.Database;
            name = dataBase.Name;
            password = dataBase.Password;
            sid = dataBase.SID;
            pseudonym = dataBase.Pseudonym;
            connectionSettings = dataBase.ConnectionSettings;
        }

        public string Type
        {
            get => type;
            set
            {
                type = value;
                OnPropertyChanged();
            }
        }

        public string Server
        {
            get => server;
            set
            {
                server = value;
                OnPropertyChanged();
            }
        }

        public int Port
        {
            get => port;
            set
            {
                port = value;
                OnPropertyChanged();
            }
        }

        public string Database
        {
            get => database;
            set
            {
                database = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get => name;
            set
            {
                name = value;
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

        public string SID
        {
            get => sid;
            set
            {
                sid = value;
                OnPropertyChanged();
            }
        }

        public string Pseudonym
        {
            get => pseudonym;
            set
            {
                pseudonym = value;
                OnPropertyChanged();
            }
        }

        public string ConnectionSettings
        {
            get => connectionSettings;
            set
            {
                connectionSettings = value;
                OnPropertyChanged();
            }
        }

        public override void CopyToEntity(BaseEntity entity)
        {
            base.CopyToEntity(entity);
            var temp = (DataBase)entity;
            temp.Type = type;
            temp.Server = server;
            temp.Port = port;
            temp.Database = database;
            temp.Name = name;
            temp.Password = password;
            temp.SID = sid;
            temp.Pseudonym = pseudonym;
            temp.ConnectionSettings = connectionSettings;
        }
    }
}
