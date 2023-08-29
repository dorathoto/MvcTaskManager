﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MvcTaskManager.Models;

namespace MvcTaskManager.Context
{
    public class AppDbContext : IdentityDbContext<Usuario, Funcao, Guid>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base (options) 
        { }
        
        public DbSet<Tarefa> Tarefas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
