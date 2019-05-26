using System;
using System.Collections.Generic;

namespace STM_React.Models.Data
{
    public partial class CTask
    {
        public CTask()
        {
            CActivity = new HashSet<CActivity>();
            CComment = new HashSet<CComment>();
            CTaskLabel = new HashSet<CTaskLabel>();
            CTaskRelTaskMaster = new HashSet<CTaskRel>();
            CTaskRelTaskSlave = new HashSet<CTaskRel>();
            InverseParentTask = new HashSet<CTask>();
        }

        public int Id { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? LastUpdate { get; set; }
        public string Code { get; set; }
        public DateTime? PlannedStart { get; set; }
        public DateTime? PlannedComplete { get; set; }
        public DateTime? FactStart { get; set; }
        public DateTime? FactComplete { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? StoryPoints { get; set; }
        public int? ProjectId { get; set; }
        public int? ListId { get; set; }
        public int? ReleaseId { get; set; }
        public int? PriorityId { get; set; }
        public int? TypeId { get; set; }
        public int? StatusId { get; set; }
        public int? CreatedById { get; set; }
        public int? AssigneeId { get; set; }
        public int? ParentTaskId { get; set; }

        public virtual CUser Assignee { get; set; }
        public virtual CUser CreatedBy { get; set; }
        public virtual CList List { get; set; }
        public virtual CTask ParentTask { get; set; }
        public virtual CTaskPriority Priority { get; set; }
        public virtual CProject Project { get; set; }
        public virtual CRelease Release { get; set; }
        public virtual CTaskStatus Status { get; set; }
        public virtual CTaskType Type { get; set; }
        public virtual ICollection<CActivity> CActivity { get; set; }
        public virtual ICollection<CComment> CComment { get; set; }
        public virtual ICollection<CTaskLabel> CTaskLabel { get; set; }
        public virtual ICollection<CTaskRel> CTaskRelTaskMaster { get; set; }
        public virtual ICollection<CTaskRel> CTaskRelTaskSlave { get; set; }
        public virtual ICollection<CTask> InverseParentTask { get; set; }
    }
}
