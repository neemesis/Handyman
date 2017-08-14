# SQL Plugin
Execute SQL queries directly from the Launcher.

## Commands
- **help**: shows help window
- **set [name] [ConnectionString]**: add new connection string under specified name
- **delete [name]**: delete connection string
- **update [name] [NewConnectionString]**: update old connection string with new

## Executing queries
To execute query you need to specify connection name, table from where to collect the data from, and optionally which columns to select as well as where parameters.
- **t:** or **table:** for table
- **s:** or **select:** to select columns
- **w:** or **where:** for where
- **top:** to select top rows
- **o:** or **order:** for sorting

## Examples
```text
sql [connectionName] s:col1,col2,col3 t:TableName w:col1<5,col2>15 top:15 o:col1,desc
```

You can just supply the required parameters
```text
sql [connectionName] t:TableName
```