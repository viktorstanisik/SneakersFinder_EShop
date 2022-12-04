﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrapper_Domain.Models
{
    public class SportVisonDbModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Brand { get; set; }
        public int RegularPrice { get; set; }
        public int PriceWithDiscount { get; set; }
        public int DiscountPercent { get; set; }
        public string Link { get; set; }
        public int Store { get; set; }
    }
}
