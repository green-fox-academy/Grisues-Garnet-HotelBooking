﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HotelBookingGarnet.Models;
using HotelBookingGarnet.ViewModels;

namespace HotelBookingGarnet.Services
{
    public class ReviewService : IReviewService
    {
        private readonly ApplicationContext applicationContext;

        public ReviewService(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }

        public async Task AddReviewAsync(long hotelId, IndexViewModel newReview, string userId)
        {
            var review = new Review
            {
                Rating = newReview.Rating,
                Text = newReview.Text,
                HotelId = hotelId,
                UserId = userId
            };
            await applicationContext.Reviews.AddAsync(review);
            await applicationContext.SaveChangesAsync();
        }

        public bool Reviewed(List<Review> reviews, User user)
        {
            bool isReviewed = false;
            if (user != null)
            {
                foreach (var review in reviews)
                {
                    if (review.UserId == user.Id)
                    {
                        isReviewed = true;
                    }
                }
            }
            return isReviewed;
        }
    }
}