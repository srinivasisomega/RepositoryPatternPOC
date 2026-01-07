using Domain.Entities;
namespace Application.Interfaces
{
   public interface IEmployeeRepository
        {
            Task<IEnumerable<Employee>> GetAllAsync();
            Task<Employee> GetByIdAsync(int id);
        Employee? GetById(int id);

        Task AddAsync(Employee employee);
            void UpdateAsync(Employee employee);
            void DeleteAsync(Employee employee);
        IQueryable<Employee> Query();


    }
}
