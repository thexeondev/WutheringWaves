using System.Net;

namespace GameServer.Settings;
internal class GatewaySettings
{
    public required string Host { get; set; }

    public IPEndPoint EndPoint
    {
        get
        {
            string[] split = Host.Split(':');

            return new IPEndPoint(IPAddress.Parse(split[0]), int.Parse(split[1]));
        }
    }
}
