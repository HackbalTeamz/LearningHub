//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LearningHub.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class StudentExamResultTbl
    {
        public long StudentExamResultID { get; set; }
        public Nullable<long> StudentExamID { get; set; }
        public Nullable<long> ExamQuestionID { get; set; }
        public string SelectedAnswer { get; set; }
        public Nullable<bool> IsTrue { get; set; }
        public Nullable<System.DateTime> AttendTime { get; set; }
    
        public virtual ExamQuestionTbl ExamQuestionTbl { get; set; }
        public virtual StudentExamTbl StudentExamTbl { get; set; }
    }
}
