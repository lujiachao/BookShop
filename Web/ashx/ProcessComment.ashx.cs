using BookShop.Web.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookShop.Web.ashx
{
    /// <summary>
    /// ProcessComment 的摘要说明
    /// </summary>
    public class ProcessComment : IHttpHandler
    {
        BLL.BookCommentManager bookCommentManager = new BLL.BookCommentManager();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string action=context.Request["action"];
            if (action == "load")
            {
                LoadCommentList(context);//加载评论
            }
            else if (action == "add")//添加评论
            {
                AddComment(context);
            }
            else
            {
                context.Response.Write("参数错误!!");
            }
        }
        #region 加载评论
         private void LoadCommentList(HttpContext context)
        {
            int id = Convert.ToInt32(context.Request["bookId"]);
            
            List<Model.BookComment> list = bookCommentManager.GetModelList("BookId=" + id);//根据书的编号找对应的评论.
            List<CommentViewModel> newList = new List<CommentViewModel>();
            foreach (Model.BookComment bookComment in list)
            {
                CommentViewModel viewModel = new CommentViewModel();
                viewModel.Msg = Common.WebCommon.HtmlDecode(bookComment.Msg);//将UBB编码转成HTML代码
                TimeSpan ts = DateTime.Now - bookComment.CreateDateTime;//计算评论时间
                viewModel.CreateDateTime = Common.WebCommon.GetTimeSpan(ts);//处理时间
                newList.Add(viewModel);
            }

            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
            context.Response.Write(js.Serialize(newList));
        }

        #endregion

         #region 添加评论
         private void AddComment(HttpContext context)
         {
             string msg = context.Request["msg"];
             //判断禁用词.
             BLL.Articel_WordsManager articelBll = new BLL.Articel_WordsManager();
             if (articelBll.CheckBannd(msg))
             {
                 context.Response.Write("no:评论中含有禁用词!!");
             }
             else if (articelBll.CheckMod(msg))//审查词过滤
             {
                 AddBookComment(context, msg);
                 context.Response.Write("no:评论中含有审查词!!");
                
             }
             else
             {
                 //替换词过滤
                msg= articelBll.CheckReplace(msg);
                AddBookComment(context, msg);
             }
            
         }
         private void AddBookComment(HttpContext context,string msg)
         {
             Model.BookComment bookComment = new Model.BookComment();
             bookComment.Msg = msg;
             bookComment.BookId = Convert.ToInt32(context.Request["bookId"]);
             bookComment.CreateDateTime = DateTime.Now;
            // bookComment.IsState=1
             if (bookCommentManager.Add(bookComment) > 0)
             {
                 context.Response.Write("ok:评论成功!!");
             }
             else
             {
                 context.Response.Write("no:评论失败!!");
             }

         }
         #endregion
       



        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}