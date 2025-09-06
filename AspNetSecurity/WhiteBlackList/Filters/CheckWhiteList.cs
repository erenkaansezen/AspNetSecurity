using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using WhiteBlackList.MiddleWares;

namespace WhiteBlackList.Filters
{
    public class CheckWhiteList : ActionFilterAttribute
    {
        private readonly IOptions<IPList> _ipList;

        public CheckWhiteList(IOptions<IPList> ipList)
        {
            _ipList = ipList;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var reqIpAdress = context.HttpContext.Connection.RemoteIpAddress;
            var isWhiteList = _ipList.Value.WhiteList.Where(ip => System.Net.IPAddress.Parse(ip).Equals(reqIpAdress)).Any();
            if (!isWhiteList)
            {
                context.Result = new StatusCodeResult((int)HttpStatusCode.Forbidden);
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}
