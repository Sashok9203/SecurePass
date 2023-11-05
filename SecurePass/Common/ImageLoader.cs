using Microsoft.Win32;
using SecurePass.ViewModels;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text.Json;

namespace SecurePass.Common
{
    internal static class ImageLoader
    {
        private static List<byte[]?> defaultCategoryImages = new();
        private static Dictionary<int, byte[]?>? userImages = new();
        private static bool fileExist,isChanget;
        private static ImageConverter converter;
        static ImageLoader()
        {
            fileExist = File.Exists("userimg.imgs");
            converter = new();
            ResourceSet? resourceSet = Resource.ResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
            foreach (DictionaryEntry item in resourceSet)
                 DefaultImages.Add(converter.ConvertTo(item.Value, typeof(byte[])) as byte[]);
            resourceSet = DefaultCategoryImagesResource.ResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
            foreach (DictionaryEntry item in resourceSet)
                 defaultCategoryImages.Add(converter.ConvertTo(item.Value, typeof(byte[])) as byte[]);
            DefaultImages.AddRange(defaultCategoryImages);
            defaultCategoryImages.Insert(0, converter.ConvertTo(PlusIconResource.plus_ico, typeof(byte[])) as byte[]);
            if (!fileExist)
            {
                isChanget = true;
                SaveUserImages();
                fileExist = true;
            }
            using Stream sr = new FileStream("userimg.imgs", FileMode.Open, FileAccess.Read);
            userImages = JsonSerializer.Deserialize<Dictionary<int, byte[]?>>(sr);
            StartIndex = DefaultImages.Count;
            isChanget = true;
        }

        public static void SaveUserImages()
        {
            if (isChanget)
            {
                using Stream wst = new FileStream("userimg.imgs", FileMode.Create, FileAccess.Write);
                JsonSerializer.Serialize(wst, userImages);
                isChanget = false;
            }
        }

        public static void DeleteUserImage(int imageId)
        {
            if (userImages?.ContainsKey(imageId) == true)
            {
                userImages.Remove(imageId);
                isChanget = true;
            }
        }

        public static int ChangeImage(int imageId)
        {
            int newImageId = userImages?.ContainsKey(imageId) == true ? imageId : StartIndex++;
            OpenFileDialog fod = new()
            {
                Filter = "Files|*.jpg;*.jpeg;*.png;"
            };
            if (fod.ShowDialog() == true)
            {
                if (Bitmap.FromFile(fod.FileName) is Bitmap tmp && userImages != null)
                {
                    Bitmap bmp = new(tmp, new((int)(tmp.Width / ((double)tmp.Height / 64)), 64));
                    userImages[newImageId] = converter.ConvertTo(bmp, typeof(byte[])) as byte[];
                }
            }
            else
            {
                DeleteUserImage(imageId);
                newImageId = -1;
            }
            isChanget = true;
            return newImageId;
        }

        public static bool IsImageExist(int id) => id >= 0 && (id <= DefaultImages.Count - 1 || userImages?.ContainsKey(id) == true);

        public static int StartIndex { get; set; }

        public static List<byte[]?> DefaultImages = new();

        public static IEnumerable<byte[]?> DefaultCategoryImages => defaultCategoryImages;

        public static byte[]? GetById(int id)
        {
            if (id <= DefaultImages.Count - 1) return DefaultImages[id];
            else if (userImages?.ContainsKey(id) == true) return userImages[id];
            else return null;
        }

        
    }
}
