using System;
using System.Collections.Generic;
using System.Text;

namespace MSR.DAL.Models
{
  public  class ResponceModel
    {
     
            public bool success { get; set; }
            public string message { get; set; }
            public object data { get; set; }
       
    }
}
