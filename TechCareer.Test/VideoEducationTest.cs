using AutoMapper;
using Core.Persistence.Extensions;
using Moq;
using System.Linq.Expressions;
using TechCareer.DataAccess.Repositories.Abstracts;
using TechCareer.Models.Dtos.VideoEducation;
using TechCareer.Models.Entities;
using TechCareer.Service.Abstracts;
using TechCareer.Service.Rules;

namespace TechCareer.Test;

public class VideoEducationTest
{
    private readonly Mock<IVideoEducationRepository> _mockRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<VideoEducationBusinessRules> _mockBusinessRules;
    private readonly IVideoEducationService _service;

    public VideoEducationTest()
    {
        _mockRepository = new Mock<IVideoEducationRepository>();
        _mockMapper = new Mock<IMapper>();
        _mockBusinessRules = new Mock<VideoEducationBusinessRules>();
        _service = new VideoEducationService(_mockRepository.Object, _mockMapper.Object, _mockBusinessRules.Object);
    }

    [Fact]
    public async Task AddAsync_Should_Add_VideoEducation_Successfully()
    {
        // Arrange
        var createDto = new CreateVideoEducationRequestDto
        {
            Title = "Test Title",
            Description = "Test Description",
            TotalHour = 10,
            IsCertified = true,
            ImageUrl = "https://example.com/image.png",
            InstructorId = Guid.NewGuid(),
            ProgrammingLanguage = "C#"
        };

        var videoEducation = new VideoEducation
        {
            Id = 1,
            Title = createDto.Title,
            Description = createDto.Description,
            TotalHour = createDto.TotalHour,
            IsCertified = createDto.IsCertified,
            ImageUrl = createDto.ImageUrl,
            InstructorId = createDto.InstructorId,
            ProgrammingLanguage = createDto.ProgrammingLanguage
        };

        _mockBusinessRules
            .Setup(x => x.VideoEducationTitleMustBeUnique(It.IsAny<string>()))
            .Returns(Task.CompletedTask);

        _mockMapper
            .Setup(m => m.Map<VideoEducation>(It.IsAny<CreateVideoEducationRequestDto>()))
            .Returns(videoEducation);

        _mockRepository
            .Setup(r => r.AddAsync(It.IsAny<VideoEducation>()))
            .ReturnsAsync(videoEducation);

        _mockMapper
            .Setup(m => m.Map<VideoEducationResponseDto>(It.IsAny<VideoEducation>()))
            .Returns(new VideoEducationResponseDto
            {
                Id = videoEducation.Id,
                Title = videoEducation.Title
            });

        // Act
        var result = await _service.AddAsync(createDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(videoEducation.Id, result.Id);
        Assert.Equal(videoEducation.Title, result.Title);

        _mockBusinessRules.Verify(x => x.VideoEducationTitleMustBeUnique(It.IsAny<string>()), Times.Once);
        _mockRepository.Verify(r => r.AddAsync(It.IsAny<VideoEducation>()), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_Should_Delete_VideoEducation_Successfully()
    {
        // Arrange
        var id = 1;
        var videoEducation = new VideoEducation { Id = id };

        _mockBusinessRules
            .Setup(x => x.VideoEducationMustExist(id))
            .ReturnsAsync(videoEducation);

        _mockRepository
            .Setup(r => r.DeleteAsync(videoEducation, true))
            .ReturnsAsync(videoEducation);

        // Act
        var result = await _service.DeleteAsync(id, true);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Video education successfully deleted.", result);

        _mockBusinessRules.Verify(x => x.VideoEducationMustExist(id), Times.Once);
        _mockRepository.Verify(r => r.DeleteAsync(It.IsAny<VideoEducation>(), true), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_Should_Update_VideoEducation_Successfully()
    {
        // Arrange
        var id = 1;
        var updateDto = new UpdateVideoEducationRequestDto
        {
            Id = id,
            Title = "Updated Title",
            Description = "Updated Description",
            InstructorId = Guid.NewGuid(),
            TotalHour = 15
        };

        var videoEducation = new VideoEducation { Id = id, Title = "Old Title" };

        _mockBusinessRules
            .Setup(x => x.VideoEducationMustExist(id))
            .ReturnsAsync(videoEducation);

        _mockMapper
            .Setup(m => m.Map(updateDto, videoEducation))
            .Callback(() =>
            {
                videoEducation.Title = updateDto.Title;
                videoEducation.Description = updateDto.Description;
                videoEducation.TotalHour = updateDto.TotalHour;
            });

        _mockRepository
            .Setup(r => r.UpdateAsync(It.IsAny<VideoEducation>()))
            .ReturnsAsync(videoEducation);

        _mockMapper
            .Setup(m => m.Map<VideoEducationResponseDto>(It.IsAny<VideoEducation>()))
            .Returns(new VideoEducationResponseDto
            {
                Id = videoEducation.Id,
                Title = updateDto.Title,
                Description = updateDto.Description,
                TotalHour = updateDto.TotalHour
            });

        // Act
        var result = await _service.UpdateAsync(id, updateDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(updateDto.Title, result.Title);
        Assert.Equal(updateDto.Description, result.Description);

        _mockBusinessRules.Verify(x => x.VideoEducationMustExist(id), Times.Once);
        _mockRepository.Verify(r => r.UpdateAsync(It.IsAny<VideoEducation>()), Times.Once);
    }

    [Fact]
    public async Task GetListAsync_Should_Return_List_Of_VideoEducations()
    {
        // Arrange
        var videoEducationList = new List<VideoEducation>
        {
            new VideoEducation { Id = 1, Title = "Title 1" },
            new VideoEducation { Id = 2, Title = "Title 2" }
        };

        _mockRepository
            .Setup(r => r.GetListAsync(
                It.IsAny<Expression<Func<VideoEducation, bool>>>(),
                It.IsAny<Func<IQueryable<VideoEducation>, IOrderedQueryable<VideoEducation>>>(),
                It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(videoEducationList);

        _mockMapper
            .Setup(m => m.Map<List<VideoEducationResponseDto>>(videoEducationList))
            .Returns(new List<VideoEducationResponseDto>
            {
                new VideoEducationResponseDto { Id = 1, Title = "Title 1" },
                new VideoEducationResponseDto { Id = 2, Title = "Title 2" }
            });

        // Act
        var result = await _service.GetListAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Equal("Title 1", result[0].Title);
        Assert.Equal("Title 2", result[1].Title);

        _mockRepository.Verify(r => r.GetListAsync(
            It.IsAny<Expression<Func<VideoEducation, bool>>>(),
            It.IsAny<Func<IQueryable<VideoEducation>, IOrderedQueryable<VideoEducation>>>(),
            It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetPaginateAsync_Should_Return_Paginated_VideoEducations()
    {
        // Arrange
        var paginatedVideoEducations = new Paginate<VideoEducation>
        {
            Items = new List<VideoEducation>
            {
                new VideoEducation { Id = 1, Title = "Title 1" },
                new VideoEducation { Id = 2, Title = "Title 2" }
            },
            Index = 0,
            Size = 10,
            Count = 2,
            Pages = 1
        };

        _mockRepository
            .Setup(r => r.GetPaginateAsync(
                It.IsAny<Expression<Func<VideoEducation, bool>>>(),
                It.IsAny<Func<IQueryable<VideoEducation>, IOrderedQueryable<VideoEducation>>>(),
                It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(paginatedVideoEducations);

        _mockMapper
            .Setup(m => m.Map<IList<VideoEducationResponseDto>>(It.IsAny<IList<VideoEducation>>()))
            .Returns(new List<VideoEducationResponseDto>
            {
                new VideoEducationResponseDto { Id = 1, Title = "Title 1" },
                new VideoEducationResponseDto { Id = 2, Title = "Title 2" }
            });

        // Act
        var result = await _service.GetPaginateAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Items.Count);
        Assert.Equal("Title 1", result.Items[0].Title);
        Assert.Equal("Title 2", result.Items[1].Title);

        _mockRepository.Verify(r => r.GetPaginateAsync(
            It.IsAny<Expression<Func<VideoEducation, bool>>>(),
            It.IsAny<Func<IQueryable<VideoEducation>, IOrderedQueryable<VideoEducation>>>(),
            It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}