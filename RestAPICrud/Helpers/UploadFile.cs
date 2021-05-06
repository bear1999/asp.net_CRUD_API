using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;

namespace RestAPICrud.Helpers
{
    public interface IUploadFile
    {
        Task<string> UploadImage(IWebHostEnvironment _hostEnvironment, string _path, IFormFile fileImage);
    }
    public class UploadFile : IUploadFile
    {
        public UploadFile()
        {
        }

        public async Task<string> UploadImage(IWebHostEnvironment _hostEnvironment, string _path, IFormFile fileImage)
        {
            if (fileImage == null) return null;
            string extension = Path.GetExtension(fileImage.FileName);
            if (!Equals(extension, ".png") && !Equals(extension, ".jpge") && !Equals(extension, ".jpg"))
                return null;
            var filename = DateTime.Now.ToString("ddMMyyyy_") + Guid.NewGuid() + extension;
            var path = Path.Combine(_hostEnvironment.ContentRootPath, _path, filename);
            if (File.Exists(path))
                return null;
            else
            {
                using FileStream fileStream = new FileStream(path, FileMode.Create);
                await fileImage.CopyToAsync(fileStream);
                return filename;
            }
        }
    }
}
