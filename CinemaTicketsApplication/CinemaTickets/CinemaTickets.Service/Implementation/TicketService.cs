using CinemaTickets.Domain.DomainModels;
using CinemaTickets.Domain.DTO;
using CinemaTickets.Repository.Interface;
using CinemaTickets.Service.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CinemaTickets.Service.Implementation
{
    public class TicketService : ITicketService
    {
        private readonly IRepository<Ticket> _ticketRepository;
        private readonly IRepository<TicketInShoppingCart> _ticketInShoppingCartRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<TicketService> _logger;

        public TicketService(IRepository<Ticket> ticketRepository, IRepository<TicketInShoppingCart> ticketInShoppingCartRepository, IUserRepository userRepository, ILogger<TicketService> logger)
        {
            _ticketRepository = ticketRepository;
            _ticketInShoppingCartRepository = ticketInShoppingCartRepository;
            _userRepository = userRepository;
            _logger = logger;
        }

        public bool AddToShoppingCart(AddToShoppingCardDto item, string userID)
        {
            var user = this._userRepository.Get(userID);

            var userShoppingCard = user.Cart;

            if (item.TicketId != null && userShoppingCard != null)
            {
                var ticket = this.GetDetailsForTicket(item.TicketId);

                if (ticket != null)
                {
                    TicketInShoppingCart itemToAdd = new TicketInShoppingCart
                    {
                        Ticket = ticket,
                        TicketId = ticket.Id,
                        Cart = userShoppingCard,
                        ShoppingCartId = userShoppingCard.Id,
                        Quantity = item.Quantity
                    };

                    this._ticketInShoppingCartRepository.Insert(itemToAdd);
                    _logger.LogInformation("Ticket was successfully added into ShoppingCart.");
                    return true;
                }
                return false;
            }
            _logger.LogInformation("Something was wrong.");
            return false;
        }

        public void CreateNewTicket(Ticket p)
        {
            this._ticketRepository.Insert(p);
        }

        public void DeleteTicket(Guid id)
        {
            var ticket = this.GetDetailsForTicket(id);
            this._ticketRepository.Delete(ticket);
        }

        public List<Ticket> GetAllTickets()
        {
            _logger.LogInformation("GetAllTickets was called!");
            return this._ticketRepository.GetAll().ToList();
        }

        public Ticket GetDetailsForTicket(Guid? id)
        {
            return this._ticketRepository.Get(id);
        }

        public AddToShoppingCardDto GetShoppingCartInfo(Guid? id)
        {
            var ticket = this.GetDetailsForTicket(id);
            AddToShoppingCardDto model = new AddToShoppingCardDto()
            {
                SelectedTicket = ticket,
                TicketId = ticket.Id,
                Quantity = 1
            };
            return model;
        }

        public void UpdateExistingTicket(Ticket p)
        {
            this._ticketRepository.Update(p);
        }
    }
}
