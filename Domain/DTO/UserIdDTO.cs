using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class UserIdDTO
    {
        public string UserFullName { get; set; }

        public string UserPassword { get; set; }
        public string UserEmail { get; set; }
        public int UserSID { get; set; }

    }
}
