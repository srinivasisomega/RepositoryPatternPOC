using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Application.Services
{     
        public class EmployeeService : IEmployeeService
        {
            private readonly IEmployeeRepository _repository;

            public EmployeeService(IEmployeeRepository repository)
            {
                _repository = repository;
            }

            public async Task<IEnumerable<EmployeeDto>> GetAllAsync()
            {
                var employees = await _repository.GetAllAsync();

                return employees.Select(e => new EmployeeDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    Email = e.Email,
                    RegistrationDate = e.RegistrationDate
                });
            }

            public async Task<EmployeeDto?> GetByIdAsync(int id)
            {
                var employee = await _repository.GetByIdAsync(id);
                if (employee == null) return null;

                return new EmployeeDto
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Email = employee.Email,
                    RegistrationDate = employee.RegistrationDate
                };
            }

            public async Task<int> CreateAsync(EmployeeCreateDto dto)
            {
                var employee = new Employee
                {
                    Name = dto.Name,
                    Email = dto.Email,
                    RegistrationDate = DateTime.UtcNow
                };

                await _repository.AddAsync(employee);
                return employee.Id;
            }

            public void UpdateAsync(int id, EmployeeUpdateDto dto)
            {
                var employee =  _repository.GetById(id);
                if (employee == null)
                    throw new KeyNotFoundException("Employee not found");

                employee.Name = dto.Name;
                employee.Email = dto.Email;

                 _repository.UpdateAsync(employee);
            }

            public void DeleteAsync(int id)
        {
            var employee = _repository.GetById(id);

            _repository.DeleteAsync(employee);
            }
        public async Task<PagedResultDto<EmployeeDto>> GetPagedAsync(
     int pageNumber,
     int pageSize)
        {
            var query = _repository.Query();

            var totalCount = await query.CountAsync();

            var employees = await query
                .OrderBy(e => e.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(e => new EmployeeDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    Email = e.Email,
                    Designation = e.Designation
                })
                .ToListAsync();

            return new PagedResultDto<EmployeeDto>
            {
                Items = employees,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }


    }


}
