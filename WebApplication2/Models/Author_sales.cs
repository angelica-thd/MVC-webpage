using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class Author_sales
    {
        public Author_sales(){
            this.titleauthor = new HashSet<titleauthor>();
            this.author = new HashSet<author>();
            this.sale = new HashSet<sales>();

        }
        public virtual ICollection<titleauthor> titleauthor { get; set; }
        public virtual ICollection<author> author { get; set; }
        public virtual ICollection<sales> sale { get; set; }
    }