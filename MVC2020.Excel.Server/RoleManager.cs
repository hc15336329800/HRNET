using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVC2020.Excal.Common;
using MVC2020.Model;

namespace MVC2020.Excel.Server
{
    public class RoleManager : ModelBaseManager<Role>
    {
        /// <summary>
        /// 帐号是否存在
        /// </summary>
        /// <param name="accounts">帐号</param>
        /// <returns></returns>
        public bool HasAccounts(string accounts)
        {
            return base.Repository.IsContains(a => a.Name.ToUpper() == accounts.ToUpper());
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
                    base.Repository.Delete(new Role() { RoleID = i }, false);
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
        public Paging<Role> FindPageList(int pageSize, int pageNumber, out int retNum)
        {
            //orderParam
            OrderParam orderParam = new OrderParam();
            orderParam.PropertyName = "RoleID";
            orderParam.Method = OrderMethod.ASC;

           
            Paging<Role> pagingUser = new Paging<Role>();

            pagingUser.Items = Repository.FindPageList(pageSize, pageNumber, out retNum, orderParam).ToList();
            pagingUser.TotalNumber = retNum;//传递总数量
            return pagingUser;
        }

        

    }
}
