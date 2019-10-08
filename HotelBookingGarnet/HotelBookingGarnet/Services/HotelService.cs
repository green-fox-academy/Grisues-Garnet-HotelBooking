using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelBookingGarnet.Models;
using HotelBookingGarnet.Utils;
using Microsoft.EntityFrameworkCore;
using HotelBookingGarnet.ViewModels;
using ReflectionIT.Mvc.Paging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HotelBookingGarnet.Services
{
    public class HotelService : IHotelService
    {
        private readonly ApplicationContext applicationContext;
        private readonly IPropertyTypeService propertyTypeService;
        private readonly IImageService imageService;

        public HotelService(ApplicationContext applicationContext, IPropertyTypeService propertyTypeService,
            IImageService imageService)
        {
            this.applicationContext = applicationContext;
            this.propertyTypeService = propertyTypeService;
            this.imageService = imageService;
        }

        public async Task EditHotelAsync(long hotelId, HotelViewModel editHotel)
        {
            var hotelToEdit = await FindHotelByIdAsync(hotelId);
            if (hotelToEdit != null)
            {
                hotelToEdit.HotelName = editHotel.HotelName;
                hotelToEdit.Country = editHotel.Country;
                hotelToEdit.Region = editHotel.Region;
                hotelToEdit.City = editHotel.City;
                hotelToEdit.Address = editHotel.Address;
                hotelToEdit.Description = editHotel.Description;
                hotelToEdit.StarRating = editHotel.StarRating;
            }

            applicationContext.Hotels.Update(hotelToEdit);
            await applicationContext.SaveChangesAsync();
        }

        public async Task<Hotel> FindHotelByIdAsync(long hotelId)
        {
            var foundHotel = await applicationContext.Hotels.Include(p => p.HotelPropertyTypes)
                .Include(h => h.Rooms).SingleOrDefaultAsync(x => x.HotelId == hotelId);

            return foundHotel;
        }

        public async Task<long> AddHotelAsync(HotelViewModel newHotel, string userId)
        {
            var propertyType = await propertyTypeService.AddPropertyTypeAsync(newHotel.PropertyType);

            var hotel = new Hotel
            {
                HotelName = newHotel.HotelName,
                Country = newHotel.Country,
                Region = newHotel.Region,
                City = newHotel.City,
                Address = newHotel.Address,
                Description = newHotel.Description,
                StarRating = newHotel.StarRating,
                UserId = userId,
                HotelPropertyTypes = new List<HotelPropertyType>()
            };

            await applicationContext.Hotels.AddAsync(hotel);
            await applicationContext.SaveChangesAsync();

            var hotelPropertyType = new HotelPropertyType();
            hotelPropertyType.Hotel = hotel;
            hotelPropertyType.HotelId = hotel.HotelId;
            hotelPropertyType.PropertyType = propertyType;
            hotelPropertyType.PropertyTypeId = propertyType.PropertyTypeId;

            hotel.HotelPropertyTypes.Add(hotelPropertyType);

            await applicationContext.SaveChangesAsync();
            return hotel.HotelId;
        }
        
        public List<Hotel> GetHotels()
        {
            var qry = applicationContext.Hotels.Include(h => h.Rooms).AsQueryable().OrderBy(h => h.HotelName).ToList();
            return qry;
        }

        public async Task<Hotel> FindHotelByName(string hotelName)
        {
            var foundedHotel = await applicationContext.Hotels.Include(a => a.HotelPropertyTypes)
                .FirstOrDefaultAsync(a => a.HotelName == hotelName);
            return foundedHotel;
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

        public async Task SetIndexImageAsync(long hotelId)
        {
            var hotel = await FindHotelByIdAsync(hotelId);
            var pictures = await imageService.ListAsync(hotelId);
            hotel.Uri = pictures[0].Path;
            applicationContext.Hotels.Update(hotel);
            await applicationContext.SaveChangesAsync();
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