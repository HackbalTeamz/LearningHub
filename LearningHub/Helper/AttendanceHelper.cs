using LearningHub.Models;
using Newtonsoft.Json;
using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;

namespace LearningHub.Helper
{
    public class AttendanceHelper
    {
        protected static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private Entities db = new Entities();
        public void StudentIsLogin(long LoginID)
        {
            StudentsAttentanceTbl studentsAttentanceTbl = new StudentsAttentanceTbl();
            try
            {
                //throw new SqlNullValueException();
                studentsAttentanceTbl = db.StudentsAttentanceTbls.Where(x => x.StudentID == LoginID).OrderByDescending(x => x.StudentAttentanceID).FirstOrDefault();
                if (studentsAttentanceTbl != null)
                {
                    if (Convert.ToDateTime(studentsAttentanceTbl.PresentDate).Day == DateTime.Now.Day)
                    {
                        studentsAttentanceTbl.IsLogin = DateTime.Now;
                        db.Entry(studentsAttentanceTbl).State = EntityState.Modified;
                        db.SaveChanges();

                    }
                    else
                    {
                        studentsAttentanceTbl = new StudentsAttentanceTbl()
                        {
                            StudentID = LoginID,
                            PresentDate = DateTime.Now,
                            IsLogin = DateTime.Now,
                            LoginMinites = 0
                        };
                        db.StudentsAttentanceTbls.Add(studentsAttentanceTbl);
                        db.SaveChanges();
                    }
                }
                else
                {
                    studentsAttentanceTbl = new StudentsAttentanceTbl()
                    {
                        StudentID = LoginID,
                        PresentDate = DateTime.Now,
                        IsLogin = DateTime.Now,
                        LoginMinites = 0
                    };
                    db.StudentsAttentanceTbls.Add(studentsAttentanceTbl);
                    db.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                log.Error($"Exception in {nameof(StudentIsLogin)} , Params {JsonConvert.SerializeObject(studentsAttentanceTbl)}", ex);

            }

        }
        public void StudentIsClassAttent(long LoginID, long SubID)
        {
            StudentClassAttend studentClassAttend = new StudentClassAttend();
            try
            {
                studentClassAttend = db.StudentClassAttends.Where(x => x.StudentID == LoginID && x.SubjectID == SubID).OrderByDescending(x => x.StudentClassAttID).FirstOrDefault();
                if (studentClassAttend != null)
                {
                    if (Convert.ToDateTime(studentClassAttend.PresentDate).Day == DateTime.Now.Day)
                    {
                        studentClassAttend.OpenTime = DateTime.Now;
                        studentClassAttend.UpdatedOn = DateTime.Now;
                        db.Entry(studentClassAttend).State = EntityState.Modified;
                        db.SaveChanges();

                    }
                    else
                    {
                        studentClassAttend = new StudentClassAttend()
                        {
                            StudentID = LoginID,
                            SubjectID = SubID,
                            PresentDate = DateTime.Now,
                            OpenTime = DateTime.Now,
                            EnteredOn = DateTime.Now,
                            UpdatedOn = DateTime.Now
                        };
                        db.StudentClassAttends.Add(studentClassAttend);
                        db.SaveChanges();
                    }
                }
                else
                {
                    studentClassAttend = new StudentClassAttend()
                    {
                        StudentID = LoginID,
                        SubjectID = SubID,
                        PresentDate = DateTime.Now,
                        OpenTime = DateTime.Now,
                        EnteredOn = DateTime.Now,
                        UpdatedOn = DateTime.Now
                    };
                    db.StudentClassAttends.Add(studentClassAttend);
                    db.SaveChanges();

                }
            }
            catch(Exception ex)
            {
                log.Error($"Exception in {nameof(StudentIsClassAttent)} , Params {JsonConvert.SerializeObject(studentClassAttend)}", ex);
            }
        }
        public void StudentIsLogout(long LoginID)
        {
            StudentsAttentanceTbl studentsAttentanceTbl = db.StudentsAttentanceTbls.Where(x => x.StudentID == LoginID).OrderByDescending(x => x.StudentAttentanceID).FirstOrDefault();
            if (studentsAttentanceTbl != null)
            {
                if (Convert.ToDateTime(studentsAttentanceTbl.PresentDate).Day == DateTime.Now.Day)
                {
                    studentsAttentanceTbl.IsLogout = DateTime.Now;
                    db.Entry(studentsAttentanceTbl).State = EntityState.Modified;
                    db.SaveChanges();

                }

            }

        }
        public void StaffIsLogin(long LoginID)
        {
            StaffAttendanceTbl studentsAttentanceTbl = db.StaffAttendanceTbls.Where(x => x.StaffID == LoginID).FirstOrDefault();
            if (studentsAttentanceTbl == null)
            {
                studentsAttentanceTbl = new StaffAttendanceTbl()
                {
                    StaffID = LoginID,
                    PresentDate = DateTime.Now,
                    IsLogin = DateTime.Now
                };
                db.StaffAttendanceTbls.Add(studentsAttentanceTbl);
                db.SaveChanges();
            }
            else
            {
                studentsAttentanceTbl.IsLogin = DateTime.Now;
                db.Entry(studentsAttentanceTbl).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        public void StaffIsLogout(long LoginID)
        {
            StaffAttendanceTbl studentsAttentanceTbl = db.StaffAttendanceTbls.Where(x => x.StaffID == LoginID).FirstOrDefault();
            studentsAttentanceTbl.IsLogout = DateTime.Now;
            db.Entry(studentsAttentanceTbl).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void StudentsAttendanceMinute(long LoginID)
        {
            StudentsAttentanceTbl studentsAttentanceTbl = db.StudentsAttentanceTbls.Where(x => x.StudentID == LoginID).OrderByDescending(x => x.StudentAttentanceID).FirstOrDefault();
            if (studentsAttentanceTbl != null)
            {
                if (Convert.ToDateTime(studentsAttentanceTbl.PresentDate).Day == DateTime.Now.Day)
                {
                    studentsAttentanceTbl.LoginMinites = studentsAttentanceTbl.LoginMinites + 1;
                    db.Entry(studentsAttentanceTbl).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

        }
    }
}