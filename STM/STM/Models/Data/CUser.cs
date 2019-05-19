using System;
using System.Collections.Generic;

namespace STM.Models.Data
{
    public partial class CUser
    {
        public CUser()
        {
            CActivity = new HashSet<CActivity>();
            CComment = new HashSet<CComment>();
            CProject = new HashSet<CProject>();
            CTaskAssignee = new HashSet<CTask>();
            CTaskCreatedBy = new HashSet<CTask>();
            CUserRole = new HashSet<CUserRole>();
            CUserTeam = new HashSet<CUserTeam>();
        }

        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Avatar { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MidName { get; set; }
        public DateTime? Created { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public virtual ICollection<CActivity> CActivity { get; set; }
        public virtual ICollection<CComment> CComment { get; set; }
        public virtual ICollection<CProject> CProject { get; set; }
        public virtual ICollection<CTask> CTaskAssignee { get; set; }
        public virtual ICollection<CTask> CTaskCreatedBy { get; set; }
        public virtual ICollection<CUserRole> CUserRole { get; set; }
        public virtual ICollection<CUserTeam> CUserTeam { get; set; }
    }
}
