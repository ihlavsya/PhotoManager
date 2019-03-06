using System;
using System.IO;
using System.Web;

namespace Helpers
{
    public static class FileExtension
    {
        public static void UploadFile(this HttpPostedFileBase file, string filename)
        {
            try
            {
                int length = 256;
                int bytesRead = 0;
                Byte[] buffer = new Byte[length];

                using (FileStream fs =new FileStream(filename, FileMode.Create))
                {
                    do
                    {
                        bytesRead = file.InputStream.Read(buffer, 0, length);
                        fs.Write(buffer, 0, bytesRead);
                    }
                    while (bytesRead == length);
                }

                file.InputStream.Dispose();
            }
            catch (Exception)
            {
                throw new FileLoadException();
            }
        }
    }
}
