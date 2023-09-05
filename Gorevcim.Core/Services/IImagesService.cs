using Gorevcim.Core.DTOs;
using Gorevcim.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gorevcim.Core.Services
{
    public interface IImagesService: IGenericService<Images>
    {
        public Task<CustomResponseDto<List<ImagesDto>>> GetApiAllImages();
        public Task<List<ImagesDto>> GetWebAllImages();
        public Task<List<ImagesDto>> GetWebAllImagesAsync();
    }
}
