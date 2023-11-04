using SecurePass.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurePass.ViewModels
{
    internal class ImageVM
    {
        public byte[]? Image { get; init; }
        public int Id { get; init; }
        public ImageVM(byte[]? image)
        {
            Image = image;
            Id = ImageLoader.DefaultImages.FindIndex(x=>x == image);
        }
    }
}
