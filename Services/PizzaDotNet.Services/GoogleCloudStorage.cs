namespace PizzaDotNet.Services
{
    using System.IO;
    using System.Threading.Tasks;

    using Google.Apis.Auth.OAuth2;
    using Google.Cloud.Storage.V1;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;

    public class GoogleCloudStorage : IGoogleCloudStorage
    {
        private readonly GoogleCredential googleCredential;
        private readonly StorageClient storageClient;
        private readonly string bucketName;

        public GoogleCloudStorage(IConfiguration configuration)
        {
            this.googleCredential = GoogleCredential.FromFile(configuration.GetValue<string>("GoogleCredentialFile"));
            this.storageClient = StorageClient.Create(this.googleCredential);
            this.bucketName = configuration.GetValue<string>("GoogleCloudStorageBucket");
        }

        public async Task<string> UploadFileAsync(IFormFile imageFile, string fileNameForStorage)
        {
            using (var memoryStream = new MemoryStream())
            {
                await imageFile.CopyToAsync(memoryStream);
                var dataObject =
                    await this.storageClient.UploadObjectAsync(this.bucketName, fileNameForStorage, null, memoryStream);
                return dataObject.MediaLink;
            }
        }

        public async Task DeleteFileAsync(string fileNameForStorage)
        {
            await this.storageClient.DeleteObjectAsync(this.bucketName, fileNameForStorage);
        }
    }
}
