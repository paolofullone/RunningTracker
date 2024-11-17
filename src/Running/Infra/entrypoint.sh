#!/bin/bash
echo "Waiting for SQL Server to start"
sleep 30s

echo "Running initialization script"
/opt/mssql-tools/bin/sqlcmd -S sqlserver -U sa -P @Password123 -d master -i /tmp/init.sql
if [ $? -eq 0 ]; then
  echo "Initialization script executed successfully"
else
  echo "Error executing initialization script"
fi