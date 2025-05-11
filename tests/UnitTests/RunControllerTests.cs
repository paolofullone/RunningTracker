using Microsoft.AspNetCore.Mvc;
using Moq;
using RunningTracker.Controllers;
using RunningTracker.Dto;
using RunningTracker.Models;
using RunningTracker.Services;

namespace UnitTests;

public class RunControllerTests
{

    private readonly Mock<IRunService> _mockRunService;
    private readonly RunController _controller;

    public RunControllerTests()
    {
        _mockRunService = new Mock<IRunService>();
        _controller = new RunController(_mockRunService.Object);
    }

    [Trait("UnitTests", "Controller")]
    [Fact]
    public async Task GetAllRuns_ReturnsOkWithRuns_WhenServiceReturnsRuns()
    {
        // Arrange
        var runs = new List<RunActivity> { new RunActivity { Id = 1, Distance = 5.0 } };
        _mockRunService.Setup(s => s.GetAllRunsAsync()).ReturnsAsync(runs);

        // Act
        var result = await _controller.GetAllRuns();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedRuns = Assert.IsAssignableFrom<IEnumerable<RunActivity>>(okResult.Value);
        Assert.Equal(runs, returnedRuns);
    }

    [Trait("UnitTests", "Controller")]
    [Fact]
    public async Task GetRunsByDate_ReturnsOkWithRuns_WhenServiceReturnsRuns()
    {
        // Arrange
        var date = new DateTime(2025, 4, 8);
        var runs = new List<RunActivity> { new RunActivity { Id = 1, Date = date } };
        _mockRunService.Setup(s => s.GetRunsByDateAsync(date)).ReturnsAsync(runs);

        // Act
        var result = await _controller.GetRunsByDate(date);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedRuns = Assert.IsAssignableFrom<IEnumerable<RunActivity>>(okResult.Value);
        Assert.Equal(runs, returnedRuns);
    }

    [Trait("UnitTests", "Controller")]
    [Fact]
    public async Task GetRunById_ReturnsOkWithRun_WhenRunExists()
    {
        // Arrange
        var run = new RunActivity { Id = 1, Distance = 10.0 };
        _mockRunService.Setup(s => s.GetRunByIdAsync(1)).ReturnsAsync(run);

        // Act
        var result = await _controller.GetRunById(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedRun = Assert.IsType<RunActivity>(okResult.Value);
        Assert.Equal(run, returnedRun);
    }

    [Trait("UnitTests", "Controller")]
    [Fact]
    public async Task GetRunById_ReturnsNotFound_WhenRunDoesNotExist()
    {
        // Arrange
        _mockRunService.Setup(s => s.GetRunByIdAsync(1)).ReturnsAsync((RunActivity)null);

        // Act
        var result = await _controller.GetRunById(1);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Trait("UnitTests", "Controller")]
    [Fact]
    public async Task AddRun_ReturnsCreatedAtAction_WithNewRun()
    {
        // Arrange
        var runDto = new RunDto { Distance = 5.0 };
        var run = new RunActivity { Id = 1, Distance = 5.0 };
        _mockRunService.Setup(s => s.AddRunAsync(runDto)).ReturnsAsync(run);

        // Act
        var result = await _controller.AddRun(runDto);

        // Assert
        var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        Assert.Equal("GetRunById", createdResult.ActionName);
        Assert.Equal(1, createdResult.RouteValues["id"]);
        var returnedRun = Assert.IsType<RunActivity>(createdResult.Value);
        Assert.Equal(run, returnedRun);
    }

    [Trait("UnitTests", "Controller")]
    [Fact]
    public async Task UpdateRun_ReturnsOkWithUpdatedRun_WhenRunExists()
    {
        // Arrange
        var runDto = new RunDto { Distance = 7.0 };
        var updatedRun = new RunActivity { Id = 1, Distance = 7.0 };
        _mockRunService.Setup(s => s.UpdateRunAsync(1, runDto)).ReturnsAsync(updatedRun);

        // Act
        var result = await _controller.UpdateRunAsync(1, runDto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedRun = Assert.IsType<RunActivity>(okResult.Value);
        Assert.Equal(updatedRun, returnedRun);
    }

    [Trait("UnitTests", "Controller")]
    [Fact]
    public async Task UpdateRun_ReturnsNotFound_WhenRunDoesNotExist()
    {
        // Arrange
        var runDto = new RunDto { Distance = 7.0 };
        _mockRunService.Setup(s => s.UpdateRunAsync(1, runDto)).ThrowsAsync(new KeyNotFoundException());

        // Act
        var result = await _controller.UpdateRunAsync(1, runDto);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Trait("UnitTests", "Controller")]
    [Fact]
    public async Task DeleteRun_ReturnsNoContent_WhenRunIsDeleted()
    {
        // Arrange
        _mockRunService.Setup(s => s.DeleteRunAsync(1)).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.DeleteRunAsync(1);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Trait("UnitTests", "Controller")]
    [Fact]
    public async Task DeleteAllRuns_ReturnsNoContent_WhenRunsAreDeleted()
    {
        // Arrange
        _mockRunService.Setup(s => s.DeleteAllRunsAsync()).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.DeleteAllRuns();

        // Assert
        Assert.IsType<NoContentResult>(result);
    }
}
