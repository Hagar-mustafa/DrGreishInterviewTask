using SampleMiniProject.Models;
using System.Collections.Generic;

namespace SampleMiniProject.Services.Repo
{
    public interface IRecievedSampleRepository
    {
        List<ReceivedSample> Get();
        ReceivedSample Get(int id);
        public int Update(int id, ReceivedSample New_Re_sample);
        public int Create(ReceivedSample NewReceivedSample);
        public int Delete(int id);


    }
}