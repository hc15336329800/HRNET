using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVC2020.Excal.Common;
using MVC2020.Model;

namespace MVC2020.Excel.Server
{
    public class ProTypeManager : ModelBaseManager<ProType>
    {
        /// <summary>
        /// 查找所有List<NodeCatalog>
        /// </summary>
        /// <returns></returns>
        public Response FinAll()
        {

            Response re = new Response();
            IQueryable<ProType> nn = base.Repository.FindList();
            if (nn != null)
            {
                re.Code = 1;
                re.Data = nn;
            }

            return re;//nn.ToList();

        }
    }
}
