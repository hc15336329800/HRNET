using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC2020.Model
{
    public class HRYT
    {
        /// <summary>
        /// 数据库需要手动设置索引
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  //设置自增
        public int 序号 { get; set; }
        public string 客户 { get; set; }
        public string 片区 { get; set; }
        public string 合同编号 { get; set; }
        public int 规格浓度 { get; set; }
        public string 规格DE值 { get; set; }
        public string 包装罐车 { get; set; }
        public string 包装75KG { get; set; }
        public string 包装280KG { get; set; }
        public int 数量 { get; set; }
        public int 含税单价 { get; set; }
        public string 现金 { get; set; }
        public string 承兑 { get; set; }
        public string 运费 { get; set; }

        public string 日期 { get; set; }

    }
}
