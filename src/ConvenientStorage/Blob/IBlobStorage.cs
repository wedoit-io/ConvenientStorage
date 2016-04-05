namespace Convenient.Storage.Blob
{
    using System.IO;
    using System.Threading.Tasks;

    public interface IBlobStorage
    {
        /// <summary>
        ///     Saves the source.
        /// </summary>
        /// <returns>Saved location.</returns>
        Task SaveAsync(Stream source, string blobName);
    }
}
