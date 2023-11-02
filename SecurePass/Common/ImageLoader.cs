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
        private static List<Bitmap?> defaultImages = new();
        private static ImageConverter converter;
        static ImageLoader()
        {
            converter = new();
            ResourceSet? resourceSet = Resource.ResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
            int i = 0;
            foreach (DictionaryEntry item in resourceSet)
            {
                defaultImages.Add(item.Value as Bitmap);
                Debug.WriteLine($"{i++} - {item.Key}");
            }
            
        }

        public static byte[]? GetBuId(int id)
        {
            if (id <= defaultImages.Count - 1) return converter.ConvertTo(defaultImages[id], typeof(byte[])) as byte[];
            else return null;
        }

        
    }
}
