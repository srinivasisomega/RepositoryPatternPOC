using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Application.DTOs
{
    
        public class EmployeeCreateDto
        {
            public string Name { get; set; } = default!;
            public string Email { get; set; } = default!;
        public string Designation { get; set; } = default!;
    }
}
