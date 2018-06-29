using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookShop.Web.ashx
{
    /// <summary>
    /// logout 的摘要说明
    /// </summary>
    public class logout : IHttpHandler,System.Web.SessionState.IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            if (context.Session["userInfo"] != null)
            {
                context.Session["userInfo"] = null;
                //清除Cookie中的值.
                context.Response.Cookies["cp1"].Expires = DateTime.Now.AddDays(-1);
                context.Response.Cookies["cp2"].Expires = DateTime.Now.AddDays(-1);
                context.Response.Write("ok");
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