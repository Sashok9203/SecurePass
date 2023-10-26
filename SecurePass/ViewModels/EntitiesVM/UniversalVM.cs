using data_access.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurePass.ViewModels.EntitiesVM
{
    internal class UniversalVM : SecureObjectVM
    {
        private string value;
        private string label;

        public UniversalVM(Universal universal):base(universal.Id,universal.ImageId,universal.CategoryId,universal.ImageId,universal.Title,universal.Label, universal.IsFavorit)
        {
            value = universal.Value;
            label = universal.Label;
        }
        
        public string Label
        {
            get => label;
            set
            {
                label = value;
                OnPropertyChanged();
            }
        }

        public string Value
        {
            get => value; 
            set
            {
                this.value = value;
                OnPropertyChanged();
            }
        }
    }
}
