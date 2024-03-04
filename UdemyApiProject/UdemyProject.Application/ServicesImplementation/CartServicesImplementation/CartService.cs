using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyProject.Contract.RepositoryContracts;
using UdemyProject.Contracts.DTOs.CartItem;
using UdemyProject.Contracts.RepositoryContracts;
using UdemyProject.Contracts.ServicesContracts;
using UdemyProject.Domain.Entities;
using static System.Collections.Specialized.BitVector32;

namespace UdemyProject.Application.ServicesImplementation.CartServicesImplementation
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _CartRepository;
        private readonly ICourseRepository _CourseRepository;
        private readonly ICartItemRepository _CartItemRepository;
        private readonly ICourseSectionRepository _CourseSectionRepository;
        private readonly IHttpContextAccessor _HttpContextAccessor;

        public CartService(
            ICartRepository cartRepository,
            ICourseRepository courseRepository,
            ICartItemRepository cartItemRepository,
            ICourseSectionRepository courseSectionRepository,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _CartRepository = cartRepository;
            _CourseRepository = courseRepository;
            _CartItemRepository = cartItemRepository;
            _CourseSectionRepository = courseSectionRepository;
            _HttpContextAccessor = httpContextAccessor;
        }

        public async Task<CartForReturn> GetCartsByUser(string userId)
        {
            var Cart = await _CartRepository.GetFirstOrDefault(c => c.applicationUserId == userId && !c.isDeleted, new[] { "cartItems" });
            if (Cart is null)
                return new CartForReturn();

            var cartitems = await _CartItemRepository.GetAllAsNoTracking(c => c.cartId == Cart.Id, new[] { "course" });

            return new CartForReturn()
            {
                cartId = Cart.Id,
                userId = userId,
                totlaPrice = cartitems.Sum(Cartitem => Cartitem.coursePrice),
                items = cartitems.Select(Cartitem => new cartItemsForReturn()
                {
                    courseId = Cartitem.courseId.Value,
                    cartItemId = Cartitem.Id,
                    cartItemImage = Cartitem.course.Image == null ? null : Path.Combine(@$"{_HttpContextAccessor.HttpContext.Request.Scheme}://{_HttpContextAccessor.HttpContext.Request.Host}", "CourseImages", Cartitem.course.Image),
                    cartItemRating = 4,
                    cartItemTitle = Cartitem.course.Title,
                    price = Cartitem.coursePrice,
                    LecturesCount = _CourseSectionRepository.GetAllAsNoTracking(c => c.CourseId == Cartitem.courseId, new[] { "Lecture" }).Result.Sum(c => c.Lecture.Count()),
                    totalMinutes = _CourseSectionRepository.GetAllAsNoTracking(c => c.CourseId == Cartitem.courseId).Result.Sum(s => s.Lecture.Sum(l => l.VideoMinuteLength.HasValue ? l.VideoMinuteLength.Value : 0)),
                }).ToList()
            };
        }
    }
}