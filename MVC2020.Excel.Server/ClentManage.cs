using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVC2020.Excal.Common;
using MVC2020.Model;

namespace MVC2020.Excel.Server
{
    public class ClentManage : ModelBaseManager<Clent>
    {
        /// <summary>
        /// 添加【返回值Response.Code:0-失败，1-成功，2-账号已存在，3-Email已存在】
        /// </summary>
        /// <param name="user">用户</param>
        /// <returns></returns>
        public override Response Add(Clent user)
        {
            Response _resp = new Response();
            //账号是否存在


            if (_resp.Code == 0) _resp = base.Add(user);
            return _resp;
        }

        ///// <summary>
        ///// 查找所有
        ///// </summary>
        ///// <returns></returns>
        //public Response FinAll()
        //{

        //    Response re = new Response();
        //    IQueryable<Clent> nn = base.Repository.FindList(X=>X.IsAccect==0);
        //    if (nn != null)
        //    {
        //        re.Code = 1;
        //        re.Data = nn;
        //    }

        //    return re;

        //}

        /// <summary>
        /// 查找所有
        /// </summary>
        /// <returns></returns>
        public Response FinID(int id)
        {

            Response re = new Response();
            Clent nn = base.FindNCount(id);
            if (nn != null)
            {
                re.Code = 1;
                re.Data = nn;
            }
            return re;

        }

        /// <summary>
        /// 分页列表 - 默认
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageNumber">当前页</param>
        /// <returns>页参数集合 Paging<Role> </returns>
        public Paging<Clent> FindPageList(int pageSize, int pageNumber, out int retNum)
        {
            //orderParam
            OrderParam orderParam = new OrderParam();
            orderParam.PropertyName = "DTime";//NewsID
            orderParam.Method = OrderMethod.DESC;
            Paging<Clent> pagingUser = new Paging<Clent>();
            //定制  订单
            pagingUser.Items = Repository.FindPageList(pageSize, pageNumber, out retNum, orderParam,x=>x.IsAccect==0).ToList();
            pagingUser.TotalNumber = retNum;//传递总数量
            return pagingUser;
        }
    }
}
