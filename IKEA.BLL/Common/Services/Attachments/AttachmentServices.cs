using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace IKEA.BLL.Common.Services.Attachments
{
    public class AttachmentServices : IAttachmentServices
    {
        private readonly List<string> AllowedExtentions = new List<string>() { ".jpg" , ".png",".jpeg"};
        private const int FileMaximumSize = 2_097_152;
        public string UploadImage(IFormFile file, string FolderName)
        {
            var FileExtention = Path.GetExtension(file.FileName);
                
                if (!AllowedExtentions.Contains(FileExtention))
                    throw new Exception("Invalid File Extention");

                if(file.Length > FileMaximumSize)
                    throw new Exception("Invalid File Size , Over Our Range !");


            var FolderPath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","files", FolderName);
            if(Directory.Exists(FolderPath))
                Directory.CreateDirectory(FolderPath);

            var FileName = $"{Guid.NewGuid()}_{file.FileName}";
            var FilePath = Path.Combine(FolderPath,FileName);

            using var FS = new FileStream(FolderPath,FileMode.Create);
            file.CopyTo(FS);
            return FileName;
        }
        public bool DeleteImage(string FilePath)
        {
           if(File.Exists(FilePath))
            {
                File.Delete(FilePath); 
                return true;
            }
           return false;
        }

       
    }
}
