using AutoMapper;
using Moq;
using TechCareer.DataAccess.Repositories.Abstracts;
using TechCareer.Models.Dtos.Events;
using TechCareer.Models.Entities;
using TechCareer.Service.Concretes;
using TechCareer.Service.Constants;
using TechCareer.Service.Rules;
using Xunit;

public class EventServiceTests
{
    private readonly Mock<IEventRepository> _eventRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<EventBusinessRules> _businessRulesMock;
    private readonly EventService _eventService;

    public EventServiceTests()
    {
        _eventRepositoryMock = new Mock<IEventRepository>();
        _mapperMock = new Mock<IMapper>();
        _businessRulesMock = new Mock<EventBusinessRules>();
        _eventService = new EventService(_eventRepositoryMock.Object, _mapperMock.Object, _businessRulesMock.Object);
    }

    [Fact]
    public async Task AddAsync_ShouldAddEventSuccessfully()
    {
        var createDto = new CreateEventRequestDto(
            "Test Event",
            "Test Description",
            "TestImage.jpg",
            "Participation Text",
            1
        );

        var eventEntity = new Event
        {
            Id = Guid.NewGuid(),
            Title = "Test Event",
            Description = "Test Description",
            ImageUrl = "TestImage.jpg",
            ParticipationText = "Participation Text"
        };

        var eventResponse = new EventResponseDto
        {
            Id = eventEntity.Id,
            Title = eventEntity.Title,
            Description = eventEntity.Description,
            ImageUrl = eventEntity.ImageUrl,
            ParticipationText = eventEntity.ParticipationText,
            CategoryName = "Test Category"
        };

        _businessRulesMock.Setup(b => b.EventTitleMustBeUnique(It.IsAny<string>())).Returns(Task.CompletedTask);
        _mapperMock.Setup(m => m.Map<Event>(createDto)).Returns(eventEntity);
        _eventRepositoryMock.Setup(r => r.AddAsync(eventEntity)).ReturnsAsync(eventEntity);
        _mapperMock.Setup(m => m.Map<EventResponseDto>(eventEntity)).Returns(eventResponse);

        var result = await _eventService.AddAsync(createDto);

        Assert.Equal(eventResponse.Title, result.Title);
        Assert.Equal(eventResponse.Description, result.Description);
        Assert.Equal(eventResponse.ImageUrl, result.ImageUrl);
        Assert.Equal(eventResponse.ParticipationText, result.ParticipationText);
        Assert.Equal(eventResponse.CategoryName, result.CategoryName);
    }

    [Fact]
    public async Task DeleteAsync_ShouldDeleteEventSuccessfully()
    {
        var eventId = Guid.NewGuid();
        var eventEntity = new Event
        {
            Id = eventId,
            Title = "Test Event"
        };

        _businessRulesMock.Setup(b => b.EventMustExist(eventId)).ReturnsAsync(eventEntity);
        _eventRepositoryMock.Setup(r => r.DeleteAsync(eventEntity, false)).Returns((Task<Event>)Task.CompletedTask);

        var result = await _eventService.DeleteAsync(eventId);

        Assert.Equal(EventMessages.EventDeleted, result);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnEvent()
    {
        var eventId = Guid.NewGuid();
        var eventEntity = new Event
        {
            Id = eventId,
            Title = "Test Event",
            Description = "Test Description",
            ImageUrl = "TestImage.jpg",
            ParticipationText = "Participation Text"
        };

        var eventResponse = new EventResponseDto
        {
            Id = eventId,
            Title = "Test Event",
            Description = "Test Description",
            ImageUrl = "TestImage.jpg",
            ParticipationText = "Participation Text",
            CategoryName = "Test Category"
        };

        _businessRulesMock.Setup(b => b.EventMustExist(eventId)).ReturnsAsync(eventEntity);
        _mapperMock.Setup(m => m.Map<EventResponseDto>(eventEntity)).Returns(eventResponse);

        var result = await _eventService.GetByIdAsync(eventId);

        Assert.Equal(eventResponse.Id, result.Id);
        Assert.Equal(eventResponse.Title, result.Title);
        Assert.Equal(eventResponse.Description, result.Description);
        Assert.Equal(eventResponse.ImageUrl, result.ImageUrl);
        Assert.Equal(eventResponse.ParticipationText, result.ParticipationText);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateEventSuccessfully()
    {
        var eventId = Guid.NewGuid();
        var updateDto = new UpdateEventRequestDto(
            eventId,
            "Updated Event",
            "Updated Description",
            "UpdatedImage.jpg",
            "Updated Participation Text"
        );

        var existingEvent = new Event
        {
            Id = eventId,
            Title = "Old Event",
            Description = "Old Description",
            ImageUrl = "OldImage.jpg",
            ParticipationText = "Old Participation Text"
        };

        _businessRulesMock.Setup(b => b.EventMustExist(eventId)).ReturnsAsync(existingEvent);
        _mapperMock.Setup(m => m.Map(updateDto, existingEvent)).Returns(existingEvent);
        _eventRepositoryMock.Setup(r => r.UpdateAsync(existingEvent)).ReturnsAsync(existingEvent);
        _mapperMock.Setup(m => m.Map<EventResponseDto>(existingEvent)).Returns(new EventResponseDto
        {
            Id = eventId,
            Title = "Updated Event",
            Description = "Updated Description",
            ImageUrl = "UpdatedImage.jpg",
            ParticipationText = "Updated Participation Text"
        });

        var result = await _eventService.UpdateAsync(eventId, updateDto);

        Assert.Equal("Updated Event", result.Title);
        Assert.Equal("Updated Description", result.Description);
        Assert.Equal("UpdatedImage.jpg", result.ImageUrl);
        Assert.Equal("Updated Participation Text", result.ParticipationText);
    }

    [Fact]
    public async Task GetListAsync_ShouldReturnFilteredEventList()
    {
        var events = new List<Event>
    {
        new Event { Id = Guid.NewGuid(), Title = "Event 1" },
        new Event { Id = Guid.NewGuid(), Title = "Event 2" }
    };

        _eventRepositoryMock.Setup(r => r.GetListAsync(null, null, false, false, true, default))
            .ReturnsAsync(events);
        _mapperMock.Setup(m => m.Map<List<EventResponseDto>>(events)).Returns(new List<EventResponseDto>
    {
        new EventResponseDto { Id = events[0].Id, Title = "Event 1" },
        new EventResponseDto { Id = events[1].Id, Title = "Event 2" }
    });

        var result = await _eventService.GetListAsync();

        Assert.Equal(2, result.Count);
        Assert.Equal("Event 1", result[0].Title);
        Assert.Equal("Event 2", result[1].Title);
    }

    [Fact]
    public async Task AddAsync_ShouldThrowExceptionIfTitleIsNotUnique()
    {
        var createDto = new CreateEventRequestDto("Duplicate Title", "Test Description", "TestImage.jpg", "Participation Text", 1);

        _businessRulesMock.Setup(b => b.EventTitleMustBeUnique(createDto.Title))
            .ThrowsAsync(new Exception("Title must be unique"));

        await Assert.ThrowsAsync<Exception>(() => _eventService.AddAsync(createDto));
    }

}

