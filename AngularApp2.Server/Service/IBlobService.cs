namespace AngularApp2.Server.Service;

public interface IBlobService
{
    Task<Guid> UploadFileAsync(Stream stream, string ContentType, CancellationToken cancellationToken = default);
    Task<FileResonse> DownloadAsync(Guid fileId, CancellationToken cancellationToken = default);

    Task<bool> DeleteAsync(Guid fileId, CancellationToken token = default);

    IAsyncEnumerable<string> GetAllFilesId();

}


public record FileResonse(Stream Stream, string contentType);