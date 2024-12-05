using Core.Persistence.Extensions;
using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using TechCareer.Models.Dtos.Instructors;
using TechCareer.Models.Entities;

namespace TechCareer.Service.Abstracts;

public interface IInstructorService
{
    // Sade Add, Update, Delete metotları
    Task<InstructorResponseDto> AddAsync(CreateInstructorRequestDto dto);
    Task<string> DeleteAsync(Guid id, bool permanent = false);
    Task<InstructorResponseDto> UpdateAsync(Guid id, UpdateInstructorRequestDto dto);

    // Listeleme ve gösterim metotları
    Task<List<InstructorResponseDto>> GetListAsync(
        Expression<Func<Instructor, bool>>? predicate = null,
        Func<IQueryable<Instructor>, IOrderedQueryable<Instructor>>? orderBy = null,
        bool include = false,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );

    Task<Paginate<InstructorResponseDto>> GetPaginateAsync(
        Expression<Func<Instructor, bool>>? predicate = null,
        Func<IQueryable<Instructor>, IOrderedQueryable<Instructor>>? orderBy = null,
        bool include = true,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );

    Task<InstructorResponseDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}
