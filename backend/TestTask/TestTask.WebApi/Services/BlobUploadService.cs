using Azure.Storage.Blobs;

namespace TestTask.WebApi.Services;

public interface IBlobUploadService
{
    void UploadFile(IFormFile file, string email);
}

public class BlobUploadService : IBlobUploadService
{
    private string blobConnectionString;
    private string blobContainer = "taskstorage";
    private readonly IConfiguration _configuration;

    public BlobUploadService(IConfiguration configuration)
    {
        _configuration = configuration;

        blobConnectionString = _configuration.GetValue<string>("BlobConnectionString");
    }

    public void UploadFile(IFormFile file, string email)
    {
        BlobContainerClient container = new BlobContainerClient(blobConnectionString, blobContainer);
        
        /// Uploaded file name will set to:
        /// *Email*|*FileName*
        /// Therefore different users can upload files with same name
        string fileName = email+"|"+file.FileName;

        var blobClient = container.GetBlobClient(fileName);

        using (var stream = file.OpenReadStream())
        {
            blobClient.Upload(stream, true);
        }
    }
}
