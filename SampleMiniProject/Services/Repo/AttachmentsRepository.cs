using SampleMiniProject.Data;
using System.Collections.Generic;
using SampleMiniProject.Models;
using System.Linq;

namespace SampleMiniProject.Services.Repo
{
    public class AttachmentsRepository : IAttachmentsRepository
    {
        protected CompanyDB ContextDb;
        public AttachmentsRepository(CompanyDB _ContextDb)
        {
            ContextDb = _ContextDb;
        }
        public List<Attachment> Get()
        {
            return ContextDb.Attachment.ToList();
        }
        public List<Attachment> GetByRecievedSample(int id)
        {
            return ContextDb.Attachment.Where(ww => ww.ReceivedSampleID == id).ToList();
        }
        //Delete by FileName
        public int Delete(string Name)
        {
            Attachment DelAttachment =ContextDb.Attachment.FirstOrDefault(ww=>ww.FileName == Name);
            ContextDb.Remove(DelAttachment);
            return ContextDb.SaveChanges();
        }
    }
}
