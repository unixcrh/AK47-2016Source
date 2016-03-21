using System;

namespace PPTS.WebAPI.Customer.ViewModels.Student
{
    public class StudentViewModel
    {       
        public string Name { get; set; }
        public string Code { get; set; }
        public string ParentsName { get; set; }
        public DateTime? FirstContractDate { get; set; }
        public string SchoolName { get; set; }
        public string GradeName { get; set; }
        public string TeacherXG { get; set; }
        public string TeacherZX { get; set; }
        public int? ContractCount { get; set; }
        public int? RemainCount { get; set; }
        public decimal? AccountValue { get; set; }
        public decimal? BalanceValue { get; set; }
        public decimal? AvalibleValue { get; set; }
        public int? HourFromLastClass { get; set; }

        //学员状态
        public int Status { get; set; }
        //联系方式
        public string Contact { get; set; }
    }
}
