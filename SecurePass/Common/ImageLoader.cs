using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SecurePass.Common
{
    internal static class ImageLoader
    {
        private static List<byte[]?> defaultImages = new();
        private static List<byte[]?> defaultCategoryCirclImages = new();

        static ImageLoader()
        {
            ImageConverter converter = new();
            ResourceSet? resourceSet = Resource.ResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
            foreach (DictionaryEntry item in resourceSet)
                  defaultImages.Add(converter.ConvertTo(item.Value, typeof(byte[])) as byte[]);
            resourceSet = DefaultCategoryCircleImagesResource.ResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
            foreach (DictionaryEntry item in resourceSet)
                defaultCategoryCirclImages.Add(converter.ConvertTo(item.Value, typeof(byte[])) as byte[]);

        }

        public static int ChangeImageId(int imageId)
        {
            return 0;
        }

        public static byte[]? GetById(int id)
        {
            if (id <= defaultImages.Count - 1) return defaultImages[id];
            else return null;
        }

        
    }
}
