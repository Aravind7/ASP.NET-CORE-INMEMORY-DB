using Microsoft.EntityFrameworkCore;

namespace EF_Demo.Models
{
    public class ApplicationDbContext:DbContext 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> context):base(context)  
    {  
  
    }  

    public DbSet<Workshop> Workshops { get; set; }  
    
    }
}