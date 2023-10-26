using data_access.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurePass.ViewModels.EntitiesVM
{
    internal class UserVM : BaseEntityVM
    {
        public UserVM(User user):base(user.Id,user.ImageId)
        {
            NickName = user.NikName;
            PasswordHash = user.PasswordHash;
        }
        public string NickName { get; private set; }
        public string PasswordHash { get; private set; } 
    }
}
