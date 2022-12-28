using Azure.Storage.Blobs;
using AzureBlobProject.Models;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Sas;
using Azure.Storage.Blobs.Specialized;

namespace AzureBlobProject.Services;
public class BlobService : IBlobService
{
    private readonly BlobServiceClient _blobClient;

    public BlobService(BlobServiceClient blobClient)
    {
        _blobClient = blobClient;
    }

    public async Task<bool> DeleteBlob(string name, string containerName)
    {
        BlobContainerClient blobContainerClient = _blobClient.GetBlobContainerClient(containerName);

        var blobClient = blobContainerClient.GetBlobClient(name);

        return await blobClient.DeleteIfExistsAsync();
    }

    public async Task<List<string>> GetAllBlobs(string containerName)
    {
        BlobContainerClient blobContainerClient = _blobClient.GetBlobContainerClient(containerName);
        var blobs = blobContainerClient.GetBlobsAsync();

        var blobString = new List<string>();

        await foreach (var item in blobs)
        {
            blobString.Add(item.Name);
        }

        return blobString;
    }

    public async Task<List<Blob>> GetAllBlobsWithUri(string containerName)
    {
        BlobContainerClient blobContainerClient = _blobClient.GetBlobContainerClient(containerName);
        Azure.AsyncPageable<BlobItem> blobs = blobContainerClient.GetBlobsAsync();
        var blobList = new List<Blob>();
        string sasContainerSignature = "";

        if (blobContainerClient.CanGenerateSasUri)
        {
            sasContainerSignature = GenerateSasUri(blobContainerClient);
        }

        await foreach (var item in blobs)
        {
            BlobClient blobClient = blobContainerClient.GetBlobClient(item.Name);
            Blob blobIndividual = new()
            {
                Uri = blobClient.Uri.AbsoluteUri + "?" + sasContainerSignature
            };

            if (blobClient.CanGenerateSasUri)
            {
                BlobSasBuilder sasBuilder = new()
                {
                    BlobContainerName = blobClient.GetParentBlobContainerClient().Name,
                    BlobName = blobClient.Name,
                    Resource = "b",
                    ExpiresOn = DateTimeOffset.UtcNow.AddHours(1)
                };

                sasBuilder.SetPermissions(BlobSasPermissions.Read);

                blobIndividual.Uri = blobClient.GenerateSasUri(sasBuilder).AbsoluteUri;
            }

            await PopulateBlobList(blobList, blobClient, blobIndividual);
        }

        return blobList;
    }

    private static string GenerateSasUri(BlobContainerClient blobContainerClient)
    {
        string sasContainerSignature;
        BlobSasBuilder sasBuilder = new()
        {
            BlobContainerName = blobContainerClient.Name,
            Resource = "c",
            ExpiresOn = DateTimeOffset.UtcNow.AddHours(1)
        };

        sasBuilder.SetPermissions(BlobSasPermissions.Read);

        sasContainerSignature = blobContainerClient.GenerateSasUri(sasBuilder).AbsoluteUri.Split('?')[1].ToString();
        return sasContainerSignature;
    }

    private static async Task PopulateBlobList(List<Blob> blobList, BlobClient blobClient, Blob blobIndividual)
    {
        BlobProperties properties = await blobClient.GetPropertiesAsync();
        if (properties.Metadata.ContainsKey("title"))
        {
            blobIndividual.Title = properties.Metadata["title"];
        }
        if (properties.Metadata.ContainsKey("comment"))
        {
            blobIndividual.Comment = properties.Metadata["comment"];
        }
        blobList.Add(blobIndividual);
    }

    public async Task<string> GetBlob(string name, string containerName)
    {
        BlobContainerClient blobContainerClient = _blobClient.GetBlobContainerClient(containerName);

        var blobClient = blobContainerClient.GetBlobClient(name);

        return blobClient.Uri.AbsoluteUri;
    }

    public async Task<bool> UploadBlob(string name, IFormFile file, string containerName, Blob blob)
    {
        BlobContainerClient blobContainerClient = _blobClient.GetBlobContainerClient(containerName);

        var blobClient = blobContainerClient.GetBlobClient(name);

        var httpHeaders = new BlobHttpHeaders()
        {
            ContentType = file.ContentType
        };

        IDictionary<string, string> metadata = new Dictionary<string, string>();

        metadata.Add("title", blob.Title);
        metadata["comment"] = blob.Comment;

        var result = await blobClient.UploadAsync(file.OpenReadStream(), httpHeaders, metadata);

        //metadata.Remove("title");

        //await blobClient.SetMetadataAsync(metadata);

        return result != null;
    }

}