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

        public string Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Avatar { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MidName { get; set; }
        public DateTime? Created { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public ICollection<CActivity> CActivity { get; set; }
        public ICollection<CComment> CComment { get; set; }
        public ICollection<CProject> CProject { get; set; }
        public ICollection<CTask> CTaskAssignee { get; set; }
        public ICollection<CTask> CTaskCreatedBy { get; set; }
        public ICollection<CUserRole> CUserRole { get; set; }
        public ICollection<CUserTeam> CUserTeam { get; set; }
    }
}
