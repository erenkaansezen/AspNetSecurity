using System.Net;
using Microsoft.Extensions.Options;

namespace WhiteBlackList.MiddleWares
{
    public class IPSafeMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly IOptions<IPList> _ipList;
        public IPSafeMiddleWare(RequestDelegate next, IOptions<IPList> ipList)
        {
            _next = next;
            _ipList = ipList;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var reqIpAdress = context.Connection.RemoteIpAddress;
            var whiteList = _ipList.Value.WhiteList.Where(ip => IPAddress.Parse(ip).Equals(reqIpAdress)).Any();
            if (!whiteList)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return;
            }
            else
            {
                await _next(context);
            }
        }
    }
}
