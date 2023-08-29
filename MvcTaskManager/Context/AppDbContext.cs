using Microsoft.EntityFrameworkCore;
using MvcTaskManager.Entities;

namespace MvcTaskManager.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base (options) { }
        
        public DbSet<Tarefa> Tarefas { get; set; }
        
        public DbSet<MvcTaskManager.Entities.Usuario> Usuario { get; set; }
    }
}
