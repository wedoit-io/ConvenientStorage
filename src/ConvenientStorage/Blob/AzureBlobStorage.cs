namespace Convenient.Storage.Blob
{
    using System.IO;
    using System.Threading.Tasks;
    using Microsoft.WindowsAzure.Storage.Blob;

    public class AzureBlobStorage : AzureStorage, IBlobStorage
    {
        private readonly string containerName;

        private CloudBlobContainer cachedBlobContainer;

        /// <param name="connectionName">Name of the setting that contains connection string to a Azure Cloud Storage Account.</param>
        /// <param name="containerName">
        ///     A container name must be a valid DNS name, conforming to the following naming rules:
        ///     (1) Container names must start with a letter or number, and can contain only letters, numbers, and the dash (-)
        ///     character;
        ///     (2) Every dash (-) character must be immediately preceded and followed by a letter or number; consecutive dashes
        ///     are
        ///     not permitted in container names;
        ///     (3) All letters in a container name must be lowercase;
        ///     (4) Container names must be from 3 through 63 characters long.
        ///     (ref.: https://msdn.microsoft.com/en-us/library/azure/dd135715.aspx)
        /// </param>
        public AzureBlobStorage(string connectionName, string containerName)
            : base(connectionName)
        {
            this.containerName = containerName;
        }

        public async Task SaveAsync(Stream source, string blobName)
        {
            var blobContainer = await this.GetCloudBlobContainer()
                                          .ConfigureAwait(false);

            var blockBlob = blobContainer.GetBlockBlobReference(blobName);

            await blockBlob.UploadFromStreamAsync(source)
                           .ConfigureAwait(false);
        }

        }

        #region /// internal ///////////////////////////////////////////////////

        private async Task<CloudBlobContainer> GetCloudBlobContainer()
        {
            if (this.cachedBlobContainer != null)
            {
                return this.cachedBlobContainer;
            }

            var blobContainer = this.CloudBlobClient.GetContainerReference(this.containerName);

            await blobContainer.CreateIfNotExistsAsync()
                               .ConfigureAwait(false);

            return this.cachedBlobContainer = blobContainer;
        }

        #endregion
    }
}
