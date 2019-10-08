using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HotelBookingGarnet.Models;
using HotelBookingGarnet.Utils;
using Microsoft.EntityFrameworkCore;
using HotelBookingGarnet.ViewModels;
using ReflectionIT.Mvc.Paging;

namespace HotelBookingGarnet.Services
{
    public class HotelService : IHotelService
    {
        private readonly ApplicationContext applicationContext;
        private readonly IPropertyTypeService propertyTypeService;
        private readonly IMapper mapper;

        public HotelService(ApplicationContext applicationContext, IPropertyTypeService propertyTypeService,
            IMapper mapper)
        {
            this.applicationContext = applicationContext;
            this.propertyTypeService = propertyTypeService;
            this.mapper = mapper;
        }

        public async Task EditHotelAsync(long hotelId, HotelViewModel editHotel)
        {
            var hotelToEdit = await FindHotelByIdAsync(hotelId);
            if (hotelToEdit != null)
            {
                hotelToEdit = mapper.Map(editHotel, hotelToEdit);
                applicationContext.Hotels.Update(hotelToEdit);
                await applicationContext.SaveChangesAsync();
            }
        }

        public async Task<Hotel> FindHotelByIdAsync(long hotelId)
        {
            var foundHotel = await applicationContext.Hotels.Include(p => p.HotelPropertyTypes)
                .Include(h => h.Rooms).SingleOrDefaultAsync(x => x.HotelId == hotelId);

            return foundHotel;
        }

        public async Task AddHotelAsync(HotelViewModel newHotel, string userId)
        {
            var propertyType = await propertyTypeService.AddPropertyTypeAsync(newHotel.PropertyType);

            var hotel = mapper.Map<HotelViewModel, Hotel>(newHotel);
            hotel.UserId = userId;
            hotel.HotelPropertyTypes = new List<HotelPropertyType>();

            {
//                HotelName = newHotel.HotelName,
//                Country = newHotel.Country,
//                Region = newHotel.Region,
//                City = newHotel.City,
//                Address = newHotel.Address,
//                Description = newHotel.Description,
//                StarRating = newHotel.StarRating,
            }
            ;

            await applicationContext.Hotels.AddAsync(hotel);
            await applicationContext.SaveChangesAsync();

            var hotelPropertyType = new HotelPropertyType();
            hotelPropertyType.Hotel = hotel;
            hotelPropertyType.HotelId = hotel.HotelId;
            hotelPropertyType.PropertyType = propertyType;
            hotelPropertyType.PropertyTypeId = propertyType.PropertyTypeId;

            hotel.HotelPropertyTypes.Add(hotelPropertyType);

            await applicationContext.SaveChangesAsync();
        }

        public List<Hotel> GetHotels()
        {
            var qry = applicationContext.Hotels.Include(h => h.Rooms).AsQueryable().OrderBy(h => h.HotelName).ToList();
            return qry;
        }

        public async Task<PagingList<Hotel>> FilterHotelsAsync(QueryParam queryParam)
        {
            var allHotels = GetHotels();
            foreach (var hotel in allHotels)
            {
                if (hotel.Rooms != null)
                {
                    foreach (var room in hotel.Rooms)
                    {
                        if (room.NumberOfAvailablePlaces >= queryParam.Guest)
                        {
                            hotel.IsItAvailable = true;
                            await applicationContext.SaveChangesAsync(hotel.IsItAvailable);
                            break;
                        }
                        else
                        {
                            hotel.IsItAvailable = false;
                            await applicationContext.SaveChangesAsync(hotel.IsItAvailable);
                        }
                    }
                }
            }

            var hotels = await applicationContext.Hotels.Include(h => h.Rooms)
                .Where(h => h.City.Contains(queryParam.City) || String.IsNullOrEmpty(queryParam.City))
                .Where(h => h.IsItAvailable || queryParam.Guest == 0)
                .OrderBy(h => h.HotelName).ToListAsync();
            return PagingList.Create(hotels, 5, queryParam.Page);
        }
    }
}