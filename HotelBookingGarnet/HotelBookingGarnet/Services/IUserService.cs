using System.Threading.Tasks;
using HotelBookingGarnet.Models;

namespace HotelBookingGarnet.Services
{
    public interface IUserService
    {
        Task<User> FindByUserIdAsync(long userId);
    }
}