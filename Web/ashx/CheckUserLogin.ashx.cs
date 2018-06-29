using BookShop.BLL;
using BookShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookShop.Web.ashx
{
    /// <summary>
    /// CheckUserLogin 的摘要说明
    /// </summary>
    public class CheckUserLogin : IHttpHandler,System.Web.SessionState.IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
           
            
            if (context.Session["userInfo"] == null)
            {
                if (context.Request.Cookies["cp1"] != null)
                {
                    UserManager userManager = new UserManager();
                   User  userInfo = userManager.GetModel(context.Request.Cookies["cp1"].Value);
                    if (Common.WebCommon.CheckCookieInfo(userInfo))
                    {

                       // context.Response.Write("yes:" + userInfo.LoginId);
                        ShowOkMsg(context);
                    }
                    else
                    {
                        ShowErrorMsg(context);
                    }

                }
                else
                {
                    ShowErrorMsg(context);
                }
            }
            else
            {
                ShowOkMsg(context);
            }
          
        }
        private void ShowErrorMsg(HttpContext context)
        {
            context.Response.Write("no:没有登录");
        }
        private void ShowOkMsg(HttpContext context)
        {
            context.Response.Write("yes:" + ((Model.User)context.Session["userInfo"]).LoginId);
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