using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using BookShop.Model;
using BookShop.DAL;
namespace BookShop.BLL
{
	/// <summary>
	/// ҵ���߼���OrdersManager ��ժҪ˵����
	/// </summary>
	public class OrdersManager
	{
        UserServices userServices = new UserServices();
		private readonly BookShop.DAL.OrdersServices dal=new BookShop.DAL.OrdersServices();
		public OrdersManager()
		{}
		#region  ��Ա����

	



		/// <summary>
		/// ����һ������
		/// </summary>
		public int  Add(BookShop.Model.Orders model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(BookShop.Model.Orders model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(string orderId)
		{

            dal.Delete(orderId);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public BookShop.Model.Orders GetModel(string orderId)
		{

            return dal.GetModel(orderId);
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
		public List<BookShop.Model.Orders> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
        public List<BookShop.Model.Orders> GetModelListByUserId(string userId)
        {
            DataSet ds = dal.GetList(string.Format(" UserId='{0}'",userId));
            return DataTableToList(ds.Tables[0]);
        }

		/// <summary>
		/// ��������б�
		/// </summary>
		public List<BookShop.Model.Orders> DataTableToList(DataTable dt)
		{
			List<BookShop.Model.Orders> modelList = new List<BookShop.Model.Orders>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				BookShop.Model.Orders model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new BookShop.Model.Orders();
                    if (dt.Rows[n]["OrderId"].ToString() != "")
					{
                        model.OrderId = dt.Rows[n]["OrderId"].ToString();
					}
					if(dt.Rows[n]["OrderDate"].ToString()!="")
					{
						model.OrderDate=DateTime.Parse(dt.Rows[n]["OrderDate"].ToString());
					}
					if(dt.Rows[n]["UserId"].ToString()!="")
					{
						int UserId=int.Parse(dt.Rows[n]["UserId"].ToString());
                        model.User = userServices.GetModel(UserId);
					}
					if(dt.Rows[n]["TotalPrice"].ToString()!="")
					{
						model.TotalPrice=decimal.Parse(dt.Rows[n]["TotalPrice"].ToString());
					}

                    if (dt.Rows[n]["PostAddress"].ToString() != "")
                    {
                        model.PostAddress = dt.Rows[n]["PostAddress"].ToString();
                        
                    }
                    if (dt.Rows[n]["State"].ToString() != "")
                    {
                        model.State = int.Parse(dt.Rows[n]["State"].ToString());
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


        public decimal CreateOrders(int userId, string orderNumber, string postAddress)
        {

           return dal.CreateOrder(userId, orderNumber, postAddress);
        }

		#endregion  ��Ա����
	}
}

