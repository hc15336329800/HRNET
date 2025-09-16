using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

////消息提示
////在进行操作的时候经常会需要对操作成功、失败、发生错误进行提示，所以专门做一个提示的模型类
////模型类

namespace MVC2020.Excal.Common
{
    /// <summary>
    /// 消息提示
    /// </summary>
    public class Mess
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 按钮组
        /// </summary>
        public List<string> Buttons { get; set; }
    }
}
