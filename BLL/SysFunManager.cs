using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using BookShop.Model;
namespace BookShop.BLL
{
	/// <summary>
	/// SysFunManager
	/// </summary>
	public partial class SysFunManager
	{
		private readonly BookShop.DAL.SysFunServices dal=new BookShop.DAL.SysFunServices();
		public SysFunManager()
		{}
		#region  Method

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
		public bool Exists(int NodeId)
		{
			return dal.Exists(NodeId);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Add(BookShop.Model.SysFun model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public bool Update(BookShop.Model.SysFun model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public bool Delete(int NodeId)
		{
			
			return dal.Delete(NodeId);
		}
		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public bool DeleteList(string NodeIdlist )
		{
			return dal.DeleteList(NodeIdlist );
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public BookShop.Model.SysFun GetModel(int NodeId)
		{
			
			return dal.GetModel(NodeId);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ�����
		/// </summary>
		public BookShop.Model.SysFun GetModelByCache(int NodeId)
		{
			
			string CacheKey = "SysFunModel-" + NodeId;
			object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(NodeId);
					if (objModel != null)
					{
						int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
						Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (BookShop.Model.SysFun)objModel;
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
		public List<BookShop.Model.SysFun> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<BookShop.Model.SysFun> DataTableToList(DataTable dt)
		{
			List<BookShop.Model.SysFun> modelList = new List<BookShop.Model.SysFun>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				BookShop.Model.SysFun model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new BookShop.Model.SysFun();
					if(dt.Rows[n]["NodeId"].ToString()!="")
					{
						model.NodeId=int.Parse(dt.Rows[n]["NodeId"].ToString());
					}
					model.DisplayName=dt.Rows[n]["DisplayName"].ToString();
					model.NodeURL=dt.Rows[n]["NodeURL"].ToString();
					if(dt.Rows[n]["DisplayOrder"].ToString()!="")
					{
						model.DisplayOrder=int.Parse(dt.Rows[n]["DisplayOrder"].ToString());
					}
					if(dt.Rows[n]["ParentNodeId"].ToString()!="")
					{
						model.ParentNodeId=int.Parse(dt.Rows[n]["ParentNodeId"].ToString());
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
		/// ��ҳ��ȡ�����б�
		/// </summary>
		//public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return dal.GetList(PageSize,PageIndex,strWhere);
		//}

		#endregion  Method
	}
}

