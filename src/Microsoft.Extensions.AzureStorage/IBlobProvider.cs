using System.Threading.Tasks;

namespace Microsoft.Extensions.AzureStorage
{
    public interface IBlobProvider
    {
        Task<string> UploadFile(string containerName, string filePath);
    }
}