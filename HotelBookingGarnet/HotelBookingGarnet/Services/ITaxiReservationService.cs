using HotelBookingGarnet.Models;
using HotelBookingGarnet.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBookingGarnet.Services
{
    public interface ITaxiReservationService
    {
        Task<long> AddTaxiReservationAsync(TaxiReservationViewModel newTaxiReservation, string userId);
        Task<List<TaxiReservation>> FindTaxiReservationByUserIdAsync(string userId);
        Task DeleteTaxiReservationByIdAsync(long taxiReservationId);
        Task<TaxiReservation> FindTaxiReservationByIdAsync(long taxiReservationId);
    }
}
