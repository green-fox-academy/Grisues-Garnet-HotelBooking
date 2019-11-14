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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using TimeZoneInfo = System.TimeZoneInfo;

namespace HotelBookingGarnet.Services
{
    public class HotelService : IHotelService
    {
        private readonly ApplicationContext applicationContext;
        private readonly IPropertyTypeService propertyTypeService;
        private readonly IImageService imageService;
        private readonly IMapper mapper;

        public HotelService(ApplicationContext applicationContext, IPropertyTypeService propertyTypeService,
            IImageService imageService, IMapper mapper)
        {
            this.applicationContext = applicationContext;
            this.propertyTypeService = propertyTypeService;
            this.imageService = imageService;
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
                .Include(h => h.Rooms).Include(r => r.Reviews).SingleOrDefaultAsync(x => x.HotelId == hotelId);

            return foundHotel;
        }

        public async Task<long> AddHotelAsync(HotelViewModel newHotel, string userId)
        {
            var propertyType = await propertyTypeService.AddPropertyTypeAsync(newHotel.PropertyType);
            var hotel = mapper.Map<HotelViewModel, Hotel>(newHotel);
            hotel.UserId = userId;
            hotel.HotelPropertyTypes = new List<HotelPropertyType>();
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

        public async Task<Hotel> FindHotelByNameAsync(string hotelName)
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

                        hotel.IsItAvailable = false;
                        await applicationContext.SaveChangesAsync(hotel.IsItAvailable);
                    }
                }
            }

            var hotels = await applicationContext.Hotels.Include(h => h.Rooms)
                .Where(h => h.City.Contains(queryParam.City) || String.IsNullOrEmpty(queryParam.City))
                .Where(h => h.IsItAvailable || queryParam.Guest == 0)
                .OrderBy(h => h.HotelName).ToListAsync();
            return PagingList.Create(hotels, 3, queryParam.Page);
        }

        public async Task SetIndexImageAsync(long hotelId)
        {
            var hotel = await FindHotelByIdAsync(hotelId);
            var pictures = await imageService.ListAsync(hotelId);
            hotel.Uri = pictures[0].Path;
            applicationContext.Hotels.Update(hotel);
            await applicationContext.SaveChangesAsync();
        }

        public async Task<List<Hotel>> ListMyHotelsAsync(string userId)
        {
            var myHotels = await applicationContext.Hotels.Include(h => h.Rooms)
                .Include(h => h.HotelPropertyTypes)
                .Where(h => h.UserId == userId).OrderBy(h => h.HotelName).ToListAsync();
            return myHotels;
        }

        public double AverageRating(List<Review> reviews)
        {
            if (reviews.Count > 0)
            {
                var sum = 0;
                foreach (var review in reviews)
                {
                    sum += review.Rating;
                }

                var avg = sum / reviews.Count;
                return avg;
            }

            return 0;
        }

        public PagingList<Review> ReviewsList(List<Review> reviews, QueryParam queryParam)
        {
            return PagingList.Create(reviews, 2, queryParam.Page);
        }
    }
}