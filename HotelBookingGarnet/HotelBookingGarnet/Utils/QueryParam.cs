using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace HotelBookingGarnet.Utils
{
    public class QueryParam
    {
        public string City { get; set; } = "";
        public int Guest { get; set; } = 0;
        public int Page { get; set; } = 1;

        public DateTime start { get; set; }
        public DateTime end { get; set; }
    }
}