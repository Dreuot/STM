using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace STM
{
    public partial class STM_DBContext : DbContext
    {
        // Unable to generate entity type for table 'dbo.c_user'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.c_project'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.c_release'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.c_board'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.c_list'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.c_task'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.c_comment'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.c_task_status'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.c_task_type'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.c_label'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.c_task_label'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.c_task_priority'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.c_role'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.c_user_role'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.c_team'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.c_team_role'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.c_workflow'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.c_activity_type'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.c_activity'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.c_attach'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.c_config'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.c_user_team'. Please see the warning messages.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-GO9H921\SQLEXPRESS;Initial Catalog=STM_DB;User ID=sa;Password=1123");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {}
    }
}
