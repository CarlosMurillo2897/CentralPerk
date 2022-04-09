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
WRITE-HOST "Build Execution STARTED. `n" -ForegroundColor Yellow
CD $SolutionPath

# Define Environment Variables ONLY for execution.
# WRITE-HOST "Define Environment Variables. `n" -ForegroundColor Green
# $env:PATH += ";$SolutionPath\node_modules\.bin\"
$env:PGPASSWORD='root'

# Start Services.
WRITE-HOST "Running 'POSTGRES SQL' Service. `n" -ForegroundColor Green
SET-SERVICE -NAME $PGService -STATUS "Running" -STARTUPTYPE manual

# Create a Tools Manifest, this will define execution tools just locally.
WRITE-HOST "Creating Tools Manifest locally. `n" -ForegroundColor Green
DOTNET new tool-manifest --force

# Install DotNet Entity Framework will be installed locally.
WRITE-HOST "Installing .NET Entity Framework locally. `n" -ForegroundColor Green
DOTNET tool install dotnet-ef

# PSQL must be a environment variable, UserName 'postgres' and Password 'root' needs to be created by default.
WRITE-HOST "Creating DB. `n" -ForegroundColor Green
PSQL -U $PGUser -f $CreateScript

# Add Migrations.
WRITE-HOST "Add Migrations. `n" -ForegroundColor Green
IF (TEST-PATH $MigrationsPath) {
    Remove-Item -Path $MigrationsPath -Force -Recurse
}

DOTNET dotnet-ef -p $DataProject -s $WebProject Migrations Add FullMigrations

# Update DataBase.
WRITE-HOST "Update DataBase. `n" -ForegroundColor Green
DOTNET dotnet-ef -p $DataProject -s $WebProject Database Update

# Insert SQL Data.
WRITE-HOST "Insert TEST Data. `n" -ForegroundColor Green
PSQL -U $PGUser -d $DBName -f $InsertsScript

# Install NPM Packages.
WRITE-HOST "Installing NPM Packages. `n" -ForegroundColor Green
# NPM I $FrontEndProject

# Close PS Console.
WRITE-HOST "Build Execution COMPLETED. `n" -ForegroundColor Yellow
EXIT