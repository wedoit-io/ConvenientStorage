namespace Convenient.Storage
{
    using Microsoft.Azure;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;

    public abstract class AzureStorage
    {
        private readonly string connectionName;

        private CloudBlobClient cachedCloudBlobClient;

        protected AzureStorage(string connectionName)
        {
            this.connectionName = connectionName;
        }

        protected CloudBlobClient CloudBlobClient
        {
            get
            {
                if (this.cachedCloudBlobClient != null)
                {
                    return this.cachedCloudBlobClient;
                }

                return this.cachedCloudBlobClient = CreateCloudBlobClient(this.connectionName);
            }
        }

        #region /// internal ///////////////////////////////////////////////////

        private static CloudBlobClient CreateCloudBlobClient(string connectionName)
        {
            var connectionString = CloudConfigurationManager.GetSetting(connectionName);
            var cloudStorageAccount = CloudStorageAccount.Parse(connectionString);

            return cloudStorageAccount.CreateCloudBlobClient();
        }

        #endregion
    }
}
