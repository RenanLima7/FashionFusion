using System.Text.RegularExpressions;
using WebLuto.Security;
using WebLuto.Services.Interfaces;
using WebLuto.Utils;
using WebLuto.Utils.Messages;

namespace WebLuto.Services
{
    public class FileService : IFileService
    {
        public string UploadBase64Image(string base64Image, string container)
        {
            try
            {
                string fileName = UtilityMethods.GenerateRandomFileName();

                string data = new Regex(@"^data:image\/[a-z]+;base64,").Replace(base64Image, "");

                byte[] imageBytes = Convert.FromBase64String(data);

                /*BlobClient blobClient = new BlobClient(new Settings().AzureStorage, container, fileName);

                using (MemoryStream stream = new MemoryStream(imageBytes))
                {
                    if (blobClient.Exists())
                    {
                        fileName = UtilityMethods.GenerateRandomFileName();
                        blobClient = new BlobClient(new Settings().AzureStorage, container, fileName);
                    }

                    blobClient.Upload(stream);
                }*/

                return " "; // blobClient.Uri.AbsoluteUri;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format(FileMsg.EXC0001, ex.Message));
            }
        }

        public string UpdateImageStorage(string imageUrl, string newBase64Image, string container)
        {
            try
            {
                string blobName = Regex.Match(imageUrl, @"[^\/]+$").Value;
                string data = new Regex(@"^data:image\/[a-z]+;base64,").Replace(newBase64Image, "");
                byte[] imageBytes = Convert.FromBase64String(data);

                /*BlobClient blobClient = new BlobClient(new Settings().AzureStorage, container, blobName);

                using (MemoryStream stream = new MemoryStream(imageBytes))
                {
                    blobClient.Upload(stream, true);
                }*/

                return " "; // blobClient.Uri.AbsoluteUri;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format(FileMsg.EXC0002, ex.Message));
            }
        }

        public void DeleteImageStorage(string imageUrl, string container)
        {
            try
            {
                //
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format(FileMsg.EXC0003, ex.Message));
            }
        }
    }
}
