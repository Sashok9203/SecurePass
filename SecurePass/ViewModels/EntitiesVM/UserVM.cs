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
        public UserVM() : base(0, -1,1)
        {
            NikName = string.Empty;
            PasswordHash = string.Empty;
        }

        public UserVM(User user):base(user.Id,user.ImageId,1)
        {
            NikName = user.NikName;
            PasswordHash = user.PasswordHash;
        }

        public string NikName { get; set; }

        public string PasswordHash { get; set; }

        public override void CopyToEntity(BaseEntity entity)
        {
            base.CopyToEntity(entity);
            var temp = (User)entity;
            temp.NikName = NikName;
            temp.PasswordHash = PasswordHash;
        }
    }
}
