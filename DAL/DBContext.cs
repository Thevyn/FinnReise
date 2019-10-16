using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DAL
{
    public class DBContextFactory : IDesignTimeDbContextFactory<DBContext>
    {
        public DBContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(@Directory.GetCurrentDirectory() + "/../FinnReise/appsettings.json").Build();
            var builder = new DbContextOptionsBuilder<DBContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            builder.UseSqlServer(connectionString);
            return new DBContext(builder.Options);
        }
    }

    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }


        public DbSet<DBStasjon> Strekning { get; set; }
        public DbSet<DBAvgang> Avgang { get; set; }
        public DbSet<DBKort> Kort { get; set; }
        public DbSet<DBOrdre> Ordre { get; set; }
        public DbSet<DBAdmin> Admin { get; set; }

        public DbSet<DBEndring> Endring { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DBKort>().ToTable("Kort");
            modelBuilder.Entity<DBOrdre>().ToTable("Ordre");
            modelBuilder.Entity<DBAdmin>().ToTable("Admin");
            modelBuilder.Entity<DBEndring>().ToTable("Endringer");


            modelBuilder.Entity<DBStasjon>().ToTable("Strekninger").HasData(new DBStasjon[]
            {
                new DBStasjon {SId = 1, Stasjon = "Oslo S"},
                new DBStasjon {SId = 2, Stasjon = "Bergen"},
                new DBStasjon {SId = 3, Stasjon = "Trondheim"},
                new DBStasjon {SId = 4, Stasjon = "Fredrikstad"},
                new DBStasjon {SId = 5, Stasjon = "Ski"},
                new DBStasjon {SId = 6, Stasjon = "Stavanger"},
                new DBStasjon {SId = 7, Stasjon = "Halden"},
                new DBStasjon {SId = 8, Stasjon = "Tønsberg"},
                new DBStasjon {SId = 9, Stasjon = "Drammen"},
                new DBStasjon {SId = 10, Stasjon = "Hamar"},
                new DBStasjon {SId = 11, Stasjon = "Lillestrøm"},
                new DBStasjon {SId = 12, Stasjon = "Ålesund"},
                new DBStasjon {SId = 13, Stasjon = "Lillehammer"},
            });

            modelBuilder.Entity<DBAvgang>().ToTable("Avganger").HasData(new DBAvgang[]
            {
                new DBAvgang {AId = 1, SId = 1, Avgangstid = "07:20", Spor = 1, Linje = "L1"},
                new DBAvgang {AId = 2, SId = 1, Avgangstid = "08:20", Spor = 2, Linje = "L1"},
                new DBAvgang {AId = 3, SId = 1, Avgangstid = "09:20", Spor = 3, Linje = "L1"},
                new DBAvgang {AId = 4, SId = 1, Avgangstid = "10:20", Spor = 4, Linje = "L1"},
                new DBAvgang {AId = 5, SId = 1, Avgangstid = "11:20", Spor = 5, Linje = "L1"},
                new DBAvgang {AId = 6, SId = 1, Avgangstid = "12:20", Spor = 6, Linje = "L1"},
                new DBAvgang {AId = 7, SId = 1, Avgangstid = "13:20", Spor = 7, Linje = "L1"},
                new DBAvgang {AId = 8, SId = 1, Avgangstid = "14:20", Spor = 8, Linje = "L1"},
                new DBAvgang {AId = 9, SId = 1, Avgangstid = "15:20", Spor = 9, Linje = "L1"},
                new DBAvgang {AId = 10, SId = 1, Avgangstid = "16:20", Spor = 10, Linje = "L1"},
                new DBAvgang {AId = 11, SId = 1, Avgangstid = "17:20", Spor = 1, Linje = "L1"},
                new DBAvgang {AId = 12, SId = 1, Avgangstid = "18:20", Spor = 2, Linje = "L1"},
                new DBAvgang {AId = 13, SId = 1, Avgangstid = "19:20", Spor = 3, Linje = "L1"},
                new DBAvgang {AId = 14, SId = 1, Avgangstid = "20:20", Spor = 4, Linje = "L1"},
                new DBAvgang {AId = 15, SId = 1, Avgangstid = "21:20", Spor = 5, Linje = "L1"},
                new DBAvgang {AId = 16, SId = 1, Avgangstid = "22:20", Spor = 6, Linje = "L1"},
                new DBAvgang {AId = 17, SId = 1, Avgangstid = "23:20", Spor = 7, Linje = "L1"},
                new DBAvgang {AId = 18, SId = 1, Avgangstid = "00:20", Spor = 8, Linje = "L1"},

                new DBAvgang {AId = 19, SId = 2, Avgangstid = "07:43", Spor = 1, Linje = "L2"},
                new DBAvgang {AId = 20, SId = 2, Avgangstid = "08:43", Spor = 2, Linje = "L2"},
                new DBAvgang {AId = 21, SId = 2, Avgangstid = "09:43", Spor = 3, Linje = "L2"},
                new DBAvgang {AId = 22, SId = 2, Avgangstid = "10:43", Spor = 4, Linje = "L2"},
                new DBAvgang {AId = 23, SId = 2, Avgangstid = "11:43", Spor = 1, Linje = "L2"},
                new DBAvgang {AId = 24, SId = 2, Avgangstid = "12:43", Spor = 2, Linje = "L2"},
                new DBAvgang {AId = 25, SId = 2, Avgangstid = "13:43", Spor = 3, Linje = "L2"},
                new DBAvgang {AId = 26, SId = 2, Avgangstid = "14:43", Spor = 4, Linje = "L2"},
                new DBAvgang {AId = 27, SId = 2, Avgangstid = "15:43", Spor = 1, Linje = "L2"},
                new DBAvgang {AId = 28, SId = 2, Avgangstid = "16:43", Spor = 2, Linje = "L2"},
                new DBAvgang {AId = 29, SId = 2, Avgangstid = "17:43", Spor = 3, Linje = "L2"},
                new DBAvgang {AId = 30, SId = 2, Avgangstid = "18:43", Spor = 4, Linje = "L2"},
                new DBAvgang {AId = 31, SId = 2, Avgangstid = "19:43", Spor = 1, Linje = "L2"},
                new DBAvgang {AId = 32, SId = 2, Avgangstid = "20:43", Spor = 2, Linje = "L2"},
                new DBAvgang {AId = 33, SId = 2, Avgangstid = "21:43", Spor = 3, Linje = "L2"},
                new DBAvgang {AId = 34, SId = 2, Avgangstid = "22:43", Spor = 4, Linje = "L2"},
                new DBAvgang {AId = 35, SId = 2, Avgangstid = "23:43", Spor = 1, Linje = "L2"},
                new DBAvgang {AId = 36, SId = 2, Avgangstid = "00:43", Spor = 2, Linje = "L2"},

                new DBAvgang {AId = 37, SId = 3, Avgangstid = "07:29", Spor = 1, Linje = "L3"},
                new DBAvgang {AId = 38, SId = 3, Avgangstid = "08:29", Spor = 2, Linje = "L3"},
                new DBAvgang {AId = 39, SId = 3, Avgangstid = "09:29", Spor = 3, Linje = "L3"},
                new DBAvgang {AId = 40, SId = 3, Avgangstid = "10:29", Spor = 4, Linje = "L3"},
                new DBAvgang {AId = 41, SId = 3, Avgangstid = "11:29", Spor = 1, Linje = "L3"},
                new DBAvgang {AId = 42, SId = 3, Avgangstid = "12:29", Spor = 2, Linje = "L3"},
                new DBAvgang {AId = 43, SId = 3, Avgangstid = "13:29", Spor = 3, Linje = "L3"},
                new DBAvgang {AId = 44, SId = 3, Avgangstid = "14:29", Spor = 4, Linje = "L3"},
                new DBAvgang {AId = 45, SId = 3, Avgangstid = "15:29", Spor = 1, Linje = "L3"},
                new DBAvgang {AId = 46, SId = 3, Avgangstid = "16:29", Spor = 2, Linje = "L3"},
                new DBAvgang {AId = 47, SId = 3, Avgangstid = "17:29", Spor = 3, Linje = "L3"},
                new DBAvgang {AId = 48, SId = 3, Avgangstid = "18:29", Spor = 4, Linje = "L3"},
                new DBAvgang {AId = 49, SId = 3, Avgangstid = "19:29", Spor = 1, Linje = "L3"},
                new DBAvgang {AId = 50, SId = 3, Avgangstid = "20:29", Spor = 2, Linje = "L3"},
                new DBAvgang {AId = 51, SId = 3, Avgangstid = "21:29", Spor = 3, Linje = "L3"},
                new DBAvgang {AId = 52, SId = 3, Avgangstid = "22:29", Spor = 4, Linje = "L3"},
                new DBAvgang {AId = 53, SId = 3, Avgangstid = "23:29", Spor = 1, Linje = "L3"},
                new DBAvgang {AId = 54, SId = 3, Avgangstid = "00:29", Spor = 2, Linje = "L3"},

                new DBAvgang {AId = 55, SId = 4, Avgangstid = "07:12", Spor = 1, Linje = "L4"},
                new DBAvgang {AId = 56, SId = 4, Avgangstid = "08:12", Spor = 2, Linje = "L4"},
                new DBAvgang {AId = 57, SId = 4, Avgangstid = "09:12", Spor = 1, Linje = "L4"},
                new DBAvgang {AId = 58, SId = 4, Avgangstid = "10:12", Spor = 2, Linje = "L4"},
                new DBAvgang {AId = 59, SId = 4, Avgangstid = "11:12", Spor = 1, Linje = "L4"},
                new DBAvgang {AId = 60, SId = 4, Avgangstid = "12:12", Spor = 2, Linje = "L4"},
                new DBAvgang {AId = 61, SId = 4, Avgangstid = "13:12", Spor = 1, Linje = "L4"},
                new DBAvgang {AId = 62, SId = 4, Avgangstid = "14:12", Spor = 2, Linje = "L4"},
                new DBAvgang {AId = 63, SId = 4, Avgangstid = "15:12", Spor = 1, Linje = "L4"},
                new DBAvgang {AId = 64, SId = 4, Avgangstid = "16:12", Spor = 2, Linje = "L4"},
                new DBAvgang {AId = 65, SId = 4, Avgangstid = "17:12", Spor = 1, Linje = "L4"},
                new DBAvgang {AId = 66, SId = 4, Avgangstid = "18:12", Spor = 2, Linje = "L4"},
                new DBAvgang {AId = 67, SId = 4, Avgangstid = "19:12", Spor = 1, Linje = "L4"},
                new DBAvgang {AId = 68, SId = 4, Avgangstid = "20:12", Spor = 2, Linje = "L4"},
                new DBAvgang {AId = 69, SId = 4, Avgangstid = "21:12", Spor = 1, Linje = "L4"},
                new DBAvgang {AId = 70, SId = 4, Avgangstid = "22:12", Spor = 2, Linje = "L4"},
                new DBAvgang {AId = 71, SId = 4, Avgangstid = "23:12", Spor = 1, Linje = "L4"},
                new DBAvgang {AId = 72, SId = 4, Avgangstid = "00:12", Spor = 2, Linje = "L4"},

                new DBAvgang {AId = 73, SId = 5, Avgangstid = "07:04", Spor = 1, Linje = "L5"},
                new DBAvgang {AId = 74, SId = 5, Avgangstid = "08:04", Spor = 2, Linje = "L5"},
                new DBAvgang {AId = 75, SId = 5, Avgangstid = "09:04", Spor = 1, Linje = "L5"},
                new DBAvgang {AId = 76, SId = 5, Avgangstid = "10:04", Spor = 2, Linje = "L5"},
                new DBAvgang {AId = 77, SId = 5, Avgangstid = "11:04", Spor = 1, Linje = "L5"},
                new DBAvgang {AId = 78, SId = 5, Avgangstid = "12:04", Spor = 2, Linje = "L5"},
                new DBAvgang {AId = 79, SId = 5, Avgangstid = "13:04", Spor = 1, Linje = "L5"},
                new DBAvgang {AId = 80, SId = 5, Avgangstid = "14:04", Spor = 2, Linje = "L5"},
                new DBAvgang {AId = 81, SId = 5, Avgangstid = "15:04", Spor = 1, Linje = "L5"},
                new DBAvgang {AId = 82, SId = 5, Avgangstid = "16:04", Spor = 2, Linje = "L5"},
                new DBAvgang {AId = 83, SId = 5, Avgangstid = "17:04", Spor = 1, Linje = "L5"},
                new DBAvgang {AId = 84, SId = 5, Avgangstid = "18:04", Spor = 2, Linje = "L5"},
                new DBAvgang {AId = 85, SId = 5, Avgangstid = "19:04", Spor = 1, Linje = "L5"},
                new DBAvgang {AId = 86, SId = 5, Avgangstid = "20:04", Spor = 2, Linje = "L5"},
                new DBAvgang {AId = 87, SId = 5, Avgangstid = "21:04", Spor = 1, Linje = "L5"},
                new DBAvgang {AId = 88, SId = 5, Avgangstid = "22:04", Spor = 2, Linje = "L5"},
                new DBAvgang {AId = 89, SId = 5, Avgangstid = "23:04", Spor = 1, Linje = "L5"},
                new DBAvgang {AId = 90, SId = 5, Avgangstid = "00:04", Spor = 2, Linje = "L5"},

                new DBAvgang {AId = 91, SId = 6, Avgangstid = "07:31", Spor = 1, Linje = "L6"},
                new DBAvgang {AId = 92, SId = 6, Avgangstid = "08:31", Spor = 2, Linje = "L6"},
                new DBAvgang {AId = 93, SId = 6, Avgangstid = "09:31", Spor = 1, Linje = "L6"},
                new DBAvgang {AId = 94, SId = 6, Avgangstid = "10:31", Spor = 2, Linje = "L6"},
                new DBAvgang {AId = 95, SId = 6, Avgangstid = "11:31", Spor = 1, Linje = "L6"},
                new DBAvgang {AId = 96, SId = 6, Avgangstid = "12:31", Spor = 2, Linje = "L6"},
                new DBAvgang {AId = 97, SId = 6, Avgangstid = "13:31", Spor = 1, Linje = "L6"},
                new DBAvgang {AId = 98, SId = 6, Avgangstid = "14:31", Spor = 2, Linje = "L6"},
                new DBAvgang {AId = 99, SId = 6, Avgangstid = "15:31", Spor = 1, Linje = "L6"},
                new DBAvgang {AId = 100, SId = 6, Avgangstid = "16:31", Spor = 2, Linje = "L6"},
                new DBAvgang {AId = 101, SId = 6, Avgangstid = "17:31", Spor = 1, Linje = "L6"},
                new DBAvgang {AId = 102, SId = 6, Avgangstid = "18:31", Spor = 2, Linje = "L6"},
                new DBAvgang {AId = 103, SId = 6, Avgangstid = "19:31", Spor = 1, Linje = "L6"},
                new DBAvgang {AId = 104, SId = 6, Avgangstid = "20:31", Spor = 2, Linje = "L6"},
                new DBAvgang {AId = 105, SId = 6, Avgangstid = "21:31", Spor = 1, Linje = "L6"},
                new DBAvgang {AId = 106, SId = 6, Avgangstid = "22:31", Spor = 2, Linje = "L6"},
                new DBAvgang {AId = 107, SId = 6, Avgangstid = "23:31", Spor = 1, Linje = "L6"},
                new DBAvgang {AId = 108, SId = 6, Avgangstid = "00:31", Spor = 2, Linje = "L6"},

                new DBAvgang {AId = 109, SId = 7, Avgangstid = "07:47", Spor = 1, Linje = "L7"},
                new DBAvgang {AId = 110, SId = 7, Avgangstid = "08:47", Spor = 2, Linje = "L7"},
                new DBAvgang {AId = 111, SId = 7, Avgangstid = "09:47", Spor = 1, Linje = "L7"},
                new DBAvgang {AId = 112, SId = 7, Avgangstid = "10:47", Spor = 2, Linje = "L7"},
                new DBAvgang {AId = 113, SId = 7, Avgangstid = "11:47", Spor = 1, Linje = "L7"},
                new DBAvgang {AId = 114, SId = 7, Avgangstid = "12:47", Spor = 2, Linje = "L7"},
                new DBAvgang {AId = 115, SId = 7, Avgangstid = "13:47", Spor = 1, Linje = "L7"},
                new DBAvgang {AId = 116, SId = 7, Avgangstid = "14:47", Spor = 2, Linje = "L7"},
                new DBAvgang {AId = 117, SId = 7, Avgangstid = "15:47", Spor = 1, Linje = "L7"},
                new DBAvgang {AId = 118, SId = 7, Avgangstid = "16:47", Spor = 2, Linje = "L7"},
                new DBAvgang {AId = 119, SId = 7, Avgangstid = "17:47", Spor = 1, Linje = "L7"},
                new DBAvgang {AId = 120, SId = 7, Avgangstid = "18:47", Spor = 2, Linje = "L7"},
                new DBAvgang {AId = 121, SId = 7, Avgangstid = "19:47", Spor = 1, Linje = "L7"},
                new DBAvgang {AId = 122, SId = 7, Avgangstid = "20:47", Spor = 2, Linje = "L7"},
                new DBAvgang {AId = 123, SId = 7, Avgangstid = "21:47", Spor = 1, Linje = "L7"},
                new DBAvgang {AId = 124, SId = 7, Avgangstid = "22:47", Spor = 2, Linje = "L7"},
                new DBAvgang {AId = 125, SId = 7, Avgangstid = "23:47", Spor = 1, Linje = "L7"},
                new DBAvgang {AId = 126, SId = 7, Avgangstid = "00:47", Spor = 2, Linje = "L7"},

                new DBAvgang {AId = 127, SId = 8, Avgangstid = "07:23", Spor = 1, Linje = "L8"},
                new DBAvgang {AId = 128, SId = 8, Avgangstid = "08:23", Spor = 2, Linje = "L8"},
                new DBAvgang {AId = 129, SId = 8, Avgangstid = "09:23", Spor = 1, Linje = "L8"},
                new DBAvgang {AId = 130, SId = 8, Avgangstid = "10:23", Spor = 2, Linje = "L8"},
                new DBAvgang {AId = 131, SId = 8, Avgangstid = "11:23", Spor = 1, Linje = "L8"},
                new DBAvgang {AId = 132, SId = 8, Avgangstid = "12:23", Spor = 2, Linje = "L8"},
                new DBAvgang {AId = 133, SId = 8, Avgangstid = "13:23", Spor = 1, Linje = "L8"},
                new DBAvgang {AId = 134, SId = 8, Avgangstid = "14:23", Spor = 2, Linje = "L8"},
                new DBAvgang {AId = 135, SId = 8, Avgangstid = "15:23", Spor = 1, Linje = "L8"},
                new DBAvgang {AId = 136, SId = 8, Avgangstid = "16:23", Spor = 2, Linje = "L8"},
                new DBAvgang {AId = 137, SId = 8, Avgangstid = "17:23", Spor = 1, Linje = "L8"},
                new DBAvgang {AId = 138, SId = 8, Avgangstid = "18:23", Spor = 2, Linje = "L8"},
                new DBAvgang {AId = 139, SId = 8, Avgangstid = "19:23", Spor = 1, Linje = "L8"},
                new DBAvgang {AId = 140, SId = 8, Avgangstid = "20:23", Spor = 2, Linje = "L8"},
                new DBAvgang {AId = 141, SId = 8, Avgangstid = "21:23", Spor = 1, Linje = "L8"},
                new DBAvgang {AId = 142, SId = 8, Avgangstid = "22:23", Spor = 2, Linje = "L8"},
                new DBAvgang {AId = 143, SId = 8, Avgangstid = "23:23", Spor = 1, Linje = "L8"},
                new DBAvgang {AId = 144, SId = 8, Avgangstid = "00:23", Spor = 2, Linje = "L8"},

                new DBAvgang {AId = 145, SId = 9, Avgangstid = "07:23", Spor = 1, Linje = "L9"},
                new DBAvgang {AId = 146, SId = 9, Avgangstid = "08:23", Spor = 2, Linje = "L9"},
                new DBAvgang {AId = 147, SId = 9, Avgangstid = "09:23", Spor = 1, Linje = "L9"},
                new DBAvgang {AId = 148, SId = 9, Avgangstid = "10:23", Spor = 2, Linje = "L9"},
                new DBAvgang {AId = 149, SId = 9, Avgangstid = "11:23", Spor = 1, Linje = "L9"},
                new DBAvgang {AId = 150, SId = 9, Avgangstid = "12:23", Spor = 2, Linje = "L9"},
                new DBAvgang {AId = 151, SId = 9, Avgangstid = "13:23", Spor = 1, Linje = "L9"},
                new DBAvgang {AId = 152, SId = 9, Avgangstid = "14:23", Spor = 2, Linje = "L9"},
                new DBAvgang {AId = 153, SId = 9, Avgangstid = "15:23", Spor = 1, Linje = "L9"},
                new DBAvgang {AId = 154, SId = 9, Avgangstid = "16:23", Spor = 2, Linje = "L9"},
                new DBAvgang {AId = 155, SId = 9, Avgangstid = "17:23", Spor = 1, Linje = "L9"},
                new DBAvgang {AId = 156, SId = 9, Avgangstid = "18:23", Spor = 2, Linje = "L9"},
                new DBAvgang {AId = 157, SId = 9, Avgangstid = "19:23", Spor = 1, Linje = "L9"},
                new DBAvgang {AId = 158, SId = 9, Avgangstid = "20:23", Spor = 2, Linje = "L9"},
                new DBAvgang {AId = 159, SId = 9, Avgangstid = "21:23", Spor = 1, Linje = "L9"},
                new DBAvgang {AId = 160, SId = 9, Avgangstid = "22:23", Spor = 2, Linje = "L9"},
                new DBAvgang {AId = 161, SId = 9, Avgangstid = "23:23", Spor = 1, Linje = "L9"},
                new DBAvgang {AId = 162, SId = 9, Avgangstid = "00:23", Spor = 2, Linje = "L9"},

                new DBAvgang {AId = 163, SId = 10, Avgangstid = "07:59", Spor = 1, Linje = "L10"},
                new DBAvgang {AId = 164, SId = 10, Avgangstid = "08:59", Spor = 2, Linje = "L10"},
                new DBAvgang {AId = 165, SId = 10, Avgangstid = "09:59", Spor = 1, Linje = "L10"},
                new DBAvgang {AId = 166, SId = 10, Avgangstid = "10:59", Spor = 2, Linje = "L10"},
                new DBAvgang {AId = 167, SId = 10, Avgangstid = "11:59", Spor = 1, Linje = "L10"},
                new DBAvgang {AId = 168, SId = 10, Avgangstid = "12:59", Spor = 2, Linje = "L10"},
                new DBAvgang {AId = 169, SId = 10, Avgangstid = "13:59", Spor = 1, Linje = "L10"},
                new DBAvgang {AId = 170, SId = 10, Avgangstid = "14:59", Spor = 2, Linje = "L10"},
                new DBAvgang {AId = 171, SId = 10, Avgangstid = "15:59", Spor = 1, Linje = "L10"},
                new DBAvgang {AId = 172, SId = 10, Avgangstid = "16:59", Spor = 2, Linje = "L10"},
                new DBAvgang {AId = 173, SId = 10, Avgangstid = "17:59", Spor = 1, Linje = "L10"},
                new DBAvgang {AId = 174, SId = 10, Avgangstid = "18:59", Spor = 2, Linje = "L10"},
                new DBAvgang {AId = 175, SId = 10, Avgangstid = "19:59", Spor = 1, Linje = "L10"},
                new DBAvgang {AId = 176, SId = 10, Avgangstid = "20:59", Spor = 2, Linje = "L10"},
                new DBAvgang {AId = 177, SId = 10, Avgangstid = "21:59", Spor = 1, Linje = "L10"},
                new DBAvgang {AId = 178, SId = 10, Avgangstid = "22:59", Spor = 2, Linje = "L10"},
                new DBAvgang {AId = 179, SId = 10, Avgangstid = "59:59", Spor = 1, Linje = "L10"},
                new DBAvgang {AId = 180, SId = 10, Avgangstid = "00:59", Spor = 2, Linje = "L10"},

                new DBAvgang {AId = 181, SId = 11, Avgangstid = "07:09", Spor = 1, Linje = "L11"},
                new DBAvgang {AId = 182, SId = 11, Avgangstid = "08:09", Spor = 2, Linje = "L11"},
                new DBAvgang {AId = 183, SId = 11, Avgangstid = "09:09", Spor = 1, Linje = "L11"},
                new DBAvgang {AId = 184, SId = 11, Avgangstid = "10:09", Spor = 2, Linje = "L11"},
                new DBAvgang {AId = 185, SId = 11, Avgangstid = "11:09", Spor = 1, Linje = "L11"},
                new DBAvgang {AId = 186, SId = 11, Avgangstid = "12:09", Spor = 2, Linje = "L11"},
                new DBAvgang {AId = 187, SId = 11, Avgangstid = "13:09", Spor = 1, Linje = "L11"},
                new DBAvgang {AId = 188, SId = 11, Avgangstid = "14:09", Spor = 2, Linje = "L11"},
                new DBAvgang {AId = 189, SId = 11, Avgangstid = "15:09", Spor = 1, Linje = "L11"},
                new DBAvgang {AId = 190, SId = 11, Avgangstid = "16:09", Spor = 2, Linje = "L11"},
                new DBAvgang {AId = 191, SId = 11, Avgangstid = "17:09", Spor = 1, Linje = "L11"},
                new DBAvgang {AId = 192, SId = 11, Avgangstid = "18:09", Spor = 2, Linje = "L11"},
                new DBAvgang {AId = 193, SId = 11, Avgangstid = "19:09", Spor = 1, Linje = "L11"},
                new DBAvgang {AId = 194, SId = 11, Avgangstid = "20:09", Spor = 2, Linje = "L11"},
                new DBAvgang {AId = 195, SId = 11, Avgangstid = "21:09", Spor = 1, Linje = "L11"},
                new DBAvgang {AId = 196, SId = 11, Avgangstid = "22:09", Spor = 2, Linje = "L11"},
                new DBAvgang {AId = 197, SId = 11, Avgangstid = "09:09", Spor = 1, Linje = "L11"},
                new DBAvgang {AId = 198, SId = 11, Avgangstid = "00:09", Spor = 2, Linje = "L11"},

                new DBAvgang {AId = 199, SId = 12, Avgangstid = "07:41", Spor = 1, Linje = "L12"},
                new DBAvgang {AId = 200, SId = 12, Avgangstid = "08:41", Spor = 2, Linje = "L12"},
                new DBAvgang {AId = 201, SId = 12, Avgangstid = "41:41", Spor = 1, Linje = "L12"},
                new DBAvgang {AId = 202, SId = 12, Avgangstid = "10:41", Spor = 2, Linje = "L12"},
                new DBAvgang {AId = 203, SId = 12, Avgangstid = "11:41", Spor = 1, Linje = "L12"},
                new DBAvgang {AId = 204, SId = 12, Avgangstid = "12:41", Spor = 2, Linje = "L12"},
                new DBAvgang {AId = 205, SId = 12, Avgangstid = "13:41", Spor = 1, Linje = "L12"},
                new DBAvgang {AId = 206, SId = 12, Avgangstid = "14:41", Spor = 2, Linje = "L12"},
                new DBAvgang {AId = 207, SId = 12, Avgangstid = "15:41", Spor = 1, Linje = "L12"},
                new DBAvgang {AId = 208, SId = 12, Avgangstid = "16:41", Spor = 2, Linje = "L12"},
                new DBAvgang {AId = 209, SId = 12, Avgangstid = "17:41", Spor = 1, Linje = "L12"},
                new DBAvgang {AId = 210, SId = 12, Avgangstid = "18:41", Spor = 2, Linje = "L12"},
                new DBAvgang {AId = 211, SId = 12, Avgangstid = "19:41", Spor = 1, Linje = "L12"},
                new DBAvgang {AId = 212, SId = 12, Avgangstid = "20:41", Spor = 2, Linje = "L12"},
                new DBAvgang {AId = 213, SId = 12, Avgangstid = "21:41", Spor = 1, Linje = "L12"},
                new DBAvgang {AId = 214, SId = 12, Avgangstid = "22:41", Spor = 2, Linje = "L12"},
                new DBAvgang {AId = 215, SId = 12, Avgangstid = "41:41", Spor = 1, Linje = "L12"},
                new DBAvgang {AId = 216, SId = 12, Avgangstid = "00:41", Spor = 2, Linje = "L12"},

                new DBAvgang {AId = 217, SId = 13, Avgangstid = "07:36", Spor = 1, Linje = "L13"},
                new DBAvgang {AId = 218, SId = 13, Avgangstid = "08:36", Spor = 2, Linje = "L13"},
                new DBAvgang {AId = 219, SId = 13, Avgangstid = "36:36", Spor = 1, Linje = "L13"},
                new DBAvgang {AId = 220, SId = 13, Avgangstid = "10:36", Spor = 2, Linje = "L13"},
                new DBAvgang {AId = 221, SId = 13, Avgangstid = "11:36", Spor = 1, Linje = "L13"},
                new DBAvgang {AId = 222, SId = 13, Avgangstid = "12:36", Spor = 2, Linje = "L13"},
                new DBAvgang {AId = 223, SId = 13, Avgangstid = "13:36", Spor = 1, Linje = "L13"},
                new DBAvgang {AId = 224, SId = 13, Avgangstid = "14:36", Spor = 2, Linje = "L13"},
                new DBAvgang {AId = 225, SId = 13, Avgangstid = "15:36", Spor = 1, Linje = "L13"},
                new DBAvgang {AId = 226, SId = 13, Avgangstid = "16:36", Spor = 2, Linje = "L13"},
                new DBAvgang {AId = 227, SId = 13, Avgangstid = "17:36", Spor = 1, Linje = "L13"},
                new DBAvgang {AId = 228, SId = 13, Avgangstid = "18:36", Spor = 2, Linje = "L13"},
                new DBAvgang {AId = 229, SId = 13, Avgangstid = "19:36", Spor = 1, Linje = "L13"},
                new DBAvgang {AId = 230, SId = 13, Avgangstid = "20:36", Spor = 2, Linje = "L13"},
                new DBAvgang {AId = 231, SId = 13, Avgangstid = "21:36", Spor = 1, Linje = "L13"},
                new DBAvgang {AId = 232, SId = 13, Avgangstid = "22:36", Spor = 2, Linje = "L13"},
                new DBAvgang {AId = 233, SId = 13, Avgangstid = "36:36", Spor = 1, Linje = "L13"},
                new DBAvgang {AId = 234, SId = 13, Avgangstid = "00:36", Spor = 2, Linje = "L13"},
            });
        }
    }
}