namespace Intranet.Services;

public class MenuChangeNotifier
{
    public event Func<Task>? MenuChanged;

    public async Task NotifyAsync()
    {
        if (MenuChanged is not null)
            await MenuChanged.Invoke();
    }
}
