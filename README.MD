# Slave - automate your PC
Slave is text-based launcher that you can esily activate with shortcut. Slave is easy to use, has a lot of plugins and also provides interface to create your own plugins.

Currently implemented plugins:
- [CommandPromptPlugin](https://github.com/neemesis/Slave/tree/master/Slave.CommandPromptPlugin)
- [EmailPlugin](https://github.com/neemesis/Slave/tree/master/Slave.EmailPlugin)
- [PowerShellPlugin](https://github.com/neemesis/Slave/tree/master/Slave.PowerShellPlugin)
- [RestPlugin](https://github.com/neemesis/Slave/tree/master/Slave.RestPlugin)
- [ScratchPadPlugin](https://github.com/neemesis/Slave/tree/master/Slave.ScratchPadPlugin)
- [ScrenShotPlugin](https://github.com/neemesis/Slave/tree/master/Slave.ScreenShotPlugin)
- [SeriesPlugin](https://github.com/neemesis/Slave/tree/master/Slave.SeriesPlugin)
- [SqlPlugin](https://github.com/neemesis/Slave/tree/master/Slave.SqlPlugin)
- [ToDoPlugin](https://github.com/neemesis/Slave/tree/master/Slave.ToDoPlugin)
- [SysPlugin](https://github.com/neemesis/Slave/tree/master/Slave.SysPlugin)
- [CalculatorPlugin](https://github.com/neemesis/Slave/tree/master/Slave.CalculatorPlugin)
- [GitPlugin](https://github.com/neemesis/Slave/tree/master/Slave.GitPlugin)
- [TranslatorPlugin](https://github.com/neemesis/Slave/tree/master/Slave.TranslatorPlugin)
- [DaysPlugin](https://github.com/neemesis/Slave/tree/master/Slave.DaysPlugin)

## Download Slave Beta
Download the zip from **[HERE](https://github.com/neemesis/Slave/raw/master/Releases/Slave_Release_1.0.0.1.zip)**

## How to use Slave
- Initial shortcut for starting Slave is **Shift - F1**, but you can change it.
- Type **help** to get info about all slaves.
- Type **add** to add new slave.
- Type **setup** to see options.
- You can modify all config files in base directory for all the plugins, just open **[PluginName].dll.config**
- To see help for specific slave type **help [SlaveName]**
- To exit type **exit**
- To start **Slave** at system boot, either use options or change directly in  **Slave.Core.dll.config**

## Create slave command
- Right click on the taskbar icon and click **New slave**
- **Slave name**: is the name used in launcher
- **Filename or url**: you can paste here link or path to some directory
- **Startup mode**: how do you want to start the application
- **Startup path**: in which directory you want to start the command
- **Arguments**: pass arguments to the application
- **Notes**: add comments to you command

## Command arguments
- ```{F}```: replace this with all the arguments from the launcher joined with empty space
- ```{FU}```: replace this with all the arguments from the launcher joined with empty space, URL encoded *(using HttpUtility.UrlEncode)*
- ```{DTN}```: replace this with **DateTime.Now.ToString()**
- ```{U}```: replace this with your username
- ```{UD}```: replace this with your user domain name
- ```{DD}```: replace this with the name of the day **DateTime.Now.DayOfWeek.ToString()**
- ```{MM}```: replace this with number of the month **DateTime.Now.Month.ToString()**
- ```{YY}```: replace this with year number **DateTime.Now.Year.ToString()**
- ```{XU}```: **X** represent number of argument **U** meand URL encoding (ie. ```{0U}, {1U}, {3U}```)
- ```{X}```: **X** represent number of argument (ie. ```{0}, {1}, {3}```)

###### Example 1
We want to launch google search with search term entered in launcher.
I want to write ```google how to write enums in c#``` in the launcher, and I want the launcher to open new tab in my browser with the search term entered.
From the command arguments we can use ```{FU}``` which means combine all arguments and url encode them. The search url for ```google``` is ```https://www.google.com/search?q=[SearchTermHere]```, we can replace everything after ```q=``` with ```{FU}``` so now our url will look like this ```https://www.google.com/search?q={FU}```. Now we can start creating our command. In the **New slave** windows for **Name** we enter ```google``` since we want to use this keyword in the launcher, in **Filename or url** we enter the url from above ```https://www.google.com/search?q={FU}```, we can leave all other fields empty. Now when we enter this in our launcher ```google search for this``` will be translated to ```https://www.google.com/search?q=search%20for%20this```.


## How to implement your own plugin
- Add reference to **Slave.Framework.dll**.
- Inherit **IMaster** interface.
- In constructor set **Alias** so the launcher can send the commands to you plugin.
- If you want you plugin to be startable from shortcut set Shortcut to some value, or set it to **Shortcut.None** (this is importat, because if you dont set your shortcut to something or None, you will get weird errors on runtime)
- Use **Initialize** method to setup your data. You can use **Slave.Framework.Persistence.Persist** to load or save your data.
- Use **Execute** to act on commands.
- See some of the other projects for easy how-to.

###### Example 1 - Creating Days Plugin
- Create **class library** project - targeting **Framework 4.5**
- Add reference to **Slave.Framework.Interfaces.IMaster**, and implement all the methods
- Add constructor method and set **Alias** and **Shortcut**.
- Here you can also define your own parser and use it for parsing the input.
	- Create new class and impelement **Slave.Framework.Interfaces.IParse** and implement the **Parse method**.
	- In the constructor of you plugin set **Parser** to new object from your newly created class
- Create your model class, here we have simple **string Name - DateTime Date** class.
- In **Initialize** method write ```Framework.Persistence.Persist.Load<DaysModel>(Alias);```, this will load all the data or create new empty file in **Documents** directory
- **Execute** method have two arguments **args** and **display**
	- **args** is array of string and this are the arguments that launcher will transfer to you, you can test this using this syntax ```[alias] dev parse [commands list]```, where alias is string keyword used in constructor and commands are space separated strings.
	- **display** is function you can use to show data on the launcher or in separate popup window, it takes two arguments **displayText** which is string and **DisplayData** enum where you choose where do you want to display your data in the laucher window or in separate popup window.
- In **Execute** method you can use ```Framework.Persistence.Persist.Save<T>(List<T> data, string alias);``` to save you list to file

## Persistence
The package **Slave.Framework** has defined static methods for saving and loading lists of simple objects. Saving is done to file in **Documents** directory, using ```System.Xml.Serialization``` the name of the file is ```[alias].slaveconfig```
### Implemented methods
###### Slave.Framework.Persistence.Persist.Save<<T>T>(IList<<T>T> data, string alias)
This method is used for saving data.
###### Slave.Framework.Persistence.Persist.Load<<T>T>(string alias)
This method is used for loading data.

## CMD & PowerShell & Application
#### CMD
You can easily execute **CMD** commands in the app using ```Slave.Framework.Utilities.CMD``` this is function with three string arguments on input. ```CMD(string commandWithArguments, string path, out string errors)```, the first is command and arguments to execute, second is directory path where we want to execute the command and the last one is out variable that will contain the error if there is one.
#### PowerShell
Executing **PowerShell** commands is same as **CMD** commands and the function is exactly the same as ```PowerShell(string commandWithArguments, string path, out string errors)```
#### Application
You can also execute various other applications using this function ```Slave.Framework.Utilities.Application(string application, string commandWithArguments, string path, out string error)``` where **application** is the name of the application you want to execute. **CMD** and **PowerShell** functions are just wrappers around this function that use **application** as **cmd.exe** and **powershell.exe** respectively.


## New Plugins
Send me your created plugins and I will add them to **plugins.json** for easier installation.
###### Syntax
The syntax for new plugins is **JSON** object like this
```json
{
	"Name": "Slave.GitPlugin",
	"Author": "Mirche Toshevski",
	"Description": "Execute git commands.",
	"HasConfig": false,
	"URL": "https://github.com/neemesis/Slave/.../bin/Release/Slave.GitPlugin.dll"
}
```
if your plugin contains **.config** file you need to provide the config file and set **HasConfig** needs to be true. If your plugin contains another **.dll**-s you will need to find a way to provide / specify them.

## Video
TBA