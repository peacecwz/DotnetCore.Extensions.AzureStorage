using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Microsoft.Extensions.AzureStorage
{
    public class BlobProvider : IBlobProvider
    {
        private readonly CloudStorageAccount _cloudStorageAccount;
        public BlobProvider(AzureStorageOptions options)
        {
            _cloudStorageAccount = new CloudStorageAccount(
                new StorageCredentials(options.AccountName,
                    options.AccountKey), options.AccountName, true);
        }

        public async Task<string> UploadFile(string containerName, string filePath)
        {
            var blobClient = _cloudStorageAccount.CreateCloudBlobClient();
            var container = !string.IsNullOrWhiteSpace(containerName)
                ? blobClient.GetContainerReference(containerName)
                : blobClient.GetRootContainerReference();

            await container.SetPermissionsAsync(new BlobContainerPermissions()
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            });
            var blob = container.GetBlockBlobReference(Path.GetFileName(filePath));
            try
            {
                await blob.UploadFromFileAsync(filePath, AccessCondition.GenerateEmptyCondition(), new BlobRequestOptions(),
                    new OperationContext());
                return blob.Uri.ToString();
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}