using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace BookShop.Web.ashx
{
    /// <summary>
    /// upload 的摘要说明
    /// </summary>
    public class upload : IHttpHandler
    {
      
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
           
            string action=context.Request["action"];
            if (action == "up")
            { // 上传图片
                fileUpload(context);
            }
            else if (action == "cut")
            {
                cutFileImage(context);//截取图片文件
            }
            else
            {
                context.Response.Write("参数异常!!");
            }
           
        }
        #region 截取图片文件
        private void cutFileImage(HttpContext context)
        {
            int x = Convert.ToInt32(context.Request["x"]);
            int y = Convert.ToInt32(context.Request["y"]);
            int width = Convert.ToInt32(context.Request["width"]);
            int height = Convert.ToInt32(context.Request["height"]);
            string imagePath = context.Request["imagePath"];
            using (Bitmap map = new Bitmap(width,height))
            {
                using (Graphics g = Graphics.FromImage(map))
                {
                    using (Image img = Image.FromFile(context.Request.MapPath(imagePath)))
                    {
                        //将原图的指定范围画到画布上.
                        //1:表示对哪张图片进行操作
                        //2:画多么大.
                        //3:画原图的哪块区域
                        g.DrawImage(img,new Rectangle(0,0,width,height),new Rectangle(x,y,width,height),GraphicsUnit.Pixel);
                        string fileNewName = Guid.NewGuid().ToString();
                        string fullDir = "/UploadImage/"+fileNewName+".jpg";
                        map.Save(context.Request.MapPath(fullDir), System.Drawing.Imaging.ImageFormat.Jpeg);//保存图片.
                        //一定要将截取后的图片路径存储到数据库中。
                        context.Response.Write(fullDir);
                    }
                }
            }
        }
        #endregion
        #region 上传图片
        private void fileUpload(HttpContext context)
        {
            HttpFileCollection collection = context.Request.Files;
            bool isSucess = false;
            if (collection.Count > 0)
            {
                HttpPostedFile file = context.Request.Files["Filedata"];
                if (file != null)
                {
                    string fileName = Path.GetFileName(file.FileName);
                    string fileExt = Path.GetExtension(fileName);
                    if (fileExt == ".jpg")
                    {
                        string dir = "/UploadImage/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day + "/";
                        if (!Directory.Exists(context.Request.MapPath(dir)))
                        {
                            Directory.CreateDirectory(context.Request.MapPath(dir));
                        }
                        string newfileName = Guid.NewGuid().ToString();
                        string fullDir = dir + newfileName + fileExt;
                        file.SaveAs(context.Request.MapPath(fullDir));
                        isSucess = true;
                        using (Image img = Image.FromFile(context.Request.MapPath(fullDir)))
                        {
                            context.Response.Write("ok:" + fullDir + ":" + img.Width + ":" + img.Height);
                        }
                        //file.SaveAs(context.Request.MapPath("/UploadImage/" + fileName));
                        //context.Response.Write("/UploadImage/" + fileName);
                    }
                }
            }
            if (!isSucess)
            {
                context.Response.Write("no:上传失败!!");
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