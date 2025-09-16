using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/////////////////////////////////////////////////2021-4-16  21:39     模型验证为null  验证失效！/////////////////////////////////////////////////////////////
//·[DisplayFormat(ConvertEmptyStringToNull = false)] //获取或设置一个值，该值指示在数据源中更新数据字段时是否将空字符串值 ("") 自动转换为 null。
//·[StringLength(20, MinimumLength = 2, ErrorMessage = "{0}长度为{2}-{1}个字符")]
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////2021-4-16  21:46   错误信息放入   模型验证   ，强行输出模型验证错误信息////////////////////////////////////////
//·ModelState.AddModelError("", _response.Message);
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

namespace MVC2020.Model
{
    /// <summary>
    /// 产品信息
    /// </summary>
    public class Product
    {
        /// <summary>
        /// 数据库需要手动设置索引
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  //设置自增
        public int ProductID { get; set; }

        /// <summary>
        /// 类型ID
        /// </summary>
        [Display(Name = "类型")]
        public int ProductType { get; set; }

        /// <summary>
        /// 品名
        /// </summary>
        [DisplayFormat(ConvertEmptyStringToNull = false)] //获取或设置一个值，该值指示在数据源中更新数据字段时是否将空字符串值 ("") 自动转换为 null。
        [StringLength(30, MinimumLength = 2, ErrorMessage = "{0}长度为{2}-{1}个字符")]
        [Display(Name = "产品")]
        public string Title { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        //[StringLength(20, MinimumLength = 2, ErrorMessage = "请先选择图片并上传！")]
       // [DisplayFormat(ConvertEmptyStringToNull = false)] 
        [Display(Name = "图片")]
        public string ImgName { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [DisplayFormat(ConvertEmptyStringToNull = false)] //获取或设置一个值，该值指示在数据源中更新数据字段时是否将空字符串值 ("") 自动转换为 null。
        [StringLength(50, MinimumLength = 4, ErrorMessage = "{0}长度为{2}-{1}个字符")]
        [Display(Name = "描述")]
        public string Section { get; set; }

        /// <summary>
        /// 文章
        /// </summary>
        [DisplayFormat(ConvertEmptyStringToNull = false)] //获取或设置一个值，该值指示在数据源中更新数据字段时是否将空字符串值 ("") 自动转换为 null。
        //[StringLength(3010, MinimumLength = 4, ErrorMessage = "{0}长度为{2}-{1}个字符")]
        [Display(Name = "内容")]
        public string Article { get; set; }


        /// <summary>
        /// 时间
        /// </summary> 
        [Required(ErrorMessage = "必须输入{0}")]
        [Display(Name = "时间")]
        public DateTime DTime { get; set; }

        /// <summary>
        /// 统计
        /// </summary>
        public int NCount { get; set; }
    }
}
