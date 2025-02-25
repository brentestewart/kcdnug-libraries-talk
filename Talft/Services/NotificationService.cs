
using MediatR;
using Talft.Dtos;
using Talft.Messaging;

namespace Talft.Services;

public class NotificationService(ILogger<NotificationService> logger, IMediator mediator) : BackgroundService
{
    public ILogger Logger { get; } = logger;
    public IMediator Mediator { get; } = mediator;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var newUser = GenerateRandomUser();
            await Mediator.Publish(new UserAddedNotification { User = newUser }, stoppingToken);
            await Task.Delay(5000, stoppingToken);
        }
    }

    private UserDto GenerateRandomUser()
    {
        var firstNames = new[] { "John", "Jane", "Alice", "Bob", "Charlie", "David", "Eve", "Frank", "Grace", "Hank", "Liam", "Olivia", "Noah", "Emma", "Ava", "Sophia", "Isabella", "Mia", "Amelia", "Harper" };
        var lastNames = new[] { "Smith", "Johnson", "Williams", "Jones", "Brown", "Davis", "Miller", "Wilson", "Moore", "Taylor", "Anderson", "Thomas", "Jackson", "White", "Harris", "Martin", "Thompson", "Garcia", "Martinez", "Robinson" };
        var emailProviders = new[] { "gmail.com", "yahoo.com", "outlook.com", "hotmail.com", "protonmail.com" };

        var random = new Random();
        var firstName = firstNames[random.Next(firstNames.Length)];
        var lastName = lastNames[random.Next(lastNames.Length)];
        var emailProvider = emailProviders[random.Next(emailProviders.Length)];
        var email = $"{firstName}.{lastName}@{emailProvider}";
        var name = $"{firstName} {lastName}";

        return new UserDto
        {
            Id = Guid.NewGuid(),
            FullName = name,
            Email = email
        };
    }
}
