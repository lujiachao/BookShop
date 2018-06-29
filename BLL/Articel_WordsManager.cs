using BookShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BookShop.BLL
{
   public class Articel_WordsManager
    {
       DAL.Articel_WordsService dal = new DAL.Articel_WordsService();
       public bool Add(Articel_Words model)
       {
           return dal.Add(model)>0;
       }
       /// <summary>
       /// 对评论中禁用词进行过滤
       /// </summary>
       /// <param name="msg">评论的内容</param>
       /// <returns></returns>
       public bool CheckBannd(string msg)
       {
           //获取所有的禁用词
          object obj= Common.CacheHelper.Get("banned");
          List<string> list = null;
          if (obj == null)
          {
              list = dal.GetBannd();//将敏感词放到缓存中
              Common.CacheHelper.Set("banned", list);
          }
          else
          {
              list = obj as List<string>;
          }

          string regex= string.Join("|",list.ToArray());//aa|bb|cc|
         return Regex.IsMatch(msg, regex);
       }
       /// <summary>
       /// 审查词过滤
       /// </summary>
       /// <param name="msg"></param>
       /// <returns></returns>
       public bool CheckMod(string msg)
       {
           object obj = Common.CacheHelper.Get("mod");
           List<string> list = null;
           if (obj == null)
           {
               list = dal.GetMod();
               Common.CacheHelper.Set("mod", list);
           }
           else
           {
               list = obj as List<string>;
           }
           string regex = string.Join("|", list.ToArray());//aa|bb|cc|
           regex = regex.Replace(@"\",@"\\").Replace("{2}",".{0,2}");
           return Regex.IsMatch(msg, regex);
       }
       /// <summary>
       /// 替换词过滤
       /// </summary>
       /// <param name="msg"></param>
       /// <returns></returns>
       public string CheckReplace(string msg)
       {
           object obj = Common.CacheHelper.Get("replace");
           List<Model.Articel_Words> list = null;
           if (obj == null)
           {
               list = dal.GetReplace();
               Common.CacheHelper.Set("replace", list);
           }
           else
           {
               list = obj as List<Model.Articel_Words>;
           }
           foreach (Model.Articel_Words model in list)
           {
               msg = msg.Replace(model.WordPattern, model.ReplaceWord);
           }
           return msg;
       }
    }
}
