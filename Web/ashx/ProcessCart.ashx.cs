using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookShop.Web.ashx
{
    /// <summary>
    /// ProcessCart 的摘要说明
    /// </summary>
    public class ProcessCart : IHttpHandler,System.Web.SessionState.IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int bookId = Convert.ToInt32(context.Request["bookId"]);
            //判断一下商品是否存在!!
            BLL.BookManager bookManager = new BLL.BookManager();
            Model.Book bookModel=bookManager.GetModel(bookId);
            if (bookModel != null)
            {
                //判断一下数据库中是否有该商品，如果有，更新数量，没有添加.
                BLL.CartManager cartManager = new BLL.CartManager();
                int userId=((Model.User)context.Session["userInfo"]).Id;
                Model.Cart cartModel=cartManager.GetCartModel(bookId,userId);
                if (cartModel != null)
                {
                    cartModel.Count = cartModel.Count + 1;
                    cartManager.Update(cartModel);
                   
                }
                else
                {
                    Model.Cart modelCart = new Model.Cart();
                    modelCart.Count = 1;
                    modelCart.User = (Model.User)context.Session["userInfo"];
                    modelCart.Book = bookModel;
                    cartManager.Add(modelCart);
                }
                context.Response.Write("yes");
            }
            else
            {
                context.Response.Write("no");
            }
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