using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using MassagerApp.DAL.Models;

namespace MassagerApp.DAL.DataAccses
{
    public class DataAccsess : IdentityDbContext<UserEntity, RoleEntity, string>
    {
        public DataAccsess(DbContextOptions<DataAccsess> options) : base(options) { Database.EnsureCreated(); }

        public DbSet<ChatsEntity> Chats { get; set; }
        public DbSet<MassageEntity> Massages { get; set; }
    }
}
