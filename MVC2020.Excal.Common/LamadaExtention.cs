using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MVC2020.Excal.Common
{
    //where T : new()的意思是,这个T必须有public构造函数bai,如果new BaseClass<T>()的时候,这个T没有public 构造函数,将编译du错误.
    //where字句后面有new()约束的话，T类型必须有公有的无参的构造函数。


    /// <summary>
    /// 动态构造自定义参数拉姆表达式
    /// </summary>
    /// <typeparam name="Dto"></typeparam>
    public class LamadaExtention<Dto> where Dto : new()
    {
        private List<Expression> m_lstExpression = null;
        private ParameterExpression m_Parameter = null;

        /// <summary>
        /// 初始话  动态表达式
        /// </summary>
        public LamadaExtention()
        {
            m_lstExpression = new List<Expression>();
            m_Parameter = Expression.Parameter(typeof(Dto), "x");
        }
        /// <summary>
        /// 动态构造表达式  反射输出
        /// </summary>
        /// <param name="strPropertyName"></param>
        /// <param name="strValue"></param>
        /// <param name="expressType"></param>
        public void GetExpression(string strPropertyName, object strValue, ExpressionType expressType)
        {
            Expression expRes = null;
            MemberExpression member = Expression.PropertyOrField(m_Parameter, strPropertyName);

            //MemberExpression member666 = Expression.PropertyOrField(m_Parameter, strValue.ToString());  //测试int

            if (expressType == ExpressionType.Contains)
            {

                expRes = Expression.Call(member, typeof(string).GetMethod("Contains"), Expression.Constant(strValue));
            }
            else if (expressType == ExpressionType.Equal)
            {
                expRes = Expression.Equal(member, Expression.Constant(strValue, member.Type));
            }
            else if (expressType == ExpressionType.LessThan)
            {
                expRes = Expression.LessThan(member, Expression.Constant(strValue, member.Type));
            }
            else if (expressType == ExpressionType.LessThanOrEqual)
            {
                expRes = Expression.LessThanOrEqual(member, Expression.Constant(strValue, member.Type));
            }
            else if (expressType == ExpressionType.GreaterThan)//大于
            {

                expRes = Expression.GreaterThan(member, Expression.Constant(strValue, member.Type)); //修改类型INT
            }
            else if (expressType == ExpressionType.GreaterThanOrEqual)
            {
                expRes = Expression.GreaterThanOrEqual(member, Expression.Constant(strValue, member.Type));
            }
            //return expRes;
            m_lstExpression.Add(expRes);
        }

        //针对Or条件的表达式
        public void GetExpression(string strPropertyName, List<object> lstValue)
        {
            Expression expRes = null;
            MemberExpression member = Expression.PropertyOrField(m_Parameter, strPropertyName);
            foreach (var oValue in lstValue)
            {
                if (expRes == null)
                {
                    expRes = Expression.Equal(member, Expression.Constant(oValue, member.Type));
                }
                else
                {
                    expRes = Expression.Or(expRes, Expression.Equal(member, Expression.Constant(oValue, member.Type)));
                }
            }


            m_lstExpression.Add(expRes);
        }

        //得到Lamada表达式的Expression对象
        public Expression<Func<Dto, bool>> GetLambda()
        {
            Expression whereExpr = null;
            foreach (var expr in this.m_lstExpression)
            {
                if (whereExpr == null) whereExpr = expr;
                else whereExpr = Expression.And(whereExpr, expr);
            }
            if (whereExpr == null)
                return null;
            return Expression.Lambda<Func<Dto, Boolean>>(whereExpr, m_Parameter);
        }
    }
    //用于区分操作的枚举
    public enum ExpressionType
    {
        /// <summary>
        /// 字符串是否相等 -like
        /// </summary>
        Contains, 
        /// <summary>
        /// 数字相等
        /// </summary>
        Equal, 
        /// <summary>
        /// 数字小于
        /// </summary>
        LessThan, 
        /// <summary>
        /// 数字小于等于
        /// </summary>
        LessThanOrEqual, 
        /// <summary>
        /// 数字大于
        /// </summary>
        GreaterThan,
        /// <summary>
        /// 数字大于等于
        /// </summary>
        GreaterThanOrEqual
    }
}
