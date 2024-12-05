using AutoMapper;
using Core.Security.Entities;
using Moq;
using TechCareer.DataAccess.Repositories.Abstracts;
using TechCareer.Models.Dtos.Instructors;
using TechCareer.Models.Entities;
using TechCareer.Service.Concretes;
using TechCareer.Service.Constants;
using TechCareer.Service.Rules;
using Xunit;

public class InstructorServiceTests
{
    private readonly Mock<IInstructorRepository> _instructorRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<InstructorBusinessRules> _businessRulesMock;
    private readonly InstructorService _instructorService;

    public InstructorServiceTests()
    {
        _instructorRepositoryMock = new Mock<IInstructorRepository>();
        _mapperMock = new Mock<IMapper>();
        _businessRulesMock = new Mock<InstructorBusinessRules>();
        _instructorService = new InstructorService(_instructorRepositoryMock.Object, _mapperMock.Object, _businessRulesMock.Object);
    }

    [Fact]
    public async Task AddAsync_ShouldAddInstructorSuccessfully()
    {
        // Arrange
        var createDto = new CreateInstructorRequestDto(
            "Test Instructor",
            "Test About"
        );

        var instructor = new Instructor
        {
            Id = Guid.NewGuid(),
            Name = "Test Instructor",
            About = "Test About"
        };

        var instructorResponse = new InstructorResponseDto
        {
            Id = instructor.Id,
            Name = instructor.Name,
            About = instructor.About
        };

        _businessRulesMock.Setup(b => b.InstructorNameMustBeUnique(It.IsAny<string>())).Returns(Task.CompletedTask);
        _mapperMock.Setup(m => m.Map<Instructor>(createDto)).Returns(instructor);
        _instructorRepositoryMock.Setup(r => r.AddAsync(instructor)).ReturnsAsync(instructor);
        _mapperMock.Setup(m => m.Map<InstructorResponseDto>(instructor)).Returns(instructorResponse);

        // Act
        var result = await _instructorService.AddAsync(createDto);

        // Assert
        Assert.Equal(instructorResponse.Name, result.Name);
        Assert.Equal(instructorResponse.About, result.About);
    }

    [Fact]
    public async Task DeleteAsync_ShouldDeleteInstructorSuccessfully()
    {
        // Arrange
        var instructorId = Guid.NewGuid();
        var instructor = new Instructor
        {
            Id = instructorId,
            Name = "Test Instructor"
        };

        _businessRulesMock.Setup(b => b.InstructorMustExist(instructorId)).ReturnsAsync(instructor);
        _instructorRepositoryMock.Setup(r => r.DeleteAsync(instructor, false)).ReturnsAsync(instructor);

        // Act
        var result = await _instructorService.DeleteAsync(instructorId);

        // Assert
        Assert.Equal(InstructorMessages.InstructorDeleted, result);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnInstructor()
    {
        // Arrange
        var instructorId = Guid.NewGuid();
        var cancellationToken = new CancellationToken();
        var instructor = new Instructor
        {
            Id = instructorId,
            Name = "Test Instructor",
            About = "Test About"
        };

        var instructorResponse = new InstructorResponseDto
        {
            Id = instructorId,
            Name = "Test Instructor",
            About = "Test About"
        };

        _businessRulesMock.Setup(b => b.InstructorMustExist(instructorId)).ReturnsAsync(instructor);
        _mapperMock.Setup(m => m.Map<InstructorResponseDto>(instructor)).Returns(instructorResponse);

        // Act
        var result = await _instructorService.GetByIdAsync(instructorId, cancellationToken);

        // Assert
        Assert.Equal(instructorResponse.Id, result.Id);
        Assert.Equal(instructorResponse.Name, result.Name);
        Assert.Equal(instructorResponse.About, result.About);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateInstructorSuccessfully()
    {
        // Arrange
        var instructorId = Guid.NewGuid();
        var updateDto = new UpdateInstructorRequestDto(
            instructorId,
            "Updated Instructor",
            "Updated About"
        );

        var existingInstructor = new Instructor
        {
            Id = instructorId,
            Name = "Old Instructor",
            About = "Old About"
        };

        _businessRulesMock.Setup(b => b.InstructorMustExist(instructorId)).ReturnsAsync(existingInstructor);
        _mapperMock.Setup(m => m.Map(updateDto, existingInstructor)).Returns(existingInstructor);
        _instructorRepositoryMock.Setup(r => r.UpdateAsync(existingInstructor)).ReturnsAsync(existingInstructor);
        _mapperMock.Setup(m => m.Map<InstructorResponseDto>(existingInstructor)).Returns(new InstructorResponseDto
        {
            Id = instructorId,
            Name = "Updated Instructor",
            About = "Updated About"
        });

        // Act
        var result = await _instructorService.UpdateAsync(instructorId, updateDto);

        // Assert
        Assert.Equal("Updated Instructor", result.Name);
        Assert.Equal("Updated About", result.About);
    }

    [Fact]
    public async Task GetListAsync_ShouldReturnFilteredInstructorList()
    {
        // Arrange
        var instructors = new List<Instructor>
        {
            new Instructor { Id = Guid.NewGuid(), Name = "Instructor 1", About = "About 1" },
            new Instructor { Id = Guid.NewGuid(), Name = "Instructor 2", About = "About 2" }
        };

        var cancellationToken = new CancellationToken();

        _instructorRepositoryMock.Setup(r => r.GetListAsync(
            null,
            null,
            false,
            false,
            true,
            cancellationToken))
            .ReturnsAsync(instructors);

        _mapperMock.Setup(m => m.Map<List<InstructorResponseDto>>(instructors))
            .Returns(new List<InstructorResponseDto>
            {
                new InstructorResponseDto { Id = instructors[0].Id, Name = "Instructor 1", About = "About 1" },
                new InstructorResponseDto { Id = instructors[1].Id, Name = "Instructor 2", About = "About 2" }
            });

        // Act
        var result = await _instructorService.GetListAsync(
            predicate: null,
            orderBy: null,
            include: false,
            withDeleted: false,
            enableTracking: true,
            cancellationToken: cancellationToken);

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal("Instructor 1", result[0].Name);
        Assert.Equal("Instructor 2", result[1].Name);
    }

    [Fact]
    public async Task AddAsync_ShouldThrowExceptionIfNameIsNotUnique()
    {
        // Arrange
        var createDto = new CreateInstructorRequestDto("Duplicate Name", "Test About");

        _businessRulesMock.Setup(b => b.InstructorNameMustBeUnique(createDto.Name))
            .ThrowsAsync(new Exception("Name must be unique"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _instructorService.AddAsync(createDto));
    }
}





