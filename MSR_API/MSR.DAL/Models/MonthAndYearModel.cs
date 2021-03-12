using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSRDAL.Models
{
    public class MonthAndYearModel
    {
        public List<MonthModel> Months { get; set; }
        public List<YearModel> Years { get; set; }
    }
    public class MonthModel
    {
        public int Id { get; set; }
        public string MonthName { get; set; }
    }
    public class YearModel
    {
        public int Id { get; set; }
        public string Year { get; set; }
    }
}
