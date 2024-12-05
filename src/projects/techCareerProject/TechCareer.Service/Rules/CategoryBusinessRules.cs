using Core.CrossCuttingConcerns.Exceptions.ExceptionTypes;
using System.Threading.Tasks;
using TechCareer.DataAccess.Repositories.Abstracts;
using TechCareer.Models.Entities;
using TechCareer.Service.Constants;

namespace TechCareer.Service.Rules
{
    public class CategoryBusinessRules
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryBusinessRules(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public CategoryBusinessRules() { }
        /// <summary>
        /// Verilen id'ye sahip bir kategori varlığının mevcut olduğunu doğrular.
        /// </summary>
        public virtual async Task<Category> CategoryMustExist(int id)
        {
            var categoryEntity = await _categoryRepository.GetAsync(c => c.Id == id);
            if (categoryEntity == null)
            {
                throw new KeyNotFoundException(CategoryMessages.CategoryNotFound);
            }
            return categoryEntity;
        }

        /// <summary>
        /// Verilen isimde bir kategorinin zaten var olup olmadığını kontrol eder.
        /// </summary>
        public virtual async Task CategoryNameMustBeUnique(string name)
        {
            var exists = await _categoryRepository.AnyAsync(c => c.Name == name);
            if (exists)
            {
                throw new InvalidOperationException(CategoryMessages.CategoryTitleAlreadyExists);
            }
        }
    }
}
