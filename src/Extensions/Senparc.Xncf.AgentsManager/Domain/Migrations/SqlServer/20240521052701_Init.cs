﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Senparc.Xncf.AgentsManager.Domain.Migrations.SqlServer
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Senparc_AgentsManager_AgentTemplate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SystemMessage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Enable = table.Column<bool>(type: "bit", nullable: false),
                    PromptCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HookRobotType = table.Column<int>(type: "int", nullable: false),
                    HookRobotParameter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Flag = table.Column<bool>(type: "bit", nullable: false),
                    AddTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    AdminRemark = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Senparc_AgentsManager_AgentTemplate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Senparc_AgentsManager_ChatGroup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Enable = table.Column<bool>(type: "bit", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdminAgentTemplateId = table.Column<int>(type: "int", nullable: false),
                    EnterAgentTemplateId = table.Column<int>(type: "int", nullable: false),
                    Flag = table.Column<bool>(type: "bit", nullable: false),
                    AddTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    AdminRemark = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Senparc_AgentsManager_ChatGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Senparc_AgentsManager_ChatGroup_Senparc_AgentsManager_AgentTemplate_AdminAgentTemplateId",
                        column: x => x.AdminAgentTemplateId,
                        principalTable: "Senparc_AgentsManager_AgentTemplate",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Senparc_AgentsManager_ChatGroup_Senparc_AgentsManager_AgentTemplate_EnterAgentTemplateId",
                        column: x => x.EnterAgentTemplateId,
                        principalTable: "Senparc_AgentsManager_AgentTemplate",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Senparc_AgentsManager_ChatGroupMember",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AgentTemplateId = table.Column<int>(type: "int", nullable: false),
                    ChatGroupId = table.Column<int>(type: "int", nullable: false),
                    Flag = table.Column<bool>(type: "bit", nullable: false),
                    AddTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    AdminRemark = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Senparc_AgentsManager_ChatGroupMember", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Senparc_AgentsManager_ChatGroupMember_Senparc_AgentsManager_AgentTemplate_AgentTemplateId",
                        column: x => x.AgentTemplateId,
                        principalTable: "Senparc_AgentsManager_AgentTemplate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Senparc_AgentsManager_ChatGroupHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChatGroupId = table.Column<int>(type: "int", nullable: false),
                    FromAgentTemplateId = table.Column<int>(type: "int", nullable: true),
                    ToAgentTemplateId = table.Column<int>(type: "int", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MessageType = table.Column<int>(type: "int", nullable: false),
                    Flag = table.Column<bool>(type: "bit", nullable: false),
                    AddTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    AdminRemark = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Senparc_AgentsManager_ChatGroupHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Senparc_AgentsManager_ChatGroupHistory_Senparc_AgentsManager_AgentTemplate_FromAgentTemplateId",
                        column: x => x.FromAgentTemplateId,
                        principalTable: "Senparc_AgentsManager_AgentTemplate",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Senparc_AgentsManager_ChatGroupHistory_Senparc_AgentsManager_AgentTemplate_ToAgentTemplateId",
                        column: x => x.ToAgentTemplateId,
                        principalTable: "Senparc_AgentsManager_AgentTemplate",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Senparc_AgentsManager_ChatGroupHistory_Senparc_AgentsManager_ChatGroup_ChatGroupId",
                        column: x => x.ChatGroupId,
                        principalTable: "Senparc_AgentsManager_ChatGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Senparc_AgentsManager_ChatGroup_AdminAgentTemplateId",
                table: "Senparc_AgentsManager_ChatGroup",
                column: "AdminAgentTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Senparc_AgentsManager_ChatGroup_EnterAgentTemplateId",
                table: "Senparc_AgentsManager_ChatGroup",
                column: "EnterAgentTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Senparc_AgentsManager_ChatGroupHistory_ChatGroupId",
                table: "Senparc_AgentsManager_ChatGroupHistory",
                column: "ChatGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Senparc_AgentsManager_ChatGroupHistory_FromAgentTemplateId",
                table: "Senparc_AgentsManager_ChatGroupHistory",
                column: "FromAgentTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Senparc_AgentsManager_ChatGroupHistory_ToAgentTemplateId",
                table: "Senparc_AgentsManager_ChatGroupHistory",
                column: "ToAgentTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Senparc_AgentsManager_ChatGroupMember_AgentTemplateId",
                table: "Senparc_AgentsManager_ChatGroupMember",
                column: "AgentTemplateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Senparc_AgentsManager_ChatGroupHistory");

            migrationBuilder.DropTable(
                name: "Senparc_AgentsManager_ChatGroupMember");

            migrationBuilder.DropTable(
                name: "Senparc_AgentsManager_ChatGroup");

            migrationBuilder.DropTable(
                name: "Senparc_AgentsManager_AgentTemplate");
        }
    }
}
