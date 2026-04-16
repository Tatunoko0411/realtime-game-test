using Microsoft.EntityFrameworkCore;
using bicycle_racing.Shared.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using bicycle_racing.Server.Models.Entities;


namespace bicycle_racing.Server.Models.Contexts
{
    public class GameDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Battle> Battles { get; set; }
        public DbSet<BattleLog> Battle_Logs { get; set; }
        public DbSet<Friend> Friends { get; set; }

        public DbSet<Mail> Mails { get; set; }
#if DEBUG
        //readonly string connectionString = "server=localhost;port=3306;database=realtime_game241202;user=jobi;password=jobi;";
       // readonly string connectionString ="server=localhost;database=bicycle_racing;user=jobi;password=jobi;";
       readonly string connectionString = "server=db-bicycle-racing.mysql.database.azure.com;port=3306;database=realtime_game241202;user=student;password=Yoshidajobi2024;";
        // readonly string connectionString = "server=db-bicycle-racing.mysql.database.azure.com;port=3306;database=realtime_game241202;user=student;password=Yoshidajobi2024;SslMode=Required;AllowPublicKeyRetrieval=True;CheckCertificateRevocationList=false;";

#else
//readonly string connectionString = "server=db-bicycle-racing.mysql.database.azure.com;port=3306;database=realtime_game241202;user=student;password=Yoshidajobi2024;SslMode=Required;";
// SSLModeをRequiredにしたまま、証明書の検証を一旦スキップする設定
readonly string connectionString = "server=db-bicycle-racing.mysql.database.azure.com;port=3306;database=realtime_game241202;user=student;password=Yoshidajobi2024;";

#endif

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (!optionsBuilder.IsConfigured)
            //{
            //    optionsBuilder.UseMySql(connectionString,
            //        ServerVersion.AutoDetect(connectionString),
            //        options => options.EnableRetryOnFailure(
            //            maxRetryCount: 5,
            //            maxRetryDelay: TimeSpan.FromSeconds(30),
            //            errorNumbersToAdd: null)
            //        );
            //}
            Console.WriteLine("OnConfiguringStart");
            optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0)));
            Console.WriteLine("OnConfiguringEnd");
        }
    }
}
