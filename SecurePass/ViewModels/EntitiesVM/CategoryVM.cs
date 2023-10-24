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

        public CategoryVM(Category category) : base(category.Id,category.ImageId)
        {
            this.name = category.Name; 
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

        public bool IsSelected
        {
            get => isSelected;
            set
            {
                isSelected = value;
                OnPropertyChanged();
            }
        }
    }
}
