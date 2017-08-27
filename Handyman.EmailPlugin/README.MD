# Email Plugin
Send emails from the launcher

##### Alias: email
##### Help: help email

## Commands
- ```set```: set variables for the plugin.
	- ```email```: set your email
	- ```pass```: set your password
	- ```port```: set port
	- ```host```: set host

## Usage
First you need to set up the variables
```cs
email set email some.email@some.host.c
email set pass p@sw0rd
email set port 25
email set host smtp.gmail.com
```
Then you can send emails like this
```cs
email [t|to]:[string] [s|subject]:[string] [b|body]:[string]
```
you can use ```[t|to]``` many times if you want to send email to more people.
Example
```cs
email t:other.someone@some.host.c s:Subject of the email b:This is the body of the email
```
