using Core.Persistence.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TechCareer.Models.Dtos.Category;
using TechCareer.Models.Entities;

namespace TechCareer.Service.Abstracts
{
    public interface ICategoryService
    {
        // Sade Add, Update, Delete metotları
        Task<CategoryResponseDto> AddAsync(CreateCategoryRequestDto dto);
        Task<string> DeleteAsync(int id, bool permanent = false);
        Task<CategoryResponseDto> UpdateAsync(int id, UpdateCategoryRequestDto dto);

        // Listeleme ve gösterim metotları
        Task<List<CategoryResponseDto>> GetListAsync(
            Expression<Func<Category, bool>>? predicate = null,
            Func<IQueryable<Category>, IOrderedQueryable<Category>>? orderBy = null,
            bool include = false,
            bool withDeleted = false,
            bool enableTracking = true,
            CancellationToken cancellationToken = default
        );

        Task<Paginate<CategoryResponseDto>> GetPaginateAsync(
            Expression<Func<Category, bool>>? predicate = null,
            Func<IQueryable<Category>, IOrderedQueryable<Category>>? orderBy = null,
            bool include = true,
            int index = 0,
            int size = 10,
            bool withDeleted = false,
            bool enableTracking = true,
            CancellationToken cancellationToken = default
        );

        Task<CategoryResponseDto> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    }
}