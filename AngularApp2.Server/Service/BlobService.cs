
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace AngularApp2.Server.Service;

public class BlobService : IBlobService
{
    private readonly BlobServiceClient _blobServiceClient;

    private const string ContainerName = "files";

    public BlobService(BlobServiceClient blobServiceClient)
    {
        _blobServiceClient = blobServiceClient;
    }


    public async Task<bool> DeleteAsync(Guid fileId, CancellationToken token)
    {
        BlobContainerClient blobContainerClient = _blobServiceClient.GetBlobContainerClient(ContainerName);
        BlobClient blobClient = blobContainerClient.GetBlobClient(fileId.ToString());

        var result =  await blobClient.DeleteAsync();

        return !result.IsError;
    }

    public async Task<FileResonse> DownloadAsync(Guid fileId, CancellationToken cancellationToken)
    {
        BlobContainerClient blobContainerClient = _blobServiceClient.GetBlobContainerClient(ContainerName);
        BlobClient blobClient = blobContainerClient.GetBlobClient(fileId.ToString());

        var response = await blobClient.DownloadContentAsync(new BlobDownloadOptions {  }, cancellationToken: cancellationToken);

        return new FileResonse(response.Value.Content.ToStream(), response.Value.Details.ContentType);
    }

    public async Task<Guid> UploadFileAsync(Stream stream, string ContentType, CancellationToken cancellationToken)
    {
        var fileId = Guid.NewGuid();

        BlobContainerClient blobContainerClient = _blobServiceClient.GetBlobContainerClient(ContainerName);
        BlobClient blobClient = blobContainerClient.GetBlobClient(fileId.ToString());

        await blobClient.UploadAsync(stream, new BlobHttpHeaders {  ContentType = ContentType },cancellationToken: cancellationToken);

        return fileId;  
    }
}
