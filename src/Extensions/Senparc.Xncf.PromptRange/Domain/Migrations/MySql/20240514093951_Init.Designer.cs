﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Senparc.Xncf.PromptRange.Models;

#nullable disable

namespace Senparc.Xncf.PromptRange.Domain.Migrations.MySql
{
    [DbContext(typeof(PromptRangeSenparcEntities_MySql))]
    [Migration("20240514093951_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Senparc.Xncf.PromptRange.Domain.Models.DatabaseModel.LlModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("AddTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("AdminRemark")
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300)");

                    b.Property<string>("Alias")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("ApiKey")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("ApiVersion")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("DeploymentName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Endpoint")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("varchar(250)");

                    b.Property<bool>("Flag")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsShared")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("LastUpdateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("MaxToken")
                        .HasColumnType("int");

                    b.Property<int>("ModelType")
                        .HasMaxLength(20)
                        .HasColumnType("int");

                    b.Property<string>("Note")
                        .HasMaxLength(1000)
                        .HasColumnType("varchar(1000)");

                    b.Property<string>("OrganizationId")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Remark")
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300)");

                    b.Property<bool>("Show")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("TenantId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Senparc_PromptRange_LlModel");
                });

            modelBuilder.Entity("Senparc.Xncf.PromptRange.Domain.Models.DatabaseModel.PromptItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("AddTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("AdminRemark")
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300)");

                    b.Property<int>("Aiming")
                        .HasMaxLength(5)
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .HasColumnType("longtext");

                    b.Property<decimal>("EvalAvgScore")
                        .HasMaxLength(3)
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("EvalMaxScore")
                        .HasMaxLength(3)
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("ExpectedResultsJson")
                        .HasColumnType("longtext");

                    b.Property<bool>("Flag")
                        .HasColumnType("tinyint(1)");

                    b.Property<float>("FrequencyPenalty")
                        .HasColumnType("float");

                    b.Property<string>("FullVersion")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<bool>("IsDraft")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsShare")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("LastRunTime")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("LastUpdateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("MaxToken")
                        .HasColumnType("int");

                    b.Property<int>("ModelId")
                        .HasColumnType("int");

                    b.Property<string>("NickName")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Note")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("ParentTac")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Prefix")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<float>("PresencePenalty")
                        .HasColumnType("float");

                    b.Property<int>("RangeId")
                        .HasColumnType("int");

                    b.Property<string>("RangeName")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Remark")
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300)");

                    b.Property<string>("StopSequences")
                        .HasColumnType("longtext");

                    b.Property<string>("Suffix")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("Tactic")
                        .HasColumnType("longtext");

                    b.Property<float>("Temperature")
                        .HasColumnType("float");

                    b.Property<int>("TenantId")
                        .HasColumnType("int");

                    b.Property<float>("TopP")
                        .HasColumnType("float");

                    b.Property<string>("VariableDictJson")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Senparc_PromptRange_PromptItem");
                });

            modelBuilder.Entity("Senparc.Xncf.PromptRange.Domain.Models.DatabaseModel.PromptRange", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("AddTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("AdminRemark")
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300)");

                    b.Property<string>("Alias")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<bool>("Flag")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("LastUpdateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("RangeName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Remark")
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300)");

                    b.Property<int>("TenantId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Senparc_PromptRange_PromptRange");
                });

            modelBuilder.Entity("Senparc.Xncf.PromptRange.Domain.Models.DatabaseModel.PromptResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("AddTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("AdminRemark")
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300)");

                    b.Property<double>("CostTime")
                        .HasColumnType("double");

                    b.Property<decimal>("FinalScore")
                        .HasColumnType("decimal(65,30)");

                    b.Property<bool>("Flag")
                        .HasColumnType("tinyint(1)");

                    b.Property<decimal>("HumanScore")
                        .HasColumnType("decimal(65,30)");

                    b.Property<DateTime>("LastUpdateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("LlmModelId")
                        .HasColumnType("int");

                    b.Property<int>("PromptCostToken")
                        .HasColumnType("int");

                    b.Property<int>("PromptItemId")
                        .HasColumnType("int");

                    b.Property<string>("PromptItemVersion")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Remark")
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300)");

                    b.Property<int>("ResultCostToken")
                        .HasColumnType("int");

                    b.Property<string>("ResultString")
                        .HasColumnType("longtext");

                    b.Property<decimal>("RobotScore")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("TenantId")
                        .HasColumnType("int");

                    b.Property<int>("TestType")
                        .HasColumnType("int");

                    b.Property<int>("TotalCostToken")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Senparc_PromptRange_PromptResult");
                });
#pragma warning restore 612, 618
        }
    }
}
