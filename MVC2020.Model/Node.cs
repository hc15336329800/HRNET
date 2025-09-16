using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC2020.Model
{
    public class Node
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  //设置自增
        //树的节点Id，区别于数据库中保存的数据Id。若要存储数据库数据的Id，添加新的Id属性；若想为节点设置路径，类中添加Path属性
        public int Id { get; set; }

        /// <summary>
        /// 父节点
        /// </summary>
        public int ParentId { get; set; }


        /// <summary>
        /// 名称
        /// </summary>
        [Required]//不允许空
        public string Name { get; set; }   //节点名称


        /// <summary>
        /// 菜单类型
        /// </summary>
        [Required]//不允许空
        public string MenuType { get; set; }
    }
}
