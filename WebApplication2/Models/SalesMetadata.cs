using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class SalesMetadata
    {
        private pubsEntities db = new pubsEntities();

        [Display(Name="Store ID")]
        [Required]
        [StringLength(4,MinimumLength = 4, ErrorMessage ="Store ID must be 4 characters!")]
        public string stor_id;


        [Display(Name = "Store")]
        [Required]
        public string stor_name;

        [Display(Name = "Order Number ")]
        [Required]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "Order number must be between 4 and 20 characters!")]
        public string ord_num;

        [Display(Name = "Order Date")]
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime ord_date;

        
        [Display(Name = "Quantity ")]
        [Required]
        [Range(1, 50)]
        public short qty;

        [Display(Name = "Payterms ")]
        [Required]
        public string payterms;

        [Display(Name = "Book title ")]
        [Required]
        public string title_id;
    }
}