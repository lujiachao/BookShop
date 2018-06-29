using BookShop.BLL;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BookShop.Web.Account
{
    public partial class Login : System.Web.UI.Page
    {
        public string Msg { get; set; }
        UserManager userManager = new UserManager();
        public string ReturnUrl { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.HttpMethod == "POST")
            {
                UserLogin();//用户登录
            }
            else //get请求
            {
                if (!string.IsNullOrEmpty(Request["returnUrl"]))
                {
                    ReturnUrl=Request["returnUrl"];
                }

                //判断Session中是否有值
                if (Session["userInfo"] != null)
                {
                    Response.Redirect("/UserInfoManager/UserCenter.aspx");
                }
                if (Request.Cookies["cp1"] != null)
                {
                    string userName = Request.Cookies["cp1"].Value;
                    Model.User userInfo = userManager.GetModel(userName);
                    if (WebCommon.CheckCookieInfo(userInfo))
                    {
                        Response.Redirect("/UserInfoManager/UserCenter.aspx");
                    }
                }
            }
        }
        #region 完成用户登录
        protected void UserLogin()
        {
            string userName = Request["username"];
            string userPwd = Request["password"];
            if (userName != "" && userPwd != "")
            {
                string msg = string.Empty;
                Model.User userInfo = null;
              
                //判断用户名密码
                if (userManager.UserLogin(userName, userPwd, out msg, out userInfo))
                {
                    Session["userInfo"] = userInfo;
                    //判断一下用户是否选择了"自动登录"
                    if (!string.IsNullOrEmpty(Request["chkRember"]))
                    {
                        HttpCookie cookie1 = new HttpCookie("cp1", userName);
                        HttpCookie cookie2 = new HttpCookie("cp2", WebCommon.Md5String(WebCommon.Md5String(userPwd)));
                        cookie1.Expires = DateTime.Now.AddDays(7);
                        cookie2.Expires = DateTime.Now.AddDays(7);
                        Response.Cookies.Add(cookie1);
                        Response.Cookies.Add(cookie2);
                    }
                    if (!string.IsNullOrEmpty(Request["returnurl"]))//接收隐藏域的值
                    {
                        Response.Redirect(Request["returnurl"]);
                    }
                    else
                    {
                        Response.Redirect("/UserInfoManager/UserCenter.aspx");
                    }
                }
                else
                {
                    Msg = msg;
                }
            }
            else
            {
                Msg = "用户名密码不能为空!!";
            }
        }
        #endregion
   
    }
}