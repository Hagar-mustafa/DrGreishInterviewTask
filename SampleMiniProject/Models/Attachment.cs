using System.ComponentModel.DataAnnotations.Schema;

namespace SampleMiniProject.Models
{
    public class Attachment
    {
        public int Id { get; set; }
        public string  FileName { get; set; }

        [ForeignKey("ReceivedSample")]
        public int ReceivedSampleID { get; set; }
        public ReceivedSample ReceivedSample { get; set; }
    }
}
