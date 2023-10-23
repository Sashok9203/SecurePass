using data_access.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurePass.ViewModels
{
    internal class CategoryVM : BaseViewModel
    {
        private bool isSelected;
        private int elementsCount, imageId;
        private string name;

        public CategoryVM(Category category)
        {
            name = category.Name;
            imageId = category.ImageId;
            Id = category.Id;
            ElementsCount = category.CreditCards.Count + category.Universals.Count + category.Emails.Count + category.Servers.Count;
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

        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        public int Id { get; private set; }

        public int ImageId
        {
            get => imageId;
            set
            {
                imageId = value;
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
    }
}
