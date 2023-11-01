using data_access.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurePass.ViewModels.EntitiesVM
{
    internal class CategoryVM : BaseEntityVM
    {
        private bool isSelected;
        private string name;
        private int elementsCount;

        public CategoryVM(): base(0, 0) { name = string.Empty;  }

        public CategoryVM(Category category) : base(category.Id, category.ImageId)
        {
            this.name = category.Name;
            UserId = category.UserId;
            elementsCount =  category.CreditCards.Count
                           + category.Universals.Count
                           + category.Emails.Count
                           + category.Servers.Count
                           + category.Contacts.Count
                           + category.WiFis.Count
                           + category.DataBases.Count
                           + category.BankAccounts.Count;
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

        public int ElementsCount
        {
            get => elementsCount;
            set
            {
                elementsCount = value;
                OnPropertyChanged();
            }
        }

        public bool IsSelected
        {
            get => isSelected;
            set
            {
                isSelected = value;
                OnPropertyChanged();
            }
        }

        public int UserId { get; set; }

        public override void CopyToEntity(BaseEntity entity)
        {
            base.CopyToEntity(entity);
            var temp = (Category)entity;
            temp.Name = name;
            temp.UserId = UserId;
        }
    }
}
