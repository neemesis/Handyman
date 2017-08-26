# Command Prompt Plugin
Execut **.bat** scripts from the launcher.

##### Alias: cmd
##### Help: help cmd

## Commands
- ```set```: set the directory where the **.bat** scripts are.

## Usage
First you need to set the directory where the **.bat** scripts are:
```cs
cmd set D:\
```
Then you can execute any script from that directory using
```cs
cmd {NameOfTheScript} arg1,arg2,arg3,...,argx
```
