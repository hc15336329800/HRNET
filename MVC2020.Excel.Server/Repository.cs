using MVC2020.Excal.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace MVC2020.Excel.Server
{
    public class Repository<T> where T : class
    {
        //数据库连接字符串
        public DbContext DbContext { get; set; }

        public Repository() { }

        public Repository(DbContext dbcontext) { DbContext = dbcontext; }

        //////////////////////////////////

        #region 【查】


        //、、、、、、、、、、、、、、、、、、、、、、一条数据 DbContext.Set<T>()、、、、、、、、、、、、、、、、、、、、、、、、、、、、、、、、


     

        // <T> 查找Find (ID)
        /// <summary>
        /// (T) 查找Find (ID) Find 只能根据主键或联合主键进行查询
        /// </summary>
        /// <param name="ID">ID</param>
        /// <returns>T</returns>
        public T Find(int ID) 
        {
            //Find 只能根据主键或联合主键进行查询
            return DbContext.Set<T>().Find(ID); //.AsNoTracking().FirstOrDefault(p => ID);//
          
        }


        //where 的参数 $a.Accounts == .Constant<MVC2020.Excel.Server.AdminManager+<>c__DisplayClasse>(MVC2020.Excel.Server.AdminManager+<>c__DisplayClasse).accounts
        //报错！！
        //原因：SingleOrDefault函数如果有多个数据满足就会报错 ！！@！
        /// <summary>
        /// (T) 查找Find (where)
        /// </summary>
        /// <param name="where">Lambda-Where</param>
        /// <returns>T</returns>
        public T Find(Expression<Func<T, bool>> where) 
        { return DbContext.Set<T>().SingleOrDefault(where); }


        //、、、、、、、、、、、、、、、、、、、、、、多条数据 DbContext.Set<T>()、、、、、、、、、、、、、、、、、、、、、、、、、、、、、、、、

        // IQueryable<T> 查找所有FindList ()
        /// <summary>
        /// IQueryable<T> 查找所有 ()
        /// </summary>
        /// <returns>IQueryable<T></returns>
        public IQueryable<T> FindList()
        {
             
            return DbContext.Set<T>();
        }


        // IQueryable<T> 查找所有FindList (Lambda)
        /// <summary>
        /// 查找实体列表
        /// </summary>
        /// <param name="where">查询Lambda表达式</param>
        /// <returns>IQueryable<T></returns>
        public IQueryable<T> FindList(Expression<Func<T, bool>> where)
        {
            return DbContext.Set<T>().Where(where);
        }

        // IQueryable<T> 查找所有FindList (Lambda，数量number)
        /// <summary>
        /// 查找实体列表
        /// </summary>
        /// <param name="where">查询Lambda表达式</param>
        /// <param name="number">获取的记录数量</param>
        /// <returns></returns>
        public IQueryable<T> FindList(Expression<Func<T, bool>> where, int number)
        {
            return DbContext.Set<T>().Where(where).Take(number);
        }

        /// <summary>
        /// 查找实体列表  动态拼接  实验
        /// </summary>
        /// <param name="where">查询Lambda表达式</param>
        /// <returns></returns>
        public IQueryable<T> FindListLam(Expression<Func<T, bool>> where)
        {
            Func<T, bool> lamada = where.Compile();
            return DbContext.Set<T>().Where(lamada).AsQueryable<T>(); ;
        }

        //、、、、、、、、、、、、、、、、、、、、、多条数据  参数排序  重载、、、、、、、、、、、、、、、、、、、

        // IQueryable<T> 查找所有FindList (Lambda，数量number)
        /// <summary>
        /// 查找实体列表
        /// </summary>
        /// <param name="where">查询Lambda表达式</param>
        /// <param name="orderParam">排序参数</param>
        /// <returns></returns>
        public IQueryable<T> FindList(Expression<Func<T, bool>> where, OrderParam orderParam)
        {
            return FindList(where, orderParam, 0);
        }

        // IQueryable<T> 查找所有FindList (Lambda，排序参数OrderParam，数量number)
        /// <summary>
        /// 查找实体列表
        /// </summary>
        /// <param name="where">查询Lambda表达式</param>
        /// <param name="orderParam">排序参数</param>
        /// <param name="number">获取的记录数量【0-不启用】</param>
        public IQueryable<T> FindList(Expression<Func<T, bool>> where, OrderParam orderParam, int number)
        {
            OrderParam[] _orderParams = null;
            if (orderParam != null) _orderParams = new OrderParam[] { orderParam };
            return FindList(where, _orderParams, number);
        }

        // IQueryable<T> 查找所有FindList (Lambda，排序参数多OrderParams，数量number)
        /// <summary>
        /// 查找实体列表
        /// </summary>
        /// <param name="where">查询Lambda表达式</param>
        /// <param name="orderParams">排序参数</param>
        /// <param name="number">获取的记录数量【0-不启用】</param>
        /// <returns></returns>
        public IQueryable<T> FindList(Expression<Func<T, bool>> where, OrderParam[] orderParams, int number)
        {
            var _list = DbContext.Set<T>().Where(where);
            var _orderParames = Expression.Parameter(typeof(T), "o");
            if (orderParams != null && orderParams.Length > 0)
            {
                for (int i = 0; i < orderParams.Length; i++)
                {
                    //根据属性名获取属性
                    var _property = typeof(T).GetProperty(orderParams[i].PropertyName);
                    //创建一个访问属性的表达式
                    var _propertyAccess = Expression.MakeMemberAccess(_orderParames, _property);
                    var _orderByExp = Expression.Lambda(_propertyAccess, _orderParames);
                    string _orderName = orderParams[i].Method == OrderMethod.ASC ? "OrderBy" : "OrderByDescending";
                    MethodCallExpression resultExp = Expression.Call(typeof(Queryable), _orderName, new Type[] { typeof(T), _property.PropertyType }, _list.Expression, Expression.Quote(_orderByExp));
                    _list = _list.Provider.CreateQuery<T>(resultExp);
                }
            }
            if (number > 0) _list = _list.Take(number);
            return _list;
        }



        //分页列表
        #region FindPageList

        /// <summary>
        /// 查找分页列表  --  默认
        /// </summary>
        /// <param name="pageSize">每页记录数。必须大于1</param>
        /// <param name="pageIndex">页码。首页从1开始，页码必须大于1</param>
        /// <param name="totalNumber">指针总记录数</param>
        /// <returns></returns>
        public IQueryable<T> FindPageList(int pageSize, int pageIndex, out int totalNumber)
        {
            OrderParam _orderParam = null;
         
            return FindPageList(pageSize, pageIndex, out totalNumber, (T) => true, _orderParam);
        }

        /// <summary>
        /// 查找分页列表
        /// </summary>
        /// <param name="pageSize">每页记录数。必须大于1</param>
        /// <param name="pageIndex">页码。首页从1开始，页码必须大于1</param>
        /// <param name="totalNumber">总记录数</param>
        /// <param name="order">排序键</param>
        /// <param name="asc">是否正序</param>
        /// <returns></returns>
        public IQueryable<T> FindPageList(int pageSize, int pageIndex, out int totalNumber, OrderParam orderParam)
        {
            return FindPageList(pageSize, pageIndex, out totalNumber, (T) => true, orderParam);
        }

        /// <summary>
        /// 查找分页列表 糖厂订单定制
        /// </summary>
        /// <param name="pageSize">每页记录数。必须大于1</param>
        /// <param name="pageIndex">页码。首页从1开始，页码必须大于1</param>
        /// <param name="totalNumber">总记录数</param>
        /// <param name="order">排序键</param>
        /// <param name="asc">是否正序</param>
        /// <returns></returns>
        public IQueryable<T> FindPageList(int pageSize, int pageIndex, out int totalNumber, OrderParam orderParam, Expression<Func<T, bool>> where)
        {
            return FindPageList(pageSize, pageIndex, out totalNumber, where, orderParam);
        }

        /// <summary>
        /// 查找分页列表
        /// </summary>
        /// <param name="pageSize">每页记录数。必须大于1</param>
        /// <param name="pageIndex">页码。首页从1开始，页码必须大于1</param>
        /// <param name="totalNumber">总记录数</param>
        /// <param name="where">查询表达式</param>
        public IQueryable<T> FindPageList(int pageSize, int pageIndex, out int totalNumber, Expression<Func<T, bool>> where)
        {
            OrderParam _param = null;
            return FindPageList(pageSize, pageIndex, out totalNumber, where, _param);
        }

        /// <summary>
        /// 查找分页列表
        /// </summary>
        /// <param name="pageSize">每页记录数。</param>
        /// <param name="pageIndex">页码。首页从1开始</param>
        /// <param name="totalNumber">总记录数</param>
        /// <param name="where">查询表达式</param>
        /// <param name="orderParam">排序【null-不设置】</param>
        /// <returns></returns>
        public IQueryable<T> FindPageList(int pageSize, int pageIndex, out int totalNumber, Expression<Func<T, bool>> where, OrderParam orderParam)
        {
            OrderParam[] _orderParams = null;
            if (orderParam != null) _orderParams = new OrderParam[] { orderParam };
            return FindPageList(pageSize, pageIndex, out totalNumber, where, _orderParams);
        }

        /// <summary>
        /// 查找分页列表（先查询所有  再筛选   效率低）
        /// </summary>
        /// <param name="pageSize">每页记录数。</param>
        /// <param name="pageIndex">页码。首页从1开始</param>
        /// <param name="totalNumber">总记录数</param>
        /// <param name="where">查询表达式</param>
        /// <param name="orderParams">排序【null-不设置】</param>
        public IQueryable<T> FindPageList(int pageSize, int pageIndex, out int totalNumber, Expression<Func<T, bool>> where, OrderParam[] orderParams)
        {
            if (pageIndex < 1) pageIndex = 1;
            if (pageSize < 1) pageSize = 5;
            IQueryable<T> _list = DbContext.Set<T>().Where(where);
            var _orderParames = Expression.Parameter(typeof(T), "o");
            if (orderParams != null && orderParams.Length > 0)
            {
                for (int i = 0; i < orderParams.Length; i++)
                {
                    //根据属性名获取属性
                    var _property = typeof(T).GetProperty(orderParams[i].PropertyName);
                    //创建一个访问属性的表达式  动态反射
                    var _propertyAccess = Expression.MakeMemberAccess(_orderParames, _property);
                    var _orderByExp = Expression.Lambda(_propertyAccess, _orderParames);
                    string _orderName = orderParams[i].Method == OrderMethod.ASC ? "OrderBy" : "OrderByDescending";
                    MethodCallExpression resultExp = Expression.Call(typeof(Queryable), _orderName, new Type[] { typeof(T), _property.PropertyType }, _list.Expression, Expression.Quote(_orderByExp));
                    //构造一个 IQueryable<T> 对象，该对象可计算指定表达式树所表示的查询。
                    _list = _list.Provider.CreateQuery<T>(resultExp);
                }
            }
            totalNumber = _list.Count();
            //好像有问题   return _list.Skip((pageSize - 1) * pageSize).Take(pageSize);
            return _list.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }

        #endregion

        #endregion

        #region 【增】

        /////// 一种方法                                               //////
        /////// DbContext.Set<T>().Add(entity);                        //////

        ////// 另一种方法                                               /////
        ////// DbContext.Set<T>().Attach(entity);                       //////
        ////// DbContext.Entry<T>(entity).State = EntityState.Added;    //////

        //////  return isSave ? DbContext.SaveChanges() : 0;            //////




        /// <summary>
        /// 添加实体-立即保存
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>受影响的对象的数目</returns>
        public int Add(T entity)
        {
            return Add(entity, true);
        }

        /// <summary>
        /// 添加实体-选择是否立即保存
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="isSave">是否立即保存</param>
        /// <returns>在“isSave”为True时返回受影响的对象的数目，为False时直接返回0</returns>
        public int Add(T entity, bool isSave)
        {
            //一种方法
            // DbContext.Set<T>().Add(entity);
            //另一种方法
            try
            {
                DbContext.Set<T>().Attach(entity);
                DbContext.Entry<T>(entity).State = EntityState.Added;

                return isSave ? DbContext.SaveChanges() : 0;
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        //输出日志 
                        //捕捉模型验证出错提示信息！
                    }
                }
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }

            //try
            //{
            //    DbContext.Set<T>().Attach(entity);
            //    DbContext.Entry<T>(entity).State = EntityState.Added;
 
            //    return isSave ? DbContext.SaveChanges() : 0;
            //}
            //catch (DbEntityValidationException dbEx)  //DbEntityValidationException 查看模型出错信息 
            //{
                
            //    throw;
            //}
          
        }

        #endregion

        #region 【改】

        /// <summary>
        /// 更新实体-立即保存
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>在“isSave”为True时返回受影响的对象的数目，为False时直接返回0</returns>
        public int Update(T entity)
        {
            return Update(entity, true);
        }

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="isSave">是否立即保存</param>
        /// <returns>在“isSave”为True时返回受影响的对象的数目，为False时直接返回0</returns>
        public int Update(T entity, bool isSave)
        {
            DbContext.Set<T>().Attach(entity);
            DbContext.Entry<T>(entity).State = EntityState.Modified;
            return isSave ? DbContext.SaveChanges() : 0;
        }

        #endregion

        #region 【删】

        #region 删除实体-单-2重载
        /// <summary>
        /// 删除实体【立即保存】
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>受影响的对象的数目</returns>
        public int Delete(T entity)
        {
            return Delete(entity, true);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="isSave">是否立即保存</param>
        /// <returns>在“isSave”为True时返回受影响的对象的数目，为False时直接返回0</returns>
        public int Delete(T entity, bool isSave)
        {

            //数据库报错  相同主键
            DbContext.Set<T>().Attach(entity);
            DbContext.Entry<T>(entity).State = EntityState.Deleted;
            return isSave ? DbContext.SaveChanges() : 0;

            //DbContext.Set<T>().Remove(entity);
            //return isSave ? DbContext.SaveChanges() : 0;
        }

        #endregion

        #region 删除实体-多-1重载


        /// <summary>
        /// 批量删除实体
        /// </summary>
        /// <param name="entities">实体集合</param>
        /// <returns>受影响的对象的数目</returns>
        public int Delete(IEnumerable<T> entities)
        {
            DbContext.Set<T>().RemoveRange(entities);
            return DbContext.SaveChanges();
        }

        #endregion



        #endregion

        /////////////////////////////////////


        #region 统计数量2（实体中记录数）

        /// <summary>
        /// 记录数
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            return DbContext.Set<T>().Count();
        }


        /// <summary>
        /// 记录数  -拉姆表达式
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <returns></returns>
        public int Count(Expression<Func<T, bool>> predicate)
        {
            return DbContext.Set<T>().Count(predicate);
        }

        #endregion

        #region 是否存在

        /// <summary>
        /// 记录是否存在-拉姆表达式
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <returns></returns>
        public bool IsContains(Expression<Func<T, bool>> predicate)
        {
            return Count(predicate) > 0;
        }

        #endregion

        #region 保存到数据库

        /// <summary>
        /// 保存数据【在Add、Upate、Delete未立即保存的情况下使用】
        /// </summary>
        /// <returns>受影响的记录数</returns>
        public int Save()
        {
            return DbContext.SaveChanges();
        }


        #endregion




        ///////////////////////////////////




    }
}
