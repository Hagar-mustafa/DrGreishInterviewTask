using SampleMiniProject.Data;
using SampleMiniProject.Models;
using System.Collections.Generic;
using System.Linq;

namespace SampleMiniProject.Services.Repo
{
    public class ClientRepository : IClientRepository
    {
        CompanyDB DbContext;
        public ClientRepository(CompanyDB _DbContext)
        {
            DbContext = _DbContext;
        }
        public List<Client> Get()
        {
            return DbContext.Clients.ToList();
        }
    }
}
