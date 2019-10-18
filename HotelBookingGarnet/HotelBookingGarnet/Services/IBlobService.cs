using Microsoft.Azure.Storage.Blob;
using System.Threading.Tasks;


namespace HotelBookingGarnet.Services
{
    public interface IBlobService
    {
        Task<CloudBlobContainer> GetBlobContainer();
    }
}