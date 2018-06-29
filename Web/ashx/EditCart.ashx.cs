using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookShop.Web.ashx
{
    /// <summary>
    /// EditCart 的摘要说明
    /// </summary>
    public class EditCart : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            //更新购物车中对应商品的数量。
            int count =Convert.ToInt32(context.Request["count"]);
            int cartId = Convert.ToInt32(context.Request["cartId"]);
            BLL.CartManager cartManager = new BLL.CartManager();
            Model.Cart cartModel=cartManager.GetModel(cartId);
            cartModel.Count = count;
            cartManager.Update(cartModel);
            context.Response.Write("ok");
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