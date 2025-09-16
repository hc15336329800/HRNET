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
   public class ExcelManage : ModelBaseManager<HRYT>
    {
        /// <summary>
        /// ADD数据
        /// </summary>
        /// <param name="yt"></param>
        public void ImExcelIn(HRYT yt)
        {
            base.Add(yt);

            return;
        }


    
        /// 查找实体列表  动态拼接  实验
        /// </summary>
        /// <param name="where">查询Lambda表达式</param>
        /// <returns></returns>
        public IQueryable<HRYT> FindListLam(Expression<Func<HRYT, bool>> where)
        {
            //Func<HRYT, bool> lamada = where.Compile();

            return base.FindListLam(where).AsQueryable<HRYT>(); ;
        }
        
 


    }
}
