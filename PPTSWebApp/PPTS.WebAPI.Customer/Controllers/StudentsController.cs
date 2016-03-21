using System.Web.Http;

namespace PPTS.WebAPI.Customer.Controllers
{
    public class StudentsController : ApiController
    {
        ////GET: api/students/index
        //[HttpGet]
        //public DataResult Index()
        //{
        //    Thread.Sleep(100);

        //    DataResult result = new DataResult()
        //    {
        //        Data = new
        //        {
        //            PagedList = StudentService.GetIndexStudentList()
        //        }
        //    };

        //    return result;
        //}

        ////POST: api/students/getlist
        //[HttpPost]
        //public DataResult GetList(StudentSimpleSearchCriteria criteria)
        //{
        //    Thread.Sleep(100);

        //    DataResult result = new DataResult()
        //    {
        //        Data = new
        //        {
        //            PagedList = StudentService.SimpleSearchStudentList(criteria)
        //        }
        //    };

        //    return result;
        //}

        ////POST: api/students/advancesearch
        //[HttpPost]
        //public DataResult AdvanceSearch(StudentSimpleSearchCriteria criteria)
        //{
        //    Thread.Sleep(100);

        //    DataResult result = new DataResult(1, "此功能还未实现")
        //    {
        //        Data = new
        //        {
        //            PagedList = StudentService.AdvanceSearchStudentList(criteria)
        //        }
        //    };

        //    return result;
        //}
    }
}
