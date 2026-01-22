using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
        public class EmployeeUpdateDto
        {
            public string Name { get; set; } = default!;
            public string Email { get; set; } = default!;
        public string designation {  get; set; } = default!;
        }
}
