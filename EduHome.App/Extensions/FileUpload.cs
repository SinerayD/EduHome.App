using System;
using Microsoft.AspNetCore.Http;

namespace EduHome.App.Extensions
{
    public static class FileUpload
    {
        public static string CreateImage(this IFormFile file, string root, string path)
        {
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string fullPath = Path.Combine(root, path, fileName);
            using (FileStream fileStream = new FileStream(fullPath, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }
            return fileName;
        }
    }
}

