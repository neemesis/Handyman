# System Plugin
Execute system commands.

##### Alias: sys
##### Help: help sys

## Commands
- ```shutdown```: shutdown pc
- ```restart```: restart pc
- ```hibernate```: hibernate pc
- ```logoff```: log off
- ```abort```: abort any of the above commands

## Usage
#### shutdown
```cs
// shutdown imediately
sys shutdown
// delay shutdown
sys shutdown {Minutes}
// example
sys shutdown 30
```
#### restart
```cs
sys shutdown
// delay shutdown
sys shutdown {Minutes}
// example
sys shutdown 30
```
#### hibernate
```cs
sys hibernate
// delay hibernate
sys hibernate {Minutes}
// example
sys hibernate 30
```
#### logoff
```cs
sys logoff
// delay logoff
sys logoff {Minutes}
// example
sys logoff 30
```
#### abort
```cs
sys abort
```