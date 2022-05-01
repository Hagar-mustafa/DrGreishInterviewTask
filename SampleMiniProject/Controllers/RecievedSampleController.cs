using Microsoft.AspNetCore.Mvc;
using SampleMiniProject.Models;
using SampleMiniProject.Services.Repo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SampleMiniProject.Controllers
{
    public class RecievedSampleController : Controller
    {
        protected IRecievedSampleRepository receivedSampleService;
        protected IClientRepository clientService;
        protected IAttachmentsRepository attachmentsService;
        public RecievedSampleController(IRecievedSampleRepository _receivedSampleService,IClientRepository _clientService,IAttachmentsRepository _attchmentsServic)
        {
            receivedSampleService = _receivedSampleService;
            clientService = _clientService;
            attachmentsService = _attchmentsServic;
        }
        // Index to Show All RecievedSamples 
        // SearchTxt parameter is the entered string in th Search inputField to filter table
        // SampleNumber Is the Number entered in the Number InputFuield to filter table
        public IActionResult Index(string SearchTxt,string SampleNumber)      
        {
            ViewData["CurrentFilter"] = SearchTxt;
            ViewData["SampleNumber"] = SampleNumber;
            List<ReceivedSample>Samples= receivedSampleService.Get();
            ViewBag.Attachments = attachmentsService.Get();
            if (!String.IsNullOrEmpty(SearchTxt))
            {
                Samples = Samples.Where(ww => ww.SampleName.ToLower().Contains(SearchTxt) ||
                          ww.SampleDescription.ToLower().Contains(SearchTxt) ||
                          ww.NumOfSamples.ToString().Contains(SearchTxt) ||
                          ww.Date.ToString().Contains(SearchTxt) ||
                          ww.SampleStatus.ToLower().Contains(SearchTxt) ||
                          ww.Client.Name.ToLower().Contains(SearchTxt)).ToList();
            }
            if (!String.IsNullOrEmpty(SampleNumber))
            {
                Samples=Samples.Take(int.Parse(SampleNumber)).ToList();
            }
                return View(Samples);
        }
        // DownloadFile function used to enable download attachments
        public FileResult DownloadFile(string fileName)
        {
            //Build the File Path.
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files");
            string FileNameWithPath = Path.Combine(path, fileName);
            //Read the File data into Byte Array.
            byte[] bytes = System.IO.File.ReadAllBytes(FileNameWithPath);

            //Send the File to Download.
            return File(bytes, "application/octet-stream", fileName);
        }
        //return Form to Edit  RecievedSample-------------------------------------------
        public IActionResult Edit(int id)
        {
            ReceivedSample OldSample=new ReceivedSample();
            OldSample= receivedSampleService.Get(id);
            ViewBag.Clients = clientService.Get();
            ViewBag.attachments=attachmentsService.GetByRecievedSample(id);
            return View(OldSample);
        }
        //the ActionMethod to Confirm add After Submitted---------------------------------
        [HttpPost]
        public async Task<IActionResult> Edit(int id,ReceivedSample New_Re_Sample)
        {
            if ( ModelState.IsValid == true)
            {
                if(New_Re_Sample.Attach.Count > 0)
                {
                    ReceivedSample OldSample = receivedSampleService.Get(id);
                    // Find Old Attachments and delete them -----------------------------------
                    List<Attachment> Attachments = attachmentsService.GetByRecievedSample(OldSample.ID);
                    foreach (var file in Attachments)
                    {
                        string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files/" + file.FileName);
                        if (System.IO.File.Exists(path))
                        {
                            attachmentsService.Delete(file.FileName);
                            System.IO.File.Delete(path);
                        }
                    }
                    //--------------------------------------
                    // for each to update to new attachments------------------------------------ 
                    foreach (var file in New_Re_Sample.Attach)
                    {
                        string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files");
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        string FileNameWithPath=Path.Combine(path,file.FileName);
                        using (var stream = new FileStream(FileNameWithPath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }
                        //add new attachments
                        OldSample.Attachments.Add(new Attachment() { FileName = file.FileName, ReceivedSampleID = New_Re_Sample.ID });
                    }
                }
                receivedSampleService.Update(id, New_Re_Sample);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Clients = clientService.Get();
                return View(New_Re_Sample);
            }
        }
        //return Form to Add New RecievedSample-------------------------------------------
        public IActionResult Add()
        {
            ViewBag.Clients= clientService.Get();
            ReceivedSample NewRecievedSample=new ReceivedSample();
            return View(NewRecievedSample);
        }
        [HttpPost]
        //the ActionMethod to Confirm add After Submitted---------------------------------
        public IActionResult Add(ReceivedSample New_Re_Sample)
        {
            if (ModelState.IsValid)
            {
                receivedSampleService.Create(New_Re_Sample);
                return RedirectToAction("Index");
            }
            ViewBag.Clients=clientService.Get();
            return View(New_Re_Sample);
        }
        #region PartialViewResualt

        //public PartialViewResult SearchSamples(string SearchTxt)
        //{
        //    List<ReceivedSample> ReceivedSamples=receivedSampleService.Get();
        //    var result=ReceivedSamples.Where(ww=>ww.SampleName.ToLower().Contains(SearchTxt)||
        //                            ww.SampleDescription.ToLower().Contains(SearchTxt)||
        //                            ww.NumOfSamples.ToString().Contains(SearchTxt)||
        //                            ww.Date.ToString().Contains(SearchTxt)||
        //                            ww.SampleStatus.ToLower().Contains(SearchTxt)||
        //                            ww.Client.Name.ToLower().Contains(SearchTxt)).ToList();
        //    return PartialView("_TableSamples", result);
        //}
        #endregion
        public IActionResult Delete(int id)
        {
            ReceivedSample Sample=receivedSampleService.Get(id);
            List<Attachment>Attachments=attachmentsService.GetByRecievedSample(id);
            foreach(var file in Attachments)
            {
                string path=Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/Files/"+ file.FileName);
                if (System.IO.File.Exists(path))
                {
                    attachmentsService.Delete(file.FileName);
                    System.IO.File.Delete(path);
                }
            }               
            receivedSampleService.Delete(id);
            return RedirectToAction("Index");           
        }        
    }
}
