using Raje.DAL.Seeds.Models;
using Raje.DL.DB.Admin;
using Raje.DL.Response.Adm;
using Raje.Infra.Enums;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Raje.DAL.EF
{
    public class EntityContext : DbContext
    {
        public EntityContext(DbContextOptions<EntityContext> options)
        : base(options)
        {
        }

        public virtual DbSet<Media> Media { get; set; }

        public virtual DbSet<City> City { get; set; }

        public virtual DbSet<State> State { get; set; }

        public virtual DbSet<User> User { get; set; }

        public virtual DbSet<UserRole> UserRole { get; set; }

        public virtual DbSet<Log> Log { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region [City]
            modelBuilder.Entity<City>()
                  .HasOne(b => b.State);
            #endregion

            #region [User]
            modelBuilder.Entity<User>()
                  .HasOne(b => b.UserRole);
            modelBuilder.Entity<User>()
                .HasOne(b => b.City)
                .WithMany()
                .HasForeignKey(b => b.CityId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<User>()
                   .HasOne(b => b.State)
                   .WithMany()
                   .HasForeignKey(b => b.StateId)
                   .OnDelete(DeleteBehavior.NoAction);
            #endregion

            #region [FlagActive - Index]
            modelBuilder.Entity<Media>()
               .HasIndex(_ => _.FlagActive);
            modelBuilder.Entity<User>()
               .HasIndex(_ => _.FlagActive);
            modelBuilder.Entity<UserRole>()
               .HasIndex(_ => _.FlagActive);
            #endregion            

            #region [Log]
            modelBuilder.Entity<Log>()
                .HasIndex(p => new { p.CreatedBy, p.Input, p.Code, p.Api, p.CreatedAt });
            #endregion

            CreateSeeds(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }

        private void CreateSeeds(ModelBuilder modelBuilder)
        {
            #region [Seed - State]
            var statesSeed = GetSeeds<StateSeed>($"Seeds{Path.DirectorySeparatorChar}JSONs{Path.DirectorySeparatorChar}states.json");
            var states = statesSeed.Select(ss => new State() { Id = ss.Id, Name = ss.Name, FlagActive = true, Abbreviation = ss.Abbreviation });
            modelBuilder.Entity<State>().HasData(states);
            #endregion

            #region [Seed - City]
            var citiesSeed = GetSeeds<CitySeed>($"Seeds{Path.DirectorySeparatorChar}JSONs{Path.DirectorySeparatorChar}cities.json");
            var cities = citiesSeed.Select(cs => new City() { Id = cs.Id, Name = cs.Name, FlagActive = true, StateId = cs.StateId });
            modelBuilder.Entity<City>().HasData(cities);
            #endregion

            #region [Seed - UserRoles]
            var seedsRoles = GetSeeds<HassUserRoleSeed>($"Seeds{Path.DirectorySeparatorChar}JSONs{Path.DirectorySeparatorChar}roles.json");
            var modelsRoles = seedsRoles.Select(data => new UserRole() { Id = data.Id, Name = data.Name, FlagActive = true, Description = data.Description, SystemCode = data.SystemCode });
            modelBuilder.Entity<UserRole>().HasData(modelsRoles);
            #endregion

            DateTime dateSeed = new DateTime(2020, 8, 1);

            #region [Seed - User]
            var userSeed = GetSeeds<UserSeed>(@"Seeds\JSONs\user.json");
            var users = userSeed.Select(data => new User()
            {
                Id = data.Id
                ,
                FullName = data.FullName
                ,
                UserName = data.UserName
                ,
                Email = data.Email
                ,
                PasswordHash = data.PasswordHash
                ,
                BirthDate = data.BirthDate
                ,
                CityId = data.CityId
                ,
                StateId = data.StateId
                ,
                UserRoleId = data.UserRoleId
                ,
                LastGuidAuthentication = data.LastGuidAuthentication
                ,
                FirstAccess = data.FirstAccess
                ,
                RefreshToken = data.RefreshToken
                ,
                FlagActive = data.FlagActive
                ,
                CreatedBy = data.CreatedBy
                ,
                CreatedAt = data.CreatedAt
                ,
                ModifiedBy = data.ModifiedBy
                ,
                ModifiedAt = data.ModifiedAt
            });
            modelBuilder.Entity<User>().HasData(users);
            #endregion
        }

        private List<T> GetSeeds<T>(string jsonPath)
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), jsonPath);
            var json = System.IO.File.ReadAllText(path, Encoding.UTF8);
            return JsonConvert.DeserializeObject<List<T>>(json);
        }
    }
}