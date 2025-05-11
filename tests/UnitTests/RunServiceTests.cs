using Moq;
using RunningTracker.Dto;
using RunningTracker.Infrastructure.Repositories.Run;
using RunningTracker.Models;
using RunningTracker.Services;

namespace UnitTests;

public class RunServiceTests
{
    private readonly Mock<IRunRepository> _mockRunRepository;
    private readonly RunService _runService;

    public RunServiceTests()
    {
        _mockRunRepository = new Mock<IRunRepository>();
        _runService = new RunService(_mockRunRepository.Object);
    }

    [Trait("UnitTests", "Service")]
    [Fact]
    public async Task AddRunAsync_Should_ReturnCreatedRun()
    {
        // Arrange
        var runDto = new RunDto();
        var expectedRun = new RunActivity();
        _mockRunRepository.Setup(r => r.AddRunAsync(It.IsAny<RunActivity>())).ReturnsAsync(expectedRun);

        // Act
        var result = await _runService.AddRunAsync(runDto);

        // Assert
        Assert.Equal(expectedRun, result);
        _mockRunRepository.Verify(r => r.AddRunAsync(It.IsAny<RunActivity>()), Times.Once);
    }
    [Trait("UnitTests", "Service")]
    [Fact]
    public async Task GetAllRunsAsync_Should_ReturnAllRuns()
    {
        // Arrange
        var expectedRuns = new List<RunActivity> { new RunActivity(), new RunActivity() };
        _mockRunRepository.Setup(r => r.GetAllRunsAsync()).ReturnsAsync(expectedRuns);

        // Act
        var result = await _runService.GetAllRunsAsync();

        // Assert
        Assert.Equal(expectedRuns, result);
        _mockRunRepository.Verify(r => r.GetAllRunsAsync(), Times.Once);
    }

    [Trait("UnitTests", "Service")]
    [Fact]
    public async Task GetRunsByDateAsync_Should_ReturnRunsForDate()
    {
        // Arrange
        var testDate = new DateTime(2025, 4, 8);
        var expectedRuns = new List<RunActivity> { new RunActivity { Date = testDate } };
        _mockRunRepository.Setup(r => r.GetRunsByDateAsync(testDate)).ReturnsAsync(expectedRuns);

        // Act
        var result = await _runService.GetRunsByDateAsync(testDate);

        // Assert
        Assert.Equal(expectedRuns, result);
        _mockRunRepository.Verify(r => r.GetRunsByDateAsync(testDate), Times.Once);
    }

    [Trait("UnitTests", "Service")]
    [Fact]
    public async Task GetRunByIdAsync_Should_ReturnRun_WhenRunExists()
    {
        // Arrange
        var runId = 1;
        var expectedRun = new RunActivity { Id = runId };
        _mockRunRepository.Setup(r => r.GetRunByIdAsync(runId)).ReturnsAsync(expectedRun);

        // Act
        var result = await _runService.GetRunByIdAsync(runId);

        // Assert
        Assert.Equal(expectedRun, result);
        _mockRunRepository.Verify(r => r.GetRunByIdAsync(runId), Times.Once);
    }

    [Trait("UnitTests", "Service")]
    [Fact]
    public async Task UpdateRunAsync_Should_ReturnUpdatedRun_WhenRunExists()
    {
        // Arrange
        var runId = 1;
        var runDto = new RunDto();
        var existingRun = new RunActivity { Id = runId };
        var updatedRun = new RunActivity { Id = runId };
        _mockRunRepository.Setup(r => r.GetRunByIdAsync(runId)).ReturnsAsync(existingRun);
        _mockRunRepository.Setup(r => r.UpdateRunAsync(existingRun)).ReturnsAsync(updatedRun);

        // Act
        var result = await _runService.UpdateRunAsync(runId, runDto);

        // Assert
        Assert.Equal(updatedRun, result);
        _mockRunRepository.Verify(r => r.GetRunByIdAsync(runId), Times.Once);
        _mockRunRepository.Verify(r => r.UpdateRunAsync(existingRun), Times.Once);
    }

    [Trait("UnitTests", "Service")]
    [Fact]
    public async Task UpdateRunAsync_Should_ThrowKeyNotFoundException_WhenRunDoesNotExist()
    {
        // Arrange
        var runId = 1;
        var runDto = new RunDto();
        _mockRunRepository.Setup(r => r.GetRunByIdAsync(runId)).ReturnsAsync((RunActivity)null);

        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() => _runService.UpdateRunAsync(runId, runDto));
        _mockRunRepository.Verify(r => r.GetRunByIdAsync(runId), Times.Once);
        _mockRunRepository.Verify(r => r.UpdateRunAsync(It.IsAny<RunActivity>()), Times.Never);
    }

    [Trait("UnitTests", "Service")]
    [Fact]
    public async Task DeleteRunAsync_Should_CallRepositoryDelete()
    {
        // Arrange
        var runId = 1;
        _mockRunRepository.Setup(r => r.DeleteRunAsync(runId)).Returns(Task.CompletedTask);

        // Act
        await _runService.DeleteRunAsync(runId);

        // Assert
        _mockRunRepository.Verify(r => r.DeleteRunAsync(runId), Times.Once);
    }

    [Trait("UnitTests", "Service")]
    [Fact]
    public async Task DeleteAllRunsAsync_Should_CallRepositoryDeleteAll()
    {
        // Arrange
        _mockRunRepository.Setup(r => r.DeleteAllRunsAsync()).Returns(Task.CompletedTask);

        // Act
        await _runService.DeleteAllRunsAsync();

        // Assert
        _mockRunRepository.Verify(r => r.DeleteAllRunsAsync(), Times.Once);
    }
}
