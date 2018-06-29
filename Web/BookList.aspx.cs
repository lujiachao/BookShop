using BookShop.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BookShop.Web
{
    public partial class BookList : System.Web.UI.Page
    {
        public List<Model.Book> ProductList { get; set; }
        public int PageIndex { get; set; }
        public int PageCount { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.HttpMethod == "GET")
            {
                BindBookList();
            }
        }
        /// <summary>
        /// 加载数据
        /// </summary>
        protected void BindBookList()
        {
            BookManager bookManager = new BookManager();
            //ProductList = bookManager.GetModelList("");
            int pageIndex=1;
            if (!int.TryParse(Request["pageIndex"], out pageIndex))
            {
                pageIndex = 1;
            }
            int pageSize = 10;
            int pageCount = bookManager.GetPageCount(pageSize);//获取总页数
            PageCount = pageCount;
            pageIndex = pageIndex < 1 ? 1 : pageIndex;
            pageIndex = pageIndex > pageCount ? pageCount : pageIndex;
            PageIndex = pageIndex;
            ProductList = bookManager.GetPageList(pageIndex, pageSize); //获取分页数据

        }
        /// <summary>
        /// 截取字符串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public string CutString(string str, int length)
        {
            if (str.Length > length)
            {
                return str.Substring(0,length)+".........";
            }
            else
            {
                return str;
            }


        }
        public string GetDir(DateTime time)
        {
            return "/BookDetails/" + time.Year + "/" + time.Month + "/" + time.Day + "/";
        }
    }
}