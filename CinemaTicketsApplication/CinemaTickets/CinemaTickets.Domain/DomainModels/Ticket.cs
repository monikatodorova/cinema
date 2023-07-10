using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaTickets.Domain.DomainModels
{
    public class Ticket : BaseEntity
    {
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string TicketImage { get; set; }
        [Required]
        public string TicketDescription { get; set; }
        [Required]
        public int Price { get; set; }
        public virtual ICollection<TicketInShoppingCart> TicketInShoppingCarts { get; set; }
        public virtual ICollection<TicketInOrder> Orders { get; set; }
    }
}
