﻿using System;
using FluentMigrator;

namespace ci_project.db.migration
{
    [Migration(1)]
    public class CreateBookStoreTable : Migration
    {
        public override void Up()
        {
            Create.Table("book_stores")
                    .WithColumn("id").AsInt32().PrimaryKey().Identity()
                    .WithColumn("storeName").AsString()
                    .WithColumn("cashBalance").AsDouble()
                    .WithColumn("openingHours").AsString();
        }

        public override void Down()
        {
            Delete.Table("book_stores");
        }
    }

    [Migration(2)]
    public class CreateBookTable : Migration
    {
        public override void Up()
        {
            Create.Table("books")
                    .WithColumn("id").AsInt32().PrimaryKey().Identity()
                    .WithColumn("bookStoreId").AsInt32().ForeignKey()
                    .WithColumn("name").AsString()
                    .WithColumn("price").AsDouble();
        }

        public override void Down()
        {
            Delete.Table("books");
        }
    }

    [Migration(3)]
    public class CreatePurchaseHistoryTable : Migration
    {
        public override void Up()
        {
            Create.Table("purchase_histories")
                    .WithColumn("id").AsInt32().PrimaryKey().Identity()
                    .WithColumn("bookId").AsInt32().ForeignKey()
                    .WithColumn("userId").AsInt32().ForeignKey()
                    .WithColumn("transactionAmount").AsDouble()
                    .WithColumn("transactionDate").AsDateTime();
        }

        public override void Down()
        {
            Delete.Table("purchase_histories");
        }
    }

    [Migration(4)]
    public class CreateUserTable : Migration
    {
        public override void Up()
        {
            Create.Table("users")
                    .WithColumn("id").AsInt32().PrimaryKey().Identity()
                    .WithColumn("name").AsString()
                    .WithColumn("cashBalance").AsDouble();
        }

        public override void Down()
        {
            Delete.Table("users");
        }
    }
}
