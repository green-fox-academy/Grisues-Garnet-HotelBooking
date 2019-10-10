using System.Threading.Tasks;
using HotelBookingGarnet.ViewModels;

namespace HotelBookingGarnet.Services
{
    public interface IGuestService
    {
        Task SaveGuestAsync(ReservationViewModel reservationViewModel, long reservationId);
    }
}