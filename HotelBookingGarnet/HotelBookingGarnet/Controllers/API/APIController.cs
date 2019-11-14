using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HotelBookingGarnet.DTOs;
using HotelBookingGarnet.Models;
using HotelBookingGarnet.Services;
using HotelBookingGarnet.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingGarnet.Controllers.API
{
    [ApiController]
    public class APIController : ControllerBase
    {
        private readonly ITaxiReservationService taxiReservationService;
        private readonly IMapper mapper;
        private readonly ApplicationContext applicationContext;
        private readonly IUserService userService;

        public APIController(ITaxiReservationService taxiReservationService, IMapper mapper, ApplicationContext applicationContext, IUserService userService)
        {
            this.taxiReservationService = taxiReservationService;
            this.mapper = mapper;
            this.applicationContext = applicationContext;
            this.userService = userService;
        }

        [HttpGet("/api/taxireservation/{taxiReservationId?}")]
        public async Task<ActionResult<TaxiReservation>> FindTaxiReservation([FromRoute] long taxiReservationId)
        {
            var taxiRes = taxiReservationService.FindTaxiReservationByIdAsync(taxiReservationId);
            if (taxiReservationId == 0 || taxiRes == null )
            {
                return BadRequest("No taxireservation found");
            }
            else
            {
                var taxiReservation = await taxiReservationService.FindTaxiReservationByIdAsync(taxiReservationId);
                return taxiReservation;
            }
        }

        [HttpPost("/api/taxireservation/{userId?}")]
        public async Task<ActionResult<TaxiReservation>> AddTaxiReservation([FromRoute] string UserId, [FromBody] APITaxiReservationDTO newAPITaxiReservationDTO)
        {
            var User = await userService.FindUserByUserIdAsync(UserId);
            if (UserId == null || User == null)
            {
                return BadRequest("No User founded");
            }
            if (UserId != null && newAPITaxiReservationDTO != null)
            {
                var TaxiReservationVM = mapper.Map<APITaxiReservationDTO, TaxiReservationViewModel>(newAPITaxiReservationDTO);
                var TaxiReservation = mapper.Map<TaxiReservationViewModel, TaxiReservation>(TaxiReservationVM);
                TaxiReservation.UserId = UserId;
                await applicationContext.TaxiReservations.AddAsync(TaxiReservation);
                await applicationContext.SaveChangesAsync();
                return TaxiReservation;
            }
            return BadRequest("TaxiReservation adding fail");
        }
        [HttpDelete]
        [Route("/api/taxireservation/{taxiReservationId?}")]
        public async Task<ActionResult> DeleteTaxiReservation([FromRoute]long taxiReservationId)
        {
            var taxiRes = taxiReservationService.FindTaxiReservationByIdAsync(taxiReservationId);
            if (taxiReservationId == 0 || taxiRes == null)
            {
                return BadRequest("There is no taxireservation that can be deleted");
            }
            else
            {
              await  taxiReservationService.DeleteTaxiReservationByIdAsync(taxiReservationId);
                return StatusCode(200);
            }
        }
    }
}