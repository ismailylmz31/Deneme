using AutoMapper;
using Core.AOP.Aspects;
using Core.Persistence.Extensions;
using System.Linq.Expressions;
using TechCareer.DataAccess.Repositories.Abstracts;
using TechCareer.Models.Dtos.Category;
using TechCareer.Models.Entities;
using TechCareer.Service.Abstracts;
using TechCareer.Service.Constants;
using TechCareer.Service.Rules;

namespace TechCareer.Service.Concretes
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly CategoryBusinessRules _businessRules;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper, CategoryBusinessRules businessRules)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _businessRules = businessRules;
        }

        //[LoggerAspect]
        //[ClearCacheAspect("Categories")]
        //[AuthorizeAspect("Admin")]
        public async Task<CategoryResponseDto> AddAsync(CreateCategoryRequestDto dto)
        {
            await _businessRules.CategoryNameMustBeUnique(dto.Name);

            var categoryEntity = _mapper.Map<Category>(dto);

            var addedCategory = await _categoryRepository.AddAsync(categoryEntity);

            CategoryResponseDto responseDto = _mapper.Map<CategoryResponseDto>(addedCategory);
            return responseDto;
        }

        ////[LoggerAspect]
        ////[ClearCacheAspect("Categories")]
        ////[AuthorizeAspect("Admin")]
        public async Task<string> DeleteAsync(int id, bool permanent = false)
        {
            var categoryEntity = await _businessRules.CategoryMustExist(id);
            await _categoryRepository.DeleteAsync(categoryEntity, permanent);
            return CategoryMessages.CategoryDeleted;
        }

        //[LoggerAspect]
        //[ClearCacheAspect("Categories")]
        //[AuthorizeAspect("Admin")]
        public async Task<CategoryResponseDto> UpdateAsync(int id, UpdateCategoryRequestDto dto)
        {
            var categoryEntity = await _businessRules.CategoryMustExist(id);

            _mapper.Map(dto, categoryEntity);

            var updatedCategory = await _categoryRepository.UpdateAsync(categoryEntity);
            CategoryResponseDto responseDto = _mapper.Map<CategoryResponseDto>(updatedCategory);
            return responseDto;
        }

        //[CacheAspect(cacheKeyTemplate: "CategoryList", bypassCache: false, cacheGroupKey: "Categories")]
        public async Task<List<CategoryResponseDto>> GetListAsync(
            Expression<Func<Category, bool>>? predicate = null,
            Func<IQueryable<Category>, IOrderedQueryable<Category>>? orderBy = null,
            bool include = false,
            bool withDeleted = false,
            bool enableTracking = true,
            CancellationToken cancellationToken = default)
        {
            var categories = await _categoryRepository.GetListAsync(predicate, orderBy, include, withDeleted, enableTracking, cancellationToken);
            return _mapper.Map<List<CategoryResponseDto>>(categories);
        }

        public async Task<Paginate<CategoryResponseDto>> GetPaginateAsync(
            Expression<Func<Category, bool>>? predicate = null,
            Func<IQueryable<Category>, IOrderedQueryable<Category>>? orderBy = null,
            bool include = true,
            int index = 0,
            int size = 10,
            bool withDeleted = false,
            bool enableTracking = true,
            CancellationToken cancellationToken = default)
        {
            var categories = await _categoryRepository.GetPaginateAsync(
                predicate,
                orderBy,
                include,
                index,
                size,
                withDeleted,
                enableTracking,
                cancellationToken
            );

            return new Paginate<CategoryResponseDto>
            {
                Items = _mapper.Map<IList<CategoryResponseDto>>(categories.Items),
                Index = categories.Index,
                Size = categories.Size,
                Count = categories.Count,
                Pages = categories.Pages
            };
        }

        public async Task<CategoryResponseDto> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var categoryEntity = await _businessRules.CategoryMustExist(id);
            CategoryResponseDto responseDto = _mapper.Map<CategoryResponseDto>(categoryEntity);
            return responseDto;
        }
    }
}