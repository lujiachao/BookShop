using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BookShop.Web.Test
{
    public partial class TestSendEmail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MailMessage mailMsg = new MailMessage();//两个类，别混了，要引入System.Net这个Assembly
            mailMsg.From = new MailAddress("wang_itcast@126.com", "王承伟");//源邮件地址 
            mailMsg.To.Add(new MailAddress("wangchengwei324@126.com", "王承伟"));//目的邮件地址。可以有多个收件人
            mailMsg.Subject = "Hello,大家好!";//发送邮件的标题 
            mailMsg.Body = "Tai Xie E le！";//发送邮件的内容 
            //指定Smtp服务地址。
            SmtpClient client = new SmtpClient("smtp.126.com");//smtp.163.com，smtp.qq.com
            client.Credentials = new NetworkCredential("wang_itcast", "wangchengwei");
            client.Send(mailMsg);
        }
    }
}