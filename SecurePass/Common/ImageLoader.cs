using Microsoft.Win32;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Text.Json;

namespace SecurePass.Common
{
    internal static class ImageLoader
    {
        private static List<byte[]?> defaultImages = new();
        private static List<byte[]?> defaultCategoryCirclImages = new();
        private static Dictionary<int, byte[]?>? userImages = new();
        private static bool fileExist;
        private static ImageConverter converter;
        static ImageLoader()
        {
            fileExist = File.Exists("userimg.imgs");
            converter = new();
            ResourceSet? resourceSet = Resource.ResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
            foreach (DictionaryEntry item in resourceSet)
                defaultImages.Add(converter.ConvertTo(item.Value, typeof(byte[])) as byte[]);
            resourceSet = DefaultCategoryCircleImagesResource.ResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
            foreach (DictionaryEntry item in resourceSet)
                defaultCategoryCirclImages.Add(converter.ConvertTo(item.Value, typeof(byte[])) as byte[]);
            if (!fileExist)
            {
                SaveUserImages();
                fileExist = true;
            }
            using Stream sr = new FileStream("userimg.imgs", FileMode.Open, FileAccess.Read);
            userImages = JsonSerializer.Deserialize<Dictionary<int, byte[]?>>(sr);
            StartIndex = defaultImages.Count;
        }

        public static void SaveUserImages()
        {
            using Stream wst = new FileStream("userimg.imgs", FileMode.Create, FileAccess.Write);
            JsonSerializer.Serialize(wst, userImages);
        }

        public static void DeleteUserImage(int imageId)
        {
            if (userImages?.ContainsKey(imageId) == true)
                userImages.Remove(imageId);
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
                Bitmap bmp = new(Bitmap.FromFile(fod.FileName) as Bitmap, new(64, 64));
                userImages[newImageId] = converter.ConvertTo(bmp, typeof(byte[])) as byte[];
            }
            else
            {
                DeleteUserImage(imageId);
                newImageId = -1;
            }
            return newImageId;
        }

        public static bool IsImageExist(int id) => id >= 0 && (id <= defaultImages.Count - 1 || userImages.ContainsKey(id));

        public static int StartIndex { get; set; } 

        public static byte[]? GetById(int id)
        {
            if (id <= defaultImages.Count - 1) return defaultImages[id];
            else if (userImages.ContainsKey(id)) return userImages[id];
            else return null;
        }

        
    }
}
