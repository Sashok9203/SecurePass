using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurePass.ViewModels.EntitiesVM
{
    internal class ServerVM : SecureObjectVM
    {
        private string url;
        private string name;
        private string password;

        public ServerVM(ServerVM server) : base(server.Id,server.ImageId,server.CategoryId,server.UserId,server.Title)
        {
            url = server.url;
            name = server.name;
            password = server.password;
        }

        public string URL 
        {
            get => url;
            set 
            {
                url = value;
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
    }
}
