using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace SC.API.CleanArchitecture.Infrastructure.Persistence.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NormalizedName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Audits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TableName = table.Column<string>(type: "text", nullable: true),
                    DateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    KeyValues = table.Column<string>(type: "text", nullable: true),
                    OldValues = table.Column<string>(type: "text", nullable: true),
                    NewValues = table.Column<string>(type: "text", nullable: true),
                    RecordedByParty = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Audits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Catalogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ParentCatalogId = table.Column<Guid>(type: "uuid", nullable: true),
                    SystemName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    UserFriendlyName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsUserDefinedSorting = table.Column<bool>(type: "boolean", nullable: false),
                    IsAlphabeticalSorting = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Catalogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Catalog_ParentCatalog",
                        column: x => x.ParentCatalogId,
                        principalTable: "Catalogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContactMediumClass",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactMediumClass", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DocumentExtension",
                columns: table => new
                {
                    Extension = table.Column<string>(type: "character varying(25)", unicode: false, maxLength: 25, nullable: false),
                    CommonName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentExtension", x => x.Extension);
                });

            migrationBuilder.CreateTable(
                name: "IdentificationSchema",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false),
                    GlobalID = table.Column<Guid>(type: "uuid", nullable: false, defaultValue: new Guid("1b0adce3-95a7-4e3a-aec4-82bc4589c749")),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    URI = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentificationSchema", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "IntegrationOrganizationSync",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TransactionID = table.Column<Guid>(type: "uuid", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsUpdate = table.Column<bool>(type: "boolean", nullable: false),
                    ExternalId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Oib = table.Column<string>(type: "text", nullable: true),
                    AddressStreet = table.Column<string>(type: "text", nullable: true),
                    AddressBuildingNumber = table.Column<string>(type: "text", nullable: true),
                    AddressPostalOfficeCode = table.Column<string>(type: "text", nullable: true),
                    AddressCity = table.Column<string>(type: "text", nullable: true),
                    AddressCountry = table.Column<string>(type: "text", nullable: true),
                    ErrorDetails = table.Column<string>(type: "text", nullable: true),
                    IsProcessed = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntegrationOrganizationSync", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InvolvementRoleType",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvolvementRoleType", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "LifeCycleClassDefinition",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false),
                    ClassName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LifeCycleClassDefinition", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "LifeCycleStateType",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LifeCycleStateType", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "LifeCycleTransition",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LifeCycleTransition", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MimeType",
                columns: table => new
                {
                    Extension = table.Column<string>(type: "character varying(25)", unicode: false, maxLength: 25, nullable: false),
                    CommonName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    FullTextSearchable = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MimeType", x => x.Extension);
                });

            migrationBuilder.CreateTable(
                name: "Operation",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operation", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PartyRoleAssociationType",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartyRoleAssociationType", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PartyRoleTypeDiscriminator",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartyRoleTypeDiscriminator", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PermissionGroup",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionGroup", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PermissionLevel",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionLevel", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PermissionTemplate",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionTemplate", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PermissionType",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionType", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: true),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationRoleClaims_ApplicationRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "ApplicationRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContactMedium",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Value = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: false),
                    ContactMediumClassID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactMedium", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ContactMedium_ContactMediumClass",
                        column: x => x.ContactMediumClassID,
                        principalTable: "ContactMediumClass",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Party",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GlobalID = table.Column<Guid>(type: "uuid", nullable: false, defaultValue: new Guid("4f9436a1-e4af-467d-ad2f-2081ac4b6c7e")),
                    ExternalID = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    ExternalIDIdentificationSchemaID = table.Column<int>(type: "integer", nullable: true),
                    AdditionalExternalID = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    AdditionalExternalIDIdentificationSchemaID = table.Column<int>(type: "integer", nullable: true),
                    Level = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Party", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Party_IdentificationSchema",
                        column: x => x.ExternalIDIdentificationSchemaID,
                        principalTable: "IdentificationSchema",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EntitySpecification",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    IsSearchable = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    LifeCycleClassDefinitionID = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntitySpecification", x => x.ID);
                    table.ForeignKey(
                        name: "FK_EntitySpecification_LifeCycleClassDefinition",
                        column: x => x.LifeCycleClassDefinitionID,
                        principalTable: "LifeCycleClassDefinition",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LifeCycle",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    LifeCycleStateTypeID = table.Column<int>(type: "integer", nullable: false),
                    LifeCycleClassDefinitionID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LifeCycle", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LifeCycle_LifeCycleClassDefinition",
                        column: x => x.LifeCycleClassDefinitionID,
                        principalTable: "LifeCycleClassDefinition",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LifeCycle_LifeCycleStateType",
                        column: x => x.LifeCycleStateTypeID,
                        principalTable: "LifeCycleStateType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LifeCycleClassDefLifeCycleTran",
                columns: table => new
                {
                    LifeCycleClassDefinitionID = table.Column<int>(type: "integer", nullable: false),
                    LifeCycleTransitionID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LifeCycleClassDefLifeCycleTran", x => new { x.LifeCycleClassDefinitionID, x.LifeCycleTransitionID });
                    table.ForeignKey(
                        name: "FK_LifeCycleClassDefLifeCycleTran_LifeCycleClassDefinition",
                        column: x => x.LifeCycleClassDefinitionID,
                        principalTable: "LifeCycleClassDefinition",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LifeCycleClassDefLifeCycleTran_LifeCycleTransition",
                        column: x => x.LifeCycleTransitionID,
                        principalTable: "LifeCycleTransition",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DocumentTemplate",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Binary = table.Column<byte[]>(type: "bytea", nullable: false),
                    Extension = table.Column<string>(type: "character varying(25)", unicode: false, maxLength: 25, nullable: false),
                    DocumentExtensionExtension = table.Column<string>(type: "character varying(25)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentTemplate", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DocumentTemplate_DocumentExtension_DocumentExtensionExtensi~",
                        column: x => x.DocumentExtensionExtension,
                        principalTable: "DocumentExtension",
                        principalColumn: "Extension",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DocumentTemplate_MimeType",
                        column: x => x.Extension,
                        principalTable: "MimeType",
                        principalColumn: "Extension",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OperationInvolvementRoleType",
                columns: table => new
                {
                    OperationID = table.Column<int>(type: "integer", nullable: false),
                    InvolvementRoleTypeID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.OperationInvolvementRoleType", x => new { x.OperationID, x.InvolvementRoleTypeID });
                    table.ForeignKey(
                        name: "FK_dbo.OperationInvolvementRoleType_dbo.InvolvementRoleType_InvolvementRoleTypeID",
                        column: x => x.InvolvementRoleTypeID,
                        principalTable: "InvolvementRoleType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.OperationInvolvementRoleType_dbo.Operation_OperationID",
                        column: x => x.OperationID,
                        principalTable: "Operation",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PartyRoleType",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    PartyRoleTypeDiscriminatorID = table.Column<int>(type: "integer", nullable: false),
                    ParentClassID = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartyRoleType", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PartyRoleType_PartyRoleType",
                        column: x => x.ParentClassID,
                        principalTable: "PartyRoleType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PartyRoleType_PartyRoleTypeDiscriminator",
                        column: x => x.PartyRoleTypeDiscriminatorID,
                        principalTable: "PartyRoleTypeDiscriminator",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Permission",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    PermissionTypeID = table.Column<int>(type: "integer", nullable: false),
                    PermissionLevelID = table.Column<int>(type: "integer", nullable: false),
                    PermissionGroupID = table.Column<int>(type: "integer", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Permission_PermissionGroup",
                        column: x => x.PermissionGroupID,
                        principalTable: "PermissionGroup",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Permission_PermissionLevel",
                        column: x => x.PermissionLevelID,
                        principalTable: "PermissionLevel",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Permission_PermissionType",
                        column: x => x.PermissionTypeID,
                        principalTable: "PermissionType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    PartyId = table.Column<int>(type: "integer", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Party_PartyId",
                        column: x => x.PartyId,
                        principalTable: "Party",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CatalogItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CatalogId = table.Column<Guid>(type: "uuid", nullable: false),
                    ParentCatalogItemId = table.Column<Guid>(type: "uuid", nullable: true),
                    SortIndex = table.Column<int>(type: "integer", nullable: false),
                    Value = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Metadata = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    RecordedByPartyId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CatalogItem_Catalog",
                        column: x => x.CatalogId,
                        principalTable: "Catalogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CatalogItem_Party",
                        column: x => x.RecordedByPartyId,
                        principalTable: "Party",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CatalogItems_CatalogItems_ParentCatalogItemId",
                        column: x => x.ParentCatalogItemId,
                        principalTable: "CatalogItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Individual",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false),
                    GivenName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    MiddleName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    FamilyName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    AliveDuringFrom = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    AliveDuringDue = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Individual", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Individual_Party",
                        column: x => x.ID,
                        principalTable: "Party",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Organization",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false),
                    TradingName = table.Column<string>(type: "character varying(510)", maxLength: 510, nullable: true),
                    ExistingDuringFrom = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ExistingDuringDue = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Type = table.Column<string>(type: "text", nullable: true),
                    LongName = table.Column<string>(type: "text", nullable: true),
                    Code = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    AddressStreetAndBuildingNumber = table.Column<string>(type: "text", nullable: true),
                    AddressPostalOfficeCode = table.Column<string>(type: "text", nullable: true),
                    AddressCity = table.Column<string>(type: "text", nullable: true),
                    AddressCountry = table.Column<string>(type: "text", nullable: true),
                    AddressLine = table.Column<string>(type: "text", nullable: true),
                    Level = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organization", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Organization_Party",
                        column: x => x.ID,
                        principalTable: "Party",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EntitySpecInvolvementRoleType",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InvolvementRoleTypeID = table.Column<int>(type: "integer", nullable: false),
                    EntitySpecificationID = table.Column<int>(type: "integer", nullable: false),
                    MinCardinality = table.Column<int>(type: "integer", nullable: false),
                    MaxCardinality = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntitySpecInvolvementRoleType", x => x.ID);
                    table.ForeignKey(
                        name: "FK_EntitySpecInvolvementRoleType_EntitySpecification",
                        column: x => x.EntitySpecificationID,
                        principalTable: "EntitySpecification",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EntitySpecInvolvementRoleType_InvolvementRoleType",
                        column: x => x.InvolvementRoleTypeID,
                        principalTable: "InvolvementRoleType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EntitySpecLifeCycleOperation",
                columns: table => new
                {
                    EntitySpecificationID = table.Column<int>(type: "integer", nullable: false),
                    LifeCycleID = table.Column<int>(type: "integer", nullable: false),
                    OperationID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntitySpecLifeCycleOperation", x => new { x.EntitySpecificationID, x.LifeCycleID, x.OperationID });
                    table.ForeignKey(
                        name: "FK_EntitySpecLifeCycleOperation_EntitySpecification",
                        column: x => x.EntitySpecificationID,
                        principalTable: "EntitySpecification",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EntitySpecLifeCycleOperation_LifeCycle",
                        column: x => x.LifeCycleID,
                        principalTable: "LifeCycle",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EntitySpecLifeCycleOperation_Operation",
                        column: x => x.OperationID,
                        principalTable: "Operation",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LifeCycleTransitionTable",
                columns: table => new
                {
                    CurrentLifeCycleID = table.Column<int>(type: "integer", nullable: false),
                    TransitionID = table.Column<int>(type: "integer", nullable: false),
                    NextLifeCycleID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LifeCycleTransitionTable", x => new { x.CurrentLifeCycleID, x.TransitionID });
                    table.ForeignKey(
                        name: "FK_dbo.LifeCycleTransitionTable_dbo.LifeCycle_CurrentLifeCycleID",
                        column: x => x.CurrentLifeCycleID,
                        principalTable: "LifeCycle",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.LifeCycleTransitionTable_dbo.LifeCycle_NextLifeCycleID",
                        column: x => x.NextLifeCycleID,
                        principalTable: "LifeCycle",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LifeCycleTransitionTable_LifeCycleTransition",
                        column: x => x.TransitionID,
                        principalTable: "LifeCycleTransition",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Document",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    FileName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Binary = table.Column<byte[]>(type: "bytea", nullable: false),
                    DocumentTemplateID = table.Column<int>(type: "integer", nullable: true),
                    Extension = table.Column<string>(type: "character varying(25)", unicode: false, maxLength: 25, nullable: false),
                    RecordedTimeStamp = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    ContentType = table.Column<string>(type: "character varying(510)", maxLength: 510, nullable: false),
                    Tags = table.Column<string>(type: "text", nullable: true),
                    DocumentDefinitionID = table.Column<int>(type: "integer", nullable: false),
                    RecordID = table.Column<Guid>(type: "uuid", nullable: false),
                    ExternalID = table.Column<Guid>(type: "uuid", nullable: true),
                    IsSynced = table.Column<bool>(type: "boolean", nullable: false),
                    DocumentExtensionExtension = table.Column<string>(type: "character varying(25)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Document", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Document_DocumentExtension_DocumentExtensionExtension",
                        column: x => x.DocumentExtensionExtension,
                        principalTable: "DocumentExtension",
                        principalColumn: "Extension",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Document_DocumentTemplate1",
                        column: x => x.DocumentTemplateID,
                        principalTable: "DocumentTemplate",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Document_MimeType_Extension",
                        column: x => x.Extension,
                        principalTable: "MimeType",
                        principalColumn: "Extension",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InvolvementRoleTypePartyRoleTypeInvolves",
                columns: table => new
                {
                    InvolvementRoleTypeID = table.Column<int>(type: "integer", nullable: false),
                    PartyRoleTypeID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvolvementRoleTypePartyRoleTypeInvolves", x => new { x.InvolvementRoleTypeID, x.PartyRoleTypeID });
                    table.ForeignKey(
                        name: "FK_dbo.InvolvementRoleTypePartyRoleTypeInvolves_dbo.InvolvementRoleType_InvolvementRoleTypeID",
                        column: x => x.InvolvementRoleTypeID,
                        principalTable: "InvolvementRoleType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.InvolvementRoleTypePartyRoleTypeInvolves_dbo.PartyRoleType_PartyRoleTypeID",
                        column: x => x.PartyRoleTypeID,
                        principalTable: "PartyRoleType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PartyRole",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PartyID = table.Column<int>(type: "integer", nullable: true),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    ValidDue = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    PartyRoleTypeID = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    JobTitle = table.Column<string>(type: "character varying(510)", maxLength: 510, nullable: true),
                    JobCode = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartyRole", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PartyRole_Party",
                        column: x => x.PartyID,
                        principalTable: "Party",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PartyRole_PartyRoleType",
                        column: x => x.PartyRoleTypeID,
                        principalTable: "PartyRoleType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PermissionRelationship",
                columns: table => new
                {
                    PermissionID = table.Column<int>(type: "integer", nullable: false),
                    RelatedPermissionID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionRelationship", x => new { x.PermissionID, x.RelatedPermissionID });
                    table.ForeignKey(
                        name: "FK_PermissionRelationship_Permission",
                        column: x => x.PermissionID,
                        principalTable: "Permission",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PermissionRelationship_Permission1",
                        column: x => x.RelatedPermissionID,
                        principalTable: "Permission",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PermissionTemplatePermission",
                columns: table => new
                {
                    PermissionTemplateID = table.Column<int>(type: "integer", nullable: false),
                    PermissionID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionTemplatePermission", x => new { x.PermissionTemplateID, x.PermissionID });
                    table.ForeignKey(
                        name: "FK_PermissionTemplatePermission_Permission",
                        column: x => x.PermissionID,
                        principalTable: "Permission",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PermissionTemplatePermission_PermissionTemplate",
                        column: x => x.PermissionTemplateID,
                        principalTable: "PermissionTemplate",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserRole",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: true),
                    RoleId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationUserRole_ApplicationRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "ApplicationRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApplicationUserRole_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Entity",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RecordID = table.Column<Guid>(type: "uuid", nullable: false, defaultValue: new Guid("d74f7cd6-d1e9-4a1b-b6cf-a67bc02f05af")),
                    RecordedTimeStamp = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    RecordedByPartyID = table.Column<int>(type: "integer", nullable: false),
                    LifeCycleChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    LifeCycleChangeByPartyID = table.Column<int>(type: "integer", nullable: true),
                    LifeCycleID = table.Column<int>(type: "integer", nullable: false),
                    EntitySpecificationID = table.Column<int>(type: "integer", nullable: false),
                    ModifiedByPartyID = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTimeStamp = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entity", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Entity_EntitySpecification",
                        column: x => x.EntitySpecificationID,
                        principalTable: "EntitySpecification",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Entity_Individual",
                        column: x => x.RecordedByPartyID,
                        principalTable: "Individual",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Entity_Individual1",
                        column: x => x.LifeCycleChangeByPartyID,
                        principalTable: "Individual",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Entity_Individual2",
                        column: x => x.ModifiedByPartyID,
                        principalTable: "Individual",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Entity_LifeCycle",
                        column: x => x.LifeCycleID,
                        principalTable: "LifeCycle",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EntitySpecInvolvRoleTypeUses",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EntitySpecInvolvementRoleTypeID = table.Column<int>(type: "integer", nullable: false),
                    PartyRoleID = table.Column<long>(type: "bigint", nullable: false),
                    PartyRoleAssociationTypeID = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntitySpecInvolvRoleTypeUses", x => x.ID);
                    table.ForeignKey(
                        name: "FK_dbo.EntitySpecInvolvRoleTypeUses_dbo.EntitySpecInvolvementRoleType_EntitySpecInvolvementRoleTypeID",
                        column: x => x.EntitySpecInvolvementRoleTypeID,
                        principalTable: "EntitySpecInvolvementRoleType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.EntitySpecInvolvRoleTypeUses_dbo.PartyRole_PartyRoleID",
                        column: x => x.PartyRoleID,
                        principalTable: "PartyRole",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.EntitySpecInvolvRoleTypeUses_dbo.PartyRoleAssociationType_PartyRoleAssociationTypeID",
                        column: x => x.PartyRoleAssociationTypeID,
                        principalTable: "PartyRoleAssociationType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InvolvementRole",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InvolvementRoleTypeID = table.Column<int>(type: "integer", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ValidDue = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    PartyRoleID = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvolvementRole", x => x.ID);
                    table.ForeignKey(
                        name: "FK_dbo.InvolvementRole_dbo.InvolvementRoleType_InvolvementRoleTypeID",
                        column: x => x.InvolvementRoleTypeID,
                        principalTable: "InvolvementRoleType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.InvolvementRole_dbo.PartyRole_PartyRoleID",
                        column: x => x.PartyRoleID,
                        principalTable: "PartyRole",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PartyRoleAssociation",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PartyRoleInvolvedWithID = table.Column<long>(type: "bigint", nullable: false),
                    PartyRoleInvolvesID = table.Column<long>(type: "bigint", nullable: false),
                    PartyRoleAssociationTypeID = table.Column<int>(type: "integer", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ValidDue = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartyRoleAssociation", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PartyRoleAssociation_PartyRole",
                        column: x => x.PartyRoleInvolvedWithID,
                        principalTable: "PartyRole",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PartyRoleAssociation_PartyRole1",
                        column: x => x.PartyRoleInvolvesID,
                        principalTable: "PartyRole",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PartyRoleAssociation_PartyRoleAssociationType",
                        column: x => x.PartyRoleAssociationTypeID,
                        principalTable: "PartyRoleAssociationType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PartyRoleContactableVia",
                columns: table => new
                {
                    ContactMediumID = table.Column<long>(type: "bigint", nullable: false),
                    PartyRoleID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartyRoleContactableVia", x => new { x.ContactMediumID, x.PartyRoleID });
                    table.ForeignKey(
                        name: "FK_PartyRoleContactableVia_ContactMedium",
                        column: x => x.ContactMediumID,
                        principalTable: "ContactMedium",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PartyRoleContactableVia_PartyRole",
                        column: x => x.PartyRoleID,
                        principalTable: "PartyRole",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EntityDocument",
                columns: table => new
                {
                    DocumentID = table.Column<long>(type: "bigint", nullable: false),
                    EntityID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.EntityDocument", x => new { x.DocumentID, x.EntityID });
                    table.ForeignKey(
                        name: "FK_dbo.EntityDocument_dbo.Document_DocumentID",
                        column: x => x.DocumentID,
                        principalTable: "Document",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.EntityDocument_dbo.Entity_EntityID",
                        column: x => x.EntityID,
                        principalTable: "Entity",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EntityLifeCycleHistoryLog",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EntityID = table.Column<long>(type: "bigint", nullable: false),
                    PreviousLifeCycleID = table.Column<int>(type: "integer", nullable: false),
                    CurrentLifeCycleID = table.Column<int>(type: "integer", nullable: false),
                    TransationID = table.Column<int>(type: "integer", nullable: false),
                    TransitionTimeStamp = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EntityInvolvementRoleID = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityLifeCycleHistoryLog", x => x.ID);
                    table.ForeignKey(
                        name: "FK_EntityLifeCycleHistoryLog_Entity",
                        column: x => x.EntityID,
                        principalTable: "Entity",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EntityLifeCycleHistoryLog_LifeCycleTransitionTable",
                        columns: x => new { x.PreviousLifeCycleID, x.TransationID },
                        principalTable: "LifeCycleTransitionTable",
                        principalColumns: new[] { "CurrentLifeCycleID", "TransitionID" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EntityOperationLog",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false),
                    EntityID = table.Column<long>(type: "bigint", nullable: false),
                    OperationID = table.Column<int>(type: "integer", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    OldObject = table.Column<string>(type: "text", nullable: false),
                    NewObject = table.Column<string>(type: "text", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityOperationLog", x => x.ID);
                    table.ForeignKey(
                        name: "FK_EntityOperationLog_AspNetUsers",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EntityOperationLog_Entity",
                        column: x => x.EntityID,
                        principalTable: "Entity",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EntityOperationLog_Operation",
                        column: x => x.OperationID,
                        principalTable: "Operation",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EntityProcessLog",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EntityID = table.Column<long>(type: "bigint", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    ExecutorName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Note = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    OperationID = table.Column<int>(type: "integer", nullable: true),
                    ApplicationUserId = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityProcessLog", x => x.ID);
                    table.ForeignKey(
                        name: "FK_EntityProcessLog_AspNetUsers",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EntityProcessLog_Entity",
                        column: x => x.EntityID,
                        principalTable: "Entity",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EntityProcessLog_Operation",
                        column: x => x.OperationID,
                        principalTable: "Operation",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EntityInvolvementRole",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false),
                    EntityID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityInvolvementRole", x => x.ID);
                    table.ForeignKey(
                        name: "FK_dbo.EntityInvolvementRole_dbo.Entity_EntityID",
                        column: x => x.EntityID,
                        principalTable: "Entity",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.EntityInvolvementRole_dbo.InvolvementRole_ID",
                        column: x => x.ID,
                        principalTable: "InvolvementRole",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PermissionPartyRoleAssociation",
                columns: table => new
                {
                    PermissionID = table.Column<int>(type: "integer", nullable: false),
                    PartyRoleAssociationID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.PermissionPartyRoleAssociation", x => new { x.PermissionID, x.PartyRoleAssociationID });
                    table.ForeignKey(
                        name: "FK_PermissionPartyRoleAssociation_PartyRoleAssociation",
                        column: x => x.PartyRoleAssociationID,
                        principalTable: "PartyRoleAssociation",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PermissionPartyRoleAssociation_Permission",
                        column: x => x.PermissionID,
                        principalTable: "Permission",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Catalogs",
                columns: new[] { "Id", "DeletedDate", "IsAlphabeticalSorting", "IsDeleted", "IsUserDefinedSorting", "ParentCatalogId", "SystemName", "UserFriendlyName" },
                values: new object[] { new Guid("878853a7-f4d9-474d-8515-766ba0ee2dd9"), null, false, false, false, null, "CatCurrencies", "Currencies" });

            migrationBuilder.InsertData(
                table: "ContactMediumClass",
                columns: new[] { "ID", "Description", "Name" },
                values: new object[,]
                {
                    { 1, null, "Email" },
                    { 2, null, "Telephone" },
                    { 3, null, "Mobile" },
                    { 4, null, "Fax" },
                    { 5, null, "InternalTelephone" }
                });

            migrationBuilder.InsertData(
                table: "DocumentExtension",
                columns: new[] { "Extension", "CommonName", "Description" },
                values: new object[,]
                {
                    { ".xls", null, null },
                    { ".svg", null, null },
                    { ".rar", null, null },
                    { ".png", null, null },
                    { ".pjpeg", null, null },
                    { ".pjp", null, null },
                    { ".jpeg", null, null },
                    { ".jpg", null, null },
                    { ".xlsx", null, null },
                    { ".docx", null, null },
                    { ".doc", null, null },
                    { ".7z", null, null },
                    { ".pdf", null, null },
                    { ".zip", null, null }
                });

            migrationBuilder.InsertData(
                table: "IdentificationSchema",
                columns: new[] { "ID", "Description", "Name", "URI" },
                values: new object[,]
                {
                    { 2, null, "City", null },
                    { 1, null, "HrVat", null }
                });

            migrationBuilder.InsertData(
                table: "InvolvementRoleType",
                columns: new[] { "ID", "Description", "Name" },
                values: new object[,]
                {
                    { 0, null, "EntityOwner" },
                    { 1, null, "Approver" },
                    { 2, null, "Administrator" },
                    { 3, null, "Creator" },
                    { 4, null, "AssignedTo" },
                    { 5, null, "Associate" },
                    { 6, null, "AssociateReadOnly" }
                });

            migrationBuilder.InsertData(
                table: "LifeCycleClassDefinition",
                columns: new[] { "ID", "ClassName" },
                values: new object[] { 1, "TicketLifeCycle" });

            migrationBuilder.InsertData(
                table: "LifeCycleStateType",
                columns: new[] { "ID", "Name" },
                values: new object[,]
                {
                    { 3, "SemiFinal" },
                    { 4, "Final" },
                    { 2, "Transition" },
                    { 1, "Initial" }
                });

            migrationBuilder.InsertData(
                table: "PartyRoleAssociationType",
                columns: new[] { "ID", "Description", "Name" },
                values: new object[,]
                {
                    { 0, null, "EntityObjectAccess" },
                    { 1, null, "Employment" },
                    { 2, null, "Membership" },
                    { 3, null, "Workplace" },
                    { 4, null, "Management" },
                    { 5, null, "EntityObjectAdministration" },
                    { 6, null, "ServiceProviding" },
                    { 7, null, "OrganizationDecomposition" }
                });

            migrationBuilder.InsertData(
                table: "PartyRoleTypeDiscriminator",
                columns: new[] { "ID", "Description", "Name" },
                values: new object[,]
                {
                    { 1, null, "OrganizationPartyRoleTypes" },
                    { 2, null, "IndividualPartyRoleTypes" }
                });

            migrationBuilder.InsertData(
                table: "CatalogItems",
                columns: new[] { "Id", "CatalogId", "DeletedDate", "IsDeleted", "Metadata", "ParentCatalogItemId", "RecordedByPartyId", "SortIndex", "Value" },
                values: new object[,]
                {
                    { new Guid("cae68d51-0477-4a83-b22a-32b6f7b8733c"), new Guid("878853a7-f4d9-474d-8515-766ba0ee2dd9"), null, false, null, null, null, 1, "EUR" },
                    { new Guid("6667ebb9-7be5-4ce8-8f85-89f23fb31ed6"), new Guid("878853a7-f4d9-474d-8515-766ba0ee2dd9"), null, false, null, null, null, 2, "HRK" },
                    { new Guid("6d56f3c9-a43b-4260-9f02-43d8db2d8223"), new Guid("878853a7-f4d9-474d-8515-766ba0ee2dd9"), null, false, null, null, null, 3, "USD" }
                });

            migrationBuilder.InsertData(
                table: "EntitySpecification",
                columns: new[] { "ID", "LifeCycleClassDefinitionID", "Name" },
                values: new object[] { 1, 1, "Ticket" });

            migrationBuilder.InsertData(
                table: "LifeCycle",
                columns: new[] { "ID", "LifeCycleClassDefinitionID", "LifeCycleStateTypeID", "Name" },
                values: new object[,]
                {
                    { 11, 1, 4, "Unavailable" },
                    { 10, 1, 4, "Issued" },
                    { 9, 1, 4, "Excluded" },
                    { 5, 1, 4, "Archived" },
                    { 7, 1, 3, "Aggregated" },
                    { 4, 1, 3, "Opened by mistake" },
                    { 6, 1, 4, "Original" },
                    { 3, 1, 2, "Delegated" },
                    { 2, 1, 1, "Received" },
                    { 1, 1, 1, "Created" },
                    { 8, 1, 2, "Suspended" }
                });

            migrationBuilder.InsertData(
                table: "Party",
                columns: new[] { "ID", "AdditionalExternalIDIdentificationSchemaID", "AdditionalExternalID", "ExternalID", "ExternalIDIdentificationSchemaID", "GlobalID", "Level" },
                values: new object[,]
                {
                    { 1, null, null, "61817894937", 2, new Guid("3cebb7da-4cfa-46e7-be74-66df1e4a6e7c"), 0 },
                    { 2, null, null, "-", 1, new Guid("a3149834-b9c7-497a-bb1c-206504a17aa4"), 0 }
                });

            migrationBuilder.InsertData(
                table: "PartyRoleType",
                columns: new[] { "ID", "Description", "Name", "ParentClassID", "PartyRoleTypeDiscriminatorID" },
                values: new object[,]
                {
                    { 20, null, "Intermediary", null, 1 },
                    { 25, null, "ServiceProviderServiceAccount", null, 1 },
                    { 26, null, "PlatformOwner", null, 1 },
                    { 31, null, "Tenant", null, 1 },
                    { 7, null, "OrganizationPost", null, 2 },
                    { 4, null, "TeamMember", null, 2 },
                    { 27, null, "Member", null, 2 },
                    { 28, null, "MemberBusinessAdministrator", null, 2 },
                    { 19, null, "ServiceProvider", null, 1 },
                    { 1, null, "Employee", null, 2 },
                    { 18, null, "Vendor", null, 1 },
                    { 6, null, "OrganizationDepartment", null, 1 },
                    { 16, null, "Buyer", null, 1 },
                    { 15, null, "Supplier", null, 1 },
                    { 14, null, "Partner", null, 1 },
                    { 13, null, "Customer", null, 1 },
                    { 9, null, "OrganizationSpecialistTeam", null, 1 },
                    { 8, null, "OrganizationExpertTeam", null, 1 },
                    { 29, null, "MemberBusinessApprover", null, 2 },
                    { 5, null, "Organization", null, 1 },
                    { 3, null, "Team", null, 1 },
                    { 2, null, "Employer", null, 1 },
                    { 17, null, "Competitor", null, 1 },
                    { 30, null, "OrganizationPostCEO", null, 2 }
                });

            migrationBuilder.InsertData(
                table: "Individual",
                columns: new[] { "ID", "AliveDuringDue", "AliveDuringFrom", "FamilyName", "GivenName", "MiddleName", "Username" },
                values: new object[] { 2, null, null, "2", "SCSA", null, "ServiceAccount" });

            migrationBuilder.InsertData(
                table: "PartyRole",
                columns: new[] { "ID", "IsActive", "JobCode", "JobTitle", "Name", "PartyID", "PartyRoleTypeID", "ValidDue", "ValidFrom" },
                values: new object[] { 1L, true, null, null, null, 1, 31, null, new DateTime(2020, 12, 21, 14, 46, 19, 263, DateTimeKind.Utc).AddTicks(7881) });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationRoleClaims_RoleId",
                table: "ApplicationRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserRole_RoleId",
                table: "ApplicationUserRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserRole_UserId",
                table: "ApplicationUserRole",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PartyId",
                table: "AspNetUsers",
                column: "PartyId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CatalogItems_CatalogId",
                table: "CatalogItems",
                column: "CatalogId");

            migrationBuilder.CreateIndex(
                name: "IX_CatalogItems_ParentCatalogItemId",
                table: "CatalogItems",
                column: "ParentCatalogItemId");

            migrationBuilder.CreateIndex(
                name: "IX_CatalogItems_RecordedByPartyId",
                table: "CatalogItems",
                column: "RecordedByPartyId");

            migrationBuilder.CreateIndex(
                name: "IX_CatalogItems_Value",
                table: "CatalogItems",
                column: "Value");

            migrationBuilder.CreateIndex(
                name: "IX_Catalogs_ParentCatalogId",
                table: "Catalogs",
                column: "ParentCatalogId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactMedium_ContactMediumClassID",
                table: "ContactMedium",
                column: "ContactMediumClassID");

            migrationBuilder.CreateIndex(
                name: "IX_Document_DocumentExtensionExtension",
                table: "Document",
                column: "DocumentExtensionExtension");

            migrationBuilder.CreateIndex(
                name: "IX_Document_DocumentTemplateID",
                table: "Document",
                column: "DocumentTemplateID");

            migrationBuilder.CreateIndex(
                name: "IX_Document_Extension",
                table: "Document",
                column: "Extension");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentTemplate_DocumentExtensionExtension",
                table: "DocumentTemplate",
                column: "DocumentExtensionExtension");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentTemplate_Extension",
                table: "DocumentTemplate",
                column: "Extension");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_EntitySpecificationID",
                table: "Entity",
                column: "EntitySpecificationID");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_ID_EntitySpecificationID",
                table: "Entity",
                columns: new[] { "ID", "EntitySpecificationID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Entity_ID_LifeCycleID_IsDeleted",
                table: "Entity",
                columns: new[] { "ID", "LifeCycleID", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_Entity_LifeCycleChangeByPartyID",
                table: "Entity",
                column: "LifeCycleChangeByPartyID");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_LifeCycleID",
                table: "Entity",
                column: "LifeCycleID");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_ModifiedByPartyID",
                table: "Entity",
                column: "ModifiedByPartyID");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_RecordedByPartyID",
                table: "Entity",
                column: "RecordedByPartyID");

            migrationBuilder.CreateIndex(
                name: "IX_EntityDocument_DocumentID",
                table: "EntityDocument",
                column: "DocumentID");

            migrationBuilder.CreateIndex(
                name: "IX_EntityDocument_EntityID",
                table: "EntityDocument",
                column: "EntityID");

            migrationBuilder.CreateIndex(
                name: "IX_EntityInvolvementRole_EntityID",
                table: "EntityInvolvementRole",
                column: "EntityID");

            migrationBuilder.CreateIndex(
                name: "IX_EntityInvolvementRole_ID",
                table: "EntityInvolvementRole",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_EntityLifeCycleHistoryLog_EntityID",
                table: "EntityLifeCycleHistoryLog",
                column: "EntityID");

            migrationBuilder.CreateIndex(
                name: "IX_EntityLifeCycleHistoryLog_EntityID_TransationID",
                table: "EntityLifeCycleHistoryLog",
                columns: new[] { "EntityID", "TransationID" });

            migrationBuilder.CreateIndex(
                name: "IX_EntityLifeCycleHistoryLog_PreviousLifeCycleID_CurrentLifeCy~",
                table: "EntityLifeCycleHistoryLog",
                columns: new[] { "PreviousLifeCycleID", "CurrentLifeCycleID" });

            migrationBuilder.CreateIndex(
                name: "IX_EntityLifeCycleHistoryLog_PreviousLifeCycleID_TransationID",
                table: "EntityLifeCycleHistoryLog",
                columns: new[] { "PreviousLifeCycleID", "TransationID" });

            migrationBuilder.CreateIndex(
                name: "IX_EntityOperationLog_ApplicationUserId",
                table: "EntityOperationLog",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityOperationLog_EntityID",
                table: "EntityOperationLog",
                column: "EntityID");

            migrationBuilder.CreateIndex(
                name: "IX_EntityOperationLog_OperationID",
                table: "EntityOperationLog",
                column: "OperationID");

            migrationBuilder.CreateIndex(
                name: "IX_EntityProcessLog_ApplicationUserId",
                table: "EntityProcessLog",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityProcessLog_EntityID",
                table: "EntityProcessLog",
                column: "EntityID");

            migrationBuilder.CreateIndex(
                name: "IX_EntityProcessLog_ID_Timestamp_ExecutorName_Note_EntityID",
                table: "EntityProcessLog",
                columns: new[] { "ID", "Timestamp", "ExecutorName", "Note", "EntityID" });

            migrationBuilder.CreateIndex(
                name: "IX_EntityProcessLog_OperationID",
                table: "EntityProcessLog",
                column: "OperationID");

            migrationBuilder.CreateIndex(
                name: "IX_EntitySpecification_LifeCycleClassDefinitionID",
                table: "EntitySpecification",
                column: "LifeCycleClassDefinitionID");

            migrationBuilder.CreateIndex(
                name: "IX_EntitySpecInvolvementRoleType_EntitySpecificationID_Involve~",
                table: "EntitySpecInvolvementRoleType",
                columns: new[] { "EntitySpecificationID", "InvolvementRoleTypeID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EntitySpecInvolvementRoleType_InvolvementRoleTypeID",
                table: "EntitySpecInvolvementRoleType",
                column: "InvolvementRoleTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_EntitySpecInvolvRoleTypeUses_EntitySpecInvolvementRoleTypeID",
                table: "EntitySpecInvolvRoleTypeUses",
                column: "EntitySpecInvolvementRoleTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_EntitySpecInvolvRoleTypeUses_PartyRoleAssociationTypeID",
                table: "EntitySpecInvolvRoleTypeUses",
                column: "PartyRoleAssociationTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_EntitySpecInvolvRoleTypeUses_PartyRoleID",
                table: "EntitySpecInvolvRoleTypeUses",
                column: "PartyRoleID");

            migrationBuilder.CreateIndex(
                name: "IX_EntitySpecLifeCycleOperation_EntitySpecificationID_LifeCycl~",
                table: "EntitySpecLifeCycleOperation",
                columns: new[] { "EntitySpecificationID", "LifeCycleID", "OperationID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EntitySpecLifeCycleOperation_LifeCycleID",
                table: "EntitySpecLifeCycleOperation",
                column: "LifeCycleID");

            migrationBuilder.CreateIndex(
                name: "IX_EntitySpecLifeCycleOperation_OperationID",
                table: "EntitySpecLifeCycleOperation",
                column: "OperationID");

            migrationBuilder.CreateIndex(
                name: "IX_Individual_Username",
                table: "Individual",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvolvementRole_InvolvementRoleTypeID",
                table: "InvolvementRole",
                column: "InvolvementRoleTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_InvolvementRole_PartyRoleID",
                table: "InvolvementRole",
                column: "PartyRoleID");

            migrationBuilder.CreateIndex(
                name: "IX_InvolvementRoleTypePartyRoleTypeInvolves_InvolvementRoleTyp~",
                table: "InvolvementRoleTypePartyRoleTypeInvolves",
                column: "InvolvementRoleTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_InvolvementRoleTypePartyRoleTypeInvolves_PartyRoleTypeID",
                table: "InvolvementRoleTypePartyRoleTypeInvolves",
                column: "PartyRoleTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_LifeCycle_LifeCycleClassDefinitionID",
                table: "LifeCycle",
                column: "LifeCycleClassDefinitionID");

            migrationBuilder.CreateIndex(
                name: "IX_LifeCycle_LifeCycleStateTypeID",
                table: "LifeCycle",
                column: "LifeCycleStateTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_LifeCycleClassDefLifeCycleTran_LifeCycleTransitionID",
                table: "LifeCycleClassDefLifeCycleTran",
                column: "LifeCycleTransitionID");

            migrationBuilder.CreateIndex(
                name: "IX_LifeCycleTransitionTable_CurrentLifeCycleID",
                table: "LifeCycleTransitionTable",
                column: "CurrentLifeCycleID");

            migrationBuilder.CreateIndex(
                name: "IX_LifeCycleTransitionTable_NextLifeCycleID",
                table: "LifeCycleTransitionTable",
                column: "NextLifeCycleID");

            migrationBuilder.CreateIndex(
                name: "IX_LifeCycleTransitionTable_TransitionID",
                table: "LifeCycleTransitionTable",
                column: "TransitionID");

            migrationBuilder.CreateIndex(
                name: "IX_Operation_Name",
                table: "Operation",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OperationInvolvementRoleType_InvolvementRoleTypeID",
                table: "OperationInvolvementRoleType",
                column: "InvolvementRoleTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_OperationInvolvementRoleType_OperationID",
                table: "OperationInvolvementRoleType",
                column: "OperationID");

            migrationBuilder.CreateIndex(
                name: "IX_Party_ExternalIDIdentificationSchemaID_ExternalID",
                table: "Party",
                columns: new[] { "ExternalIDIdentificationSchemaID", "ExternalID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PartyRole_PartyID",
                table: "PartyRole",
                column: "PartyID");

            migrationBuilder.CreateIndex(
                name: "IX_PartyRole_PartyRoleTypeID",
                table: "PartyRole",
                column: "PartyRoleTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_PartyRoleAssociation_PartyRoleAssociationTypeID",
                table: "PartyRoleAssociation",
                column: "PartyRoleAssociationTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_PartyRoleAssociation_PartyRoleInvolvedWithID",
                table: "PartyRoleAssociation",
                column: "PartyRoleInvolvedWithID");

            migrationBuilder.CreateIndex(
                name: "IX_PartyRoleAssociation_PartyRoleInvolvedWithID_PartyRoleInvo~1",
                table: "PartyRoleAssociation",
                columns: new[] { "PartyRoleInvolvedWithID", "PartyRoleInvolvesID", "PartyRoleAssociationTypeID" });

            migrationBuilder.CreateIndex(
                name: "IX_PartyRoleAssociation_PartyRoleInvolvedWithID_PartyRoleInvol~",
                table: "PartyRoleAssociation",
                columns: new[] { "PartyRoleInvolvedWithID", "PartyRoleInvolvesID" });

            migrationBuilder.CreateIndex(
                name: "IX_PartyRoleAssociation_PartyRoleInvolvesID",
                table: "PartyRoleAssociation",
                column: "PartyRoleInvolvesID");

            migrationBuilder.CreateIndex(
                name: "IX_PartyRoleContactableVia_ContactMediumID",
                table: "PartyRoleContactableVia",
                column: "ContactMediumID");

            migrationBuilder.CreateIndex(
                name: "IX_PartyRoleContactableVia_PartyRoleID",
                table: "PartyRoleContactableVia",
                column: "PartyRoleID");

            migrationBuilder.CreateIndex(
                name: "IX_PartyRoleType_ParentClassID",
                table: "PartyRoleType",
                column: "ParentClassID");

            migrationBuilder.CreateIndex(
                name: "IX_PartyRoleType_PartyRoleTypeDiscriminatorID",
                table: "PartyRoleType",
                column: "PartyRoleTypeDiscriminatorID");

            migrationBuilder.CreateIndex(
                name: "IX_Permission_Name",
                table: "Permission",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Permission_PermissionGroupID",
                table: "Permission",
                column: "PermissionGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_Permission_PermissionLevelID",
                table: "Permission",
                column: "PermissionLevelID");

            migrationBuilder.CreateIndex(
                name: "IX_Permission_PermissionTypeID",
                table: "Permission",
                column: "PermissionTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionPartyRoleAssociation_PartyRoleAssociationID",
                table: "PermissionPartyRoleAssociation",
                column: "PartyRoleAssociationID");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionPartyRoleAssociation_PermissionID",
                table: "PermissionPartyRoleAssociation",
                column: "PermissionID");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionRelationship_RelatedPermissionID",
                table: "PermissionRelationship",
                column: "RelatedPermissionID");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionTemplatePermission_PermissionID",
                table: "PermissionTemplatePermission",
                column: "PermissionID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationRoleClaims");

            migrationBuilder.DropTable(
                name: "ApplicationUserRole");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Audits");

            migrationBuilder.DropTable(
                name: "CatalogItems");

            migrationBuilder.DropTable(
                name: "EntityDocument");

            migrationBuilder.DropTable(
                name: "EntityInvolvementRole");

            migrationBuilder.DropTable(
                name: "EntityLifeCycleHistoryLog");

            migrationBuilder.DropTable(
                name: "EntityOperationLog");

            migrationBuilder.DropTable(
                name: "EntityProcessLog");

            migrationBuilder.DropTable(
                name: "EntitySpecInvolvRoleTypeUses");

            migrationBuilder.DropTable(
                name: "EntitySpecLifeCycleOperation");

            migrationBuilder.DropTable(
                name: "IntegrationOrganizationSync");

            migrationBuilder.DropTable(
                name: "InvolvementRoleTypePartyRoleTypeInvolves");

            migrationBuilder.DropTable(
                name: "LifeCycleClassDefLifeCycleTran");

            migrationBuilder.DropTable(
                name: "OperationInvolvementRoleType");

            migrationBuilder.DropTable(
                name: "Organization");

            migrationBuilder.DropTable(
                name: "PartyRoleContactableVia");

            migrationBuilder.DropTable(
                name: "PermissionPartyRoleAssociation");

            migrationBuilder.DropTable(
                name: "PermissionRelationship");

            migrationBuilder.DropTable(
                name: "PermissionTemplatePermission");

            migrationBuilder.DropTable(
                name: "ApplicationRoles");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Catalogs");

            migrationBuilder.DropTable(
                name: "Document");

            migrationBuilder.DropTable(
                name: "InvolvementRole");

            migrationBuilder.DropTable(
                name: "LifeCycleTransitionTable");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Entity");

            migrationBuilder.DropTable(
                name: "EntitySpecInvolvementRoleType");

            migrationBuilder.DropTable(
                name: "Operation");

            migrationBuilder.DropTable(
                name: "ContactMedium");

            migrationBuilder.DropTable(
                name: "PartyRoleAssociation");

            migrationBuilder.DropTable(
                name: "Permission");

            migrationBuilder.DropTable(
                name: "PermissionTemplate");

            migrationBuilder.DropTable(
                name: "DocumentTemplate");

            migrationBuilder.DropTable(
                name: "LifeCycleTransition");

            migrationBuilder.DropTable(
                name: "Individual");

            migrationBuilder.DropTable(
                name: "LifeCycle");

            migrationBuilder.DropTable(
                name: "EntitySpecification");

            migrationBuilder.DropTable(
                name: "InvolvementRoleType");

            migrationBuilder.DropTable(
                name: "ContactMediumClass");

            migrationBuilder.DropTable(
                name: "PartyRole");

            migrationBuilder.DropTable(
                name: "PartyRoleAssociationType");

            migrationBuilder.DropTable(
                name: "PermissionGroup");

            migrationBuilder.DropTable(
                name: "PermissionLevel");

            migrationBuilder.DropTable(
                name: "PermissionType");

            migrationBuilder.DropTable(
                name: "DocumentExtension");

            migrationBuilder.DropTable(
                name: "MimeType");

            migrationBuilder.DropTable(
                name: "LifeCycleStateType");

            migrationBuilder.DropTable(
                name: "LifeCycleClassDefinition");

            migrationBuilder.DropTable(
                name: "Party");

            migrationBuilder.DropTable(
                name: "PartyRoleType");

            migrationBuilder.DropTable(
                name: "IdentificationSchema");

            migrationBuilder.DropTable(
                name: "PartyRoleTypeDiscriminator");
        }
    }
}
