using BookShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace Common
{
  public  class WebCommon
    {
      /// <summary>
      /// 完成验证码的校验
      /// </summary>
      /// <returns></returns>
      public static bool CheckValidateCode()
      {
          HttpContext context = HttpContext.Current;
          bool isSucess = false;
          if (context.Session["vCode"] != null)
          {
              string code = context.Request["txtCode"];
              string sysCode = context.Session["vCode"].ToString();
              if (sysCode.Equals(code, StringComparison.InvariantCultureIgnoreCase))
              {
                  //context.Session["vCode"] = null;
                  isSucess = true;
              }
          }
          return isSucess;
      }
      /// <summary>
      /// 对字符串进行MD5运算
      /// </summary>
      /// <param name="str"></param>
      /// <returns></returns>
      public static string Md5String(string str)
      {
          MD5 md5 = MD5.Create();
          byte[] buffer = System.Text.Encoding.UTF8.GetBytes(str);
          byte[]md5Buffer=md5.ComputeHash(buffer);
          StringBuilder sb = new StringBuilder();
          foreach (byte b in md5Buffer)
          {
              sb.Append(b.ToString("x2"));
          }
          md5.Clear();
          return sb.ToString();
      }
      /// <summary>
      /// 完成Cookie信息的校验
      /// </summary>
      /// <param name="userInfo"></param>
      /// <returns></returns>
      public static bool CheckCookieInfo(User userInfo)
      {
           HttpContext context = HttpContext.Current;
           if (context.Request.Cookies["cp1"] != null && context.Request.Cookies["cp2"] != null)
           {
               string userName = context.Request.Cookies["cp1"].Value;
               string userPwd = context.Request.Cookies["cp2"].Value;
               if (userInfo != null)
               {
                   if (userInfo.LoginId == userName)
                   {
                       if (userInfo.LoginPwd == userPwd)
                       {
                           context.Session["userInfo"]=userInfo;
                           return true;
                       }
                   }
               }
               context.Response.Cookies["cp1"].Expires = DateTime.Now.AddDays(-1);
               context.Response.Cookies["cp2"].Expires = DateTime.Now.AddDays(-1);
           }
           return false;
      }
      /// <summary>
      /// 跳转到登录页面
      /// </summary>
      public static void ReturnRedirect()
      {
          HttpContext context = HttpContext.Current;
          context.Response.Redirect("/Account/Login.aspx?returnUrl="+context.Server.UrlEncode(context.Request.Url.ToString()));
      }
      /// <summary>
      /// 对时间差进行处理--3天5小时40分钟---》3*24+5+40/60
      /// </summary>
      /// <param name="ts"></param>
      /// <returns></returns>
      public static string GetTimeSpan(TimeSpan ts)
      {
          if (ts.TotalDays >= 365)
          {
             return Math.Floor(ts.TotalDays / 365) + "年前";
          }
          else if (ts.TotalDays >= 30)
          {
              return Math.Floor(ts.TotalDays/30)+"月前";
          }
          else if (ts.TotalHours >= 24)
          {
              return Math.Floor(ts.TotalDays)+"天前";
          }
          else if (ts.TotalHours >= 1)
          {
              return Math.Floor(ts.TotalHours) + "小时前";
          }
          else if (ts.TotalMinutes >= 1)
          {
              return Math.Floor(ts.TotalMinutes) + "分钟前";
          }
          else
          {
              return "刚刚";
          }
      }
      /// <summary>
      /// 将UBB编码转成标准的HTML代码
      /// </summary>
      /// <param name="argString"></param>
      /// <returns></returns>
      public static string HtmlDecode(string argString)
      {
          string tString = argString;
          if (tString != "")
          {
              Regex tRegex;
              bool tState = true;
              tString = tString.Replace("&", "&amp;");
              tString = tString.Replace(">", "&gt;");
              tString = tString.Replace("<", "&lt;");
              tString = tString.Replace("\"", "&quot;");
              tString = Regex.Replace(tString, @"\[br\]", "<br />", RegexOptions.IgnoreCase);
              string[,] tRegexAry = {
          {@"\[p\]([^\[]*?)\[\/p\]", "$1<br />"},
          {@"\[b\]([^\[]*?)\[\/b\]", "<b>$1</b>"},
          {@"\[i\]([^\[]*?)\[\/i\]", "<i>$1</i>"},
          {@"\[u\]([^\[]*?)\[\/u\]", "<u>$1</u>"},
          {@"\[ol\]([^\[]*?)\[\/ol\]", "<ol>$1</ol>"},
          {@"\[ul\]([^\[]*?)\[\/ul\]", "<ul>$1</ul>"},
          {@"\[li\]([^\[]*?)\[\/li\]", "<li>$1</li>"},
          {@"\[code\]([^\[]*?)\[\/code\]", "<div class=\"ubb_code\">$1</div>"},
          {@"\[quote\]([^\[]*?)\[\/quote\]", "<div class=\"ubb_quote\">$1</div>"},
          {@"\[color=([^\]]*)\]([^\[]*?)\[\/color\]", "<font style=\"color: $1\">$2</font>"},
          {@"\[hilitecolor=([^\]]*)\]([^\[]*?)\[\/hilitecolor\]", "<font style=\"background-color: $1\">$2</font>"},
          {@"\[align=([^\]]*)\]([^\[]*?)\[\/align\]", "<div style=\"text-align: $1\">$2</div>"},
          {@"\[url=([^\]]*)\]([^\[]*?)\[\/url\]", "<a href=\"$1\">$2</a>"},
          {@"\[img\]([^\[]*?)\[\/img\]", "<img src=\"$1\" />"}
        };
              while (tState)
              {
                  tState = false;
                  for (int ti = 0; ti < tRegexAry.GetLength(0); ti++)
                  {
                      tRegex = new Regex(tRegexAry[ti, 0], RegexOptions.IgnoreCase);
                      if (tRegex.Match(tString).Success)
                      {
                          tState = true;
                          tString = Regex.Replace(tString, tRegexAry[ti, 0], tRegexAry[ti, 1], RegexOptions.IgnoreCase);
                      }
                  }
              }
          }
          return tString;
      }



    }
}
