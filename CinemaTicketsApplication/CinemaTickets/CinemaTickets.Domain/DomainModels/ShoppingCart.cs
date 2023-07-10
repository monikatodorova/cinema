﻿using CinemaTickets.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaTickets.Domain.DomainModels
{
    public class ShoppingCart : BaseEntity
    {
        public string OwnerId { get; set; }
        public virtual CinemaTicketsApplicationUser Owner { get; set; }
        public virtual ICollection<TicketInShoppingCart> Tickets { get; set; }

        public ShoppingCart() { }
    }
}
