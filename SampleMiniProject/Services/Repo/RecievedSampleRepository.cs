using Microsoft.EntityFrameworkCore;
using SampleMiniProject.Data;
using SampleMiniProject.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SampleMiniProject.Services.Repo
{
    public class RecievedSampleRepository : IRecievedSampleRepository
    {
        protected CompanyDB DbContext { get; set; }
        public RecievedSampleRepository(CompanyDB _DbContext)
        {
            DbContext = _DbContext;
        }
        // Get All ReceivedSamples From our Database-------------------------
        public List<ReceivedSample> Get()
        {
            return DbContext.ReceivedSamples.Include(ww=>ww.Client).ToList();
        }
        //Get by ReceivedsampleId-------------------
        public ReceivedSample Get(int id)
        {
            return DbContext.ReceivedSamples.Include(cc=>cc.Client).FirstOrDefault(ww=>ww.ID == id);
        }
        public int Update(int id,ReceivedSample New_Re_sample)
        {
            ReceivedSample Old_re_Sample= DbContext.ReceivedSamples.Include(FF=>FF.Attachments).FirstOrDefault(ww => ww.ID == id);
            if (Old_re_Sample != null)
            {
                Old_re_Sample.SampleStatus=New_Re_sample.SampleStatus ;
                Old_re_Sample.NumOfSamples=New_Re_sample.NumOfSamples;
                Old_re_Sample.SampleDescription=New_Re_sample.SampleDescription;
                Old_re_Sample.Date =New_Re_sample.Date;
                Old_re_Sample.ClientId=New_Re_sample.ClientId;
                Old_re_Sample.SampleName=New_Re_sample.SampleName;                
                Old_re_Sample.SampleStatus=New_Re_sample.SampleStatus;
            }
            return DbContext.SaveChanges();

        }
        public int Create(ReceivedSample NewReceivedSample)
        {
            DbContext.ReceivedSamples.Add(NewReceivedSample);
            if (NewReceivedSample.Attach.Count > 0)
            {
                foreach (var file in NewReceivedSample.Attach)
                {
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    string FileNameWithPath = Path.Combine(path, file.FileName);
                    using (var stream = new FileStream(FileNameWithPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    NewReceivedSample.Attachments.Add(new Attachment() { FileName = file.FileName, ReceivedSampleID = NewReceivedSample.ID });
                }
            }
            DbContext.SaveChanges();
            return NewReceivedSample.ID;
        }
        public int Delete(int id)
        {
            ReceivedSample Sample=DbContext.ReceivedSamples.FirstOrDefault(ww=>ww.ID==id);
            DbContext.Remove(Sample);
            return DbContext.SaveChanges();
        }

    }
}
