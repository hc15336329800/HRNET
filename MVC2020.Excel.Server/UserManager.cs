using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MVC2020.Excal.Common;
using MVC2020.Model;

namespace MVC2020.Excel.Server
{
    /// <summary>
    /// 用户管理类-  扩展
    /// </summary>
    /// 

   
    public class UserManager : ModelBaseManager<User>
    {
        //查找实体根据id
        public User FindId(int id)
        {
            return base.FindNCount(id);
        }

        /// <summary>
        /// 添加【返回值Response.Code:0-失败，1-成功，2-账号已存在，3-Email已存在】
        /// </summary>
        /// <param name="user">用户</param>
        /// <returns></returns>
        public override Response Add(User user)
        {
            Response _resp = new Response();
            //账号是否存在
            if (!string.IsNullOrEmpty(user.Username) && HasUsername(user.Username))
            {
                _resp.Code = 2;
                _resp.Message = "用户名已存在";
            }
            //Email是否存在
            if (!string.IsNullOrEmpty(user.Email) && HasUsername(user.Email))
            {
                _resp.Code = 3;
                _resp.Message = "Email已存在";
            }
            if (_resp.Code == 0) _resp = base.Add(user);
            return _resp;
        }

        /// <summary>
        /// 用户名是否存在
        /// </summary>
        /// <param name="accounts">用户名[不区分大小写]</param>
        /// <returns></returns>
        public bool HasUsername(string username)
        {
            return base.Repository.IsContains(u => u.Username.ToUpper() == username.ToUpper());
        }

        /// <summary>
        /// Email是否存在
        /// </summary>
        /// <param name="email">Email[不区分大小写]</param>
        /// <returns></returns>
        public bool HasEmail(string email)
        {
            return base.Repository.IsContains(u => u.Email.ToUpper() == email.ToUpper());
        }

        /// <summary>
        /// 删除【批量】返回值Code：1-成功，2-部分删除，0-失败
        /// </summary>
        /// <param name="administratorIDList"></param>
        /// <returns></returns>
        public Response Delete(List<int> RoleIDList)
        {
            Response _resp = new Response();
            int _totalDel = RoleIDList.Count;  //需要删除的数量
            int _totalAdmin = Count();   //总量

            foreach (int i in RoleIDList)
            {
                if (_totalAdmin > 1)
                {
                    base.Repository.Delete(new User() { UserID = i }, false);
                    _totalAdmin--;
                }
                else _resp.Message = "最少需保留1名管理员";
            }
            ///返回执行成功的条数
            _resp.Data = base.Repository.Save();//批量可以拼接  只执行一次数据库！！高！！

            ///总共执行几条  ，实际成功几条！
            if (_resp.Data == _totalDel)
            {
                _resp.Code = 1;
                _resp.Message = "成功删除" + _resp.Data + "名管理员";
            }
            else if (_resp.Data > 0)
            {
                _resp.Code = 2;
                _resp.Message = "成功删除" + _resp.Data + "名管理员";
            }
            else
            {
                _resp.Code = 0;
                _resp.Message = "删除失败";
            }
            return _resp;
        }



        /// <summary>
        /// 分页列表 - 默认
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageNumber">当前页</param>
        /// <returns>页参数集合 Paging<Role> </returns>
        public Paging<User> FindPageList(int pageSize, int pageNumber, out int retNum)
        {
            //orderParam
            OrderParam orderParam = new OrderParam();
            orderParam.PropertyName = "RoleID";
            orderParam.Method = OrderMethod.ASC;


            Paging<User> pagingUser = new Paging<User>();

            pagingUser.Items = Repository.FindPageList(pageSize, pageNumber, out retNum, orderParam).ToList();
            pagingUser.TotalNumber = retNum;//传递总数量
            return pagingUser;
        }


        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="accounts">帐号</param>
        /// <param name="password">密码【密文】</param>
        /// <returns>Code:1-成功;2-帐号不存在;3-密码错误</returns>
        public Response Verify(string accounts, string password)
        {
            Response _resp = new Response();

            //Expression<Func<Admin, bool>> return5 = a => a.Accounts == accounts;   表达式树  数据结构

            var _admin = FindWhere(a => a.Username == accounts);

            //var _admin = base.Repository.Find(a => a.Accounts == accounts);
            if (_admin == null)
            {
                _resp.Code = 2;
                _resp.Message = "帐号为:【" + accounts + "】的用户不存在";
            }
            else if (_admin.Password == password)
            {
                _resp.Code = 1;
                _resp.Message = "验证通过";
            }
            else
            {
                _resp.Code = 3;
                _resp.Message = "帐号密码错误";
            }
            return _resp;
        }


