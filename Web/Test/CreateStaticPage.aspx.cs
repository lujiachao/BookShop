using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BookShop.Web.Test
{
    public partial class CreateStaticPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                BLL.BookManager bookManager = new BLL.BookManager();
               List<Model.Book>list= bookManager.GetModelList("");
               foreach (Model.Book bookModel in list)
               {
                   bookManager.CreateHtmlPage(bookModel.Id);
               }
            }
        }
    }
}