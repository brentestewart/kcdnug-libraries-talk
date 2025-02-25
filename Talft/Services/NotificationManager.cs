using Talft.Dtos;

namespace Talft.Services;

public class NotificationManager
{
    public void UserAdded(UserDto user)
    {
        UserAddedEvent?.Invoke(this, user);
    }

    public event EventHandler<UserDto>? UserAddedEvent;
}
