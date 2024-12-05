

using Core.CrossCuttingConcerns.Exceptions.ExceptionTypes;
using Core.CrossCuttingConcerns.Rules;
using Core.Security.Entities;
using Core.Security.Hashing;
using Microsoft.Extensions.Logging;
using TechCareer.DataAccess.Repositories.Abstracts;
using TechCareer.DataAccess.Repositories.Concretes;
using TechCareer.Models.Entities;
using TechCareer.Service.Constants;

namespace TechCareer.Service.Rules;

public sealed class VideoEducationBusinessRules(IVideoEducationRepository _videoEducationRepository) : BaseBusinessRules
{


    public async Task<VideoEducation> VideoEducationMustExist(int id)
    {
        var videoEducationEntity = await _videoEducationRepository.GetAsync(e => e.Id == id);
        if (videoEducationEntity == null)
        {
            throw new KeyNotFoundException(VideoEducationMessage.VideoEducaitonDontExists);
        }
        return videoEducationEntity;
    }

    public async Task VideoEducationTitleMustBeUnique(string title)
    {
        var exists = await _videoEducationRepository.AnyAsync(e => e.Title == title);
        if (exists)
        {
            throw new InvalidOperationException(VideoEducationMessage.VideoEducationTitleAlreadyExists);
        }
    }




}
