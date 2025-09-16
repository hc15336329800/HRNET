using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVC2020.Excal.Common;
using MVC2020.Model;

namespace MVC2020.Excel.Server
{
    /// <summary>
    /// 侧边栏 产品  目录树
    /// </summary>
    public class NodeCatalogManager : ModelBaseManager<NodeCatalog>
    {
        /// <summary>
        /// 查找所有List<NodeCatalog>
        /// </summary>
        /// <returns></returns>
        public Response FinAll()
        {

            Response re = new Response();
            IQueryable<NodeCatalog> nn = base.Repository.FindList();
            if (nn != null)
            {
                re.Code = 1;
                re.Data = nn;
            }

            return re;//nn.ToList();

        }
    }
}
