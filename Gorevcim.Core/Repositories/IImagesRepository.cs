using Gorevcim.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gorevcim.Core.Repositories
{
    public interface IImagesRepository : IGenericRepository<Images>
    {
        Task<List<Images>> GetApiAllImagesAsync();
        Task<List<Images>> GetWebAllImagesAsync();
    }
}
