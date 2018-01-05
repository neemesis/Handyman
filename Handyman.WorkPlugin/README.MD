# Work Plugin
Work / rest timer.

##### Alias: work
##### Help: help work

## Commands
- ```work {X} rest {Y}```: work for X minutes then rest for Y minutes.
- ```stop```: stop timer.
- ```status```: check status of the timer.
- ```reset```: reset the timer from start
- ```work```: start timer

## Usage
Default work / rest cycle is 53 / 17 minutes.
To change this write
```cs
work {X} rest {Y}
// example
work 45 rest 15
```
Start timer, type
```cs
work
// if the timer is running the second call will return status
```
Reset timer
```cs
work reset
```
Get status of the timer
```cs
work status
// or if the timer is running just type
work
```
