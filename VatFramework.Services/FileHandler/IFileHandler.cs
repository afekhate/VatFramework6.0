using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace VatFramework.Services.FileHandler
{
    public interface IFileHandler
    {
        Task<string> UploadFile(IFormFile FileDetail, string UploadPath, string FileAllowExtension,
            int _oneMegaByte, int _fileMaxSize);

        Task<string> AcceptAllUploadFile(IFormFile FileDetail, string UploadPath,
           int _oneMegaByte, int _fileMaxSize, string extraParameter);

        Task<string> UploadFileValues(IFormFile FileDetail, string UploadPath, string FileAllowExtension,
            int _oneMegaByte, int _fileMaxSize);

        Task<string> UploadByteFile(Byte[] attachment, string UploadPath, string FileName);

    }


    public enum FileType
    {
        PICTURE = 1,
        VIDEOS =2,
        Excel = 3,
        PDF = 4,
        WORD = 5
    }
}
