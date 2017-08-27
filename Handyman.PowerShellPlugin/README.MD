# PowerShell Plugin
Execut **.ps1** scripts from the launcher.

##### Alias: ps
##### Help: help ps

## Commands
- ```set```: set the directory where the **.ps1** scripts are.

## Usage
First you need to set the directory where the **.ps1** scripts are:
```cs
ps set D:\
```
Then you can execute any script from that directory using
```cs
ps {NameOfTheScript} arg1,arg2,arg3,...,argx
```
