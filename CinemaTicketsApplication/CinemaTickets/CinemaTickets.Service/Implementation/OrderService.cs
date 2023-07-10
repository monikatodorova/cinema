﻿using CinemaTickets.Domain.DomainModels;
using CinemaTickets.Repository.Interface;
using CinemaTickets.Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaTickets.Service.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public List<Order> getAllOrders()
        {
            return this._orderRepository.getAllOrders();
        }

        public Order getOrderDetails(BaseEntity model)
        {
            return this._orderRepository.getOrderDetails(model);
        }
    }
}
