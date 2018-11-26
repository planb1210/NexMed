using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexMed.Entities
{
    public class UserRegisterModel
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string City { get; set; }

        public string Password1 { get; set; }

        public string Password2 { get; set; }
    }
}
