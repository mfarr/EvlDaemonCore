namespace Network;

public interface IEvlClient
{
    public void Connect();

    public Task ListenForEventsAsync(CancellationToken externalCancellationToken);

    public void Disconnect();
}