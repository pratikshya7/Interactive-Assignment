using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Models
{
    public class GithubSearchObject
    {
        public int Total_count { get; set; }
        public List<Items> Items { get; set; }
    }
}
