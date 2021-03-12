using System;
using System.Collections.Generic;

#nullable disable

namespace MSR.DAL.Entities
{
    public partial class WebTrendResourceDatum
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int? Month { get; set; }
        public int? Year { get; set; }
        public int? Status { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
