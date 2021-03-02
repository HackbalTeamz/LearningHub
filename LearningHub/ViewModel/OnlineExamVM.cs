using LearningHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearningHub.ViewModel
{
    public class OnlineExamVM
    {
        public StudentExamTbl studentExamTbl { get; set; }
        public List<StudentExamResultTbl> studentExamResultTbl { get; set; }
    }
}