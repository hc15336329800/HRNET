using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MVC2020.Excal.Common;
using MVC2020.Excel.DataContext;  //Expression - link引用

namespace MVC2020.Excel.Server
{
    public abstract class ModelBaseManager<T> where T : class
    {
        /// <summary>
        /// 数据仓储类
        /// </summary>
        public Repository<T> Repository;

        /// <summary>
        /// 默认构造函数==  程序内赋值调用  有参参数
        /// </summary>
        public ModelBaseManager()
            : this(CurrentContext.GetCurrentContext())
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbContext">数据上下文</param>
        public ModelBaseManager(DbContext dbContext)
        {
            Repository = new Repository<T>(dbContext);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        #region 增 删 该


        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">实体数据</param>
        /// <returns>成功时属性【Data】为添加后的数据实体</returns>
        public virtual Response Add(T entity)
        {
            Response _response = new Response();
            if (Repository.Add(entity) > 0)
            {
                _response.Code = 1;
                _response.Message = "添加数据成功！";
                _response.Data = entity;
            }
            else
            {
                _response.Code = 0;
                _response.Message = "添加数据失败！";
            }

            return _response;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID">主键</param>
        /// <returns>Code：0-删除失败；1-删除陈功；10-记录不存在</returns>
        public virtual Response Delete(int ID)
        {
            Response _response = new Response();
            var _entity = FindNCount(ID);
            if (_entity == null)
            {
                _response.Code = 10;
                _response.Message = "记录不存在！";
            }
            else
            {
                if (Repository.Delete(_entity) > 0)
                {
                    _response.Code = 1;
                    _response.Message = "删除数据成功！";
                }
                else
                {
                    _response.Code = 0;
                    _response.Message = "删除数据失败！";
                }
            }


            return _response;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">实体数据</param>
        /// <returns>成功时属性【Data】为更新后的数据实体</returns>
        public virtual Response Update(T entity)
        {
            Response _response = new Response();
            if (Repository.Update(entity) > 0)
            {
                _response.Code = 1;
                _response.Message = "更新数据成功！";
                _response.Data = entity;
            }
            else
            {
                _response.Code = 0;
                _response.Message = "更新数据失败！";
            }

            return _response;
        }


        #endregion



        #region 条件查找


        /// <summary>
        /// 查找：单wherre
        /// </summary>
        /// <param name="ID">主键</param>
        /// <returns>实体</returns>
        public virtual T FindWhere(Expression<Func<T, bool>> where)
        {
            return Repository.Find(where);
        }


        /// <summary>
        /// 查找：单
        /// </summary>
        /// <param name="ID">主键</param>
        /// <returns>实体</returns>
        public virtual T FindNCount(int ID)
        {
            return Repository.Find(ID);
        }

        /// <summary>
        /// 查找：  所有
        /// </summary>
        /// <returns>所有数据</returns>
        public virtual Response FindList()
        {
            Response _response = new Response();

            List<T> vv = Repository.FindList().ToList();//注意
            if (vv == null || vv.Count() == 0)
            {
                _response.Code = 0;
                _response.Message = "添加数据失败！";
            }
            else
            {
                _response.Code = 1;
                _response.Message = "添加数据成功！";
                _response.Data = vv;

            }

            return _response;

        }

        /// <summary>
        /// 查找： 筛选（ 条件-排序-数量）
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="orderParam">排序名与顺序</param>
        /// <param name="number">数量</param>
        /// <returns></returns>
        public virtual IQueryable<T> FindList(Expression<Func<T, bool>> where, OrderParam orderParam, int number)
        {
            return Repository.FindList(where, orderParam, number);
        }


        /// <summary>
        /// 查找：分页数据类
        /// </summary>
        /// <param name="paging">分页数据</param>
        /// <returns>分页数据</returns>
        public virtual Paging<T> FindPageList(Paging<T> paging)
        {
            paging.Items = Repository.FindPageList(paging.PageSize, paging.PageIndex, out paging.TotalNumber).ToList();
            return paging;
        }

        /// <summary>
        /// 查找实体列表  动态拼接  实验
        /// </summary>
        /// <param name="where">查询Lambda表达式</param>
        /// <returns></returns>
        public IQueryable<T> FindListLam(Expression<Func<T, bool>> where)
        {
            //Func<T, bool> lamada = where.Compile();

            return Repository.FindListLam(where);
        }

        #endregion


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 统计
        /// </summary>
        /// <returns></returns>
        public virtual int Count()
        {
            return Repository.Count();
        }

        ///// <summary>
        ///// 下一页
        ///// </summary>
        ///// <returns></returns>
        //public IQueryable<T> FindNext()
        //{
        //    return Repository.FindNext(3);
        //}




    }
}
