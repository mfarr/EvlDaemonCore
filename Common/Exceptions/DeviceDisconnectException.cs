using System.Runtime.Serialization;

namespace Common.Exceptions;

public class DeviceDisconnectException : Exception
{
    public DeviceDisconnectException()
    {
    }

    protected DeviceDisconnectException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public DeviceDisconnectException(string? message) : base(message)
    {
    }

    public DeviceDisconnectException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
