﻿using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.Models
{
    public class In_Store_Order
    {
        [Key]
        public int Order_ID { get; set; }
        public int waiter_ID { get; set; }
    }
}
