using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentEmail.Core;
using FluentEmail.Mailgun;
using HotelBookingGarnet.Models;
using HotelBookingGarnet.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingGarnet.Services
{
    public class TaxiReservationService : ITaxiReservationService
    {
        private readonly ApplicationContext applicationContext;
        private readonly IMapper mapper;
        private readonly IUserService userService;

        public TaxiReservationService(ApplicationContext applicationContext, IMapper mapper, IUserService userService)
        {
            this.applicationContext = applicationContext;
            this.mapper = mapper;
            this.userService = userService;
        }

        public async Task<long> AddTaxiReservationAsync(TaxiReservationViewModel newTaxiReservation, string userId)
        {
            var TaxiReservation = mapper.Map<TaxiReservationViewModel, TaxiReservation>(newTaxiReservation);
            TaxiReservation.UserId = userId;
            await applicationContext.TaxiReservations.AddAsync(TaxiReservation);
            await applicationContext.SaveChangesAsync();
            await SendEmailAsync(TaxiReservation);
            return TaxiReservation.TaxiReservationId;
        }

        public async Task DeleteTaxiReservationByIdAsync(long taxiReservationId)
        {
            var taxiReservation =
               await applicationContext.TaxiReservations.FirstOrDefaultAsync(r => r.TaxiReservationId == taxiReservationId);
            applicationContext.TaxiReservations.Remove(taxiReservation);
            applicationContext.SaveChanges();
        }

        public async Task<TaxiReservation> FindTaxiReservationByIdAsync(long taxiReservationId)
        {
            var taxiReservation = await applicationContext.TaxiReservations.FirstOrDefaultAsync(t => t.TaxiReservationId == taxiReservationId);
            return taxiReservation;
        }

        public async Task<List<TaxiReservation>> FindTaxiReservationByUserIdAsync(string userId)
        {
            var taxiReservation = await applicationContext.TaxiReservations
                .Where(t => t.UserId == userId).OrderBy(t => t.TaxiReservationStart).ToListAsync();
            return taxiReservation;
        }
        public async Task<List<string>> TaxiReservationValidationAsync(TaxiReservationViewModel newTaxiReservation)
        {
            var dateValid = DateValidation(newTaxiReservation);
            AddErrorMessages(newTaxiReservation, dateValid);

            return newTaxiReservation.ErrorMessages;
        }
        private static void AddErrorMessages(TaxiReservationViewModel newTaxiReservation, string dateValid)
        {
            if (dateValid != null)
                newTaxiReservation.ErrorMessages.Add(dateValid);
        }
        private static string DateValidation(TaxiReservationViewModel newTaxiReservation)
        {
            var startDate = newTaxiReservation.TaxiReservationStart;

            return startDate < DateTime.Today ? "The booking cannot begin earlier than today!" : null;
        }
        private async Task SendEmailAsync(TaxiReservation taxiReservation)
        {
            var sender = new MailgunSender(
                "sandbox0ec3cdedf8584e3fa03c7b70b98fc52f.mailgun.org",
                "869a1d058062aee81f0348cb5cd5ace5-2dfb0afe-68088ff5"
            );
            Email.DefaultSender = sender;

            var user = await userService.FindUserByTaxiReservationIdAsync(taxiReservation.UserId);
            var taxi = await FindTaxiReservationByIdAsync(taxiReservation.TaxiReservationId);

            var template =
                " You received this message because your taxi has been booked in the Garnet Travel " +
                "reservation system." +
                "\r\n Your reservation time: " + taxi.TaxiReservationStart +
                "\r\n Pick-up location: " + taxi.StartLocal +
                "\r\n Drop-off location: " + taxi.EndLocal +
                "\r\n Number of travelers: " + taxi.NumberOfGuest +
                "\r\n Phone number: " + taxi.PhoneNumber +
                "\r\n\n Sincerely, Garnet Travel team" +
                "\r\n (This is an auto generated message, please do not reply!)";

            var email = Email
                .From("mailgun@sandbox0ec3cdedf8584e3fa03c7b70b98fc52f.mailgun.org", "GarnetTravel.Info")
                .To(user.Email)
                .Subject($"Reservation notification #{taxiReservation.TaxiReservationId}")
                .UsingTemplate(template, false, false);

            await email.SendAsync();
        }
    }
}
