using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
namespace Infrastructure.Repositories
{
    public class EmployeeRepository :IEmployeeRepository
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<Employee> _dbSet;

        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<Employee>();
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }
        public Employee? GetById(int id)
        {
            return _dbSet.Find(id);
        }
        public async Task AddAsync(Employee entity)
        {
            try
            {
                await _dbSet.AddAsync(entity);
                await _context.SaveChangesAsync();

            }
            catch (DbException ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void UpdateAsync(Employee entity)
        {
            _dbSet.Update(entity);
            _context.SaveChanges();

        }

        public void DeleteAsync(Employee entity)
        {
            _dbSet.Remove(entity);
            _context.SaveChanges();

        }
        public IQueryable<Employee> Query()
        {
            return _context.Employees.AsNoTracking();
        }

    }
}
