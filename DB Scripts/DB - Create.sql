-- REMOVE Warning Messages. Example: "NOTICE: database|role 'name' does not exist, skipping".
SELECT set_config('client_min_messages', 'warning', false);

-- DROP DB & USER IF ALREADY EXISTS.
DROP DATABASE IF EXISTS centralperkdev;
DROP USER IF EXISTS centralperkuser;

-- CREATE USER, DATABASE & GRANT PRIVILEGES WITH JUST CREATED USER & DATABASE.
CREATE USER centralperkuser with PASSWORD 'centralperk123';
CREATE DATABASE centralperkdev;
GRANT ALL PRIVILEGES ON DATABASE centralperkdev TO centralperkuser;