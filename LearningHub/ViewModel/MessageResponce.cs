using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearningHub.ViewModel
{
    public class SMSGWHResp
    {
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public string JobId { get; set; }
        public List<SMSGWHMSG> MessageData { get; set; }
    }
    public class SMSGWHMSG
    {
        public string Number { get; set; }
        public string MessageId { get; set; }
    }
}