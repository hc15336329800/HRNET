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
    
    public class AdminManager : ModelBaseManager<Admin>
    {

        /// <summary>
        /// 全部管理员 top10
        /// </summary>
        /// <returns>所有数据</returns>
        public override Response FindList()
        {
            OrderParam orderParam = null;
            Response _resp = new Response();
            var vv = base.FindList(a => true, orderParam, 2);
            if (vv != null)
            {
                _resp.Code = 1;
                _resp.Message = "成功";
                _resp.Data = vv;
            }
            else
            {
                _resp.Code = 0;
                _resp.Message = "失败";
                _resp.Data = null;
            }
            return _resp;
        }


        /// <summary>
        /// 分页列表 - 默认
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageNumber">当前页</param>
        /// <returns>页参数集合 Paging<Role> </returns>
        public Paging<Admin> FindPageList(int pageSize, int pageNumber, out int retNum)
        {
            //orderParam
            OrderParam orderParam = new OrderParam();
            orderParam.PropertyName = "AdministratorID";
            orderParam.Method = OrderMethod.ASC;

            Paging<Admin> pagingUser = new Paging<Admin>();

            pagingUser.Items = Repository.FindPageList(pageSize, pageNumber, out retNum, orderParam).ToList();
            pagingUser.TotalNumber = retNum;//传递总数量
            return pagingUser;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="admin">管理员实体</param>
        /// <returns></returns>
        public override Response Add(Admin admin)
        {
            Response _resp = new Response();
            if (HasAccounts(admin.Accounts))
            {
                _resp.Code = 0;
                _resp.Message = "帐号已存在";
            }
            else _resp = base.Add(admin);

            return _resp;
        }

        ///// <summary>
        /// 删除---重写
        /// </summary>
        /// <param name="administratorID">主键</param>
        /// <returns></returns>
        public override Response Delete(int administratorID)
        {

            Response _resp = new Response();
            if (Count() == 1)
            {
                _resp.Code = 0;
                _resp.Message = "不能删除唯一的管理员帐号";
            }
            else _resp = base.Delete(administratorID);
            return _resp;
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
                    base.Repository.Delete(new Admin() { AdministratorID = i }, false);
                    _totalAdmin--;
                }
                else
                {
                    _resp.Message = "最少需保留1名管理员";
                    return _resp;
                }
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
        /// 帐号是否存在
        /// </summary>
        /// <param name="accounts">帐号</param>
        /// <returns></returns>
        public bool HasAccounts(string accounts)
        {
            return base.Repository.IsContains(a => a.Accounts.ToUpper() == accounts.ToUpper());
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

            var _admin = FindWhere(a => a.Accounts == accounts);

            //var _admin = base.Repository.Find(a => a.Accounts == accounts);
            if (_admin == null)
            {
                _resp.Code = 2;
                _resp.Message = "帐号为:【" + accounts + "】的管理员不存在";
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
        public Admin FindWhere(Expression<Func<Admin, bool>> where)
        {
            //备用方法  防止读取多条数据出来  原来的方法会报错
            //IQueryable<Admin> aa = base.Repository.FindList(where);
            //Admin ad = aa.First();
            //return ad;

            return base.Repository.Find(where);
        }

    }
}
