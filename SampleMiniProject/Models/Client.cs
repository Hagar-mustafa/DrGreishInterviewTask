using System.Collections.Generic;

namespace SampleMiniProject.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string address { get; set; }
        public ICollection<ReceivedSample>  ReceivedSamples { get; set; }

    }
}
