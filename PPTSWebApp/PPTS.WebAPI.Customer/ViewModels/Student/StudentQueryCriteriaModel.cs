using System;

namespace PPTS.WebAPI.Customer.ViewModels.Student
{
    [Serializable]
    public class StudentQueryCriteriaModel
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string TeacherZX { get; set; }
        public string TeacherXG { get; set; }
        public string Contact { get; set; }
        public PagedParam PagedParam { get; set; }
    }
}
