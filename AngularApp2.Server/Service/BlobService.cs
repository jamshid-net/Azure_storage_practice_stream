
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace AngularApp2.Server.Service;

public class BlobService : IBlobService
{
    

    private readonly BlobContainerClient _containerClient;

    private const string ContainerName = "files";

    public BlobService(BlobServiceClient blobServiceClient)
    {

        _containerClient = blobServiceClient.GetBlobContainerClient(ContainerName);
        _containerClient.CreateIfNotExistsAsync(PublicAccessType.Blob)
                        .ConfigureAwait(true);
    }


    public async Task<bool> DeleteAsync(Guid fileId, CancellationToken token)
    {
       
        BlobClient blobClient = _containerClient.GetBlobClient(fileId.ToString());

        var result =  await blobClient.DeleteAsync();

        return !result.IsError;
    }

    public async Task<FileResonse> DownloadAsync(Guid fileId, CancellationToken cancellationToken)
    {
        
        BlobClient blobClient = _containerClient.GetBlobClient(fileId.ToString());

        var response = await blobClient.DownloadContentAsync(new BlobDownloadOptions {  }, cancellationToken: cancellationToken);

        return new FileResonse(response.Value.Content.ToStream(), response.Value.Details.ContentType);
    }

    public async Task<Guid> UploadFileAsync(Stream stream, string ContentType, CancellationToken cancellationToken)
    {
        var fileId = Guid.NewGuid();

      
        BlobClient blobClient = _containerClient.GetBlobClient(fileId.ToString());

        await blobClient.UploadAsync(stream, new BlobHttpHeaders {  ContentType = ContentType },cancellationToken: cancellationToken);

        return fileId;  
    }

    public async IAsyncEnumerable<string> GetAllFilesId()
    {
       
        var blobs = _containerClient.GetBlobsAsync();

        await foreach (var blob in blobs)
        {
            yield return blob.Name;
        }
    }

   
    
}
