using Microsoft.AspNetCore.Mvc;
using ShoppingProject.Data.Models;

namespace ShoppingProject.Services
{
    public interface IWishlistService
    {
        Task<List<Wishlist>> AddToWishlist();
    }
}
