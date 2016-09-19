namespace ERP_BANK.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FNameClient = c.String(unicode: false),
                        LNameClient = c.String(unicode: false),
                        CinClient = c.String(unicode: false),
                        AccountBalance = c.Single(nullable: false),
                        Libelle = c.String(unicode: false),
                        Currency = c.String(unicode: false),
                        Note = c.String(unicode: false),
                        salary = c.Single(nullable: false),
                        Type = c.String(unicode: false),
                        DirectionId = c.Int(),
                        DateDeblocked = c.DateTime(precision: 0),
                        Blocked = c.Boolean(),
                        Benefit = c.Single(),
                        DateofSaving = c.DateTime(precision: 0),
                        Blocked1 = c.Boolean(),
                        Benefit1 = c.Single(),
                        TypeAccount = c.String(maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.directions", t => t.DirectionId)
                .Index(t => t.DirectionId);
            
            CreateTable(
                "dbo.directions",
                c => new
                    {
                        idDirection = c.Int(nullable: false, identity: true),
                        DirectionName = c.String(maxLength: 45, storeType: "nvarchar"),
                        DirectionType = c.String(maxLength: 45, storeType: "nvarchar"),
                        DirectionBudget = c.String(maxLength: 45, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.idDirection);
            
            CreateTable(
                "dbo.invoices",
                c => new
                    {
                        IdInvoice = c.Int(nullable: false, identity: true),
                        InvoiceDate = c.DateTime(precision: 0),
                        InvoiceType = c.String(maxLength: 45, storeType: "nvarchar"),
                        InvoiceOwner = c.String(maxLength: 45, storeType: "nvarchar"),
                        InvoiceDescription = c.String(maxLength: 255, storeType: "nvarchar"),
                        InvoicePrice = c.Double(),
                        StatusInventory = c.Boolean(),
                        Quantity = c.Int(),
                        Product = c.String(unicode: false),
                        ItemsString = c.String(unicode: false),
                        PK_InvoiceDirection = c.Int(),
                    })
                .PrimaryKey(t => t.IdInvoice)
                .ForeignKey("dbo.directions", t => t.PK_InvoiceDirection)
                .Index(t => t.PK_InvoiceDirection);
            
            CreateTable(
                "dbo.loan_join_account",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        FK_loan = c.Int(nullable: false),
                        FK_account = c.Int(nullable: false),
                        date = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Accounts", t => t.FK_account, cascadeDelete: true)
                .ForeignKey("dbo.loans", t => t.FK_loan, cascadeDelete: true)
                .Index(t => t.FK_loan)
                .Index(t => t.FK_account);
            
            CreateTable(
                "dbo.loans",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(unicode: false),
                        Amount = c.Double(nullable: false),
                        Period = c.Int(nullable: false),
                        MonthlyPayment = c.Double(),
                        Warranty = c.String(unicode: false),
                        InterestRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AmountWithInterest = c.Double(),
                        RestAmount = c.Double(),
                        IdAccount = c.Int(nullable: false),
                        IsPayed = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.IdAccount, cascadeDelete: true)
                .Index(t => t.IdAccount);
            
            CreateTable(
                "dbo.brokers",
                c => new
                    {
                        idbroker = c.Int(nullable: false, identity: true),
                        defaultCurrency = c.String(nullable: false, maxLength: 45, storeType: "nvarchar"),
                        minDeposit = c.Single(nullable: false),
                        bonus = c.Single(nullable: false),
                        spred = c.Single(nullable: false),
                        payment = c.String(nullable: false, maxLength: 45, storeType: "nvarchar"),
                        title = c.String(nullable: false, maxLength: 45, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.idbroker);
            
            CreateTable(
                "dbo.shares",
                c => new
                    {
                        idshare = c.Int(nullable: false, identity: true),
                        currency1 = c.String(nullable: false, maxLength: 45, storeType: "nvarchar"),
                        currency2 = c.String(nullable: false, maxLength: 45, storeType: "nvarchar"),
                        amountInvested = c.Single(nullable: false),
                        dateOfPurchase = c.DateTime(precision: 0),
                        dateOfSale = c.DateTime(precision: 0),
                        gain = c.Single(nullable: false),
                        email = c.String(nullable: false, maxLength: 45, storeType: "nvarchar"),
                        idBroker = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.idshare)
                .ForeignKey("dbo.brokers", t => t.idBroker, cascadeDelete: true)
                .Index(t => t.idBroker);
            
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TransmitterAccountId = c.Int(),
                        BeneficiaryAccountId = c.Int(),
                        OperationDate = c.DateTime(nullable: false, precision: 0),
                        ValueDate = c.DateTime(nullable: false, precision: 0),
                        Amount = c.Single(nullable: false),
                        Status = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.BeneficiaryAccountId)
                .ForeignKey("dbo.Accounts", t => t.TransmitterAccountId)
                .Index(t => t.TransmitterAccountId)
                .Index(t => t.BeneficiaryAccountId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transactions", "TransmitterAccountId", "dbo.Accounts");
            DropForeignKey("dbo.Transactions", "BeneficiaryAccountId", "dbo.Accounts");
            DropForeignKey("dbo.shares", "idBroker", "dbo.brokers");
            DropForeignKey("dbo.loan_join_account", "FK_loan", "dbo.loans");
            DropForeignKey("dbo.loans", "IdAccount", "dbo.Accounts");
            DropForeignKey("dbo.loan_join_account", "FK_account", "dbo.Accounts");
            DropForeignKey("dbo.Accounts", "DirectionId", "dbo.directions");
            DropForeignKey("dbo.invoices", "PK_InvoiceDirection", "dbo.directions");
            DropIndex("dbo.Transactions", new[] { "BeneficiaryAccountId" });
            DropIndex("dbo.Transactions", new[] { "TransmitterAccountId" });
            DropIndex("dbo.shares", new[] { "idBroker" });
            DropIndex("dbo.loans", new[] { "IdAccount" });
            DropIndex("dbo.loan_join_account", new[] { "FK_account" });
            DropIndex("dbo.loan_join_account", new[] { "FK_loan" });
            DropIndex("dbo.invoices", new[] { "PK_InvoiceDirection" });
            DropIndex("dbo.Accounts", new[] { "DirectionId" });
            DropTable("dbo.Transactions");
            DropTable("dbo.shares");
            DropTable("dbo.brokers");
            DropTable("dbo.loans");
            DropTable("dbo.loan_join_account");
            DropTable("dbo.invoices");
            DropTable("dbo.directions");
            DropTable("dbo.Accounts");
        }
    }
}
