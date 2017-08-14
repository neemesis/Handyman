# Slave - automate your PC
Slave is text-based launcher that you can esily activate with shortcut. Slave is easy to use, has a lot of plugins and also provides interface to create your own plugin.

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
- [TranslatorPlugin](https://github.com/neemesis/Slave/tree/master/Slave.TranslatorPlugin)

## How to use Slave
- Initial shortcut for starting Slave is **Alt-Backspace**, but you can change it.
- Type **help** to get info about all slaves.
- Type **add** to add new slave.
- Type **setup** to see options.
- You can modify all config files in base directory for all the plugins, just open **[PluginName].dll.config**
- To see help for specific slave type **[SlaveName] help**
- To exit type **exit**
- To start **Slave** at system boot, either use options or change directly in  **Slave.Core.dll.config**

## How to implement your own plugin
- Add reference to **Slave.Framework.dll**.
- Inherit **IMaster** interface.
- Use **Initialize** method to setup your data.
- Use **Execute** to act on commands.
- See some of the other projects for easy how-to.

Send me your newly created plugin and I will add it to **plugins.json** for easier installation.

## Video
TBA