using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HotelBookingGarnet.Models;
using HotelBookingGarnet.ViewModels;

namespace HotelBookingGarnet.Services
{
    public interface IReviewService
    {
        Task AddReviewAsync(long hotelId, IndexViewModel newReview, string userId);
        bool Reviewed(List<Review> reviews, string userId);
    }
}