using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC2020.Model
{
    /// <summary>
    /// 客户信息ClentManage
    /// </summary>
    public class Clent
    {

        [Key]
        public int ClentID { get; set; }

        /// <summary>
        /// 客户
        /// </summary>
        [StringLength(20, MinimumLength = 2, ErrorMessage = "{0}长度为{2}-{1}个字符")]
        [Display(Name = "客户名")]
        public string ClentName { get; set; }

        /// <summary>
        /// 提货单位
        /// </summary>
        [StringLength(25, MinimumLength = 2, ErrorMessage = "{0}长度为{2}-{1}个字符")]
        [Display(Name = "提货单位")]
        public string ClentAddress { get; set; }

        /// <summary>
        /// 车号
        /// </summary>
        [StringLength(15, MinimumLength = 7, ErrorMessage = "{0}长度为{2}-{1}个字符")]
        [Display(Name = "车号")]
        public string CarNum { get; set; }

        /// <summary>
        /// 箱号
        /// </summary>
        [StringLength(15, MinimumLength = 7, ErrorMessage = "{0}长度为{2}-{1}个字符")]
        [Display(Name = "箱号")]
        public string CarBoxNum { get; set; }

        /// <summary>
        /// 铅封
        /// </summary>
        [StringLength(15, MinimumLength = 7, ErrorMessage = "{0}长度为{2}-{1}个字符")]
        [Display(Name = "铅封")]
        public string CarSealNum { get; set; }


        /// <summary>
        /// 车主
        /// </summary>
        [StringLength(10, MinimumLength = 2, ErrorMessage = "{0}长度为{2}-{1}个字符")]
        [Display(Name = "车主")]
        public string CarName { get; set; }


        /// <summary>
        /// 手机号
        /// </summary>
        [StringLength(13, MinimumLength = 12, ErrorMessage = "{0}长度为{2}-{1}个字符")]
        [Display(Name = "手机号")]
        public string CarPhone { get; set; }

        /// <summary>
        /// 身份证
        /// </summary>
        [StringLength(18, MinimumLength = 15, ErrorMessage = "{0}长度为{2}-{1}个字符")]
        [Display(Name = "身份证号")]
        public string CarIdentity { get; set; }


        /// <summary>
        /// 产品
        /// </summary>
        [StringLength(15, MinimumLength = 2, ErrorMessage = "{0}长度为{2}-{1}个字符")]
        [Display(Name = "产品")]
        public string ProName { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        [StringLength(15, MinimumLength = 2, ErrorMessage = "{0}长度为{2}-{1}个字符")]
        [Display(Name = "规格")]
        public string ProType { get; set; }



        /// <summary>
        /// 数量
        /// </summary>
        [Required(ErrorMessage = "必须输入{0}")]
        [Range(0, 1000, ErrorMessage = "{0}范围{1}-{2}")]
        [Display(Name = "数量")]
        public int ProNum { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(25, MinimumLength = 2, ErrorMessage = "{0}长度为{2}-{1}个字符")]
        [Display(Name = "备注")]
        public string ProRemark { get; set; }


        /// <summary>
        /// 最后登录时间
        /// </summary>
        [DataType(DataType.DateTime)]
        [Display(Name = "发布时间")]

        public Nullable<DateTime> DTime { get; set; }


        /// <summary>
        /// 最后登录IP
        /// </summary>
        [Display(Name = "发布IP")]
        public string IP { get; set; }


        /// <summary>
        /// 审核状态
        /// </summary>
         [Display(Name = "发布IP")]
        public int IsAccect { get; set; }


    }
}
