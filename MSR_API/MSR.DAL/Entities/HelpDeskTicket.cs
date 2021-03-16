using System;
using System.Collections.Generic;

#nullable disable

namespace MSR.DAL.Entities
{
    public partial class HelpDeskTicket
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public int? Value { get; set; }
        public int? Month { get; set; }
        public int? Year { get; set; }
    }
}
