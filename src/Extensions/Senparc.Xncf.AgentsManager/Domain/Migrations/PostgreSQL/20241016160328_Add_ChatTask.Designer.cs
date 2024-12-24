﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Senparc.Xncf.AgentsManager.Models;

#nullable disable

namespace Senparc.Xncf.AgentsManager.Domain.Migrations.PostgreSQL
{
    [DbContext(typeof(AgentsManagerSenparcEntities_PostgreSQL))]
    [Migration("20241016160328_Add_ChatTask")]
    partial class Add_ChatTask
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Senparc.Xncf.AgentsManager.Domain.Models.DatabaseModel.ChatTask", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("AddTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("AdminRemark")
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<int>("AiModelId")
                        .HasColumnType("integer");

                    b.Property<int>("ChatGroupId")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("Flag")
                        .HasColumnType("boolean");

                    b.Property<int>("HookPlatform")
                        .HasColumnType("integer");

                    b.Property<string>("HookPlatformParameter")
                        .HasColumnType("text");

                    b.Property<bool>("IsPersonality")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("LastUpdateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.Property<string>("PromptCommand")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Remark")
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<string>("ResultComment")
                        .HasColumnType("text");

                    b.Property<bool>("Score")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<int>("TenantId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Senparc_AgentsManager_ChatTask");
                });

            modelBuilder.Entity("Senparc.Xncf.AgentsManager.Models.DatabaseModel.AgentTemplate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("AddTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("AdminRemark")
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<string>("Avastar")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<bool>("Enable")
                        .HasColumnType("boolean");

                    b.Property<bool>("Flag")
                        .HasColumnType("boolean");

                    b.Property<string>("HookRobotParameter")
                        .HasColumnType("text");

                    b.Property<int>("HookRobotType")
                        .HasColumnType("integer");

                    b.Property<DateTime>("LastUpdateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PromptCode")
                        .HasColumnType("text");

                    b.Property<string>("Remark")
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<string>("SystemMessage")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TenantId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Senparc_AgentsManager_AgentTemplate");
                });

            modelBuilder.Entity("Senparc.Xncf.AgentsManager.Models.DatabaseModel.Models.ChatGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("AddTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("AdminAgentTemplateId")
                        .HasColumnType("integer");

                    b.Property<string>("AdminRemark")
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<bool>("Enable")
                        .HasColumnType("boolean");

                    b.Property<int>("EnterAgentTemplateId")
                        .HasColumnType("integer");

                    b.Property<bool>("Flag")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("LastUpdateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Remark")
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<int>("State")
                        .HasColumnType("integer");

                    b.Property<int>("TenantId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AdminAgentTemplateId");

                    b.HasIndex("EnterAgentTemplateId");

                    b.ToTable("Senparc_AgentsManager_ChatGroup");
                });

            modelBuilder.Entity("Senparc.Xncf.AgentsManager.Models.DatabaseModel.Models.ChatGroupHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("AddTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("AdminRemark")
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<int>("ChatGroupId")
                        .HasColumnType("integer");

                    b.Property<int>("ChatTaskId")
                        .HasColumnType("integer");

                    b.Property<bool>("Flag")
                        .HasColumnType("boolean");

                    b.Property<int?>("FromAgentTemplateId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("LastUpdateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("MessageType")
                        .HasColumnType("integer");

                    b.Property<int>("MyProperty")
                        .HasColumnType("integer");

                    b.Property<string>("Remark")
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<int>("TenantId")
                        .HasColumnType("integer");

                    b.Property<int?>("ToAgentTemplateId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ChatGroupId");

                    b.HasIndex("ChatTaskId");

                    b.HasIndex("FromAgentTemplateId");

                    b.HasIndex("ToAgentTemplateId");

                    b.ToTable("Senparc_AgentsManager_ChatGroupHistory");
                });

            modelBuilder.Entity("Senparc.Xncf.AgentsManager.Models.DatabaseModel.Models.ChatGroupMember", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("AddTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("AdminRemark")
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<int>("AgentTemplateId")
                        .HasColumnType("integer");

                    b.Property<int>("ChatGroupId")
                        .HasColumnType("integer");

                    b.Property<bool>("Flag")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("LastUpdateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Remark")
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<int>("TenantId")
                        .HasColumnType("integer");

                    b.Property<string>("UID")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AgentTemplateId");

                    b.ToTable("Senparc_AgentsManager_ChatGroupMember");
                });

            modelBuilder.Entity("Senparc.Xncf.AgentsManager.Models.DatabaseModel.Models.ChatGroup", b =>
                {
                    b.HasOne("Senparc.Xncf.AgentsManager.Models.DatabaseModel.AgentTemplate", "AdminAgentTemplate")
                        .WithMany("AdminChatGroups")
                        .HasForeignKey("AdminAgentTemplateId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Senparc.Xncf.AgentsManager.Models.DatabaseModel.AgentTemplate", "EnterAgentTemplate")
                        .WithMany("EnterAgentChatGroups")
                        .HasForeignKey("EnterAgentTemplateId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("FK_Senparc_AgentsManager_ChatGroup_Senparc_AgentsManager_Agen~1");

                    b.Navigation("AdminAgentTemplate");

                    b.Navigation("EnterAgentTemplate");
                });

            modelBuilder.Entity("Senparc.Xncf.AgentsManager.Models.DatabaseModel.Models.ChatGroupHistory", b =>
                {
                    b.HasOne("Senparc.Xncf.AgentsManager.Models.DatabaseModel.Models.ChatGroup", "ChatGroup")
                        .WithMany()
                        .HasForeignKey("ChatGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Senparc.Xncf.AgentsManager.Domain.Models.DatabaseModel.ChatTask", "ChatTask")
                        .WithMany()
                        .HasForeignKey("ChatTaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Senparc_AgentsManager_ChatGroupHistory_Senparc_AgentsManag~1");

                    b.HasOne("Senparc.Xncf.AgentsManager.Models.DatabaseModel.AgentTemplate", "FromAgentTemplate")
                        .WithMany("FromChatGroupHistories")
                        .HasForeignKey("FromAgentTemplateId")
                        .HasConstraintName("FK_Senparc_AgentsManager_ChatGroupHistory_Senparc_AgentsManag~2");

                    b.HasOne("Senparc.Xncf.AgentsManager.Models.DatabaseModel.AgentTemplate", "ToAgentTemplate")
                        .WithMany("ToChatGroupHistoies")
                        .HasForeignKey("ToAgentTemplateId")
                        .HasConstraintName("FK_Senparc_AgentsManager_ChatGroupHistory_Senparc_AgentsManag~3");

                    b.Navigation("ChatGroup");

                    b.Navigation("ChatTask");

                    b.Navigation("FromAgentTemplate");

                    b.Navigation("ToAgentTemplate");
                });

            modelBuilder.Entity("Senparc.Xncf.AgentsManager.Models.DatabaseModel.Models.ChatGroupMember", b =>
                {
                    b.HasOne("Senparc.Xncf.AgentsManager.Models.DatabaseModel.AgentTemplate", "AgentTemplate")
                        .WithMany("ChatGroupMembers")
                        .HasForeignKey("AgentTemplateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AgentTemplate");
                });

            modelBuilder.Entity("Senparc.Xncf.AgentsManager.Models.DatabaseModel.AgentTemplate", b =>
                {
                    b.Navigation("AdminChatGroups");

                    b.Navigation("ChatGroupMembers");

                    b.Navigation("EnterAgentChatGroups");

                    b.Navigation("FromChatGroupHistories");

                    b.Navigation("ToChatGroupHistoies");
                });
#pragma warning restore 612, 618
        }
    }
}
