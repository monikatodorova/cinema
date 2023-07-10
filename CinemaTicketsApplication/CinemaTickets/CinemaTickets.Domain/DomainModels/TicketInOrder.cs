﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaTickets.Domain.DomainModels
{
    public class TicketInOrder : BaseEntity
    {
        public Guid TicketId { get; set; }
        public Ticket SelectedTicket { get; set; }
        public Guid OrderId { get; set; }
        public Order UserOrder { get; set; }
    }
}
