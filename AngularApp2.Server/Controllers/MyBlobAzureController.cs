using AngularApp2.Server.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AngularApp2.Server.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class MyBlobAzureController : ControllerBase
{
    private readonly IBlobService _blobservice;

    public MyBlobAzureController(IBlobService blobservice)
    {
        _blobservice = blobservice;
    }


    [HttpPost]
    public async Task<Guid> PostFile(IFormFile file, CancellationToken  token)
    {
        using var stream = file.OpenReadStream();

        var fileId =  await _blobservice.UploadFileAsync(stream, file.ContentType);

        return fileId;
    }

    [HttpGet]
    public async Task<IActionResult> GetFile(Guid id, CancellationToken token)
    {
        var foundFile = await _blobservice.DownloadAsync(id, token);

        return File(foundFile.Stream, foundFile.contentType, enableRangeProcessing: true);
    }
}
