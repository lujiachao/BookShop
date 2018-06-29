using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookShop.Web.ashx
{
    /// <summary>
    /// CheckValidateCode 的摘要说明
    /// </summary>
    public class CheckValidateCode : IHttpHandler,System.Web.SessionState.IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            if (WebCommon.CheckValidateCode())
            {
                context.Response.Write("yes");
            }
            else
            {
                context.Response.Write("no");
            }

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}