//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace BoardGame.Service.Data
//{
//    using Microsoft.EntityFrameworkCore;
//    using Microsoft.EntityFrameworkCore.Design;
//    using Microsoft.Extensions.Configuration;
//    using Extensions;

//    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
//    {
//        private readonly IConfiguration config;

//        public ApplicationDbContextFactory(IConfiguration config)
//        {
//            this.config = config;
//        }

//        public ApplicationDbContext CreateDbContext(string[] args)
//        {
//            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
//            builder.UseSqlServer(this.config.GetConnectionString());
//            return new ApplicationDbContext(builder.Options);
//        }
//    }
//}
