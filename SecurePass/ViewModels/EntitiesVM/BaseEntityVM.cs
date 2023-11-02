using data_access.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurePass.ViewModels.EntitiesVM
{
    internal class BaseEntityVM : BaseViewModel ,ICloneable
    {
		private int imageId;
        private  readonly int defaultImageId;

        public BaseEntityVM(int id,int imageId,int defaulImageId)
        {
            this.defaultImageId = defaulImageId;
            this.imageId = imageId;
			this.Id = id;
        }

        public virtual int ImageId
		{
			get => imageId < 0 ? defaultImageId : imageId;
         	set
			{
				imageId = value;
				OnPropertyChanged();
			}
		}

		public int Id { get; set; }

        public object Clone() => MemberwiseClone();

        public virtual void CopyToEntity(BaseEntity entity) { entity.ImageId = ImageId; }
    }
}
