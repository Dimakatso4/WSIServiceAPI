using System;
using System.IO;
using System.Threading.Tasks;

namespace WSIServiceAPI.Services
{
    public interface IBlobService
    {
        Task<Uri> UploadFileBlobAsync(string blobContainerName, Stream content, string contentType, string fileName);
    }
}
