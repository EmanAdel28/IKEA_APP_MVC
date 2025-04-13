using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;


namespace IKEA.BLL.Common.Services.Attachments
{
    public interface IAttachmentServices
    {
        public string UploadImage(IFormFile file, string FolderName);

        public bool DeleteImage(string FilePath);
    }
}
