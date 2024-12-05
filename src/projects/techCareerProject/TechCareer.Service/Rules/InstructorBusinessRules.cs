using Core.CrossCuttingConcerns.Exceptions.ExceptionTypes;
using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechCareer.DataAccess.Repositories.Abstracts;
using TechCareer.Models.Entities;
using TechCareer.Service.Constants;

namespace TechCareer.Service.Rules
{
    public class InstructorBusinessRules
    {
        private readonly IInstructorRepository _instructorRepository;

        public InstructorBusinessRules(IInstructorRepository instructorRepository)
        {
            _instructorRepository = instructorRepository;
        }
        public InstructorBusinessRules() { }    
        public virtual async Task<Instructor> InstructorMustExist(Guid id)
        {
            var instructor = await _instructorRepository.GetAsync(i => i.Id == id);
            if (instructor == null)
            {
                throw new KeyNotFoundException(InstructorMessages.InstructorNotFound);
            }
            return instructor;
        }

        public virtual async Task InstructorNameMustBeUnique(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new BusinessException(InstructorMessages.InvalidInstructorData);
            }

            var exists = await _instructorRepository.AnyAsync(i => i.Name == name);
            if (exists)
            {
                throw new BusinessException(InstructorMessages.InstructorNameAlreadyExists);
            }
        }

        public virtual void ValidateInstructorData(Instructor instructor)
        {
            if (instructor == null)
            {
                throw new BusinessException(InstructorMessages.InvalidInstructorData);
            }

            if (string.IsNullOrWhiteSpace(instructor.Name))
            {
                throw new BusinessException(InstructorMessages.InvalidInstructorData);
            }

            if (string.IsNullOrWhiteSpace(instructor.About))
            {
                throw new BusinessException(InstructorMessages.InvalidInstructorData);
            }
        }
    }
}
