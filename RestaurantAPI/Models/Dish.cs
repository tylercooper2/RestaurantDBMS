﻿using System;
using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.Models
{
    public class Dish
    {
        [Key]
        public int Dish_ID { get; set; }
        public bool Available { get; set; }
        public Decimal Price { get; set; }
        public string Description { get; set; }
        public string Menu_Type { get; set; }
    }
}
