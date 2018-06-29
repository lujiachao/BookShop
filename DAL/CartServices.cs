using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//�����������
namespace BookShop.DAL
{
	/// <summary>
	/// ���ݷ�����CartServices��
	/// </summary>
	public class CartServices
	{
        UserServices userServices = new UserServices();
        BookServices bookServices = new BookServices();
		public CartServices()
		{}
		#region  ��Ա����

		/// <summary>
		/// �õ����ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("Id", "Cart"); 
		}

		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(int Id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Cart");
			strSql.Append(" where Id=@Id ");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)};
			parameters[0].Value = Id;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// ����һ������
		/// </summary>
		public int Add(BookShop.Model.Cart model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Cart(");
			strSql.Append("UserId,BookId,Count )");
			strSql.Append(" values (");
			strSql.Append("@UserId,@BookId,@Count)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@BookId", SqlDbType.Int,4),
					new SqlParameter("@Count", SqlDbType.Int,4)}
					;
			parameters[0].Value = model.User.Id;
			parameters[1].Value = model.Book.Id;
			parameters[2].Value = model.Count;
		

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
			if (obj == null)
			{
				return 1;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
		}
		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(BookShop.Model.Cart model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Cart set ");
			strSql.Append("UserId=@UserId,");
			strSql.Append("BookId=@BookId,");
			strSql.Append("Count=@Count ");
		
			strSql.Append(" where Id=@Id ");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4),
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@BookId", SqlDbType.Int,4),
					new SqlParameter("@Count", SqlDbType.Int,4)};
			parameters[0].Value = model.Id;
            parameters[1].Value = model.User.Id;
			parameters[2].Value = model.Book.Id;
			parameters[3].Value = model.Count;
		

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(int Id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Cart ");
			strSql.Append(" where Id=@Id ");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)};
			parameters[0].Value = Id;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}


		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public BookShop.Model.Cart GetModel(int Id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 Id,UserId,BookId,Count from Cart ");
			strSql.Append(" where Id=@Id ");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)};
			parameters[0].Value = Id;

			BookShop.Model.Cart model=new BookShop.Model.Cart();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["Id"].ToString()!="")
				{
					model.Id=int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
				}
				if(ds.Tables[0].Rows[0]["UserId"].ToString()!="")
				{
					int UserId=int.Parse(ds.Tables[0].Rows[0]["UserId"].ToString());
                    model.User = userServices.GetModel(UserId);
				}
				if(ds.Tables[0].Rows[0]["BookId"].ToString()!="")
				{
					int BookId=int.Parse(ds.Tables[0].Rows[0]["BookId"].ToString());
                    model.Book = bookServices.GetModel(BookId);
				}
				if(ds.Tables[0].Rows[0]["Count"].ToString()!="")
				{
					model.Count=int.Parse(ds.Tables[0].Rows[0]["Count"].ToString());
				}
	
				return model;
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// ��������б�
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select Id,UserId,BookId,Count ");
			strSql.Append(" FROM Cart ");
			if(strWhere!=null && strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// ���ǰ��������
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" Id,UserId,BookId,Count ");
			strSql.Append(" FROM Cart ");
			if(strWhere!=null && strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

	//------------------------------
        /// <summary>
        /// ��ѯһ�µ�ǰ�û��Ƿ��Ѿ�ѡ���˸���Ʒ��
        /// </summary>
        /// <param name="bookId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Model.Cart GetCartModel(int bookId, int userId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Id,UserId,BookId,Count from Cart ");
            strSql.Append(" where BookId=@BookId and UserId=@UserId ");
            SqlParameter[] parameters = {
					new SqlParameter("@BookId", SqlDbType.Int,4),
                                        
                                        new SqlParameter("@UserId", SqlDbType.Int,4)
                                        };
            parameters[0].Value = bookId;
            parameters[1].Value = userId;

            BookShop.Model.Cart model = new BookShop.Model.Cart();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UserId"].ToString() != "")
                {
                    int UserId = int.Parse(ds.Tables[0].Rows[0]["UserId"].ToString());
                    model.User = userServices.GetModel(UserId);
                }
                if (ds.Tables[0].Rows[0]["BookId"].ToString() != "")
                {
                    int BookId = int.Parse(ds.Tables[0].Rows[0]["BookId"].ToString());
                    model.Book = bookServices.GetModel(BookId);
                }
                if (ds.Tables[0].Rows[0]["Count"].ToString() != "")
                {
                    model.Count = int.Parse(ds.Tables[0].Rows[0]["Count"].ToString());
                }

                return model;
            }
            else
            {
                return null;
            }
        }

		#endregion  ��Ա����
	}
}

