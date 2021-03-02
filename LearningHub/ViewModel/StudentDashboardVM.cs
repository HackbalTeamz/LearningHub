using LearningHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearningHub.ViewModel
{
    public class StudentDashboardVM
    {
        public List<SubjectVideoVM> subjectVideoVMs { get; set; }
        public List<StudentNotificationVM> studentNotificationVMs { get; set; }
        public List<SubjectTbl> subjectTbls { get; set; }
        public List<StudyMeterialTbl> studyMeterialTbls { get; set; }
        public IEnumerable<StudentAssignmentTbl> studentAssignmentTbls { get; set; }
        public StudentAssignmentTbl studentAssignmentTbl { get; set; }
        public List<VideoTbl> videoTbls { get; set; }

        public OnlineExamVM onlineExamVM { get; set; }
    }
    public class SubjectVideoVM
    {
        public string SubjectName { get; set; }
        public int VideoCount { get; set; }
    }
    public class StudentNotificationVM
    {
        public string Message { get; set; }
        public string Time { get; set; }
        public long LinkID { get; set; }
    }
}