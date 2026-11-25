using Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
namespace Infrastructure.Migrations;
[DbContext(typeof(AppDbContext))]
[Migration("20261125000000_InitialTaskManager")]
public class InitialTaskManager : Migration
{
 protected override void Up(MigrationBuilder m)
 {
  m.CreateTable("projects", c=>new { Id=c.Column<int>(type:"integer",nullable:false).Annotation("Npgsql:ValueGenerationStrategy",NpgsqlValueGenerationStrategy.IdentityByDefaultColumn), Name=c.Column<string>(type:"character varying(120)",maxLength:120,nullable:false), Description=c.Column<string>(type:"character varying(1000)",maxLength:1000,nullable:false), StartDate=c.Column<DateTime>(type:"timestamp with time zone",nullable:false), EndDate=c.Column<DateTime>(type:"timestamp with time zone",nullable:true) }, constraints:t=>t.PrimaryKey("PK_projects",x=>x.Id));
  m.CreateTable("users", c=>new { Id=c.Column<int>(type:"integer",nullable:false).Annotation("Npgsql:ValueGenerationStrategy",NpgsqlValueGenerationStrategy.IdentityByDefaultColumn), Name=c.Column<string>(type:"character varying(120)",maxLength:120,nullable:false), Email=c.Column<string>(type:"character varying(200)",maxLength:200,nullable:false), RegistrationDate=c.Column<DateTime>(type:"timestamp with time zone",nullable:false) }, constraints:t=>t.PrimaryKey("PK_users",x=>x.Id));
  m.CreateTable("tasks", c=>new { Id=c.Column<int>(type:"integer",nullable:false).Annotation("Npgsql:ValueGenerationStrategy",NpgsqlValueGenerationStrategy.IdentityByDefaultColumn), Title=c.Column<string>(type:"character varying(200)",maxLength:200,nullable:false), Description=c.Column<string>(type:"text",nullable:false), DueDate=c.Column<DateTime>(type:"timestamp with time zone",nullable:false), ProjectId=c.Column<int>(type:"integer",nullable:false), UserId=c.Column<int>(type:"integer",nullable:false) }, constraints:t=>{t.PrimaryKey("PK_tasks",x=>x.Id);t.ForeignKey("FK_tasks_projects_ProjectId",x=>x.ProjectId,"projects","Id",onDelete:ReferentialAction.Cascade);t.ForeignKey("FK_tasks_users_UserId",x=>x.UserId,"users","Id",onDelete:ReferentialAction.Restrict);});
  m.CreateTable("task_assignments", c=>new { Id=c.Column<int>(type:"integer",nullable:false).Annotation("Npgsql:ValueGenerationStrategy",NpgsqlValueGenerationStrategy.IdentityByDefaultColumn), TaskId=c.Column<int>(type:"integer",nullable:false), UserId=c.Column<int>(type:"integer",nullable:false), AssignedDate=c.Column<DateTime>(type:"timestamp with time zone",nullable:false) }, constraints:t=>{t.PrimaryKey("PK_task_assignments",x=>x.Id);t.ForeignKey("FK_task_assignments_tasks_TaskId",x=>x.TaskId,"tasks","Id",onDelete:ReferentialAction.Cascade);t.ForeignKey("FK_task_assignments_users_UserId",x=>x.UserId,"users","Id",onDelete:ReferentialAction.Cascade);});
  m.CreateIndex("IX_users_Email","users","Email",unique:true);
  m.CreateIndex("IX_tasks_ProjectId","tasks","ProjectId"); m.CreateIndex("IX_tasks_UserId","tasks","UserId");
  m.CreateIndex("IX_task_assignments_TaskId_UserId","task_assignments",new[]{"TaskId","UserId"},unique:true);
  m.CreateIndex("IX_task_assignments_UserId","task_assignments","UserId");
 }
 protected override void Down(MigrationBuilder m) { m.DropTable("task_assignments"); m.DropTable("tasks"); m.DropTable("projects"); m.DropTable("users"); }
}