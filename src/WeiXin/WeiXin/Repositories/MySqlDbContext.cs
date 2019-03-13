using Infrastructure.Core.DDD;
using Infrastructure.Core.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace WeiXin.Repositories
{
    public class MySqlDbContext: DbContext, IUnitOfWork
    {
        private ConnectionStrings _connectionStrings;
        public MySqlDbContext(IOptions<ConnectionStrings> options)
        {
            this._connectionStrings = options.Value;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(_connectionStrings.WeiXinContext);
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.AutoMap(typeof(MySqlDbContext));

        }

        #region 事务提交
        /// <summary>
        /// 事务提交
        /// </summary>
        /// <returns></returns>
        public int Commit()
        {
            return this.SaveChanges();
        }
        #endregion
    }
}
