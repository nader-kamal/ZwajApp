using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ZwajApp.API.Models;

namespace ZwajApp.API.Data
{
    public class ZwajRepository : IZwajRepository
    {
        private readonly DataContext _context;
        public ZwajRepository(DataContext context)
        {
            _context = context;

        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity); 
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<IEnumerable<User>> GetAll()
        {
           var userlst=await _context.Users.Include(u=>u.Photos).ToListAsync();
           return userlst;
        }

        public async Task<User> GetUser(int Id)
        {
            var user=await _context.Users.Include(u=>u.Photos).FirstOrDefaultAsync(x=>x.Id==Id);
            return user;
        }

        public async Task<bool> SaveAll()
        {
           return await _context.SaveChangesAsync()>0;
        }
    }
}