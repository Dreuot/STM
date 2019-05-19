using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace STM.Models.Data
{
    public partial class STM_DBContext : DbContext
    {
        public STM_DBContext()
        {
        }

        public STM_DBContext(DbContextOptions<STM_DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CActivity> CActivity { get; set; }
        public virtual DbSet<CActivityType> CActivityType { get; set; }
        public virtual DbSet<CAttach> CAttach { get; set; }
        public virtual DbSet<CBoard> CBoard { get; set; }
        public virtual DbSet<CComment> CComment { get; set; }
        public virtual DbSet<CConfig> CConfig { get; set; }
        public virtual DbSet<CLabel> CLabel { get; set; }
        public virtual DbSet<CList> CList { get; set; }
        public virtual DbSet<CProject> CProject { get; set; }
        public virtual DbSet<CRelease> CRelease { get; set; }
        public virtual DbSet<CRole> CRole { get; set; }
        public virtual DbSet<CTask> CTask { get; set; }
        public virtual DbSet<CTaskLabel> CTaskLabel { get; set; }
        public virtual DbSet<CTaskPriority> CTaskPriority { get; set; }
        public virtual DbSet<CTaskRel> CTaskRel { get; set; }
        public virtual DbSet<CTaskStatus> CTaskStatus { get; set; }
        public virtual DbSet<CTaskType> CTaskType { get; set; }
        public virtual DbSet<CTeam> CTeam { get; set; }
        public virtual DbSet<CTeamRole> CTeamRole { get; set; }
        public virtual DbSet<CUser> CUser { get; set; }
        public virtual DbSet<CUserRole> CUserRole { get; set; }
        public virtual DbSet<CUserTeam> CUserTeam { get; set; }
        public virtual DbSet<CWorkflow> CWorkflow { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-GO9H921\\SQLEXPRESS;Database=STM_DB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<CActivity>(entity =>
            {
                entity.ToTable("c_activity");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ActivityTypeId).HasColumnName("activity_type_id");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("ntext");

                entity.Property(e => e.TaskId).HasColumnName("task_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.ActivityType)
                    .WithMany(p => p.CActivity)
                    .HasForeignKey(d => d.ActivityTypeId)
                    .HasConstraintName("FK__c_activit__activ__778AC167");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.CActivity)
                    .HasForeignKey(d => d.TaskId)
                    .HasConstraintName("FK__c_activit__task___797309D9");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.CActivity)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__c_activit__user___787EE5A0");
            });

            modelBuilder.Entity<CActivityType>(entity =>
            {
                entity.ToTable("c_activity_type");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("ntext");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<CAttach>(entity =>
            {
                entity.ToTable("c_attach");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(15);

                entity.Property(e => e.Path)
                    .HasColumnName("path")
                    .HasMaxLength(250);
            });

            modelBuilder.Entity<CBoard>(entity =>
            {
                entity.ToTable("c_board");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("ntext");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<CComment>(entity =>
            {
                entity.ToTable("c_comment");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Comment)
                    .HasColumnName("comment")
                    .HasColumnType("ntext");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.Edited).HasColumnName("edited");

                entity.Property(e => e.LastUpdate)
                    .HasColumnName("last_update")
                    .HasColumnType("datetime");

                entity.Property(e => e.TaskId).HasColumnName("task_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.CComment)
                    .HasForeignKey(d => d.TaskId)
                    .HasConstraintName("FK__c_comment__task___59063A47");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.CComment)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__c_comment__user___5812160E");
            });

            modelBuilder.Entity<CConfig>(entity =>
            {
                entity.ToTable("c_config");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.KeyC)
                    .HasColumnName("key_c")
                    .HasMaxLength(300);

                entity.Property(e => e.ValueC)
                    .HasColumnName("value_c")
                    .HasMaxLength(300);
            });

            modelBuilder.Entity<CLabel>(entity =>
            {
                entity.ToTable("c_label");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<CList>(entity =>
            {
                entity.ToTable("c_list");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BoardId).HasColumnName("board_id");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("ntext");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.HasOne(d => d.Board)
                    .WithMany(p => p.CList)
                    .HasForeignKey(d => d.BoardId)
                    .HasConstraintName("FK__c_list__board_id__412EB0B6");
            });

            modelBuilder.Entity<CProject>(entity =>
            {
                entity.ToTable("c_project");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("ntext");

                entity.Property(e => e.Manager).HasColumnName("manager");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(100);

                entity.Property(e => e.Prefix)
                    .HasColumnName("prefix")
                    .HasMaxLength(50);

                entity.HasOne(d => d.ManagerNavigation)
                    .WithMany(p => p.CProject)
                    .HasForeignKey(d => d.Manager)
                    .HasConstraintName("FK__c_project__manag__398D8EEE");
            });

            modelBuilder.Entity<CRelease>(entity =>
            {
                entity.ToTable("c_release");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("ntext");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.ProjectId).HasColumnName("project_id");

                entity.Property(e => e.ReleaseDate)
                    .HasColumnName("release_date")
                    .HasColumnType("date");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.CRelease)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("FK__c_release__proje__3C69FB99");
            });

            modelBuilder.Entity<CRole>(entity =>
            {
                entity.ToTable("c_role");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("ntext");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<CTask>(entity =>
            {
                entity.ToTable("c_task");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AssigneeId).HasColumnName("assignee_id");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedById).HasColumnName("created_by_id");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("ntext");

                entity.Property(e => e.FactComplete)
                    .HasColumnName("fact_complete")
                    .HasColumnType("datetime");

                entity.Property(e => e.FactStart)
                    .HasColumnName("fact_start")
                    .HasColumnType("datetime");

                entity.Property(e => e.LastUpdate)
                    .HasColumnName("last_update")
                    .HasColumnType("datetime");

                entity.Property(e => e.ListId).HasColumnName("list_id");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(100);

                entity.Property(e => e.ParentTaskId).HasColumnName("parent_task_id");

                entity.Property(e => e.PlannedComplete)
                    .HasColumnName("planned_complete")
                    .HasColumnType("datetime");

                entity.Property(e => e.PlannedStart)
                    .HasColumnName("planned_start")
                    .HasColumnType("datetime");

                entity.Property(e => e.PriorityId).HasColumnName("priority_id");

                entity.Property(e => e.ProjectId).HasColumnName("project_id");

                entity.Property(e => e.ReleaseId).HasColumnName("release_id");

                entity.Property(e => e.StatusId).HasColumnName("status_id");

                entity.Property(e => e.StoryPoints).HasColumnName("story_points");

                entity.Property(e => e.TypeId).HasColumnName("type_id");

                entity.HasOne(d => d.Assignee)
                    .WithMany(p => p.CTaskAssignee)
                    .HasForeignKey(d => d.AssigneeId)
                    .HasConstraintName("FK__c_task__assignee__5070F446");

                entity.HasOne(d => d.CreatedBy)
                    .WithMany(p => p.CTaskCreatedBy)
                    .HasForeignKey(d => d.CreatedById)
                    .HasConstraintName("FK__c_task__created___4F7CD00D");

                entity.HasOne(d => d.List)
                    .WithMany(p => p.CTask)
                    .HasForeignKey(d => d.ListId)
                    .HasConstraintName("FK__c_task__list_id__4AB81AF0");

                entity.HasOne(d => d.ParentTask)
                    .WithMany(p => p.InverseParentTask)
                    .HasForeignKey(d => d.ParentTaskId)
                    .HasConstraintName("FK__c_task__parent_t__5165187F");

                entity.HasOne(d => d.Priority)
                    .WithMany(p => p.CTask)
                    .HasForeignKey(d => d.PriorityId)
                    .HasConstraintName("FK__c_task__priority__4CA06362");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.CTask)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("FK__c_task__project___49C3F6B7");

                entity.HasOne(d => d.Release)
                    .WithMany(p => p.CTask)
                    .HasForeignKey(d => d.ReleaseId)
                    .HasConstraintName("FK__c_task__release___4BAC3F29");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.CTask)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK__c_task__status_i__4E88ABD4");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.CTask)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK__c_task__type_id__4D94879B");
            });

            modelBuilder.Entity<CTaskLabel>(entity =>
            {
                entity.ToTable("c_task_label");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.LabelId).HasColumnName("label_id");

                entity.Property(e => e.TaskId).HasColumnName("task_id");

                entity.HasOne(d => d.Label)
                    .WithMany(p => p.CTaskLabel)
                    .HasForeignKey(d => d.LabelId)
                    .HasConstraintName("FK__c_task_la__label__5EBF139D");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.CTaskLabel)
                    .HasForeignKey(d => d.TaskId)
                    .HasConstraintName("FK__c_task_la__task___5DCAEF64");
            });

            modelBuilder.Entity<CTaskPriority>(entity =>
            {
                entity.ToTable("c_task_priority");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.Icon)
                    .HasColumnName("icon")
                    .HasMaxLength(250);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<CTaskRel>(entity =>
            {
                entity.ToTable("c_task_rel");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.RelType)
                    .HasColumnName("rel_type")
                    .HasMaxLength(200);

                entity.Property(e => e.TaskMasterId).HasColumnName("task_master_id");

                entity.Property(e => e.TaskSlaveId).HasColumnName("task_slave_id");

                entity.HasOne(d => d.TaskMaster)
                    .WithMany(p => p.CTaskRelTaskMaster)
                    .HasForeignKey(d => d.TaskMasterId)
                    .HasConstraintName("FK__c_task_re__task___5441852A");

                entity.HasOne(d => d.TaskSlave)
                    .WithMany(p => p.CTaskRelTaskSlave)
                    .HasForeignKey(d => d.TaskSlaveId)
                    .HasConstraintName("FK__c_task_re__task___5535A963");
            });

            modelBuilder.Entity<CTaskStatus>(entity =>
            {
                entity.ToTable("c_task_status");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.Icon)
                    .HasColumnName("icon")
                    .HasMaxLength(250);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.Stage)
                    .HasColumnName("stage")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<CTaskType>(entity =>
            {
                entity.ToTable("c_task_type");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.Icon)
                    .HasColumnName("icon")
                    .HasMaxLength(250);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<CTeam>(entity =>
            {
                entity.ToTable("c_team");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("ntext");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(15);
            });

            modelBuilder.Entity<CTeamRole>(entity =>
            {
                entity.ToTable("c_team_role");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.TeamId).HasColumnName("team_id");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.CTeamRole)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK__c_team_ro__role___6E01572D");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.CTeamRole)
                    .HasForeignKey(d => d.TeamId)
                    .HasConstraintName("FK__c_team_ro__team___6D0D32F4");
            });

            modelBuilder.Entity<CUser>(entity =>
            {
                entity.ToTable("c_user");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Avatar)
                    .HasColumnName("avatar")
                    .HasMaxLength(250);

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(100);

                entity.Property(e => e.FirstName)
                    .HasColumnName("first_name")
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .HasColumnName("last_name")
                    .HasMaxLength(50);

                entity.Property(e => e.Login)
                    .HasColumnName("login")
                    .HasMaxLength(50);

                entity.Property(e => e.MidName)
                    .HasColumnName("mid_name")
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(50);

                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<CUserRole>(entity =>
            {
                entity.ToTable("c_user_role");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.CUserRole)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK__c_user_ro__role___6477ECF3");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.CUserRole)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__c_user_ro__user___6383C8BA");
            });

            modelBuilder.Entity<CUserTeam>(entity =>
            {
                entity.ToTable("c_user_team");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.TeamId).HasColumnName("team_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.CUserTeam)
                    .HasForeignKey(d => d.TeamId)
                    .HasConstraintName("FK__c_user_te__team___6A30C649");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.CUserTeam)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__c_user_te__user___693CA210");
            });

            modelBuilder.Entity<CWorkflow>(entity =>
            {
                entity.ToTable("c_workflow");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.StatusFromId).HasColumnName("status_from_id");

                entity.Property(e => e.StatusToId).HasColumnName("status_to_id");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.CWorkflow)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK__c_workflo__role___70DDC3D8");

                entity.HasOne(d => d.StatusFrom)
                    .WithMany(p => p.CWorkflowStatusFrom)
                    .HasForeignKey(d => d.StatusFromId)
                    .HasConstraintName("FK__c_workflo__statu__71D1E811");

                entity.HasOne(d => d.StatusTo)
                    .WithMany(p => p.CWorkflowStatusTo)
                    .HasForeignKey(d => d.StatusToId)
                    .HasConstraintName("FK__c_workflo__statu__72C60C4A");
            });
        }
    }
}
