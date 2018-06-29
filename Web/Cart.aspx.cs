using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BookShop.Web
{
    public partial class Cart:CheckUserState.CheckSession
    {
        public List<Model.Cart> CartList { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            int userId = ((Model.User)Session["userInfo"]).Id;
            BLL.CartManager cartManager = new BLL.CartManager();
            List<Model.Cart>list=cartManager.GetModelList("UserId="+userId);
            CartList = list;
        }
    }
}