using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HotelBookingGarnet.Services
{
    public class DateTimeService : IDateTimeService
    {
        private readonly ApplicationContext applicationContext;
        
        public DateTimeService(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }

        public List<SelectListItem> FindTimeZones() 
        { 
            var timeZones = TimeZoneInfo.GetSystemTimeZones(); 
            List<SelectListItem> items = new List<SelectListItem>(); 
            foreach (var timeZone in timeZones)
            { 
                items.Add(new SelectListItem() {Text = timeZone.Id});
            } 
            return items; 
        } 
    } 
}
