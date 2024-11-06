using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PDFLines.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    ClientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "VARCHAR(128)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ClientId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Alias = table.Column<string>(type: "VARCHAR(128)", nullable: false),
                    NameSurname = table.Column<string>(type: "NVARCHAR(512)", nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR(512)", nullable: false),
                    AccessLevel = table.Column<string>(type: "VARCHAR(128)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(128)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    FileName = table.Column<string>(type: "VARCHAR(128)", nullable: false),
                    FileLocation = table.Column<string>(type: "VARCHAR(1028)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.ProjectId);
                    table.ForeignKey(
                        name: "FK_Projects_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Projects_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Maintenance",
                columns: table => new
                {
                    MaintenanceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "NVARCHAR(1024)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maintenance", x => x.MaintenanceId);
                    table.ForeignKey(
                        name: "FK_Maintenance_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Maintenance_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Maintenance_ProjectId",
                table: "Maintenance",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Maintenance_UserId",
                table: "Maintenance",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ClientId",
                table: "Projects",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_UserId",
                table: "Projects",
                column: "UserId");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "EmployeeId", "Alias", "NameSurname", "Email", "AccessLevel" },
                values: new object[,]
                {
                    { "87E3D1DC-6503-4E1C-83DA-26D7311EDE80","tczksliw","Karol Śliwka","KarolJacek.Sliwka@flex.com","super" },
                    { "BCBF2A61-8182-42A8-A437-A191FF31270A","tczasob","Adam Sobisz","adam.sobisz@flex.com","admin" },
                    { "00000000-0000-0000-0000-000000000000","tcz_smt_gener","SMT Generic Account","-","user" }
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Name", "Active" },
                values: new object[,]
                {
                    {"AXIS",true},
                    {"DAIKIN",true},
                    {"ERICSSON",true},
                    {"FIREANGEL",true},
                    {"GNERGY",true},
                    {"HUDDLY",true},
                    {"LASER",true},
                    {"MEDIA",true},
                    {"NON-SMT",true},
                    {"PROGRAMMER",true},
                    {"SCHLUMBERGER",true},
                    {"SHEARWATER",true},
                    {"SMT",true},
                    {"WONDERWALL",true}
                });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "ClientId", "Name", "Type", "FileName", "FileLocation", "Active", "Order", "UserId" },
                values: new object[,]
                {
                    {13,"ALPHA",1,"SMT_ALPHA_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,1,1},
                    {13,"BETA",1,"SMT_BETA_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,2,1},
                    {13,"CHARLIE",1,"SMT_CHARLIE_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,3,1},
                    {13,"DELTA",1,"SMT_DELTA_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,4,1},
                    {13,"EPSILON",1,"SMT_EPSILON_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,5,1},
                    {13,"GAMMA",1,"SMT_GAMMA_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,6,1},
                    {13,"HOTEL",1,"SMT_HOTEL_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,7,1},
                    {13,"INDIA",1,"SMT_INDIA_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,8,1},
                    {13,"KILO",1,"SMT_KILO_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,9,1},
                    {7,"LASER 1",1,"SMT_LASER_1_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,1,1},
                    {7,"LASER 2",1,"SMT_LASER_2_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,2,1},
                    {7,"LASER 3",1,"SMT_LASER_3_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,3,1},
                    {13,"LIMA",1,"SMT_LIMA_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,10,1},
                    {13,"MIKE",1,"SMT_MIKE_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,11,1},
                    {13,"NOVEMBER",1,"SMT_NOVEMBER_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,12,1},
                    {13,"PAPA",1,"SMT_PAPA_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,13,1},
                    {10,"PROGRAMMER BEEPROG2C MANUAL",1,"SMT_PROGRAMMER_BEEPROG2C_MANUAL_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,1,1},
                    {10,"PROGRAMMER BP1710 MANUAL",1,"SMT_PROGRAMMER_BP1710_MANUAL_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,2,1},
                    {10,"PROGRAMMER BP2800F MANUAL",1,"SMT_PROGRAMMER_BP2800F_MANUAL_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,3,1},
                    {13,"SIERRA",1,"SMT_SIERRA_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,14,1},
                    {13,"THETA",1,"SMT_THETA_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,15,1},
                    {13,"WHISKEY",1,"SMT_WHISKEY_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,16,1},
                    {13,"YANKEE",1,"SMT_YANKEE_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,17,1},
                    {13,"ZULU",1,"SMT_ZULU_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,18,1},
                    {9,"AOI SCN",2,"PTH_AOI_SCN_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,1,1},
                    {9,"CC MANUAL TOUCHUP",2,"PTH_CC_MANUAL_TOUCHUP_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,2,1},
                    {9,"CONFORMAL COATING",2,"PTH_CONFORMAL_COATING_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,3,1},
                    {9,"ERSA II Sel. Soldering Sn100",2,"PTH_ERSA_II_Sel._Soldering_Sn100_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,4,1},
                    {9,"ERSA III Sel.Soldering",2,"PTH_ERSA_III_Sel.Soldering_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,5,1},
                    {9,"ERSA IV Sel.Soldering",2,"PTH_ERSA_IV_Sel.Soldering_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,6,1},
                    {9,"ERSA V Sel.Soldering",2,"PTH_ERSA_V_Sel.Soldering_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,7,1},
                    {9,"ICT 1 TCZW2723(tczux103)",2,"PTH_ICT_1_TCZW2723(tczux103)_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,8,1},
                    {9,"ICT 2 TCZW2722(tczux106)",2,"PTH_ICT_2_TCZW2722(tczux106)_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,9,1},
                    {9,"ICT 3 TCZW2724(tczux105)",2,"PTH_ICT_3_TCZW2724(tczux105)_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,10,1},
                    {9,"ICT 4 TCZW3958(tczux100)",2,"PTH_ICT_4_TCZW3958(tczux100)_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,11,1},
                    {9,"Maestro",2,"PTH_Maestro_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,12,1},
                    {9,"MANUAL SOLDERING SCN",2,"PTH_MANUAL_SOLDERING_SCN_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,13,1},
                    {9,"PRESSFIT HAND PRESS",2,"PTH_PRESSFIT_HAND_PRESS_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,14,1},
                    {9,"PRESSFIT NEW",2,"PTH_PRESSFIT_NEW_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,15,1},
                    {9,"PRESSFIT OLD",2,"PTH_PRESSFIT_OLD_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,16,1},
                    {9,"ROUTER ELITE 7 ROU007",2,"PTH_ROUTER_ELITE_7_ROU007_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,17,1},
                    {9,"ROUTER GETECH 1 ROU001",2,"PTH_ROUTER_GETECH_1_ROU001_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,18,1},
                    {9,"ROUTER GETECH 2 ROU002",2,"PTH_ROUTER_GETECH_2_ROU002_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,19,1},
                    {9,"ROUTER GETECH 3 ROU003",2,"PTH_ROUTER_GETECH_3_ROU003_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,20,1},
                    {9,"ROUTER GETECH 5 ROU005",2,"PTH_ROUTER_GETECH_5_ROU005_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,21,1},
                    {9,"ROUTER GETECH 6 ROU006",2,"PTH_ROUTER_GETECH_6_ROU006_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,22,1},
                    {9,"TRIDENT 3 PCBA WASH MACHINE",2,"PTH_TRIDENT_3_PCBA_WASH_MACHINE_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,23,1},
                    {9,"UNDERFILL 1",2,"PTH_UNDERFILL_1_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,24,1},
                    {9,"UNDERFILL 2",2,"PTH_UNDERFILL_2_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,25,1},
                    {9,"UNDERFILL 3",2,"PTH_UNDERFILL_3_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,26,1},
                    {9,"WAVE DELTA 5 ASSY1",2,"PTH_WAVE_DELTA_5_ASSY1_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,27,1},
                    {9,"WAVE DELTA 5 ASSY2",2,"PTH_WAVE_DELTA_5_ASSY2_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,28,1},
                    {9,"WAVE DELTA 7 ASSY1",2,"PTH_WAVE_DELTA_7_ASSY1_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,29,1},
                    {9,"WAVE DELTA 7 ASSY2",2,"PTH_WAVE_DELTA_7_ASSY2_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,30,1},
                    {9,"X-RAY 5DX I",2,"PTH_X-RAY_5DX_I_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,31,1},
                    {9,"X-RAY 5DX II",2,"PTH_X-RAY_5DX_II_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,32,1},
                    {9,"X-RAY VITROX II V810S2 XXL",2,"PTH_X-RAY_VITROX_II_V810S2_XXL_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,33,1},
                    {9,"X-RAY VITROX III V810S2 EX",2,"PTH_X-RAY_VITROX_III_V810S2_EX_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,34,1},
                    {9,"X-RAY VITROX IV V810S2 EX",2,"PTH_X-RAY_VITROX_IV_V810S2_EX_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,35,1},
                    {9,"X-RAY VITROX V810S2 EX",2,"PTH_X-RAY_VITROX_V810S2_EX_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,36,1},
                    {1,"AXIS CR IBAS 2 Teebo T01",3,"MANUAL_AXIS_CR_IBAS_2_Teebo_T01_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,1,1},
                    {1,"AXIS CR IBAS 2 Unduli U01",3,"MANUAL_AXIS_CR_IBAS_2_Unduli_U01_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,2,1},
                    {1,"AXIS I Martha",3,"MANUAL_AXIS_I_Martha_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,3,1},
                    {1,"AXIS I Packing",3,"MANUAL_AXIS_I_Packing_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,4,1},
                    {1,"AXIS I Scanfil I",3,"MANUAL_AXIS_I_Scanfil_I_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,5,1},
                    {1,"AXIS I Seafire",3,"MANUAL_AXIS_I_Seafire_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,6,1},
                    {1,"AXIS I Shallot",3,"MANUAL_AXIS_I_Shallot_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,7,1},
                    {1,"AXIS I Trofort",3,"MANUAL_AXIS_I_Trofort_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,8,1},
                    {1,"AXIS I Vidar MegaMind",3,"MANUAL_AXIS_I_Vidar_MegaMind_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,9,1},
                    {1,"AXIS II Boostrix",3,"MANUAL_AXIS_II_Boostrix_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,10,1},
                    {1,"AXIS II Chanel",3,"MANUAL_AXIS_II_Chanel_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,11,1},
                    {1,"AXIS II Chanel Acc",3,"MANUAL_AXIS_II_Chanel_Acc_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,12,1},
                    {1,"AXIS II Packing",3,"MANUAL_AXIS_II_Packing_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,13,1},
                    {1,"AXIS II Shark",3,"MANUAL_AXIS_II_Shark_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,14,1},
                    {1,"AXIS II Volley",3,"MANUAL_AXIS_II_Volley_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,15,1},
                    {5,"CHEETAH PACKAGE",3,"MANUAL_CHEETAH_PACKAGE_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,1,1},
                    {3,"CORE LINE 1",3,"MANUAL_CORE_LINE_1_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,1,1},
                    {3,"CORE LINE 2",3,"MANUAL_CORE_LINE_2_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,2,1},
                    {2,"DAIKIN MB FCT",3,"MANUAL_DAIKIN_MB_FCT_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,1,1},
                    {2,"DAIKIN MMI",3,"MANUAL_DAIKIN_MMI_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,2,1},
                    {2,"DAIKIN PCB PROGRAMMER",3,"MANUAL_DAIKIN_PCB_PROGRAMMER_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,3,1},
                    {2,"DAIKIN PSU",3,"MANUAL_DAIKIN_PSU_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,4,1},
                    {2,"DAIKIN S1",3,"MANUAL_DAIKIN_S1_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,5,1},
                    {2,"DAIKIN S3",3,"MANUAL_DAIKIN_S3_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,6,1},
                    {2,"DAIKIN TPA",3,"MANUAL_DAIKIN_TPA_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,7,1},
                    {3,"E    AIR Line 1",3,"MANUAL_E____AIR_Line_1_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,3,1},
                    {3,"E    AIR Line 2",3,"MANUAL_E____AIR_Line_2_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,4,1},
                    {3,"E    AIR Line 3",3,"MANUAL_E____AIR_Line_3_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,5,1},
                    {3,"E    AIR Line 4",3,"MANUAL_E____AIR_Line_4_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,6,1},
                    {3,"E    AIR Packing",3,"MANUAL_E____AIR_Packing_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,7,1},
                    {3,"E    BURN TEST",3,"MANUAL_E____BURN_TEST_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,8,1},
                    {3,"E    Line - ARUS B4",3,"MANUAL_E____Line_-_ARUS_B4_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,9,1},
                    {3,"E    Line - VISBY",3,"MANUAL_E____Line_-_VISBY_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,10,1},
                    {3,"E    Line 4 - 2217 2219",3,"MANUAL_E____Line_4_-_2217_2219_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,11,1},
                    {3,"E    Line 5 - 2238",3,"MANUAL_E____Line_5_-_2238_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,12,1},
                    {3,"E    Line 6 - 4415",3,"MANUAL_E____Line_6_-_4415_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,13,1},
                    {3,"E    Line 7 - 88XX",3,"MANUAL_E____Line_7_-_88XX_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,14,1},
                    {3,"E    OSLO LINE 1",3,"MANUAL_E____OSLO_LINE_1_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,15,1},
                    {3,"E    OSLO LINE 2",3,"MANUAL_E____OSLO_LINE_2_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,16,1},
                    {3,"E    OSLO LINE 3",3,"MANUAL_E____OSLO_LINE_3_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,17,1},
                    {3,"E    OSLO LINE BM",3,"MANUAL_E____OSLO_LINE_BM_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,18,1},
                    {3,"E    PAM 2212 Test",3,"MANUAL_E____PAM_2212_Test_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,19,1},
                    {3,"E    PAM 4415 Test",3,"MANUAL_E____PAM_4415_Test_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,20,1},
                    {3,"E    PAM 88xx Test",3,"MANUAL_E____PAM_88xx_Test_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,21,1},
                    {3,"E    PAM Packing",3,"MANUAL_E____PAM_Packing_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,22,1},
                    {3,"E    PAM PAX OSLO 2T 4T Test",3,"MANUAL_E____PAM_PAX_OSLO_2T_4T_Test_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,23,1},
                    {3,"E    PAM PAX OSLO ENC Test",3,"MANUAL_E____PAM_PAX_OSLO_ENC_Test_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,24,1},
                    {3,"E    PAX Milano Test",3,"MANUAL_E____PAX_Milano_Test_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,25,1},
                    {3,"E    PAX Stockholm Test",3,"MANUAL_E____PAX_Stockholm_Test_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,26,1},
                    {3,"E    R6K FCT 3001 3004",3,"MANUAL_E____R6K_FCT_3001_3004_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,27,1},
                    {3,"E    R6K FCT 3006 LC",3,"MANUAL_E____R6K_FCT_3006_LC_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,28,1},
                    {3,"E    R6K FCT 3007 6273 RP",3,"MANUAL_E____R6K_FCT_3007_6273_RP_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,29,1},
                    {3,"E    R6K FCT 3007 6274 LC",3,"MANUAL_E____R6K_FCT_3007_6274_LC_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,30,1},
                    {3,"E    R6K FCT 3007 6274 RP",3,"MANUAL_E____R6K_FCT_3007_6274_RP_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,31,1},
                    {3,"E    R6K FCT 3008 BFZ",3,"MANUAL_E____R6K_FCT_3008_BFZ_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,32,1},
                    {3,"E    R6K FCT 3010 3012 6673 RP",3,"MANUAL_E____R6K_FCT_3010_3012_6673_RP_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,33,1},
                    {3,"E    R6K Pack SELL",3,"MANUAL_E____R6K_Pack_SELL_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,34,1},
                    {3,"E    RAN Line 1",3,"MANUAL_E____RAN_Line_1_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,35,1},
                    {3,"E    RAN Line 2 BIS",3,"MANUAL_E____RAN_Line_2_BIS_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,36,1},
                    {3,"E    RAN Line 3",3,"MANUAL_E____RAN_Line_3_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,37,1},
                    {3,"E    RAN Line 4 6621",3,"MANUAL_E____RAN_Line_4_6621_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,38,1},
                    {3,"E    RAN Line 4 6631 R608",3,"MANUAL_E____RAN_Line_4_6631_R608_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,39,1},
                    {3,"E    RAN Line 4 6641 6648",3,"MANUAL_E____RAN_Line_4_6641_6648_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,40,1},
                    {3,"E    ROR PACKING",3,"MANUAL_E____ROR_PACKING_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,41,1},
                    {3,"E    ROR ROUTER",3,"MANUAL_E____ROR_ROUTER_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,42,1},
                    {3,"E    ROR TEST 1",3,"MANUAL_E____ROR_TEST_1_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,43,1},
                    {3,"E    ROR TEST 2",3,"MANUAL_E____ROR_TEST_2_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,44,1},
                    {3,"ERICSSON DOT",3,"MANUAL_ERICSSON_DOT_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,45,1},
                    {3,"ERICSSON DOT BIS",3,"MANUAL_ERICSSON_DOT_BIS_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,46,1},
                    {3,"ERICSSON GPS",3,"MANUAL_ERICSSON_GPS_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,47,1},
                    {3,"ERICSSON IRU",3,"MANUAL_ERICSSON_IRU_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,48,1},
                    {3,"ERICSSON SPITFIRE",3,"MANUAL_ERICSSON_SPITFIRE_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,49,1},
                    {4,"FIREANGEL AKE",3,"MANUAL_FIREANGEL_AKE_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,1,1},
                    {4,"FIREANGEL BB1 ASSY",3,"MANUAL_FIREANGEL_BB1_ASSY_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,2,1},
                    {4,"FIREANGEL BB1 OCA",3,"MANUAL_FIREANGEL_BB1_OCA_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,3,1},
                    {4,"FIREANGEL BB2 ASSY",3,"MANUAL_FIREANGEL_BB2_ASSY_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,4,1},
                    {4,"FIREANGEL BB2 OCA",3,"MANUAL_FIREANGEL_BB2_OCA_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,5,1},
                    {4,"FIREANGEL BB2 PBA 623 625",3,"MANUAL_FIREANGEL_BB2_PBA_623_625_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,6,1},
                    {4,"FIREANGEL BB2 PBA 630",3,"MANUAL_FIREANGEL_BB2_PBA_630_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,7,1},
                    {4,"FIREANGEL BB2 PBA SONA",3,"MANUAL_FIREANGEL_BB2_PBA_SONA_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,8,1},
                    {4,"FIREANGEL BB3 ASSY",3,"MANUAL_FIREANGEL_BB3_ASSY_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,9,1},
                    {4,"FIREANGEL BB4 ASSY",3,"MANUAL_FIREANGEL_BB4_ASSY_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,10,1},
                    {4,"FIREANGEL MOD ASSY",3,"MANUAL_FIREANGEL_MOD_ASSY_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,11,1},
                    {4,"FIREANGEL Pack 1",3,"MANUAL_FIREANGEL_Pack_1_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,12,1},
                    {4,"FIREANGEL Pack 2",3,"MANUAL_FIREANGEL_Pack_2_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,13,1},
                    {4,"FIREANGEL Pack 3",3,"MANUAL_FIREANGEL_Pack_3_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,14,1},
                    {4,"FIREANGEL Pack 4",3,"MANUAL_FIREANGEL_Pack_4_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,15,1},
                    {5,"GNERGY PACKAGE",3,"MANUAL_GNERGY_PACKAGE_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,2,1},
                    {6,"HUDDLY BASE",3,"MANUAL_HUDDLY_BASE_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,1,1},
                    {6,"HUDDLY FALCON",3,"MANUAL_HUDDLY_FALCON_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,2,1},
                    {6,"HUDDLY IQ",3,"MANUAL_HUDDLY_IQ_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,3,1},
                    {6,"HUDDLY L1 KIT",3,"MANUAL_HUDDLY_L1_KIT_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,4,1},
                    {6,"HUDDLY SEE",3,"MANUAL_HUDDLY_SEE_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,5,1},
                    {8,"MEDIA FUNCTIONAL VCM IO",3,"MANUAL_MEDIA_FUNCTIONAL_VCM_IO_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,1,1},
                    {8,"MEDIA JTAG CH2 MX",3,"MANUAL_MEDIA_JTAG_CH2_MX_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,2,1},
                    {8,"MEDIA JTAG OPTION AVP",3,"MANUAL_MEDIA_JTAG_OPTION_AVP_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,3,1},
                    {8,"MEDIA JTAG RX MAIN",3,"MANUAL_MEDIA_JTAG_RX_MAIN_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,4,1},
                    {8,"MEDIA JTAG RX MB 0026",3,"MANUAL_MEDIA_JTAG_RX_MB_0026_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,5,1},
                    {8,"MEDIA JTAG RX OPTION 0030",3,"MANUAL_MEDIA_JTAG_RX_OPTION_0030_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,6,1},
                    {8,"MEDIA MANUAL SOLDERING",3,"MANUAL_MEDIA_MANUAL_SOLDERING_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,7,1},
                    {8,"MEDIA RX MANUAL ASSEMBLY",3,"MANUAL_MEDIA_RX_MANUAL_ASSEMBLY_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,8,1},
                    {8,"MEDIA RX MANUAL SOLDERING",3,"MANUAL_MEDIA_RX_MANUAL_SOLDERING_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,9,1},
                    {8,"MEDIA SOAK",3,"MANUAL_MEDIA_SOAK_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,10,1},
                    {8,"MEDIA TEST",3,"MANUAL_MEDIA_TEST_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,11,1},
                    {8,"MEDIA TEST RX AUDIO 0027",3,"MANUAL_MEDIA_TEST_RX_AUDIO_0027_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,12,1},
                    {8,"MEDIA TEST RX OPTION 0028",3,"MANUAL_MEDIA_TEST_RX_OPTION_0028_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,13,1},
                    {8,"MEDIA TEST RX OPTION 0029",3,"MANUAL_MEDIA_TEST_RX_OPTION_0029_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,14,1},
                    {8,"MEDIA UPGRADE AVP",3,"MANUAL_MEDIA_UPGRADE_AVP_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,15,1},
                    {8,"MEDIA UPGRADE AVP2",3,"MANUAL_MEDIA_UPGRADE_AVP2_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,16,1},
                    {11,"SCHLUMBERGER",3,"MANUAL_SCHLUMBERGER_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,1,1},
                    {12,"SHEARWATER",3,"MANUAL_SHEARWATER_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,1,1},
                    {14,"WONDRWALL MANUAL SOLDERING",3,"MANUAL_WONDRWALL_MANUAL_SOLDERING_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,1,1},
                    {14,"WONDRWALL PACKING",3,"MANUAL_WONDRWALL_PACKING_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,2,1},
                    {14,"WONDRWALL TEST PBA",3,"MANUAL_WONDRWALL_TEST_PBA_","\\\\tcznt114\\CAMPUS_TCZEW\\PCBA\\ProductionPlanning\\PDFLines",true,3,1}
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Maintenance");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
