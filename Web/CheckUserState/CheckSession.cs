using BookShop.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookShop.Web.CheckUserState
{
    public class CheckSession:System.Web.UI.Page
    {
        public void Page_Init(object sender, EventArgs e)
        {
            if (Session["userInfo"] == null)
            {
                if (Request.Cookies["cp1"] != null)
                {
                    UserManager userManager = new UserManager();
                    string userName = Request.Cookies["cp1"].Value;
                    Model.User userInfo = userManager.GetModel(userName);
                    if (!Common.WebCommon.CheckCookieInfo(userInfo))
                    {
                       // Response.Redirect("/Account/Login.aspx");
                        Common.WebCommon.ReturnRedirect();
                    }
                }
                else
                {
                    //Response.Redirect("/Account/Login.aspx");
                    Common.WebCommon.ReturnRedirect();
                }
            }
        }
    }
}