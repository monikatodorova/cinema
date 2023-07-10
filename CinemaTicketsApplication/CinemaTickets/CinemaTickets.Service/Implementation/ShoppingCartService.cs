using CinemaTickets.Domain.DomainModels;
using CinemaTickets.Domain.DTO;
using CinemaTickets.Repository.Interface;
using CinemaTickets.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CinemaTickets.Service.Implementation
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IRepository<ShoppingCart> _shoppingCartRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<TicketInOrder> _ticketInOrderRepository;
        private readonly IUserRepository _userRepository;

        public ShoppingCartService(IRepository<ShoppingCart> shoppingCartRepository, IRepository<Order> orderRepository, IRepository<TicketInOrder> ticketInOrderRepository, IUserRepository userRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _orderRepository = orderRepository;
            _ticketInOrderRepository = ticketInOrderRepository;
            _userRepository = userRepository;
        }

        public bool deleteTicketFromSoppingCart(string userId, Guid ticketId)
        {
            if (!string.IsNullOrEmpty(userId) && ticketId != null)
            {
                var loggedInUser = this._userRepository.Get(userId);

                var userShoppingCart = loggedInUser.Cart;

                var itemToDelete = userShoppingCart.Tickets.Where(z => z.TicketId.Equals(ticketId)).FirstOrDefault();

                userShoppingCart.Tickets.Remove(itemToDelete);

                this._shoppingCartRepository.Update(userShoppingCart);

                return true;
            }
            return false;
        }

        public ShoppingCartDto getShoppingCartInfo(string userId)
        {
            var loggedInUser = this._userRepository.Get(userId);

            var userShoppingCart = loggedInUser.Cart;

            var allTickets = userShoppingCart.Tickets.ToList();

            var ticketPrice = allTickets.Select(z => new 
            {
                TicketPrice = z.Ticket.Price,
                Quantity = z.Quantity
            }).ToList();

            var totalPrice = 0;

            foreach (var item in ticketPrice)
            {
                totalPrice += item.TicketPrice * item.Quantity;
            }

            ShoppingCartDto shoppingCartDtoItem = new ShoppingCartDto
            {
                TicketInShoppingCarts = allTickets,
                TotalPrice = totalPrice
            };

            return shoppingCartDtoItem;
        }

        public bool OrderNow(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                var loggedInUser = this._userRepository.Get(userId);

                var userShoppingCart = loggedInUser.Cart;

                Order orderItem = new Order
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    User = loggedInUser
                };

                this._orderRepository.Insert(orderItem);

                List<TicketInOrder> ticketInOrders = new List<TicketInOrder>();

                var result = userShoppingCart.Tickets.Select(z => new TicketInOrder
                    {
                        OrderId = orderItem.Id,
                        TicketId = z.Ticket.Id,
                        SelectedTicket = z.Ticket,
                        UserOrder = orderItem
                    }).ToList();

                ticketInOrders.AddRange(result);

                foreach (var item in ticketInOrders)
                {
                    this._ticketInOrderRepository.Insert(item);
                }

                loggedInUser.Cart.Tickets.Clear();

                this._userRepository.Update(loggedInUser);

                return true;
            }
            return false;
        }
    }
}
