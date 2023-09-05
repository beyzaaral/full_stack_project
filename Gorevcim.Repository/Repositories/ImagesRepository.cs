using Gorevcim.Core.Models;
using Gorevcim.Core.Repositories;
using Gorevcim.Repository.AppDbContexts.AppDbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gorevcim.Repository.Repositories
{
    public class ImagesRepository : GenericRepository<Images>, IImagesRepository
    {
        public ImagesRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Images>> GetApiAllImagesAsync()
        {
            return await _context.Images.ToListAsync();
        }

        public async Task<List<Images>> GetWebAllImagesAsync()
        {
            return await _context.Images.ToListAsync();
        }
    }
}
