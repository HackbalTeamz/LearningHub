using LearningHub.Models;
using LearningHub.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;

namespace LearningHub.Helper
{
    public class TestingClass
    {
        Entities db = new Entities();
        public async System.Threading.Tasks.Task SMSSendAsync(long? ClassID, long? MsgID)
        {
            //string apitokenurl = APITokenBuilder();
            MessageTbl MsgObj = db.MessageTbls.Find(MsgID);
            foreach (var item in db.StudentTbls.Where(x => x.ClassID == ClassID))
            {
                //JavaScriptSerializer js = new JavaScriptSerializer();
                //TopHead tophead = new TopHead();
                //AccountVM accountVM = new AccountVM();
                //accountVM.User = "hackbal";
                //accountVM.Password = "hackbal";
                //accountVM.SenderId = "HBSPVT";
                //MessagesVM messagesVM = new MessagesVM();
                //messagesVM.Number = "919539391527";
                //messagesVM.Text = "919539391527";
                //tophead.Account = accountVM;
                //tophead.Messages = new List<MessagesVM>();
                //tophead.Messages.Add(messagesVM);
                //string jsomsg = js.Serialize(messagesVM);
                //string jsoacc = js.Serialize(accountVM);
                ////string buildurl = URLBuilder(apitokenurl, item.Mobile, MsgBuilder(MsgObj.MessageText, item.StudentName));
                //using (var wb = new WebClient())
                //{
                //    byte[] response = wb.UploadValues("https://www.smsgatewayhub.com/RestAPI/MT.svc/mt", new NameValueCollection()
                //{
                //{"Account" , jsoacc },
                //{"Messages" , jsomsg}
                //});
                //    string result = System.Text.Encoding.UTF8.GetString(response);
                //    //blogObject = js.Deserialize<MessageResp>(result);
                //    //if (blogObject.messages != null)
                //    //{
                //        //foreach (var item in blogObject.messages)
                //        //{
                //            //if (item.message != null)
                //            //{
                //                //MessageSendDetail messageSendDetail = new MessageSendDetail();
                //                //messageSendDetail.MessageID = messageDetail.MessageID;
                //                //foreach (var item2 in item.messages)
                //                //{
                //                    //CustomerDetail customerDetail = db.CustomerDetails.Where(x => x.CustPhone == item2.recipient).FirstOrDefault();
                //                    //if (customerDetail != null)
                //                    //{
                //                        //messageSendDetail.CustID = customerDetail.CustID;
                //                    //}

                //                //}
                //                //messageSendDetail.RespText = item.message.content;
                //                //messageSendDetail.SendOn = DateTime.Now;
                //                //db.MessageSendDetails.Add(messageSendDetail);
                //                //db.SaveChanges();
                //            //}
                //        //}
                //    //}
                //}

                string requestXml = "<SmsQueue><Account><User>abc</User><Password>123</Password><SenderId>TESTIN</SenderId><Channel>1</Channel><DCS>0</DCS><FlashSms>0</FlashSms><Route>1</Route></Account><Messages><Message><Number>9198981XXXXX</Number><Text>First message from xml</Text></Message><Message><Number>9198982XXXXX</Number><Text>Second messge from xml</Text></Message></Messages></SmsQueue>";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://www.smsgatewayhub.com/RestAPI/MT.svc/mt");
                byte[] bytes;
                bytes = System.Text.Encoding.ASCII.GetBytes(requestXml);
                request.ContentType = "text/xml; encoding='utf-8'";
                request.ContentLength = bytes.Length;
                request.Method = "POST";
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Close();
                HttpWebResponse response;
                response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream responseStream = response.GetResponseStream();
                    string responseStr = new StreamReader(responseStream).ReadToEnd();
                    //return "";
                }
                //return View()


            }
        }

        public void CreateCSV()
        {
            
            List<StudentTbl> customers = db.StudentTbls.ToList();
            StringBuilder sb = new StringBuilder();
            sb.Append("Stud ID , Stud Name\r\n");
            foreach(var item in customers)
            {
                sb.Append(item.StudentID + "," + item.StudentName+ "\r\n");
            }
            File.WriteAllText("E:\\SampleCSV.csv", sb.ToString());
            
        }

    
}
    public class TopHead
    {
        public AccountVM Account { get; set; }
        public List<MessagesVM> Messages { get; set; }
    }
    public class AccountVM
    {
        public string User { get; set; }
        public string Password { get; set; }
        public string SenderId { get; set; }
        public string Channel { get; set; }
        public string DCS { get; set; }
        public string SchedTime { get; set; }
        public string GroupId { get; set; }
    }
    public class MessagesVM
    {
        public string Number { get; set; }
        public string Text { get; set; }
    }

   
}