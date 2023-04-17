namespace WebLuto.Services.Interfaces
{
    public interface IFileService
    {
        string UploadBase64Image(string entityIdentifier, string base64Image, string container);

        string UpdateImageStorage(string imageUrl, string newBase64Image, string container);

        void DeleteImageStorage(string imageUrl, string container);
    }
}
