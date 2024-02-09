using iSchoolWebApp.Models;
using iSchoolWebApp.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Dynamic;

namespace iSchoolWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> About()
        {
            //build a model to hold my data
            //go and get the data
            DataRetrieval dataR = new DataRetrieval();
            var loadedAbout = await dataR.GetData("about/");
            //turn the data into JSON
            //cast the JSON into my Model
            var rtnResult = JsonConvert.DeserializeObject<AboutModel>(loadedAbout);
            //add to the aboutmodel the pageTitle
            rtnResult.pageTitle = "About iSchool";
            //add more data to my Model
            //end the model to the View
            return View(rtnResult);
        }
        public async Task<IActionResult> Degrees()
        {
            DataRetrieval dataR = new DataRetrieval();
            var loadedDegrees= await dataR.GetData("degrees/");
            var rtnResult = JsonConvert.DeserializeObject<DegreesModel>(loadedDegrees);
            //add to the degreesmodel the pageTitle
            rtnResult.pageTitle = "Our Degrees";
            return View(rtnResult);
        }
        public async Task<IActionResult> Minors()
        {
            DataRetrieval dataR = new DataRetrieval();
            var loadedMinors = await dataR.GetData("minors/");
            var rtnResult = JsonConvert.DeserializeObject<MinorsModel>(loadedMinors);

            //dict to hold the course data
            var coursesDict = new Dictionary<string, object>();

            //iterate through rtnResult minor courses and add to coursesDict
            foreach (var minor in rtnResult.UgMinors)
            {
                foreach (var course in minor.courses)
                {
                    // makes sure there is no duplicate keys
                    if (!coursesDict.ContainsKey(course)) 
                    {
                        var loadedCourse = await dataR.GetData("course/courseID=" + course);
                        var courseRtnResult = JsonConvert.DeserializeObject<CourseModel>(loadedCourse);
                        coursesDict.Add(course, courseRtnResult);
                    }
                }
            }

            //using System.dynamic
            dynamic expando = new ExpandoObject();
            var comboModel = expando as IDictionary<string, object>;

            comboModel.Add("Minors", rtnResult);
            comboModel.Add("Courses", coursesDict);
            comboModel.Add("pageTitle", "Minors");

            return View(comboModel);
        }
        public async Task<IActionResult> Employment()
        {
            DataRetrieval dataR = new DataRetrieval();
            var loadedEmployment = await dataR.GetData("employment/");
            var rtnResult = JsonConvert.DeserializeObject<EmploymentModel>(loadedEmployment);
            //add to the employmentmodel the pageTitle
            rtnResult.pageTitle = "Employment";
            return View(rtnResult);
        }
        public async Task<IActionResult> People()
        {  
            DataRetrieval dataR = new DataRetrieval();
            var loadedPeople = await dataR.GetData("people/");
            var rtnResult = JsonConvert.DeserializeObject<PeopleModel>(loadedPeople);
            //add to the peoplemodel the pageTitle
            rtnResult.pageTitle = "Our People";
            return View(rtnResult);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}