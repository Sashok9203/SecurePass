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
        private int typeId;

        public UniversalVM() : base(0, -1, 0, "", "", false,-1)
        {
            value = string.Empty;
            label = string.Empty;
        }

        public UniversalVM(Universal universal):base(universal.Id,universal.ImageId,universal.CategoryId,universal.Title,universal.Label, universal.IsFavorit,-1)
        {
            value = universal.Value;
            label = universal.Label;
            typeId = universal.TypeId;
        }

        public override int ImageId 
        {
            get => base.ImageId >= 0 ? base.ImageId : TypeId switch
            {
                1 => 10,
                2 => 4,
                3 => 5,
                _ => throw new ArgumentException("Invalid TypeId value...")
            };
            set => base.ImageId = value; 
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

        public int TypeId
        {
            get => typeId;
            set
            {
                this.typeId = value;
                OnPropertyChanged();
            }
        }

        public override void CopyToEntity(BaseEntity entity)
        {
            base.CopyToEntity(entity);
            var temp = (Universal)entity;
            temp.Value = value;
            temp.Label = label;
            temp.TypeId = typeId;
        }
    }
}
