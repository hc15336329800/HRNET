using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVC2020.Excal.Common;
using MVC2020.Model;

namespace MVC2020.Excel.Server
{


    //准备写产品 模型类

    public class ProductManager :ModelBaseManager<Product>
    {
        //查找
        /// <summary>
        /// 查找所有
        /// </summary>
        /// <returns></returns>
        public Response FinID(int id)
        {

            Response re = new Response();
            Product nn = base.FindNCount(id);
            if (nn != null)
            {
                re.Code = 1;
                re.Data = nn;
            }
            return re;

        }

   //查找
        /// <summary>
        /// 查找所有
        /// </summary>
        /// <returns></returns>
        public Response FineWhere(int NCount)
        {

            Response re = new Response();
            Product nn = base.FindWhere(x => x.NCount== NCount);
            if (nn != null)
            {
                re.Code = 1;
                re.Data = nn;
            }
            return re;

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
                    base.Repository.Delete(new Product() { ProductID = i }, false);
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
                _resp.Message = "成功删除" + _resp.Data + "条数据";
            }
            else if (_resp.Data > 0)
            {
                _resp.Code = 2;
                _resp.Message = "成功删除" + _resp.Data + "条数据";
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
        public Paging<Product> FindPageList(int pageSize, int pageNumber, out int retNum)
        {
            //orderParam
            OrderParam orderParam = new OrderParam();
            orderParam.PropertyName = "ProductID";
            orderParam.Method = OrderMethod.ASC;


            Paging<Product> pagingUser = new Paging<Product>();

            pagingUser.Items = Repository.FindPageList(pageSize, pageNumber, out retNum, orderParam).ToList();
            pagingUser.TotalNumber = retNum;//传递总数量
            return pagingUser;
        }

    }
}
