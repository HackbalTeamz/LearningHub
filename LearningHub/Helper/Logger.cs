using log4net;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace LearningHub.Helper
{
    public static class Logger
    {
        public static void LOGEntityValidationErrors(this ILog log, DbEntityValidationException e)
        {
            foreach (var eve in e.EntityValidationErrors)
            {
                log.InfoFormat("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State);
                foreach (var ve in eve.ValidationErrors)
                {
                    log.InfoFormat("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                }
            }
        }
    }
}