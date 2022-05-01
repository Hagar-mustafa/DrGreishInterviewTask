using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleMiniProject.Models
{
    public class ReceivedSample
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "*مطلوب")]
        [StringLength(int.MaxValue ,MinimumLength = 2, ErrorMessage = "*اسم العينه يجب الا يقل عن 2 حروف ")]
        [DataType(DataType.Text)]
        public string SampleName { get; set; }
        [ForeignKey("Client")]
        public int ClientId { get; set; }
        [Required(ErrorMessage = "*مطلوب")]
        public string ReceivingSide { get; set; }
        //[Required(ErrorMessage = "*مطلوب")]
        [NotMapped]
        public List<IFormFile> Attach { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "*مطلوب")]
        [StringLength(int.MaxValue, MinimumLength = 5, ErrorMessage = "*وصف العينه يجب الا يقل عن 5 حروف")]
        [DataType(DataType.Text)]
        public string SampleDescription { get; set; }
        [Required(ErrorMessage = "*مطلوب")]
        public int NumOfSamples { get; set; }
        [Required(ErrorMessage = "*مطلوب")]
        public string SampleStatus { get; set; }
        public Client Client { get; set; }
        public ICollection<Attachment> Attachments { get; set; }
        public ReceivedSample()
        {
            Attachments = new List<Attachment>();
            Attach=new List<IFormFile>();
        }
    }
}
