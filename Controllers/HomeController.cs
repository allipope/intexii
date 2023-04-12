using intexii.Models;
using intexii.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;


namespace intexii.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ebdbContext ebdbContext { get; set; }


        public HomeController(IebdbContextRepository temp,  ILogger<HomeController> logger, ebdbContext ebdb)
        {
            repo = temp;
            _logger = logger;
            ebdbContext = ebdb;
        }


        private IebdbContextRepository repo;
        public IActionResult BurialRecords(int pageNum = 1)
        {
            int pageSize = 10;
            var x = new BurialViewModel
            {
                Burialmains = repo.Burialmains
                .OrderBy(b => b.Area)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize),

                BurialViews = (from t in repo.Textiles
                               join bmt in repo.BurialmainTextiles
                               on t.Id equals bmt.MainTextileid
                               join bm in repo.Burialmains
                               on bmt.MainBurialmainid equals bm.Id
                               select new BurialPageModel
                               {
                                   Id = bm.Id,
                                   Dateofexcavation = bm.Dateofexcavation,
                                   Squarenorthsouth = bm.Squarenorthsouth,
                                   Squareeastwest = bm.Squareeastwest,
                                   Depth = bm.Depth,
                                   Sex = bm.Sex,


                                   TextileDescription = t.Description
                               })
                            .OrderBy(b => b.Dateofexcavation)
                            .ToList(),

                PageInfo = new PageInfo
                {
                    TotalNumProjects = repo.Burialmains.Count(),
                    ProjectsPerPage = pageSize,
                    CurrentPage = pageNum
                }
            };

            return View(x);
        }

        public IActionResult ViewMore(int Id)
        {
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }

        // public IActionResult Privacy()
        // {
        //     return View();
        // }

        [HttpGet]
        public IActionResult AddRecord()
        {
            return View("AddRecord", new Burialmain());
        }

        [HttpPost]
        public IActionResult AddRecord(Burialmain mum)
        {
            // if (ModelState.IsValid)
            // {
                ebdbContext.Add(mum);
                ebdbContext.SaveChanges();
                return View("RecordConfirmation", mum);
            // }

            // else
            // {
            //     ViewBag.burialmain = MummyContext.burialmain.ToList();
            //     return View();
            // }
        }

        [HttpGet]
        public IActionResult EditRecord(int id)
        {
            var bm = ebdbContext.Burialmains.ToList();
            var submission = ebdbContext.Burialmains.Single(x => x.Id == id);
            return View("AddRecord", submission);
        }

        [HttpPost]
        public IActionResult EditRecord(Burialmain mum)
        {
            ebdbContext.Update(mum);
            ebdbContext.SaveChanges();
            return RedirectToAction("BurialRecords");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var submission = ebdbContext.Burialmains.Single(x => x.Id == id);
            return View(submission);
        }

        [HttpPost]
        public IActionResult Delete(Burialmain mum)
        {
            ebdbContext.Burialmains.Remove(mum);
            ebdbContext.SaveChanges();
            return RedirectToAction("BurialRecords");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}