using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using BookShop.Model;
using BookShop.DAL;
namespace BookShop.BLL
{
	/// <summary>
	/// ҵ���߼���OrderBookManager ��ժҪ˵����
	/// </summary>
	public class OrderBookManager
	{
        BookServices bookServices = new BookServices();
		private readonly BookShop.DAL.OrderBookServices dal=new BookShop.DAL.OrderBookServices();
		public OrderBookManager()
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
		public int  Add(BookShop.Model.OrderBook model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(BookShop.Model.OrderBook model)
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
		public BookShop.Model.OrderBook GetModel(int Id)
		{
			
			return dal.GetModel(Id);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public BookShop.Model.OrderBook GetModelByCache(int Id)
		{
			
			string CacheKey = "OrderBookModel-" + Id;
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
			return (BookShop.Model.OrderBook)objModel;
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
		public List<BookShop.Model.OrderBook> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}

        /// <summary>
        /// ��������б�
        /// </summary>
        public List<BookShop.Model.OrderBook> GetModelListByOrderId(string orderId)
        {
            DataSet ds = dal.GetListByOrderId(orderId);
            return DataTableToList(ds.Tables[0]);
        }
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<BookShop.Model.OrderBook> DataTableToList(DataTable dt)
		{
			List<BookShop.Model.OrderBook> modelList = new List<BookShop.Model.OrderBook>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				BookShop.Model.OrderBook model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new BookShop.Model.OrderBook();
					if(dt.Rows[n]["Id"].ToString()!="")
					{
						model.Id=int.Parse(dt.Rows[n]["Id"].ToString());
					}
					if(dt.Rows[n]["OrderID"].ToString()!="")
					{
						model.OrderID=dt.Rows[n]["OrderID"].ToString();
					}
					if(dt.Rows[n]["BookID"].ToString()!="")
					{
				

                        int BookID = int.Parse(dt.Rows[n]["BookID"].ToString());
                        model.Book = bookServices.GetModel(BookID);
					}
					if(dt.Rows[n]["Quantity"].ToString()!="")
					{
						model.Quantity=int.Parse(dt.Rows[n]["Quantity"].ToString());
					}
					if(dt.Rows[n]["UnitPrice"].ToString()!="")
					{
						model.UnitPrice=decimal.Parse(dt.Rows[n]["UnitPrice"].ToString());
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

		/// <summary>
		/// ��������б�
		/// </summary>
		//public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return dal.GetList(PageSize,PageIndex,strWhere);
		//}



		#endregion  ��Ա����
	}
}

