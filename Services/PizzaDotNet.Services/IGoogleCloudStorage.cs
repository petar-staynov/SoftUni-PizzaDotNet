namespace PizzaDotNet.Services
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public interface IGoogleCloudStorage
    {
        Task<string> UploadFileAsync(IFormFile imageFile, string fileNameForStorage);

        Task DeleteFileAsync(string fileNameForStorage);
    }
}
