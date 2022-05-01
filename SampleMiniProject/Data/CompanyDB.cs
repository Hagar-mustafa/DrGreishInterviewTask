using Microsoft.EntityFrameworkCore;
using SampleMiniProject.Models;

namespace SampleMiniProject.Data
{
    public class CompanyDB:DbContext
    {
        public CompanyDB(DbContextOptions<CompanyDB> options) 
            : base(options)
        {

        }
        public DbSet<ReceivedSample> ReceivedSamples { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Attachment> Attachment { get; set; }
    }
}
