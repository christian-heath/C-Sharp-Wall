using Microsoft.EntityFrameworkCore;

namespace wall.Models
{
    public class wallContext : DbContext
    {
        public wallContext(DbContextOptions<wallContext> options) : base(options) {}
        public  DbSet<User> Users { get; set;}
        public  DbSet<Message> Messages { get; set;}
        public  DbSet<Comment> Comments { get; set;}
    }
}