using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Models
{
    public class User
    {
        public int MemberId { get; set; }
        public string Token { get; set; }
        public string Pseudo { get; set; }
        public string Email { get; set; }
    }
}
