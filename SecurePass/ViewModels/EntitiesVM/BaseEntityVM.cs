using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurePass.ViewModels.EntitiesVM
{
    internal class BaseEntityVM : BaseViewModel
    {
		private int imageId;

        public BaseEntityVM(int id,int imageId)
        {
			this.imageId = imageId;
			this.Id = id;
        }

        public int ImageId
		{
			get => imageId;
         	set
			{
				imageId = value;
				OnPropertyChanged();
			}
		}

		public int Id { get; private set; }
	}
}
