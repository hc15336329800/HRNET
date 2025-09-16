using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;   // [Key]
using System.ComponentModel.DataAnnotations.Schema; //[DatabaseGenerated]

namespace MVC2020.Model
{
    public class New
    {
        /// <summary>
        /// 数据库需要手动设置索引
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  //设置自增
        public int NewsID { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [StringLength(50, MinimumLength = 2, ErrorMessage = "{0}长度为{2}-{1}个字符")]
        [Display(Name = "标题")]
        public string Title { get; set; }


        /// <summary>
        /// 描述
        /// </summary>
        [StringLength(150, MinimumLength = 4, ErrorMessage = "{0}长度为{2}-{1}个字符")]
        [Display(Name = "描述")]
        public string Section { get; set; }

        /// <summary>
        /// 文章
        /// </summary>
        //[StringLength(1010, MinimumLength = 4, ErrorMessage = "{0}长度为{2}-{1}个字符")]
        [Display(Name = "文章")]
        public string Article { get; set; }

        /// <summary>
        /// 时间
        /// </summary> 
        [Required(ErrorMessage = "必须输入{0}")]
        [Display(Name = "时间")]
        public DateTime DTime { get; set; }

        /// <summary>
        /// 名称【可做昵称、真实姓名等】
        /// </summary>
        [StringLength(30, ErrorMessage = "{0}必须少于{1}个字符")]
        [Display(Name = " 昵称")]
        public string Name { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        //[StringLength(20, ErrorMessage = "{0}必须少于{1}个字符")]
        [Display(Name = "图片名称")]
        public string ImgName { get; set; }

        /// <summary>
        /// 统计
        /// </summary>
        public int NCount { get; set; }
    }
}
