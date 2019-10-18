using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HotelBookingGarnet.Services
{
    public interface IDateTimeService
    {
        List<SelectListItem> FindTimeZones(); 
    }
}