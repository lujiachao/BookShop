using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using BookShop.Model;
using BookShop.DAL;
namespace BookShop.BLL
{
	/// <summary>
	/// ҵ���߼���CartManager ��ժҪ˵����
	/// </summary>
	public class CartManager
	{
        
		private readonly BookShop.DAL.CartServices dal=new BookShop.DAL.CartServices();
        UserServices userServices = new UserServices();
        BookServices bookServices = new BookServices();

		public CartManager()
		{}
		#region  ��Ա����

		/// <summary>
		/// �õ����ID
		/// </summary>
		public int GetMaxId()
		{
			return dal.GetMaxId();
		}

		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(int Id)
		{
			return dal.Exists(Id);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public int  Add(BookShop.Model.Cart model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(BookShop.Model.Cart model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(int Id)
		{
			
			dal.Delete(Id);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public BookShop.Model.Cart GetModel(int Id)
		{
			
			return dal.GetModel(Id);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public BookShop.Model.Cart GetModelByCache(int Id)
		{
			
			string CacheKey = "CartModel-" + Id;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(Id);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (BookShop.Model.Cart)objModel;
		}

		/// <summary>
		/// ��������б�
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
		/// <summary>
		/// ���ǰ��������
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<BookShop.Model.Cart> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<BookShop.Model.Cart> DataTableToList(DataTable dt)
		{
			List<BookShop.Model.Cart> modelList = new List<BookShop.Model.Cart>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				BookShop.Model.Cart model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new BookShop.Model.Cart();
					if(dt.Rows[n]["Id"].ToString()!="")
					{
						model.Id=int.Parse(dt.Rows[n]["Id"].ToString());
					}
					if(dt.Rows[n]["UserId"].ToString()!="")
					{
						 int UserId=int.Parse(dt.Rows[n]["UserId"].ToString());
                         model.User = userServices.GetModel(UserId);
					}
					if(dt.Rows[n]["BookId"].ToString()!="")
					{
						int BookId=int.Parse(dt.Rows[n]["BookId"].ToString());
                        model.Book = bookServices.GetModel(BookId);
					}
					if(dt.Rows[n]["Count"].ToString()!="")
					{
						model.Count=int.Parse(dt.Rows[n]["Count"].ToString());
					}

					modelList.Add(model);
				}
			}
			return modelList;
		}

		/// <summary>
		/// ��������б�
		/// </summary>
		public DataSet GetAllList()
		{
			return GetList("");
		}

		//-------------------------------
        public Model.Cart GetCartModel(int bookId, int userId)
        {
            return dal.GetCartModel(bookId, userId);
        }

		#endregion  ��Ա����
	}
}

