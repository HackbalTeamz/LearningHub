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
    
    public partial class SuperAdminTbl
    {
        public int SuperAdminID { get; set; }
        public long CredID { get; set; }
        public string SuperAdminName { get; set; }
    
        public virtual CredentialTbl CredentialTbl { get; set; }
    }
}
