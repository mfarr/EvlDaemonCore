namespace Network;

public interface IEvlClient
{
    public void Connect();

    public Task ListenForEventsAsync(CancellationToken cancellationToken);

    public void Disconnect();
}