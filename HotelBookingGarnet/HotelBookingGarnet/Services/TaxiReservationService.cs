using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HotelBookingGarnet.Models;
using HotelBookingGarnet.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingGarnet.Services
{
    public class TaxiReservationService : ITaxiReservationService
    {
        private readonly ApplicationContext applicationContext;
        private readonly IMapper mapper;

        public TaxiReservationService(ApplicationContext applicationContext, IMapper mapper)
        {
            this.applicationContext = applicationContext;
            this.mapper = mapper;
        }

        public async Task<long> AddTaxiReservationAsync(TaxiReservationViewModel newTaxiReservation, string userId)
        {
            var TaxiReservation = mapper.Map<TaxiReservationViewModel, TaxiReservation>(newTaxiReservation);
            TaxiReservation.UserId = userId;
            await applicationContext.TaxiReservations.AddAsync(TaxiReservation);
            await applicationContext.SaveChangesAsync();

            return TaxiReservation.TaxiReservationId;
        }

        public async Task<List<TaxiReservation>> FindTaxiReservationByUserIdAsync(string userId)
        {
            var taxiReservation = await applicationContext.TaxiReservations
                .Where(t => t.UserId == userId).OrderBy(t => t.TaxiReservationStart).ToListAsync();
            return taxiReservation;
        }
    }
}
