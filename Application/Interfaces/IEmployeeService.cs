using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks;
namespace Application.Interfaces
{
    

    
        public interface IEmployeeService
        {
            Task<IEnumerable<EmployeeDto>> GetAllAsync();
            Task<EmployeeDto?> GetByIdAsync(int id);
            Task<int> CreateAsync(EmployeeCreateDto dto);
            void UpdateAsync(int id, EmployeeUpdateDto dto);
            void DeleteAsync(int id);
        Task<PagedResultDto<EmployeeDto>> GetPagedAsync(
           int pageNumber,
           int pageSize);
    }
    

}
