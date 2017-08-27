# Translator Plugin
Open google translate customized.

##### Alias: tr
##### Help: help tr

## Commands
- ```set```: shutdown pc
	- ```from```: set default from language
	- ```to```: set default to language

## Usage
#### set
```cs
// from language
tr set from mk // macedonian
// or
tr set from en // english

// to language
tr set to mk // macedonian
// or
tr set to en // english
```
#### Translate
```cs
tr {L1}:{L2} {text to translate}
// example
tr en:mk Translate this text
// if you omit {L1}:{L2} the default language is used
tr Translate this text // will use en:mk by default or the values you set using the set command
```
