namespace IPInfo.Library.Interfaces
{
    public interface IIPInfoProvider
    {
        IPDetails GetIPDetails(string ip);
    }
}
