using BookShop.Model;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace BookShop.BLL
{
   public partial class UserManager
    {
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public BookShop.Model.User GetModel(string userName)
        {

            return dal.GetModel(userName);
        }
       //判断此邮箱是否存在
        public bool CheckUserMail(string mail)
        {
            return dal.CheckUserMail(mail)>0;
        }

       /// <summary>
		/// 增加一条数据
		/// </summary>
        public int Add(BookShop.Model.User model,out string msg)
        {
            //判断用户名是否被占用.
            if (dal.GetModel(model.LoginId) == null)
            {
                msg = "注册成功!!";
                return dal.Add(model);
            }
            else
            {
                msg = "注册失败";
                return -1;
            }
          
        }
       /// <summary>
       /// 完成用户登录
       /// </summary>
       /// <param name="userName"></param>
       /// <param name="userPwd"></param>
       /// <param name="msg"></param>
       /// <param name="userInfo"></param>
       /// <returns></returns>
        public bool UserLogin(string userName, string userPwd, out string msg, out User userInfo)
        {
            userInfo=dal.GetModel(userName);
            if (userInfo != null)
            {
                if (userInfo.LoginPwd == WebCommon.Md5String(WebCommon.Md5String(userPwd)))
                {
                    msg = "用户登录成功";
                    return true;
                }
                else
                {
                    msg = "用户名密码错误!!";
                    return false;
                }
            }
            else
            {
                msg = "此用户不存在!!";
                return false;
            }
        }
       /// <summary>
       /// 发送邮件，找回密码
       /// </summary>
        public void SendUserMail(string mail)
        {
            //1：系统产生一个新的密码(一定要替换用户在数据库中的旧密码,但是发送到用户邮箱中的密码必须是明文)，发送到用户邮箱中。
            //2:发送一个连接.
            SettingsManager settingManager = new SettingsManager();
            string newPwd = Guid.NewGuid().ToString().Substring(0,8);

            Model.User userInfo=dal.GetUserByMail(mail);//根据邮箱找用户
            userInfo.LoginPwd = Common.WebCommon.Md5String(Common.WebCommon.Md5String(newPwd));
            dal.Update(userInfo);
            MailMessage mailMsg = new MailMessage();//两个类，别混了，要引入System.Net这个Assembly
            mailMsg.From = new MailAddress(settingManager.GetModel("系统邮件地址").Value);//源邮件地址 
            mailMsg.To.Add(new MailAddress(mail));//目的邮件地址。可以有多个收件人
            mailMsg.Subject = "您在xxx网站新的账户";//发送邮件的标题 
            StringBuilder sb = new StringBuilder();
            sb.Append("您在xxx网站中新的账户如下:");
            sb.Append("用户名:"+userInfo.LoginId);
            sb.Append("密码是:"+newPwd);
            mailMsg.Body = sb.ToString();//发送邮件的内容 
            mailMsg.IsBodyHtml = true;
            //指定Smtp服务地址。(根据发件人邮箱指定对应的SMTP服务器地址)
            SmtpClient client = new SmtpClient(settingManager.GetModel("系统邮件SMTP").Value);//smtp.163.com，smtp.qq.com
            client.Credentials = new NetworkCredential(settingManager.GetModel("系统邮件用户名").Value, settingManager.GetModel("系统邮件密码").Value);//发件人邮箱的用户名密码
            client.Send(mailMsg);
        }
    }
}
