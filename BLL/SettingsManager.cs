﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BookShop.BLL
{
    public class SettingsManager
    {
        private readonly BookShop.DAL.SettingsService dal = new BookShop.DAL.SettingsService();
        public SettingsManager()
        { }
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            return dal.Exists(Id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(BookShop.Model.Settings model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(BookShop.Model.Settings model)
        {
            dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int Id)
        {

            dal.Delete(Id);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public BookShop.Model.Settings GetModel(int Id)
        {

            return dal.GetModel(Id);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中。
        /// </summary>
        public BookShop.Model.Settings GetModelByCache(int Id)
        {

            string CacheKey = "SettingsModel-" + Id;
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
                catch { }
            }
            return (BookShop.Model.Settings)objModel;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<BookShop.Model.Settings> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<BookShop.Model.Settings> DataTableToList(DataTable dt)
        {
            List<BookShop.Model.Settings> modelList = new List<BookShop.Model.Settings>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                BookShop.Model.Settings model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new BookShop.Model.Settings();
                    if (dt.Rows[n]["Id"].ToString() != "")
                    {
                        model.Id = int.Parse(dt.Rows[n]["Id"].ToString());
                    }
                    model.Name = dt.Rows[n]["Name"].ToString();
                    model.Value = dt.Rows[n]["Value"].ToString();
                    modelList.Add(model);
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }
        //-----------------------------------------
        /// <summary>
        /// 根据配置项的名称找对应配置信息
        /// </summary>
        /// <param name="name">配置项的名称(key)</param>
        /// <returns></returns>
        public Model.Settings GetModel(string name)
        {
            object obj=Common.CacheHelper.Get("setting_" + name);
            if (obj == null)
            {
               Model.Settings setting=dal.GetModel(name);
               Common.CacheHelper.Set("setting_"+ name, setting);
               return setting;
            }
            else
            {
              Model.Settings setting= obj as Model.Settings;
              return setting;

            }
        }
        /// <summary>
        /// 更新配置信息
        /// </summary>
        /// <param name="name"></param>
        public void UpdateModel(string name,string value)
        {
            Model.Settings setting = dal.GetModel(name);
            setting.Value = value;
            dal.Update(setting);
            Common.CacheHelper.Set("setting_" + name, value);
        }
       

        #endregion

    }
}

