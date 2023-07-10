using CinemaTickets.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaTickets.Service.Interface
{
    public interface IShoppingCartService
    {
        ShoppingCartDto getShoppingCartInfo(string userId);
        bool deleteTicketFromSoppingCart(string userId, Guid ticketId);
        bool OrderNow(string userId);
    }
}
