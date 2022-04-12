WRITE-HOST "`n ***** Build Execution STARTED. *****" -ForegroundColor DarkBlue  -BackgroundColor Yellow

# 01. Declare Variables.
WRITE-HOST "`n01. Defining Variables." -ForegroundColor Green

$PGUser = "postgres"
$PGPassword = "root"
$DBName = "centralperkdev"

$SolutionPath = $ARGS[0]
$PGService = "postgresql-x64-13"
$WebProject = ".\CentralPerk.Web"
$DataProject = ".\CentralPerk.Data"
$FrontEndProject = ".\CentralPerk.frontend"
$MigrationsPath = ".\CentralPerk.Data\Migrations"

$CreateScript = ".\DB Scripts\DB - Create.sql"
$InsertScript = ".\DB Scripts\DB - Insert.sql"

# 02. Access Solution Path.
WRITE-HOST "`n02. Accessing Solution Path." -ForegroundColor Green
CD $SolutionPath

# 03. Define Environment Variables ONLY for execution.
WRITE-HOST "`n03. Defining Environment Variables." -ForegroundColor Green
# $env:PATH += ";$SolutionPath\node_modules\.bin\"
$env:PGPASSWORD=$PGPassword

# 04. Start Services.
WRITE-HOST "`n04. Running 'POSTGRES SQL' Service." -ForegroundColor Green
SET-SERVICE -NAME $PGService -STATUS "Running" -STARTUPTYPE manual

# 05. Create a Tools Manifest, this will define execution tools just locally.
WRITE-HOST "`n05. Creating Tools Manifest locally." -ForegroundColor Green
DOTNET new tool-manifest --force

# 06. Install DotNet Entity Framework will be installed locally.
WRITE-HOST "`n06. Installing .NET Entity Framework locally." -ForegroundColor Green
DOTNET tool install dotnet-ef

# 07. Creating DB with Script. PSQL MUST be running and PGPASSWORD should be a environment variable.
WRITE-HOST "`n07. Creating DB." -ForegroundColor Green
PSQL -U $PGUser -f $CreateScript

# 08. Add Migrations.
WRITE-HOST "`n08. Adding Migrations." -ForegroundColor Green
IF (TEST-PATH $MigrationsPath) {
    Remove-Item -Path $MigrationsPath -Recurse
}

DOTNET dotnet-ef -p $DataProject -s $WebProject Migrations Add CompleteMigration

# 09. Update DataBase.
WRITE-HOST "`n09. Updating DataBase." -ForegroundColor Green
DOTNET dotnet-ef -p $DataProject -s $WebProject Database Update

# 10. Insert SQL Data with Script.PSQL MUST be running and PGPASSWORD should be a environment variable.
WRITE-HOST "`n10. Inserting TEST Data." -ForegroundColor Green
 PSQL -U $PGUser -d $DBName -f $InsertScript

# 11. Install NPM Packages.
WRITE-HOST "`n11. Installing NPM Packages." -ForegroundColor Green
CD $FrontEndProject
NPM i
CD ..

# Close PS Console.
WRITE-HOST "`n***** Build Execution COMPLETED. *****`n" -ForegroundColor DarkBlue  -BackgroundColor Yellow
EXIT