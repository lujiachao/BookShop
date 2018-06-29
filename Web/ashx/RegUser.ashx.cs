using BookShop.BLL;
using BookShop.Model;
using BookShop.Model.Enum;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace BookShop.Web.ashx
{
    /// <summary>
    /// RegUser 的摘要说明
    /// </summary>
    public class RegUser : IHttpHandler,System.Web.SessionState.IRequiresSessionState
    {
        //客户端校验完成，服务端必须校验.
        User userInfo = null;
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            if (WebCommon.CheckValidateCode())//判断验证码
            {
                context.Session["vCode"] = null;
                string msg = string.Empty;
                //完成用户注册
                if (UserRegister(context, out msg))
                {
                    //1：注册成功表示已经登录。
                    context.Session["userInfo"] = userInfo;
                    //2:发送激活的邮件

                    context.Response.Write("yes:"+msg);
                }
                else
                {
                    context.Response.Write("no:" + msg);
                }
            }
            else
            {
                context.Response.Write("no:验证码错误!!");
            }
        }
        //完成用户的注册
        private bool UserRegister(HttpContext context,out string msg)
        {
             userInfo = new User();
            userInfo.Address = context.Request["txtAddress"];
            userInfo.LoginId=context.Request["txtName"];
            userInfo.LoginPwd = WebCommon.Md5String(WebCommon.Md5String(context.Request["txtPwd"]));
            userInfo.Mail=context.Request["txtMail"];
            userInfo.Name = context.Request["txtRealName"];
            userInfo.Phone=context.Request["txtPhone"];
            userInfo.UserState.Id =Convert.ToInt32(UserStateEnum.UserNormarl);
            UserManager userManager = new UserManager();
            return userManager.Add(userInfo, out msg) > 0;
           
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