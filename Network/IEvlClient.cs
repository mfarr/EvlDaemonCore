namespace Network;

public interface IEvlClient : IDisposable
{
    public void Connect();

    public Task ListenForEventsAsync(CancellationToken externalCancellationToken);

    public void Disconnect();
}
