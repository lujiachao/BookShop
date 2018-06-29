using BookShop.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookShop.Web.ashx
{
    /// <summary>
    /// FindUserPwd 的摘要说明
    /// </summary>
    public class FindUserPwd : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string mail = context.Request["mail"];
            UserManager userInfoManager = new UserManager();
            if (userInfoManager.CheckUserMail(mail))//表示有这个邮箱
            {
                userInfoManager.SendUserMail(mail);
                context.Response.Write("ok");
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