namespace Common.Exceptions;

public class DeviceDisconnectException : Exception
{
    public DeviceDisconnectException() { }
    
    public DeviceDisconnectException(string message) : base(message) { }
    
    public DeviceDisconnectException(string message, Exception? innerException) : base(message, innerException) { }
}