using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiNet6.Models
{
    public partial class Ncd
    {
        public Ncd()
        {
            NcdDetails = new HashSet<NcdDetail>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<NcdDetail> NcdDetails { get; set; }
    }
}
