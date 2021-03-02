using LearningHub.Models;
using LearningHub.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace LearningHub.Helper
{
    public class SMSSendHelper
    {
        Entities db = new Entities();
        public async System.Threading.Tasks.Task SMSSendAsync(long ClassID, long MsgID)
        {
            string apitokenurl = APITokenBuilder();
            MessageTbl MsgObj = db.MessageTbls.Find(MsgID);
            foreach (var item in db.StudentTbls.Where(x=>x.ClassID == ClassID))
            {
                string buildurl = URLBuilder(apitokenurl, item.Mobile, MsgBuilder(MsgObj.MessageText,item.StudentName));
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://www.smsgatewayhub.com/api/mt/");
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.GetAsync(buildurl);
                    if (response.IsSuccessStatusCode)
                    {
                        var asdf = response.Content.ReadAsStringAsync().Result;
                        var Rech = JsonConvert.DeserializeObject<SMSGWHResp>(asdf);

                    }
                }
            }
            

        }

        public string URLBuilder(string apitokenurl,string mobile, string Msg)
        {
            return apitokenurl + "&number=91" + mobile + "&text=" + Msg;
        }

        public string APITokenBuilder()
        {
            APITokenTbl apitoken = db.APITokenTbls.Where(x => x.IsActive == true).FirstOrDefault();
            if (apitoken.APIName == "SMSGateWay")
            {
                return "SendSMS?APIKey=" + apitoken.APIKey + "&senderid=" + apitoken.SenderID + "&channel=" + apitoken.IsChannel + "&DCS=" + apitoken.IsUnicord + "&flashsms=" + apitoken.IsFlash + "&route=" + apitoken.Route;
            }
            else
            {
                return "";
            }
        }

        //public string SMSURLBuilder(long ClassID, long MsgID)
        //{
        //    APITokenTbl apitoken = db.APITokenTbls.Where(x => x.IsActive == true).FirstOrDefault();
        //    if (apitoken.APIName == "SMSGateWay")
        //    {
        //        return "SendSMS?APIKey=" + apitoken.APIKey + "&senderid=" + apitoken.SenderID + "&channel=" + apitoken.IsChannel + "&DCS=" + apitoken.IsUnicord + "&flashsms=" + apitoken.IsFlash + "&number=918086308803&text=Hai&route=" + apitoken.Route;
        //    }
        //    else
        //    {
        //        return "";
        //    }
        //}
        public string MsgBuilder(string Msg, string Name)
        {
            return String.Format(Msg, Name);
        }
        //public string MsgBuilder(long Msg)
        //{
        //    MessageTbl MsgObj = db.MessageTbls.Find(Msg);
        //    if(MsgObj.FifthPara == null)
        //    {
        //        if(MsgObj.FourthPara == null)
        //        {
        //            if (MsgObj.ThirdPara == null)
        //            {
        //                if (MsgObj.SecondPara == null)
        //                {
        //                    if (MsgObj.FirstPara == null)
        //                    {
        //                        return MsgObj.MessageText;
        //                    }
        //                    else
        //                    {
        //                        return String.Format(MsgObj.MessageText, MsgObj.FirstPara);
        //                    }
        //                }
        //                else
        //                {
        //                    return String.Format(MsgObj.MessageText, MsgObj.FirstPara, MsgObj.SecondPara);
        //                }
        //            }
        //            else
        //            {
        //                return String.Format(MsgObj.MessageText, MsgObj.FirstPara, MsgObj.SecondPara, MsgObj.ThirdPara);
        //            }
        //        }
        //        else
        //        {
        //            return String.Format(MsgObj.MessageText, MsgObj.FirstPara, MsgObj.SecondPara, MsgObj.ThirdPara,FourthPara);
        //        }

        //    }
        //    else
        //    {
        //        return String.Format(MsgObj.MessageText, MsgObj.FirstPara, MsgObj.SecondPara, MsgObj.ThirdPara, MsgObj.FourthPara, MsgObj.FifthPara);
        //    }
        //}
    }
}
