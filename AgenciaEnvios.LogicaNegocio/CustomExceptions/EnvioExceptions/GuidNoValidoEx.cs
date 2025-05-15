
public class GuidNoValidoEx : Exception
{
    public GuidNoValidoEx()
    {
    }

    public GuidNoValidoEx(string? message) : base(message)
    {
    }

    public GuidNoValidoEx(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}