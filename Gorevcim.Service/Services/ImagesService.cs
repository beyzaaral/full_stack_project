using AutoMapper;
using Gorevcim.Core.DTOs;
using Gorevcim.Core.Models;
using Gorevcim.Core.Repositories;
using Gorevcim.Core.Services;
using Gorevcim.Core.UnitOfWorks;
using Gorevcim.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gorevcim.Service.Services
{
    public class ImagesService : GenericService<Images>, IImagesService
    {
        private readonly IImagesRepository _imagesRepository;
        private readonly IMapper _mapper;

        public ImagesService(IGenericRepository<Images> repository,IImagesRepository imagesRepository ,IMapper mapper,IGenericUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
            _imagesRepository = imagesRepository;
            _mapper = mapper;
        }

        public async Task<CustomResponseDto<List<ImagesDto>>> GetApiAllImages()
        {
            var images = await _imagesRepository.GetApiAllImagesAsync();
            var imagesDtos = _mapper.Map<List<ImagesDto>>(images);
            return CustomResponseDto<List<ImagesDto>>.Success(200, imagesDtos);
        }

        public async Task<List<ImagesDto>> GetWebAllImages()
        {
            var images = await _imagesRepository.GetWebAllImagesAsync();
            var imagesDtos = _mapper.Map<List<ImagesDto>>(images);
            return imagesDtos;
        }

        public async Task<List<ImagesDto>> GetWebAllImagesAsync()
        {
            var images = await _imagesRepository.GetWebAllImagesAsync();
            var imagesDtos = _mapper.Map<List<ImagesDto>>(images);
            return imagesDtos;
        }
    }
}
