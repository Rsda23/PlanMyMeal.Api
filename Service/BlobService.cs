using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Sas;
using PlanMyMeal.Api.Interface;

namespace PlanMyMeal.Api.Service
{
    public class BlobService : IBlobService
    {
        private readonly IUserService _userService;
        private readonly BlobServiceClient _blobService;
        public BlobService(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _blobService = new BlobServiceClient(configuration["AzureBlob:ConnectionString"]);
        }

        public async Task<string> PostImage(string container, IFormFile image)
        {
            var blob = _blobService.GetBlobContainerClient(container);
            await blob.CreateIfNotExistsAsync();

            var blobName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";

            using var s = image.OpenReadStream();
            await blob.GetBlobClient(blobName).UploadAsync(s, new BlobUploadOptions
            {
                HttpHeaders = new BlobHttpHeaders
                {
                    ContentType = image.ContentType ?? "application/octet-stream"
                }
            });

            return $"{container}/{blobName}";
        }
    }

}
