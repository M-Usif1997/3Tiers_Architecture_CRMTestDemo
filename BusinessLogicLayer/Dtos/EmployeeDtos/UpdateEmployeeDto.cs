using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Dtos.EmployeeDtos
{
    public class UpdateEmployeeDto
    {
        public string? Name { get; set; } // Optional
        public string? Email { get; set; } // Optional
        public string? Phone { get; set; } // Optional
        public string? Position { get; set; } // Optional
        public Guid? ManagerID { get; set; }
    }
}
