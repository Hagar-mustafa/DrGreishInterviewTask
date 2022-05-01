using SampleMiniProject.Models;
using System.Collections.Generic;

namespace SampleMiniProject.Services.Repo
{
    public interface IAttachmentsRepository
    {
        List<Attachment> GetByRecievedSample(int id);
        public List<Attachment> Get();
        public int Delete(string Name);
    }
}