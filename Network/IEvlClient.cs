namespace Network;

public interface IEvlClient
{
    public void Connect();

    public Task ListenForEventsAsync();

    public void Disconnect();
}