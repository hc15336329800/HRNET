using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVC2020.Model;

namespace MVC2020.Excel.DataContext
{

    /// <summary>
    /// 配置上下文(被动调用)
    /// </summary>
    public class Context : DbContext
    {
        /// <summary>
        /// 实现父类构造函数初始化
        /// </summary>
        public Context()
            : base("DefaultConnection")
        {
            Database.SetInitializer<Context>(new CreateDatabaseIfNotExists<Context>());
        }


        /// <summary>
        /// 导入的液糖模型
        /// </summary>
        public DbSet<HRYT> HRYTs { get; set; }


        /// <summary>
        /// 管理员模型
        /// </summary>
        public DbSet<Admin> Admin { get; set; }

        /// <summary>
        /// 角色模型
        /// </summary>
        public DbSet<Role> Roles { get; set; }

        /// <summary>
        /// 用户模型
        /// </summary>
        public DbSet<User> User { get; set; }


        //////////////////////////////////////////////////////////////////////////////////////////
        ///// <summary>
        ///// 新闻模型
        ///// </summary>
        public DbSet<New> News { get; set; }


        //<summary>
        //产品类型
        //</summary>
        public DbSet<ProType> ProTypes { get; set; }

        //<summary>
        //产品
        //</summary>

        public DbSet<Product> ProductS { get; set; }

        /// <summary>
        /// 树
        /// </summary>
        public DbSet<Node> Nodes { get; set; }

        /// <summary>
        /// 树（带父节点）
        /// </summary>
        public DbSet<NodeCatalog> NodeCatalog { get; set; }


        ////////////////////////////////////////////////////////////////////////////////////

        //<summary>
        //客户信息
        //</summary>

        public DbSet<Clent> ClentS { get; set; }
    }
}
