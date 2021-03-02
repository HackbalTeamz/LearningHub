using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LearningHub.Models
{
    //CredentialDetail Validation
    #region AssignmentModel
    [MetadataType(typeof(AssignmentValidation))]
    public partial class AssignmentTbl
    {

    }

    public class AssignmentValidation
    {
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> DeadLineOn { get; set; }
        //[DataType(DataType.DateTime)]
        //[DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> PublishedOn { get; set; }

    }
    #endregion

    #region RegistrationModel
    public class RegistrationVM
    {
        public CredentialTbl cred { get; set; }
        public AdminTbl admin { get; set; }
        public StaffTbl staff { get; set; }
        public ParentTbl parent { get; set; }
        public StudentTbl student { get; set; }
    }
    #endregion

    #region RegistrationModel
    public class MessageSendVM
    {
       public long ClassID { get; set; }
       public long MsgID { get; set; }
    }
    #endregion

}