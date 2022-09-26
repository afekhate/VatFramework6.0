
using System.Threading.Tasks;
using VatFramework.Services.FileHandler;
using VatFramework.Utilities.ExceptionUtility;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Drawing;
using System.Linq;

namespace VatFramework.Services.Handler.FileHandler
{
    public class FileHandlerServices : IFileHandler
    {

       

        public async Task<string> UploadFile(IFormFile FileDetail, string UploadPath, string FileAllowExtension,
                           int _oneMegaByte, int _fileMaxSize)
        {
            try
            {
                string msg = "";
                if (FileDetail != null)
                {
                    var extension = FileDetail.FileName.Split('.')[1];
                    var supportedTypes = FileAllowExtension.Split(',');
                    int checkExtension = 0;


                    foreach (var item in supportedTypes)
                    {

                        var kk = item.Replace("\"", "");
                        if (kk.ToLower() == extension.ToLower())

                        {
                            checkExtension = checkExtension + 1;
                        }
                    }

                    var filesize = _fileMaxSize * _oneMegaByte;

                    if (checkExtension == 0)

                    {
                        msg = ResponseErrorMessageUtility.INVALID_FILE_EXTENSION + FileAllowExtension;
                        return msg;
                    }

                    else if (FileDetail.Length == 0)
                    {
                        msg = ResponseErrorMessageUtility.NO_FILE_CONTENT;
                        return msg;
                    }



                    else if (FileDetail.Length > filesize)
                    {
                        msg = ResponseErrorMessageUtility.FILE_SIZE_EXCEEDED + _fileMaxSize + ResponseErrorMessageUtility.FILE_SIZE_UNIT;
                        return msg;
                    }


                    var fileName = Guid.NewGuid().ToString();
                    fileName += "." + extension;

                    string fullPath = Directory.GetCurrentDirectory() + UploadPath + fileName;



                    // var fullPath = Path.Combine(Directory.GetCurrentDirectory(), UploadPath, fileName);
                    using (var fileSrteam = new FileStream(fullPath, FileMode.Create))
                    {
                        await FileDetail.CopyToAsync(fileSrteam);
                    }




                    msg = fileName;

                    return msg;
                }

                return ResponseErrorMessageUtility.NO_FILE_UPLOADED;
            }
            catch (Exception ex)
            {
                var ec = ex;
                return ResponseErrorMessageUtility.FILE_UPLOAD_FAILED + ex.Message;
            }
        }


        public async Task<string> AcceptAllUploadFile(IFormFile FileDetail, string UploadPath,   int _oneMegaByte, int _fileMaxSize, string extraParameter)
        {
            try
            {
                string msg = "";
                if (FileDetail != null)
                {
                   // var extension = FileDetail.FileName.Split('.')[1];
                    var extension = FileDetail.FileName.Split('.').LastOrDefault();
                 
                  
                  
                    var filesize = _fileMaxSize * _oneMegaByte;

                    if (FileDetail.Length == 0)
                    {
                        msg = ResponseErrorMessageUtility.NO_FILE_CONTENT;
                        return msg;
                    }



                    else if (FileDetail.Length > filesize)
                    {
                        msg = ResponseErrorMessageUtility.FILE_SIZE_EXCEEDED + _fileMaxSize + ResponseErrorMessageUtility.FILE_SIZE_UNIT;
                        return msg;
                    }


                    var fileName = Guid.NewGuid().ToString();
                    fileName += "." + extension;

                    string fullPath = Directory.GetCurrentDirectory() + UploadPath + fileName;



                    // var fullPath = Path.Combine(Directory.GetCurrentDirectory(), UploadPath, fileName);
                    using (var fileSrteam = new FileStream(fullPath, FileMode.Create))
                    {
                        await FileDetail.CopyToAsync(fileSrteam);
                    }




                    msg = fileName;

                    return msg;
                }

                return ResponseErrorMessageUtility.NO_FILE_UPLOADED;
            }
            catch (Exception ex)
            {
                var ec = ex;
                return ResponseErrorMessageUtility.FILE_UPLOAD_FAILED + ex.Message;
            }
        }

        public async Task<string> UploadFileValues(IFormFile FileDetail, string UploadPath, string FileAllowExtension,
                             int _oneMegaByte, int _fileMaxSize)
        {
            try
            {
                string msg = "";
                if (FileDetail != null)
                {
                    var extension = FileDetail.FileName.Split('.')[1];
                    var supportedTypes = FileAllowExtension.Split(',');
                    int checkExtension = 0;
                    foreach (var item in supportedTypes)
                    {

                        var kk = item.Replace("\"", "");
                        if (kk.ToLower().Trim() == extension.ToLower().Trim())

                        {
                            checkExtension = checkExtension + 1;
                        }
                    }

                    var filesize = _fileMaxSize * _oneMegaByte;

                    if (checkExtension == 0)

                    {
                        msg = ResponseErrorMessageUtility.INVALID_FILE_EXTENSION + FileAllowExtension;
                        return msg;
                    }

                    else if (FileDetail.Length == 0)
                    {
                        msg = ResponseErrorMessageUtility.NO_FILE_CONTENT;
                        return msg;
                    }



                    else if (FileDetail.Length > filesize)
                    {
                        msg = ResponseErrorMessageUtility.FILE_SIZE_EXCEEDED + _fileMaxSize + ResponseErrorMessageUtility.FILE_SIZE_UNIT;
                        return msg;
                    }


                    var fileName = Guid.NewGuid().ToString();
                    fileName += "." + extension;

                    string fullPath = Directory.GetCurrentDirectory() + UploadPath + fileName;



                    // var fullPath = Path.Combine(Directory.GetCurrentDirectory(), UploadPath, fileName);
                    using (var fileSrteam = new FileStream(fullPath, FileMode.Create))
                    {
                        await FileDetail.CopyToAsync(fileSrteam);
                    }



                    msg = fileName;

                    return msg;
                }

                return ResponseErrorMessageUtility.NO_FILE_UPLOADED;
            }
            catch (Exception ex)
            {
                var ec = ex;
                return ResponseErrorMessageUtility.FILE_UPLOAD_FAILED + ex.Message;
            }
        }

        public async Task<string> UploadByteFile(Byte[] attachment, string UploadPath, string fileName)
        {
            try
            {
                var filePath = Directory.GetCurrentDirectory() + UploadPath + fileName;
                var fs = new BinaryWriter(new FileStream(filePath, FileMode.Append, FileAccess.Write));
                fs.Write(attachment);
                fs.Close();

                return "";

            }
            catch (Exception ex)
            {
                return ResponseErrorMessageUtility.FILE_UPLOAD_FAILED + ex.Message;
            }
        }
    }
}