
using TarefasApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository{
    public class DataContext : DbContext{
        public DataContext(DbContextOptions options) : base(options){}

            public DbSet<Tarefa> Tarefas{get; set;}
            public DbSet<Usuario> Usuarios{get; set;}

    }
}