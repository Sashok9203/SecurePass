using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace SecurePass.ViewModels.EntitiesVM
{
    internal class SecureObjectVM : BaseEntityVM
    {
        private int categoryId;
        private string title;

        public SecureObjectVM(int id,int imageId,int categoryId,int userId,string title) : base(id ,imageId)
        {
            this.title = title;
            this.UserId = userId;
            this.categoryId = categoryId;
        }

        public string Title
        {
            get => title;
            set 
            { 
                title = value;
                OnPropertyChanged();
            }
        }

        public int CategoryId
        {
            get => categoryId;
            set
            {
                categoryId = value;
                OnPropertyChanged();
            }
        }

        public int UserId { get; set; }
    }
}
