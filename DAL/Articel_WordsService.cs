using BookShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Maticsoft.DBUtility;
namespace BookShop.DAL
{
   public class Articel_WordsService
    {
       public int Add(Articel_Words model)
       {
           string sql = "insert into Articel_Words(WordPattern, IsForbid, IsMod, ReplaceWord)values( @WordPattern, @IsForbid, @IsMod, @ReplaceWord)";
           SqlParameter[] pars = { 
                                 new SqlParameter("@WordPattern",model.WordPattern),
                                   new SqlParameter("@IsForbid",model.IsForbid),
                                     new SqlParameter("@IsMod",model.IsMod),
                                       new SqlParameter("@ReplaceWord",model.ReplaceWord)
                                 
                                 };
           return DbHelperSQL.ExecuteSql(sql, pars);
       }
       /// <summary>
       /// 获取所有的禁用词
       /// </summary>
       /// <returns></returns>
       public List<string> GetBannd()
       {
           string sql = "select WordPattern from Articel_Words where IsForbid=1";
           List<string> list = null;
           using (SqlDataReader reader = DbHelperSQL.ExecuteReader(sql))
           {
               if (reader.HasRows)
               {
                   list = new List<string>();
                   while (reader.Read())
                   {
                       list.Add(reader.GetString(0));
                   }
               }
           }
           return list;
       }
       /// <summary>
       /// 获取所有的审查词
       /// </summary>
       /// <returns></returns>
       public List<string> GetMod()
       {
           string sql = "select WordPattern from Articel_Words where IsMod=1";
           List<string> list = null;
           using (SqlDataReader reader = DbHelperSQL.ExecuteReader(sql))
           {
               if (reader.HasRows)
               {
                   list = new List<string>();
                   while (reader.Read())
                   {
                       list.Add(reader.GetString(0));
                   }
               }
           }
           return list;
       }
       /// <summary>
       /// 获取所有的替换词
       /// </summary>
       /// <returns></returns>
       public List<Model.Articel_Words> GetReplace()
       {
           string sql = "select WordPattern,ReplaceWord from Articel_Words where IsForbid=0 and IsMod=0";
           List<Model.Articel_Words> list = null;
           using (SqlDataReader reader = DbHelperSQL.ExecuteReader(sql))
           {
               if (reader.HasRows)
               {
                   list = new List<Articel_Words>();
                   while (reader.Read())
                   {
                       Model.Articel_Words model = new Articel_Words();
                       model.WordPattern = reader.GetString(0);
                       model.ReplaceWord = reader.GetString(1);
                       list.Add(model);
                   }
               }
               return list;
           }
       }
    }
}
