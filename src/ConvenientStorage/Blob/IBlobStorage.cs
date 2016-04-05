namespace Convenient.Storage.Blob
{
    using System.IO;
    using System.Threading.Tasks;

    public interface IBlobStorage
    {
        Task SaveAsync(Stream source, string blobName);

        Task LoadAsync(string blobName, Stream destination);
    }
}
