using AutoMapper;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Talft.Dtos;
using Talft.Models;
using Talft.Services;

namespace Talft.ViewModels;

public class HomeViewModel
{
    public ObservableCollection<User> Users { get; set; } = new();
    public Action? Refresh { get; set; }
    public NotificationManager NotificationManager { get; }
    public IMapper Mapper { get; }

    public HomeViewModel(NotificationManager notificationManager, IMapper mapper)
    {
        Mapper = mapper;
        NotificationManager = notificationManager;
        NotificationManager.UserAddedEvent += OnUserAdded;
    }

    private void OnUserAdded(object? sender, UserDto userDto)
    {
        var user = Mapper.Map<User>(userDto);

        Users.Add(user);
        Refresh?.Invoke();
    }
}
