using SampleMiniProject.Models;
using System.Collections.Generic;

namespace SampleMiniProject.Services.Repo
{
    public interface IClientRepository
    {
        List<Client> Get();
    }
}