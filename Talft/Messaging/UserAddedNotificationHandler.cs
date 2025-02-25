using MediatR;
using Talft.Services;

namespace Talft.Messaging;

public class UserAddedNotificationHandler(NotificationManager notificationManager) : INotificationHandler<UserAddedNotification>
{
    public NotificationManager NotificationManager { get; } = notificationManager;

    public Task Handle(UserAddedNotification notification, CancellationToken cancellationToken)
    {

        NotificationManager.UserAdded(notification.User);

        return Task.CompletedTask;
    }
}
