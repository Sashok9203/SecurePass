using data_access.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurePass.ViewModels.EntitiesVM
{
    internal class EmailVM : SecureObjectVM
    {
        private string type;
        private string name;
        private string server;
        private int port;
        private string password;
        private string safety;
        private string authenticationMethod;

        public EmailVM(Email email): base(email.Id,email.ImageId,email.CategoryId,email.UserId,email.Title)
        {
            type = email.Type;
            name = email.Name;
            server = email.Server;
            port = email.Port;
            password = email.Password;
            safety = email.Safety;
            authenticationMethod = email.AuthenticationMethod;
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

        public string Name
        {
            get => name;
            set
            {
                name = value;
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

        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged();
            }
        }

        public string Safety
        {
            get => safety;
            set
            {
                safety = value;
                OnPropertyChanged();
            }
        }

        public string AuthenticationMethod
        {
            get => authenticationMethod;
            set
            {
                authenticationMethod = value;
                OnPropertyChanged();
            }
        }
    }
}
