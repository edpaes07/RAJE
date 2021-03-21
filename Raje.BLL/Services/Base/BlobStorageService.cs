using Raje.DL.Services.BLL.Base;
using Raje.Infra.Const;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Raje.BLL.Services
{
    public class BlobStorageService : IBlobStorageService
    {
        private readonly IConfiguration _configuration;

        public BlobStorageService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Saves an image file to the blob storage configured on app settings file
        /// </summary>
        /// <param name="filename">Image file name</param>
        /// <param name="imageBuffer">Image file bytes buffer</param>
        /// <param name="stream">Image file in-memory stream</param>
        /// <returns>Returns TRUE and STRING containing the public URL of the image saved</returns>
        public async Task<(bool, string)> SaveImageToStorage(string filename, string folder, byte[] imageBuffer = null, Stream stream = null, string oldFileName = null)
        {
            var cloudBlobContainerResult = await GetCloudBlobContainer();
            // Check if a container was returned
            if (!cloudBlobContainerResult.Item1)
                return (false, null);

            CloudBlobDirectory directory = cloudBlobContainerResult.Item2.GetDirectoryReference(folder);
            CloudBlockBlob cloudBlockBlob = await GetCloudBlockBlob(directory, filename, oldFileName);
            return await UploadFile(cloudBlockBlob, imageBuffer, stream);
        }

        #region [private]
        private (bool, CloudBlobClient) GetCloudBlobClient()
        {
            string storageConnectionString = _configuration["StorageConnectionString"];

            // Check whether the connection string can be parsed.
            if (CloudStorageAccount.TryParse(storageConnectionString, out CloudStorageAccount storageAccount))
            {
                // Create the CloudBlobClient that represents the Blob storage endpoint for the storage account.
                CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
                return (true, cloudBlobClient);
            }
            else
                return (false, null);
        }

        private async Task<(bool, CloudBlobContainer)> GetCloudBlobContainer(string containerName = RajeBlobContainer.rajeblob, bool publicAcess = true)
        {
            var cloudClientResult = GetCloudBlobClient();
            if (!cloudClientResult.Item1)
                return (false, null);
            CloudBlobClient cloudBlobClient = cloudClientResult.Item2;

            // Create a container called 'uploadblob' and append a GUID value to it to make the name unique. 
            CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(containerName);
            if (!cloudBlobContainer.Exists())
                await cloudBlobContainer.CreateAsync();

            if (!publicAcess)
                return (true, cloudBlobContainer);

            // Set the permissions so the blobs are public. 
            BlobContainerPermissions permissions = new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            };

            await cloudBlobContainer.SetPermissionsAsync(permissions);
            return (true, cloudBlobContainer);
        }

        private async Task<CloudBlockBlob> GetCloudBlockBlob(CloudBlobContainer container, string filename, string oldFileName = null)
        {
            //delete old file if exist
            if (!String.IsNullOrEmpty(oldFileName))
            {
                CloudBlockBlob cloudBlockBlobToBeDeleted = container.GetBlockBlobReference(oldFileName);
                await cloudBlockBlobToBeDeleted.DeleteIfExistsAsync();
            }

            // Get a reference to the blob address, then upload the file to the blob.
            CloudBlockBlob cloudBlockBlob = container.GetBlockBlobReference(filename);
            return cloudBlockBlob;
        }

        private async Task<CloudBlockBlob> GetCloudBlockBlob(CloudBlobDirectory container, string filename, string oldFileName = null)
        {
            //delete old file if exist
            if (!String.IsNullOrEmpty(oldFileName))
            {
                CloudBlockBlob cloudBlockBlobToBeDeleted = container.GetBlockBlobReference(oldFileName);
                await cloudBlockBlobToBeDeleted.DeleteIfExistsAsync();
            }

            // Get a reference to the blob address, then upload the file to the blob.
            CloudBlockBlob cloudBlockBlob = container.GetBlockBlobReference(filename);
            return cloudBlockBlob;
        }

        private async Task<(bool, string)> UploadFile(CloudBlockBlob cloudBlockBlob, byte[] imageBuffer = null, Stream stream = null)
        {
            if (imageBuffer != null)
            {
                await cloudBlockBlob.UploadFromByteArrayAsync(imageBuffer, 0, imageBuffer.Length);
            }
            else if (stream != null)
            {
                await cloudBlockBlob.UploadFromStreamAsync(stream);
            }
            else
            {
                return (false, null);
            }

            return (true, cloudBlockBlob.SnapshotQualifiedStorageUri.PrimaryUri.ToString());
        }

        private async Task<(bool, string)> UploadFile(CloudBlockBlob cloudBlockBlob, string fileContent, string contentType)
        {
            if (fileContent == null)
                return (false, null);

            cloudBlockBlob.Properties.ContentType = contentType;
            await cloudBlockBlob.UploadTextAsync(fileContent);

            return (true, cloudBlockBlob.SnapshotQualifiedStorageUri.PrimaryUri.ToString());
        }
        #endregion
    }
}
