using CinemaTickets.Domain.DomainModels;
using CinemaTickets.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaTickets.Service.Interface
{
    public interface ITicketService
    {
        List<Ticket> GetAllTickets();
        Ticket GetDetailsForTicket(Guid? id);
        void CreateNewTicket(Ticket p);
        void UpdateExistingTicket(Ticket p);
        AddToShoppingCardDto GetShoppingCartInfo(Guid? id);
        void DeleteTicket(Guid id);
        bool AddToShoppingCart(AddToShoppingCardDto item, string userID);
    }
}
