using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;

namespace RestAPICrud.Helper
{
    public class UploadFile
    {
        private IWebHostEnvironment _hostEnvironment;
        [DataType(DataType.Upload)]
        private IFormFile fileImage;

        public UploadFile(IWebHostEnvironment hostEnvironment, IFormFile fileImage)
        {
            this._hostEnvironment = hostEnvironment;
            this.fileImage = fileImage;
        }

        public async Task<string> uploadImage()
        {
            if (fileImage == null) return null;
            string extension = Path.GetExtension(fileImage.FileName);
            if (!Equals(extension, ".png") && !Equals(extension, ".jpge") && !Equals(extension, ".jpg"))
                return null;
            var filename = DateTime.Now.ToString("ddMMyyyy_") + Guid.NewGuid() + extension;
            var path = Path.Combine(_hostEnvironment.ContentRootPath, "Assets/ProfileImage/", filename);
            if (File.Exists(path))
                return null;
            else
            {
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await fileImage.CopyToAsync(fileStream);
                    return filename;
                }
            }
        }
    }
}
