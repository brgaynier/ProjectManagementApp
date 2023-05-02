﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
//using ProjectManagementApp.Api.Data;
using Infrastructure.Data;


#nullable disable

namespace ProjectManagementApp.Api.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20221031151421_addedItems")]
    partial class addedItems
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc.2.22472.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.Data.SqlClient.DataClassification.Label", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Label");
                });

            modelBuilder.Entity("ProjectManagementApp.Api.Data.Entities.Block", b =>
                {
                    b.Property<int>("BlockId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BlockId"));

                    b.Property<string>("BlockName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("BoardId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("BlockId");

                    b.HasIndex("BoardId");

                    b.ToTable("Blocks");
                });

            modelBuilder.Entity("ProjectManagementApp.Api.Data.Entities.Board", b =>
                {
                    b.Property<int>("BoardId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BoardId"));

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BoardId");

                    b.ToTable("Boards");
                });

            modelBuilder.Entity("ProjectManagementApp.Api.Data.Entities.Card", b =>
                {
                    b.Property<int>("CardId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CardId"));

                    b.Property<DateTime?>("AssignedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("BlockId")
                        .HasColumnType("int");

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CompletedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CoverId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LabelId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CardId");

                    b.HasIndex("BlockId");

                    b.HasIndex("CoverId");

                    b.HasIndex("LabelId");

                    b.ToTable("Cards");
                });

            modelBuilder.Entity("ProjectManagementApp.Api.Data.Entities.Checklist", b =>
                {
                    b.Property<int>("ChecklistId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ChecklistId"));

                    b.Property<int?>("CardId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("MemberId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ChecklistId");

                    b.HasIndex("CardId");

                    b.HasIndex("MemberId");

                    b.ToTable("Checklists");
                });

            modelBuilder.Entity("ProjectManagementApp.Api.Data.Entities.ChecklistItem", b =>
                {
                    b.Property<int>("ChecklistItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ChecklistItemId"));

                    b.Property<int?>("ChecklistId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("WorkFlowId")
                        .HasColumnType("int");

                    b.HasKey("ChecklistItemId");

                    b.HasIndex("ChecklistId");

                    b.HasIndex("WorkFlowId");

                    b.ToTable("ChecklistItem");
                });

            modelBuilder.Entity("ProjectManagementApp.Api.Data.Entities.Cover", b =>
                {
                    b.Property<int>("CoverId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CoverId"));

                    b.Property<int>("Black")
                        .HasColumnType("int");

                    b.Property<int>("Blue")
                        .HasColumnType("int");

                    b.Property<int>("Green")
                        .HasColumnType("int");

                    b.Property<int>("Orange")
                        .HasColumnType("int");

                    b.Property<int>("Pink")
                        .HasColumnType("int");

                    b.Property<int>("Purple")
                        .HasColumnType("int");

                    b.Property<int>("Red")
                        .HasColumnType("int");

                    b.Property<int>("SeaGrass")
                        .HasColumnType("int");

                    b.Property<int>("Turquoise")
                        .HasColumnType("int");

                    b.Property<int>("Yellow")
                        .HasColumnType("int");

                    b.HasKey("CoverId");

                    b.ToTable("Covers");
                });

            modelBuilder.Entity("ProjectManagementApp.Api.Data.Entities.Member", b =>
                {
                    b.Property<int>("MemberId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MemberId"));

                    b.Property<string>("Bio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CardId")
                        .HasColumnType("int");

                    b.Property<int?>("ChecklistItemId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("WorkFlowItemId")
                        .HasColumnType("int");

                    b.HasKey("MemberId");

                    b.HasIndex("CardId");

                    b.HasIndex("ChecklistItemId");

                    b.HasIndex("WorkFlowItemId");

                    b.ToTable("Members");
                });

            modelBuilder.Entity("ProjectManagementApp.Api.Data.Entities.WorkFlow", b =>
                {
                    b.Property<int>("WorkFlowId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WorkFlowId"));

                    b.Property<string>("WorkFlowName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("WorkFlowId");

                    b.ToTable("WorkFlow");
                });

            modelBuilder.Entity("ProjectManagementApp.Api.Data.Entities.WorkFlowItem", b =>
                {
                    b.Property<int>("WorkFlowItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WorkFlowItemId"));

                    b.Property<int?>("CardId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("WorkFlowId")
                        .HasColumnType("int");

                    b.HasKey("WorkFlowItemId");

                    b.HasIndex("CardId");

                    b.HasIndex("WorkFlowId");

                    b.ToTable("WorkFlowItems");
                });

            modelBuilder.Entity("ProjectManagementApp.Api.Data.Entities.Block", b =>
                {
                    b.HasOne("ProjectManagementApp.Api.Data.Entities.Board", "Board")
                        .WithMany("Blocks")
                        .HasForeignKey("BoardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Board");
                });

            modelBuilder.Entity("ProjectManagementApp.Api.Data.Entities.Card", b =>
                {
                    b.HasOne("ProjectManagementApp.Api.Data.Entities.Block", "Block")
                        .WithMany("Cards")
                        .HasForeignKey("BlockId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectManagementApp.Api.Data.Entities.Cover", "Cover")
                        .WithMany()
                        .HasForeignKey("CoverId");

                    b.HasOne("Microsoft.Data.SqlClient.DataClassification.Label", "Label")
                        .WithMany()
                        .HasForeignKey("LabelId");

                    b.Navigation("Block");

                    b.Navigation("Cover");

                    b.Navigation("Label");
                });

            modelBuilder.Entity("ProjectManagementApp.Api.Data.Entities.Checklist", b =>
                {
                    b.HasOne("ProjectManagementApp.Api.Data.Entities.Card", "Card")
                        .WithMany()
                        .HasForeignKey("CardId");

                    b.HasOne("ProjectManagementApp.Api.Data.Entities.Member", "Member")
                        .WithMany()
                        .HasForeignKey("MemberId");

                    b.Navigation("Card");

                    b.Navigation("Member");
                });

            modelBuilder.Entity("ProjectManagementApp.Api.Data.Entities.ChecklistItem", b =>
                {
                    b.HasOne("ProjectManagementApp.Api.Data.Entities.Checklist", "Checklist")
                        .WithMany("ChecklistItems")
                        .HasForeignKey("ChecklistId");

                    b.HasOne("ProjectManagementApp.Api.Data.Entities.WorkFlow", null)
                        .WithMany("ChecklistItems")
                        .HasForeignKey("WorkFlowId");

                    b.Navigation("Checklist");
                });

            modelBuilder.Entity("ProjectManagementApp.Api.Data.Entities.Member", b =>
                {
                    b.HasOne("ProjectManagementApp.Api.Data.Entities.Card", null)
                        .WithMany("Member")
                        .HasForeignKey("CardId");

                    b.HasOne("ProjectManagementApp.Api.Data.Entities.ChecklistItem", null)
                        .WithMany("Member")
                        .HasForeignKey("ChecklistItemId");

                    b.HasOne("ProjectManagementApp.Api.Data.Entities.WorkFlowItem", null)
                        .WithMany("Member")
                        .HasForeignKey("WorkFlowItemId");
                });

            modelBuilder.Entity("ProjectManagementApp.Api.Data.Entities.WorkFlowItem", b =>
                {
                    b.HasOne("ProjectManagementApp.Api.Data.Entities.Card", null)
                        .WithMany("WorkFlowItem")
                        .HasForeignKey("CardId");

                    b.HasOne("ProjectManagementApp.Api.Data.Entities.WorkFlow", "WorkFlow")
                        .WithMany("WorkFlowItems")
                        .HasForeignKey("WorkFlowId");

                    b.Navigation("WorkFlow");
                });

            modelBuilder.Entity("ProjectManagementApp.Api.Data.Entities.Block", b =>
                {
                    b.Navigation("Cards");
                });

            modelBuilder.Entity("ProjectManagementApp.Api.Data.Entities.Board", b =>
                {
                    b.Navigation("Blocks");
                });

            modelBuilder.Entity("ProjectManagementApp.Api.Data.Entities.Card", b =>
                {
                    b.Navigation("Member");

                    b.Navigation("WorkFlowItem");
                });

            modelBuilder.Entity("ProjectManagementApp.Api.Data.Entities.Checklist", b =>
                {
                    b.Navigation("ChecklistItems");
                });

            modelBuilder.Entity("ProjectManagementApp.Api.Data.Entities.ChecklistItem", b =>
                {
                    b.Navigation("Member");
                });

            modelBuilder.Entity("ProjectManagementApp.Api.Data.Entities.WorkFlow", b =>
                {
                    b.Navigation("ChecklistItems");

                    b.Navigation("WorkFlowItems");
                });

            modelBuilder.Entity("ProjectManagementApp.Api.Data.Entities.WorkFlowItem", b =>
                {
                    b.Navigation("Member");
                });
#pragma warning restore 612, 618
        }
    }
}