        /// <summary>
        /// 返回筛选结果
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public User FindWhere(Expression<Func<User, bool>> where)
        {
            //备用方法  防止读取多条数据出来  原来的方法会报错
            //IQueryable<Admin> aa = base.Repository.FindList(where);
            //Admin ad = aa.First();
            //return ad;

            return base.Repository.Find(where);
        }


        /// <summary>
        /// 分页列表  --老方法
        /// </summary>
        /// <param name="pagingUser">分页数据</param>
        /// <param name="roleID">角色ID</param>
        /// <param name="username">用户名</param>
        /// <param name="name">名称</param>
        /// <param name="sex">性别</param>
        /// <param name="email">Email</param>
        /// <param name="order">排序【null（默认）-ID降序，0-ID升序，1-ID降序，2-注册时间降序，3-注册时间升序，4-最后登录时间升序，5-最后登录时间降序】</param>
        /// <returns></returns>
        //public Paging<User> FindPageList(Paging<User> pagingUser, int? roleID, string username, string name, int? sex, string email, int? order)
        //{
        //    //查询表达式
        //    var _where = PredicateBuilder.True<User>();
        //    if (roleID != null && roleID > 0) _where = _where.And(u => u.RoleID == roleID);
        //    if (!string.IsNullOrEmpty(username)) _where = _where.And(u => u.Username.Contains(username));
        //    if (!string.IsNullOrEmpty(name)) _where = _where.And(u => u.Name.Contains(name));
        //    if (sex != null && sex >= 0 && sex <= 2) _where = _where.And(u => u.Sex == sex);
        //    if (!string.IsNullOrEmpty(email)) _where = _where.And(u => u.Email.Contains(email));
        //    //排序
        //    OrderParam _orderParam;
        //    switch (order)
        //    {
        //        case 0://ID升序
        //            _orderParam = new OrderParam() { PropertyName = "UserID", Method = OrderMethod.ASC };
        //            break;
        //        case 1://ID降序
        //            _orderParam = new OrderParam() { PropertyName = "UserID", Method = OrderMethod.DESC };
        //            break;
        //        case 2://注册时间降序
        //            _orderParam = new OrderParam() { PropertyName = "RegTime", Method = OrderMethod.ASC };
        //            break;
        //        case 3://注册时间升序
        //            _orderParam = new OrderParam() { PropertyName = "RegTime", Method = OrderMethod.DESC };
        //            break;
        //        case 4://最后登录时间升序
        //            _orderParam = new OrderParam() { PropertyName = "LastLoginTime", Method = OrderMethod.ASC };
        //            break;
        //        case 5://最后登录时间降序
        //            _orderParam = new OrderParam() { PropertyName = "LastLoginTime", Method = OrderMethod.DESC };
        //            break;
        //        default://ID降序
        //            _orderParam = new OrderParam() { PropertyName = "UserID", Method = OrderMethod.DESC };
        //            break;
        //    }



        //    // Repository re - new Repository();
        //    //List<User> _list1 = Repository.FindPageList(pagingUser.PageSize,pagingUser.PageIndex,out  pagingUser.TotalNumber,_where.Expand(),_orderParam).ToList();
        //    pagingUser.Items = Repository.FindPageList(pagingUser.PageSize, pagingUser.PageIndex, out  pagingUser.TotalNumber, _where.Expand(), _orderParam).ToList();
        //    return pagingUser;
        //}

    }
}
