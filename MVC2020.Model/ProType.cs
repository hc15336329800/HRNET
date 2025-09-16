using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC2020.Model
{

    /// <summary>
    /// 产品分类
    /// </summary>
    public class ProType
    {

        public ProType() { }
        public ProType(int id1, string str, string strurl, int pid)//int pid,
        {
            Id = id1;
            //  PrentId = pid;
            Name = str;
            Href = strurl;
            ParentId = pid;
            //cid = cd;

        }

        /// <summary>
        /// 父节点id
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  //设置自增
        public int Id { get; set; }

        /// <summary>
        /// 节点名称
        /// </summary>
        [StringLength(10, MinimumLength = 2, ErrorMessage = "{0}长度为{2}-{1}个字符")]
        [Display(Name = "节点名称")]
        public string Name { get; set; }
        /// <summary>
        /// 节点链接
        /// </summary>
        [Display(Name = "链接")]
        public string Href { get; set; }

        /// <summary>
        /// 子节点id
        /// </summary>
        [Display(Name = "父节点")]
        public Nullable<Int32> ParentId { get; set; }

        //[Display(Name = "子节点")]
        //public Nullable<Int32> cid { get; set; }


    }
}
