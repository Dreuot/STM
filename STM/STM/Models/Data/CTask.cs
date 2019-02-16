using System;
using System.Collections.Generic;

namespace STM.Models.Data
{
    public partial class CTask
    {
        public CTask()
        {
            CActivity = new HashSet<CActivity>();
            CComment = new HashSet<CComment>();
            CTaskLabel = new HashSet<CTaskLabel>();
            InverseParentTask = new HashSet<CTask>();
        }

        public string Id { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? LastUpdate { get; set; }
        public DateTime? Resolved { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? StoryPoints { get; set; }
        public string ProjectId { get; set; }
        public string ListId { get; set; }
        public string ReleaseId { get; set; }
        public string PriorityId { get; set; }
        public string TypeId { get; set; }
        public string StatusId { get; set; }
        public string CreatedById { get; set; }
        public string AssigneeId { get; set; }
        public string ParentTaskId { get; set; }

        public CUser Assignee { get; set; }
        public CUser CreatedBy { get; set; }
        public CList List { get; set; }
        public CTask ParentTask { get; set; }
        public CTaskPriority Priority { get; set; }
        public CProject Project { get; set; }
        public CRelease Release { get; set; }
        public CTaskStatus Status { get; set; }
        public CTaskType Type { get; set; }
        public ICollection<CActivity> CActivity { get; set; }
        public ICollection<CComment> CComment { get; set; }
        public ICollection<CTaskLabel> CTaskLabel { get; set; }
        public ICollection<CTask> InverseParentTask { get; set; }
    }
}
