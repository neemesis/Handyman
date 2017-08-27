# Series Plugin
Keep track to your progress when watching tv shows.

##### Alias: tv
##### Help: help tv

## Commands
- ```set```: set the tv show to specific season and episode
- ```play```: play specific season and episod from the show
- ```next```: play next
- ```prev```: play previous

## Usage
#### set
First you need to set the current episode and season for tv show
```cs
tv set {TVShowName} S{XX}E{YY}
// example
tv set Friends S01E05
```
#### play
This also sets selected episode in database.
```cs
tv play {TVShowName} S{XX}E{YY}
// example
tv play Friends S01E06
```
#### next
Play next episod trom tv show
```cs
tv next {TVShowName}
// example
tv next Friends
```
#### prev
Play previous episod trom tv show
```cs
tv prev {TVShowName}
// example
tv prev Friends
```

## Info
You can find saved data in **Documents** folder under name **{username}.seriesHandyman**