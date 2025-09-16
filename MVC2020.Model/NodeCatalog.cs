using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC2020.Model
{
    //数据库实体层
    public class NodeCatalog
    {
        public NodeCatalog() { }
        public NodeCatalog(int id1, string str, string strurl, int pid,int cd)//int pid,
        {
            id = id1;
            //  PrentId = pid;
            name = str;
            href = strurl;
            parentId = pid;
            cid = cd;

        }

        /// <summary>
        /// 父节点id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 节点名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 节点链接
        /// </summary>
        public string href { get; set; }
        /// <summary>
        /// 子节点id
        /// </summary>
        public int?  parentId { get; set; }

        public int cid { get; set; }
    }
}
