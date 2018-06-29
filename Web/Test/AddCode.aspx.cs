using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BookShop.Web.Test
{
    public partial class AddCode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                string msg=Request["msg"];
                msg = msg.Trim();
                string[]words=msg.Split(new char[]{'\r','\n'},StringSplitOptions.RemoveEmptyEntries);
                BLL.Articel_WordsManager bll = new BLL.Articel_WordsManager();
                foreach (string str in words)
                {
                    string[] word = str.Split('=');
                    Model.Articel_Words model = new Model.Articel_Words();
                    model.WordPattern = word[0];
                    if (word[1] =="{BANNED}")
                    {
                        model.IsForbid = true;
                    }
                    else if (word[1] == "{MOD}")
                    {
                        model.IsMod = true;
                    }
                    else
                    {
                        model.ReplaceWord = word[1];
                    }
                    bll.Add(model);
                }
            }
        }
    }
}