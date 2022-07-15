using DemoWIUTGallery.Data;
using DemoWIUTGallery.Interfaces;
using DemoWIUTGallery.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoWIUTGallery.Repositories
{
    public class GalleryRepo : ICreate<Gallery>, IDelete<Gallery>, IRead<Gallery>, IReadRange<Gallery>, IUpdate<Gallery>
    {
        private readonly ApplicationDbContext _dbContext;

        public GalleryRepo(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> CreateAsync(Gallery _object)
        {
            if(_object.PathFile == null)
            {
                _object.PathFile = "avatar.png";
            }
            await _dbContext.Galleries.AddAsync(_object);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public bool Delete(Gallery _object)
        {
            _dbContext.Galleries.Remove(_object);
            _dbContext.SaveChanges();
            return true;
        }

        public async Task<IEnumerable<Gallery>> GetAllAsync()
        {
            return await _dbContext.Galleries.OrderBy(x=>x.AddedDate).ToListAsync();
        }

        public async Task<Gallery> GetByIdAsync(Guid? id)
        {
            return await _dbContext.Galleries.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> Update(Gallery _object)
        {
            _dbContext.Update(_object);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
