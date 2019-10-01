using System.Threading.Tasks;
using HotelBookingGarnet.ViewModels;

namespace HotelBookingGarnet.Services
{
    public interface IBedService
    {
        Task AddBedAsync(BedViewModel newBed, long roomId);
    }
}