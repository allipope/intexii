using intexii.Models;
using intexii.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;


namespace intexii.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ebdbContext ebdbContext { get; set; }


        public HomeController(IebdbContextRepository temp, ILogger<HomeController> logger, ebdbContext ebdb)
        {
            repo = temp;
            _logger = logger;
            ebdbContext = ebdb;
        }


        private IebdbContextRepository repo;


        public IActionResult BurialRecords(string Sex = null, string Depth = null, int pageNum = 1)
        {
            int pageSize = 10;
            if (Sex != null)
            {

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
                                   join ct in repo.ColorTextiles
                                   on t.Id equals ct.MainTextileid into ctass
                                   from cta in ctass.DefaultIfEmpty()
                                   join c in repo.Colors
                                   on cta.MainColorid equals c.Id into biga
                                   from ba in biga.DefaultIfEmpty()
                                   join st in repo.StructureTextiles
                                   on t.Id equals st.MainTextileid into stass
                                   from sta in stass.DefaultIfEmpty()
                                   join s in repo.Structures
                                   on sta.MainStructureid equals s.Id into bigb
                                   from bb in bigb.DefaultIfEmpty()
                                   where bm.Sex == Sex
                                   select new BurialPageModel
                                   {
                                       Id = bm.Id,
                                       Dateofexcavation = bm.Dateofexcavation,
                                       Squarenorthsouth = bm.Squarenorthsouth,
                                       Squareeastwest = bm.Squareeastwest,
                                       Depth = bm.Depth,
                                       Sex = bm.Sex,
                                       Locale = t.Locale,
                                       Color = ba.Value,
                                       Structure = bb.Value,
                                       TextileDescription = t.Description,
                                       Ageatdeath = bm.Ageatdeath

                                   })
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


            else if (Depth != null)
            {

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
                                   join ct in repo.ColorTextiles
                                   on t.Id equals ct.MainTextileid into ctass
                                   from cta in ctass.DefaultIfEmpty()
                                   join c in repo.Colors
                                   on cta.MainColorid equals c.Id into biga
                                   from ba in biga.DefaultIfEmpty()
                                   join st in repo.StructureTextiles
                                   on t.Id equals st.MainTextileid into stass
                                   from sta in stass.DefaultIfEmpty()
                                   join s in repo.Structures
                                   on sta.MainStructureid equals s.Id into bigb
                                   from bb in bigb.DefaultIfEmpty()
                                   where bm.Depth == Depth
                                   select new BurialPageModel
                                   {
                                       Id = bm.Id,
                                       Dateofexcavation = bm.Dateofexcavation,
                                       Squarenorthsouth = bm.Squarenorthsouth,
                                       Squareeastwest = bm.Squareeastwest,
                                       Depth = bm.Depth,
                                       Sex = bm.Sex,
                                       Locale = t.Locale,
                                       Color = ba.Value,
                                       Structure = bb.Value,
                                       TextileDescription = t.Description,
                                       Ageatdeath = bm.Ageatdeath
                                   })
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

            else
            {

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
                                   join ct in repo.ColorTextiles
                                   on t.Id equals ct.MainTextileid into ctass
                                   from cta in ctass.DefaultIfEmpty()
                                   join c in repo.Colors
                                   on cta.MainColorid equals c.Id into biga
                                   from ba in biga.DefaultIfEmpty()
                                   join st in repo.StructureTextiles
                                   on t.Id equals st.MainTextileid into stass
                                   from sta in stass.DefaultIfEmpty()
                                   join s in repo.Structures
                                   on sta.MainStructureid equals s.Id into bigb
                                   from bb in bigb.DefaultIfEmpty()
                                   select new BurialPageModel

                                   {
                                       Id = bm.Id,
                                       Dateofexcavation = bm.Dateofexcavation,
                                       Squarenorthsouth = bm.Squarenorthsouth,
                                       Squareeastwest = bm.Squareeastwest,
                                       Depth = bm.Depth,
                                       Sex = bm.Sex,
                                       Locale = t.Locale,
                                       Color = ba.Value,
                                       Structure = bb.Value,
                                       TextileDescription = t.Description,
                                       Ageatdeath = bm.Ageatdeath
                                   })
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

        }




        public IActionResult ViewMore(long Id)
        {
            var y = new BurialViewModel
            {
                SummaryViews = (from t in repo.Textiles
                                join bmt in repo.BurialmainTextiles
                                on t.Id equals bmt.MainTextileid
                                join bm in repo.Burialmains
                                on bmt.MainBurialmainid equals bm.Id
                                join ct in repo.ColorTextiles
                                on t.Id equals ct.MainTextileid into ctass
                                from cta in ctass.DefaultIfEmpty()
                                join c in repo.Colors
                                on cta.MainColorid equals c.Id into biga
                                from ba in biga.DefaultIfEmpty()
                                join st in repo.StructureTextiles
                                on t.Id equals st.MainTextileid into stass
                                from sta in stass.DefaultIfEmpty()
                                join s in repo.Structures
                                on sta.MainStructureid equals s.Id into bigb
                                from bb in bigb.DefaultIfEmpty()
                                where bm.Id == Id
                                select new SummaryPageModel
                                {
                                    Locale = t.Locale,
                                    Dateofexcavation = bm.Dateofexcavation,
                                    Squarenorthsouth = bm.Squarenorthsouth,
                                    Squareeastwest = bm.Squareeastwest,
                                    Depth = bm.Depth,
                                    Headdirection = bm.Headdirection,
                                    AgeatDeath = bm.Ageatdeath,
                                    Sex = bm.Sex,
                                    Burialnumber = bm.Burialnumber,
                                    Estimatedperiod = t.Estimatedperiod,
                                    TextileDescription = t.Description,
                                    Wrapping = bm.Wrapping,
                                    Adultsubadult = bm.Adultsubadult,
                                    Text = bm.Text,
                                    Haircolor = bm.Haircolor,
                                    Color = ba.Value,
                                    Structure = bb.Value,
                                    Id = bm.Id




                                })
                            .ToList(),

                identification = (from bm in repo.Burialmains
                                  where bm.Id == Id
                                  select new SummaryPageModel
                                  {
                                      Id = bm.Id
                                  }).ToList()



            };
            return View(y);
        }



        [Authorize(Roles = "Admin")]
        public IActionResult Admin()
        {
            return View();
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Predict()
        {
            return View();
        }


        //add burial main record
        [Authorize(Roles = "Admin,Researcher")]
        [HttpGet]
        public IActionResult AddRecord()
        {
            return View("AddRecord", new Burialmain());
        }

        [Authorize(Roles = "Admin,Researcher")]
        [HttpPost]
        public IActionResult AddRecord(Burialmain mum)
        {
            ebdbContext.Add(mum);
            ebdbContext.SaveChanges();

            return RedirectToAction("BurialConfirmation");
        }

        //add burialmaintextile GET
        [Authorize(Roles = "Admin,Researcher")]
        [HttpGet]
        public IActionResult BurialConfirmation()
        {
            long bid = ebdbContext.Burialmains.Max(x => x.Id);
            long tid = ebdbContext.Bodyanalysischarts.Max(x => x.Id);

            var values = new { Bid = bid, Tid = tid + 1 };

            ViewBag.Values = values;

            return View(new BurialmainBodyanalysischart());
        }

        [Authorize(Roles = "Admin,Researcher")]
        [HttpPost]
        public IActionResult BurialConfirmation(BurialmainBodyanalysischart mumtex)
        {
            ebdbContext.Add(mumtex);
            ebdbContext.SaveChanges();

            return RedirectToAction("AddBodyAnalysis");
        }

        //body analysis check GET
        [Authorize(Roles = "Admin,Researcher")]
        [HttpGet]
        public IActionResult BodyAnalysisCheck()
        {
            long bid = ebdbContext.Burialmains.Max(x => x.Id);
            long tid = ebdbContext.Bodyanalysischarts.Max(x => x.Id);

            var values = new { Bid = bid, Tid = tid + 1 };

            ViewBag.Values = values;

            return View("BodyAnalysisCheck", new BurialmainBodyanalysischart());
        }

        //body analysis check POST
        [Authorize(Roles = "Admin,Researcher")]
        [HttpPost]
        public IActionResult BodyAnalysisCheck(BurialmainBodyanalysischart burbod)
        {
            ebdbContext.Add(burbod);
            ebdbContext.SaveChanges();

            return View();
        }

        //add ba GET

        [Authorize(Roles = "Admin,Researcher")]
        [HttpGet]
        public IActionResult AddBodyAnalysis()
        {
            long tid = ebdbContext.BurialmainBodyanalysischarts.Max(x => x.MainBodyanalysischartid);

            ViewBag.Did = tid;

            return View("AddBodyAnalysis", new Bodyanalysischart());
        }

        //add ba POST

        [Authorize(Roles = "Admin,Researcher")]
        [HttpPost]
        public IActionResult AddBodyAnalysis(Bodyanalysischart ba)
        {
            ebdbContext.Add(ba);
            ebdbContext.SaveChanges();
            return RedirectToAction("BodyAnalysisConfirmation");
        }

        //ba confirmation GET
        [Authorize(Roles = "Admin,Researcher")]
        [HttpGet]
        public IActionResult BodyAnalysisConfirmation()
        {
            long bid = ebdbContext.Burialmains.Max(x => x.Id);
            long tid = ebdbContext.Textiles.Max(x => x.Id);

            var values = new { Bid = bid, Tid = tid + 1 };

            ViewBag.Values = values;

            return View(new BurialmainTextile());
        }

        //ba confirmation POST
        [Authorize(Roles = "Admin,Researcher")]
        [HttpPost]
        public IActionResult BodyAnalysisConfirmation(BurialmainTextile anatex)
        {
            ebdbContext.Add(anatex);
            ebdbContext.SaveChanges();

            return RedirectToAction("AddTextile");
        }

        //add textile GET
        [Authorize(Roles = "Admin,Researcher")]
        [HttpGet]
        public IActionResult AddTextile()
        {
            long tid = ebdbContext.BurialmainTextiles.Max(x => x.MainTextileid);

            ViewBag.Tid = tid;

            return View("AddTextile", new Textile());
        }

        //add textile POST

        [Authorize(Roles = "Admin,Researcher")]
        [HttpPost]
        public IActionResult AddTextile(Textile tex)
        {
            ebdbContext.Add(tex);
            ebdbContext.SaveChanges();
            return RedirectToAction("TextileConfirmation");
        }

        //add textileanalysis GET

        [Authorize(Roles = "Admin,Researcher")]
        [HttpGet]
        public IActionResult TextileConfirmation()
        {
            long tid = ebdbContext.Textiles.Max(x => x.Id);
            long aid = ebdbContext.Analyses.Max(x => x.Id);

            var values = new { Tid = tid, Aid = aid + 1 };

            ViewBag.Values = values;

            return View(new AnalysisTextile());
        }

        //add textileanalysis POST

        [Authorize(Roles = "Admin,Researcher")]
        [HttpPost]
        public IActionResult TextileConfirmation(AnalysisTextile anatex)
        {
            ebdbContext.Add(anatex);
            ebdbContext.SaveChanges();

            return RedirectToAction("AddAnalysis");
        }

        //add analysis GET

        [Authorize(Roles = "Admin,Researcher")]
        [HttpGet]
        public IActionResult AddAnalysis()
        {
            long aid = ebdbContext.AnalysisTextiles.Max(x => x.MainAnalysisid);

            ViewBag.Aid = aid;

            return View("AddAnalysis", new Analysis());
        }

        //Add analysis POST

        [Authorize(Roles = "Admin,Researcher")]
        [HttpPost]
        public IActionResult AddAnalysis(Analysis ana)
        {
            ebdbContext.Add(ana);
            ebdbContext.SaveChanges();
            return RedirectToAction("AnalysisConfirmation");
        }


        //dec Textile GET
        [Authorize(Roles = "Admin,Researcher")]
        [HttpGet]
        public IActionResult AnalysisConfirmation()
        {
            long tid = ebdbContext.Textiles.Max(x => x.Id);
            long did = ebdbContext.Decorations.Max(x => x.Id);

            var values = new { Tid = tid, Did = did + 1 };

            ViewBag.Values = values;

            return View(new DecorationTextile());
        }

        //dec textile POST

        [Authorize(Roles = "Admin,Researcher")]
        [HttpPost]
        public IActionResult AnalysisConfirmation(DecorationTextile dectex)
        {
            ebdbContext.Add(dectex);
            ebdbContext.SaveChanges();

            return RedirectToAction("AddDecoration");
        }


        //add decoration GET

        [Authorize(Roles = "Admin,Researcher")]
        [HttpGet]
        public IActionResult AddDecoration()
        {
            long did = ebdbContext.DecorationTextiles.Max(x => x.MainDecorationid);

            ViewBag.Did = did;

            return View("AddDecoration", new Decoration());
        }

        //Add decoration POST

        [Authorize(Roles = "Admin,Researcher")]
        [HttpPost]
        public IActionResult AddDecoration(Decoration dec)
        {
            ebdbContext.Add(dec);
            ebdbContext.SaveChanges();
            return RedirectToAction("DecorationConfirmation");
        }
        //dimension Textile GET
        [Authorize(Roles = "Admin,Researcher")]
        [HttpGet]
        public IActionResult DecorationConfirmation()
        {
            long tid = ebdbContext.Textiles.Max(x => x.Id);
            long did = ebdbContext.Dimensions.Max(x => x.Id);

            var values = new { Tid = tid, Did = did + 1 };

            ViewBag.Values = values;

            return View(new DimensionTextile());
        }

        //dimension textile POST

        [Authorize(Roles = "Admin,Researcher")]
        [HttpPost]
        public IActionResult DecorationConfirmation(DimensionTextile dimtex)
        {
            ebdbContext.Add(dimtex);
            ebdbContext.SaveChanges();

            return RedirectToAction("AddDimension");
        }


        //add dimension GET

        [Authorize(Roles = "Admin,Researcher")]
        [HttpGet]
        public IActionResult AddDimension()
        {
            long did = ebdbContext.DimensionTextiles.Max(x => x.MainDimensionid);

            ViewBag.Did = did;

            return View("AddDimension", new Dimension());
        }

        //Add dimesion POST

        [Authorize(Roles = "Admin,Researcher")]
        [HttpPost]
        public IActionResult AddDimension(Dimension dim)
        {
            ebdbContext.Add(dim);
            ebdbContext.SaveChanges();
            return RedirectToAction("DimensionConfirmation");
        }


        //structure Textile GET
        [Authorize(Roles = "Admin,Researcher")]
        [HttpGet]
        public IActionResult DimensionConfirmation()
        {
            long tid = ebdbContext.Textiles.Max(x => x.Id);
            long did = ebdbContext.Structures.Max(x => x.Id);

            var values = new { Tid = tid, Did = did + 1 };

            ViewBag.Values = values;

            return View(new StructureTextile());
        }

        //structure textile POST

        [Authorize(Roles = "Admin,Researcher")]
        [HttpPost]
        public IActionResult DimensionConfirmation(StructureTextile strtex)
        {
            ebdbContext.Add(strtex);
            ebdbContext.SaveChanges();

            return RedirectToAction("AddStructure");
        }


        //add structure GET

        [Authorize(Roles = "Admin,Researcher")]
        [HttpGet]
        public IActionResult AddStructure()
        {
            long did = ebdbContext.StructureTextiles.Max(x => x.MainStructureid);

            ViewBag.Did = did;

            return View("AddStructure", new Structure());
        }

        //Add structure POST

        [Authorize(Roles = "Admin,Researcher")]
        [HttpPost]
        public IActionResult AddStructure(Structure str)
        {
            ebdbContext.Add(str);
            ebdbContext.SaveChanges();
            return RedirectToAction("StructureConfirmation");
        }


        //color Textile GET
        [Authorize(Roles = "Admin,Researcher")]
        [HttpGet]
        public IActionResult StructureConfirmation()
        {
            long tid = ebdbContext.Textiles.Max(x => x.Id);
            long did = ebdbContext.Colors.Max(x => x.Id);

            var values = new { Tid = tid, Did = did + 1 };

            ViewBag.Values = values;

            return View(new ColorTextile());
        }

        //color textile POST

        [Authorize(Roles = "Admin,Researcher")]
        [HttpPost]
        public IActionResult StructureConfirmation(ColorTextile coltex)
        {
            ebdbContext.Add(coltex);
            ebdbContext.SaveChanges();

            return RedirectToAction("AddColor");
        }


        //add color GET

        [Authorize(Roles = "Admin,Researcher")]
        [HttpGet]
        public IActionResult AddColor()
        {
            long did = ebdbContext.ColorTextiles.Max(x => x.MainColorid);

            ViewBag.Did = did;

            return View("AddColor", new Color());
        }

        //Add color POST

        [Authorize(Roles = "Admin,Researcher")]
        [HttpPost]
        public IActionResult AddColor(Color col)
        {
            ebdbContext.Add(col);
            ebdbContext.SaveChanges();
            return RedirectToAction("ColorConfirmation");
        }

        //textilefunction Textile GET
        [Authorize(Roles = "Admin,Researcher")]
        [HttpGet]
        public IActionResult ColorConfirmation()
        {
            long tid = ebdbContext.Textiles.Max(x => x.Id);
            long did = ebdbContext.Textilefunctions.Max(x => x.Id);

            var values = new { Tid = tid, Did = did + 1 };

            ViewBag.Values = values;

            return View(new TextilefunctionTextile());
        }

        //textilefunction textile POST

        [Authorize(Roles = "Admin,Researcher")]
        [HttpPost]
        public IActionResult ColorConfirmation(TextilefunctionTextile funtex)
        {
            ebdbContext.Add(funtex);
            ebdbContext.SaveChanges();

            return RedirectToAction("AddTextileFunction");
        }


        //add textilefunction GET

        [Authorize(Roles = "Admin,Researcher")]
        [HttpGet]
        public IActionResult AddTextileFunction()
        {
            long did = ebdbContext.TextilefunctionTextiles.Max(x => x.MainTextilefunctionid);

            ViewBag.Did = did;

            return View("AddTextileFunction", new Textilefunction());
        }

        //Add textilefunction POST

        [Authorize(Roles = "Admin,Researcher")]
        [HttpPost]
        public IActionResult AddTextileFunction(Textilefunction fun)
        {
            ebdbContext.Add(fun);
            ebdbContext.SaveChanges();
            return RedirectToAction("TextileFunctionConfirmation");
        }

        //textile function confirmation GET
        [Authorize(Roles = "Admin,Researcher")]
        public IActionResult TextileFunctionConfirmation()
        {
            long tid = ebdbContext.Textiles.Max(x => x.Id);
            long did = ebdbContext.Textilefunctions.Max(x => x.Id);

            var values = new { Tid = tid, Did = did };

            ViewBag.Values = values;

            return View();
        }

        //START EDITING

        [Authorize(Roles = "Admin,Researcher")]
        [HttpGet]
        public IActionResult EditRecord(long id)
        {
            var bm = ebdbContext.Burialmains.ToList();
            var submission = ebdbContext.Burialmains.Single(x => x.Id == id);
            return View("AddRecord", submission);
        }

        [Authorize(Roles = "Admin,Researcher")]
        [HttpPost]
        public IActionResult EditRecord(Burialmain mum)
        {
            ebdbContext.Update(mum);
            ebdbContext.SaveChanges();
            return RedirectToAction("BurialRecords");
        }

        [Authorize(Roles = "Admin,Researcher")]
        [HttpGet]
        public IActionResult DeleteBurial(long id)
        {
            var submission = ebdbContext.Burialmains.Single(x => x.Id == id);

            //var tid = ebdbContext.BurialmainTextiles.Single(x => x.MainBurialmainid == submission.Id);

            //ViewBag.Tid = tid;

            return View(submission);
        }

        [Authorize(Roles = "Admin,Researcher")]
        [HttpPost]
        public IActionResult DeleteBurial(Burialmain mum)
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