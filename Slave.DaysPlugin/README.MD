# Days Plugin
Count from and to dates.

##### Alias: days
##### Help: help days

## Commands
- ```add```: add new date to the launcher data.
- ```set```: set new date for specific name in launcher data.
- ```delete```: delete date from the launcher data

## Usage
#### add
Add dates in library.
```cs
days add {Name} {Date}
// example
days add NewYear 2018.01.01
```
#### set
Change date
```cs
days set {ExistingName} {NewDate}
// example
days set NewYear 2019.01.01
```
#### delete
```cs
days delete {ExistingName}
// example
days delete NewYear
```
## Calculate
```cs
days {Name}
// example
days NewYear
```

## Info
Repository data is stored in you **Documents** folder under name **days.slaveconfig**