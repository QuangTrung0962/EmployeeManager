using BUS.Interfaces;
using System.Web.Mvc;

namespace GUI.Controllers
{
    public class QualificationsController : Controller
    {
        private readonly IQualificationBus _qualificationBUS;

        public QualificationsController(IQualificationBus qualificationBus)
        {
            _qualificationBUS = qualificationBus;
        }

        // GET: Qualification
        public ActionResult Index(string searchString)
        {
            var qualifications =
                _qualificationBUS.GetQualificationsData(searchString);

            return View(qualifications);
        }
    }
}