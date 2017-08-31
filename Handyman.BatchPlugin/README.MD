# Batch Plugin
Execut batch of applications.

##### Alias: batch
##### Help: help batch

## Commands
- ```create```: create new batch.
- ```delete```: delete existing batch.
- ```change```: change existing batch.

## Usage
First you need to create batch
```cs
batch create {NameOfBatch} {PathToApp1} # {PathToApp2} # ... # {PathToAppX}
// example
batch create paint2 pbrush.exe # pbrush.exe // this will launch Paint twice
```
Then you can execute the batch using
```cs
batch {NameOfTheBatch}
// example
batch paint2
```
Delete batch
```cs
batch delete {NameOfBatch}
// example
batch delete paint2
```
