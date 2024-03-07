using Microsoft.AspNetCore.Http;
using SimpleEcommerce.Infrastructure.RepositoryImplementation;
using Stripe.Checkout;
using System.Reflection.Metadata;
using UdemyProject.Contract.RepositoryContracts;
using UdemyProject.Contracts.DTOs.CartItem;
using UdemyProject.Contracts.RepositoryContracts;
using UdemyProject.Contracts.ServicesContracts;
using UdemyProject.Domain.Entities;

namespace UdemyProject.Application.ServicesImplementation.CartServicesImplementation
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _CartRepository;
        private readonly ICourseRepository _CourseRepository;
        private readonly ICartItemRepository _CartItemRepository;
        private readonly ICourseSectionRepository _CourseSectionRepository;
        private readonly IHttpContextAccessor _HttpContextAccessor;
        private readonly IUserRepository _UserRepository;

        public CartService(
            ICartRepository cartRepository,
            ICourseRepository courseRepository,
            ICartItemRepository cartItemRepository,
            ICourseSectionRepository courseSectionRepository,
            IHttpContextAccessor httpContextAccessor,
            IUserRepository userRepository
            )
        {
            _CartRepository = cartRepository;
            _CourseRepository = courseRepository;
            _CartItemRepository = cartItemRepository;
            _CourseSectionRepository = courseSectionRepository;
            _HttpContextAccessor = httpContextAccessor;
            _UserRepository = userRepository;
        }

        public async Task<Session> CheckOut(CheckOutProperties checkOutProperties)
        {
            var cart = await _CartRepository.GetFirstOrDefault(c => c.Id == checkOutProperties.cartId && c.applicationUserId == checkOutProperties.userId && !c.isPaid, new[] { "cartItems" });

            if (cart is null)

                return null;

            var user = await _UserRepository.GetFirstOrDefault(c => c.Id == cart.applicationUserId, new[] { "coursesInrollments" });

            //var s = _HttpContextAccessor.HttpContext.Request.Headers.ContainsKey("");

            if (user is null)
                return null;

            var options = new SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>()
       ,
                Mode = "payment",
                PaymentMethodTypes = new List<string>
                     {
                         "card",
                     },
                SuccessUrl = checkOutProperties.url + $"?paymentsuccessfuly={true}",
                CancelUrl = checkOutProperties.url + $"?paymentsuccessfuly={false}",
            };

            var sessionLineItem = new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    UnitAmount = (long)cart.totalPrice * 100,
                    Currency = "egp",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = "Any thing",
                        Description = "Any Discription"
                    },
                },
                Quantity = 1
            };
            options.LineItems.Add(sessionLineItem);
            var service = new SessionService();
            Session session = service.Create(options);

            cart.sessionPaymentId = session.Id;
            _CartRepository.Update(cart);
            await _CartItemRepository.SaveChanges();
            return session;
        }

        public async Task CoursePaymentConfirmation(string userId)
        {
            var cart = await _CartRepository.GetFirstOrDefault(c => c.applicationUserId == userId && !c.isPaid, new[] { "cartItems" });

            var user = await _UserRepository.GetFirstOrDefault(c => c.Id == userId, new[] { "coursesInrollments" });
            if (cart is null)
            {
                return;
            }

            var service = new SessionService();
            Session session = service.Get(cart.sessionPaymentId);

            if (session.PaymentStatus.ToLower() == "paid")
            {
                foreach (var cartItem in cart.cartItems)
                    user.coursesInrollments.Add(await _CourseRepository.GetFirstOrDefault(c => c.Id == cartItem.courseId));

                _UserRepository.Update(user);
                var IsInrollment = await _UserRepository.SaveChanges();

                if (IsInrollment)
                {
                    cart.isPaid = true;
                    _CartRepository.Update(cart);
                    await _CartRepository.SaveChanges();
                }
            }
        }

        public async Task<CartForReturn> GetCartsByUser(string userId)
        {
            var Cart = await _CartRepository.GetFirstOrDefault(c => c.applicationUserId == userId && !c.isPaid, new[] { "cartItems" });
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