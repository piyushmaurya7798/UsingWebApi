using System.ComponentModel.DataAnnotations;

namespace UsingWebApi.Models
{
    public class Emp
    {

            public int id { get; set; }
            public string pname { get; set; }
            public string department { get; set; }
            public double salary { get; set; }
    }
}
