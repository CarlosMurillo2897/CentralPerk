# Declare Variables.
$PGUser = "postgres"
$PGPassword = "root"
$DBName = "CentralPerkDev"

$SolutionPath = $ARGS[0]
$PGService = "postgresql-x64-13"
$WebProject = ".\CentralPerk.Web"
$DataProject = ".\CentralPerk.Data"
$FrontEndProject = ".\CentralPerk.frontend"
$MigrationsPath = ".\CentralPerk.Data\Migrations"

# Access Solution Path.
WRITE-HOST "`nBuild Execution STARTED." -ForegroundColor Yellow
CD $SolutionPath

# Define Environment Variables ONLY for execution.
WRITE-HOST "`nDefine Environment Variables." -ForegroundColor Green
# $env:PATH += ";$SolutionPath\node_modules\.bin\"
$env:PGPASSWORD=$PGPassword

# Start Services.
WRITE-HOST "`nRunning 'POSTGRES SQL' Service." -ForegroundColor Green
SET-SERVICE -NAME $PGService -STATUS "Running" -STARTUPTYPE manual

# Create a Tools Manifest, this will define execution tools just locally.
WRITE-HOST "`nCreating Tools Manifest locally." -ForegroundColor Green
DOTNET new tool-manifest --force

# Install DotNet Entity Framework will be installed locally.
WRITE-HOST "`n`nInstalling .NET Entity Framework locally." -ForegroundColor Green
DOTNET tool install dotnet-ef

# PSQL must be a environment variable, UserName 'postgres' and Password 'root' needs to be created by default.
# WRITE-HOST "`nCreating DB." -ForegroundColor Green
# PSQL -U $PGUser -f $CreateScript

# Add Migrations.
WRITE-HOST "`nAdd Migrations." -ForegroundColor Green
IF (TEST-PATH $MigrationsPath) {
    Remove-Item -Path $MigrationsPath -Recurse
}

DOTNET dotnet-ef -p $DataProject -s $WebProject Migrations Add CompleteMigration

# Update DataBase.
WRITE-HOST "`nUpdate DataBase." -ForegroundColor Green
# DOTNET dotnet-ef -p $DataProject -s $WebProject Database Update

# Insert SQL Data.
# WRITE-HOST "`nInsert TEST Data." -ForegroundColor Green
# PSQL -U $PGUser -d $DBName -f $InsertsScript

# Install NPM Packages.
WRITE-HOST "`nInstalling NPM Packages." -ForegroundColor Green
CD $FrontEndProject
NPM I
CD ..

# Close PS Console.
WRITE-HOST "`nBuild Execution COMPLETED.`n" -ForegroundColor Yellow
EXIT