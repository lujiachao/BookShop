using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace BookShop.Web
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {
           
        }

        /// <summary>
        /// 完成URL地址重写.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            string url = Request.AppRelativeCurrentExecutionFilePath;//~
            //判断用户请求的URL地址的格式(BookDetail_4939.aspx)
            Match match = Regex.Match(url, @"~/BookDetail_(\d+).aspx");
            if (match.Success)
            {
                Context.RewritePath("/BookDetail.aspx?id="+match.Groups[1].Value);
            }
            //Match match1 = Regex.Match(url, @"~/BookDetail_(\d+).aspx");
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}