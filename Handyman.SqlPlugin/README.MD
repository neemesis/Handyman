# SQL Plugin
Execute SQL queries directly from the Launcher.

##### Alias: sql
##### Help: help sql

## Commands
- **help**: shows help window
- **set [name] [ConnectionString]**: add new connection string under specified name
- **remove [name]**: delete connection string
- **change [name] [NewConnectionString]**: update old connection string with new

## Executing queries
To execute query you need to specify connection name, table from where to collect the data from, and optionally which columns to select as well as where parameters.
- **t:** or **table:** for table **REQUIRED**
- **s:** or **select:** to select columns
- **w:** or **where:** for where
- **top:** to select top rows
- **o:** or **order:** for sorting

## Setup
First we need to add connection strings
```cs
// set
sql set {ConnectionName} {ConnectionString}
// remove
sql remove {ConnectionName}
// change
sql change {ConnectionName}
```

## Usage
#### select
```cs
sql [connectionName] s:col1,col2,col3 t:TableName w:col1 < 5,col2 > 15 top:15 o:col1,desc
```
You can just supply the required parameters
```cs
sql [connectionName] t:TableName
```
#### insert
```cs
sql insert t:{TableName} v:{value1},{value2},..,{valueX}
```

## Info
You can find saved data in **Documents** folder under name **{username}.sqlHandyman**