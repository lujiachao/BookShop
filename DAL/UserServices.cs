using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//�����������
namespace BookShop.DAL
{
	/// <summary>
	/// ���ݷ�����UsersServices��
	/// </summary>
	public partial class UserServices
	{
        
        UserStateServices userStateServices = new UserStateServices();
		public UserServices()
		{}
		#region  ��Ա����

		/// <summary>
		/// �õ����ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("Id", "Users"); 
		}

		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(int Id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Users");
			strSql.Append(" where Id=@Id ");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)};
			parameters[0].Value = Id;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// ����һ������
		/// </summary>
		public int Add(BookShop.Model.User model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Users(");
			strSql.Append("LoginId,LoginPwd,Name,Address,Phone,Mail,UserStateId)");
			strSql.Append(" values (");
			strSql.Append("@LoginId,@LoginPwd,@Name,@Address,@Phone,@Mail,@UserStateId)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@LoginId", SqlDbType.NVarChar,50),
					new SqlParameter("@LoginPwd", SqlDbType.NVarChar,50),
					new SqlParameter("@Name", SqlDbType.NVarChar,50),
					new SqlParameter("@Address", SqlDbType.NVarChar,200),
					new SqlParameter("@Phone", SqlDbType.NVarChar,100),
					new SqlParameter("@Mail", SqlDbType.NVarChar,100),
					new SqlParameter("@UserStateId", SqlDbType.Int,4)};
			parameters[0].Value = model.LoginId;
			parameters[1].Value = model.LoginPwd;
			parameters[2].Value = model.Name;
			parameters[3].Value = model.Address;
			parameters[4].Value = model.Phone;
			parameters[5].Value = model.Mail;
			parameters[6].Value = model.UserState.Id;
        

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
		public void Update(BookShop.Model.User model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Users set ");
			strSql.Append("LoginId=@LoginId,");
			strSql.Append("LoginPwd=@LoginPwd,");
			strSql.Append("Name=@Name,");
			strSql.Append("Address=@Address,");
			strSql.Append("Phone=@Phone,");
			strSql.Append("Mail=@Mail,");
			strSql.Append("UserStateId=@UserStateId");
			strSql.Append(" where Id=@Id ");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4),
					new SqlParameter("@LoginId", SqlDbType.NVarChar,50),
					new SqlParameter("@LoginPwd", SqlDbType.NVarChar,50),
					new SqlParameter("@Name", SqlDbType.NVarChar,50),
					new SqlParameter("@Address", SqlDbType.NVarChar,200),
					new SqlParameter("@Phone", SqlDbType.NVarChar,100),
					new SqlParameter("@Mail", SqlDbType.NVarChar,100),
					new SqlParameter("@UserStateId", SqlDbType.Int,4)};
			parameters[0].Value = model.Id;
			parameters[1].Value = model.LoginId;
			parameters[2].Value = model.LoginPwd;
			parameters[3].Value = model.Name;
			parameters[4].Value = model.Address;
			parameters[5].Value = model.Phone;
			parameters[6].Value = model.Mail;
			parameters[7].Value = model.UserState.Id;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(int Id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Users ");
			strSql.Append(" where Id=@Id ");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)};
			parameters[0].Value = Id;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}


		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public BookShop.Model.User GetModel(int Id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 Id,LoginId,LoginPwd,Name,Address,Phone,Mail,UserStateId from Users ");
			strSql.Append(" where Id=@Id ");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)};
			parameters[0].Value = Id;

			BookShop.Model.User model=new BookShop.Model.User();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["Id"].ToString()!="")
				{
					model.Id=int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
				}
				model.LoginId=ds.Tables[0].Rows[0]["LoginId"].ToString();
				model.LoginPwd=ds.Tables[0].Rows[0]["LoginPwd"].ToString();
				model.Name=ds.Tables[0].Rows[0]["Name"].ToString();
				model.Address=ds.Tables[0].Rows[0]["Address"].ToString();
				model.Phone=ds.Tables[0].Rows[0]["Phone"].ToString();
				model.Mail=ds.Tables[0].Rows[0]["Mail"].ToString();
				
				if(ds.Tables[0].Rows[0]["UserStateId"].ToString()!="")
				{
					int UserStateId=int.Parse(ds.Tables[0].Rows[0]["UserStateId"].ToString());
                    model.UserState = userStateServices.GetModel(UserStateId);
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
			strSql.Append("select Id,LoginId,LoginPwd,Name,Address,Phone,Mail,UserStateId ");
			strSql.Append(" FROM Users ");
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
			strSql.Append(" Id,LoginId,LoginPwd,Name,Address,Phone,Mail,UserRoleId,UserStateId ");
			strSql.Append(" FROM Users ");
			if(strWhere!=null && strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/*
		/// <summary>
		/// ��ҳ��ȡ�����б�
		/// </summary>
		public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@tblName", SqlDbType.VarChar, 255),
					new SqlParameter("@fldName", SqlDbType.VarChar, 255),
					new SqlParameter("@PageSize", SqlDbType.Int),
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@IsReCount", SqlDbType.Bit),
					new SqlParameter("@OrderType", SqlDbType.Bit),
					new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
					};
			parameters[0].Value = "Users";
			parameters[1].Value = "ID";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  ��Ա����
	}
}

