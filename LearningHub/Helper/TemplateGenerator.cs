using LearningHub.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace LearningHub.Helper
{
    public class TemplateGenerator
    {
        public static void CreateCSV(RegistrationVM RegVM)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Student Name,Student Phone, Parent Name, Parent Phone, Parent Email\r\n");
            //string fullPath = Path.Combine(Server.MapPath("~/Template"), "SampleCSV.csv");
            //File.WriteAllText(fullPath, sb.ToString());

        }
    }
}