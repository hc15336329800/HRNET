using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;   //CallContext 来源
using System.Text;
using System.Threading.Tasks;

namespace MVC2020.Excel.DataContext
{

    /// <summary>
    ///  获取当前线程的数据上下文(被动调用)
    /// </summary>
    public class CurrentContext
    {
        
        public static Context GetCurrentContext()
        {
            Context _nContext = CallContext.GetData("Context") as Context;//CallContext.GetData获取Context
            if (_nContext == null)
            {
                _nContext = new Context();
                CallContext.SetData("Context", _nContext);//CallContext.SetData存储Context
            }
            return _nContext;
        }
    }
}
