# SpecializationsCustomizer
This mod is makes customizing IXION building specializations easy. Everything is simply changed via the config file.  
It's built for BepInEx IL2Cpp version 6.0.0-pre.2. Every BepInEx BE built should work but 6.0.0-pre.2 is the most stable and recommended.  
<br>
Thanks to [XSNomad](https://github.com/XSNomad) for a bit of example code to jumpstart this project and thanks to Akhane (@akhane on Discord) for beta testing and making config presets.

# Installation
Simply download [BepInEx](https://github.com/BepInEx/BepInEx/releases/tag/v6.0.0-pre.2) and extract it into your IXION ("\steamapps\common\IXION\") folder.  
Then download an put the [latest release](github.com/captnced2/IXION-SpecializationsCustomizer/releases/latest) into the plugins ("\IXION\BepInEx\plugins\") folder.  
Finally run the game once to generate the config file (located in "\IXION\BepInEx\config\").

# How to use
Simply edit the specialization scores and tags of each building and the required scores for each tier inside the config.  
The score of each building is also shown in the description inside its UI. The DLS additionally shows the specialization score totals of the sector it's built in.  
<br>
Scores and tags work like this:    
Every building that is built (turned on or off) adds onto the total score of the sector it is in.  
The building adds it's score to the total of all specialization tags.  
<br>
For example, the Fusion Station has a score of 72 and the tags Food and Recycling. A sector with only a Fusion Station would look like this:  
- Industry: 0  
- Population: 0  
- Space: 0  
- Food: 72  
- Recycle: 72

The default required scores to reach the specific specialization tiers in a sector are (you can change those values at the bottom of the config):  
- Industry: T1: 300, T2: 800  
- Population: T1: 400, T2: 1000  
- Space: T1: 450, T2: 700  
- Food: T1: 400, T2: 800  
- Recycle: T1: 300, T2: 800
<br/>
(Different names because of code: Mess Hall -> Refectory, Legislative Center -> LawEnforcement)

# Issues and Questions
If you find any bugs please report them in the issues tab and for any questions simply ask in the [DOLOS A.E.C.](https://discord.gg/UMtuJrSmY3) modding channel (with ping pls) or message me directly (@captnced).
