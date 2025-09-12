using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Sas;
using PlanMyMeal.Api.Interface;

namespace PlanMyMeal.Api.Service
{
    public class BlobService : IBlobService
    {
        private readonly BlobServiceClient _blobService;
        public BlobService(IConfiguration configuration)
        {
            _blobService = new BlobServiceClient(configuration["AzureBlob:ConnectionString"]);
        }
    }

}
