using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BookShop.Web.Account
{
    public partial class Register : System.Web.UI.Page
    {
        public string ReturnUrl { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.HttpMethod == "GET")
            {
                if (!string.IsNullOrEmpty(Request["returnUrl"]))
                {
                    ReturnUrl = Request["returnUrl"];
                }
            }
        }
    }
}