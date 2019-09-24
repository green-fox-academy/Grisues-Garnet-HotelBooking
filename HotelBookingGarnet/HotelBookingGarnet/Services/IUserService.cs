using System.Threading.Tasks;

namespace HotelBookingGarnet.Services
{
    public interface IUserService
    {
        Task<string> FindByEmailAsync(string email);
        
    }
}