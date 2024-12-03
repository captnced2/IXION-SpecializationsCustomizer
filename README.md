# SpecializationsCustomizer
This mod is makes customizing IXION building specializations easy. Everything is simply changed via the config file.  
It's built for BepInEx IL2Cpp version 6.0.0-pre.2. Every BepInEx BE built should work but 6.0.0-pre.2 is the most stable and recommended.

# Installation
Simply download [BepInEx](https://github.com/BepInEx/BepInEx/releases/tag/v6.0.0-pre.2) and extract it into your IXION ("\steamapps\common\IXION\") folder.  
Then download an put the [latest release](github.com/captnced2/IXION-SpecializationsCustomizer/releases/latest) into the plugins ("\IXION\BepInEx\plugins\") folder.  
Finally run the game once to generate the config file (located in "\IXION\BepInEx\config\").

# How to use
Just edit the specialization scores and tags from each building inside the config.  
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

The required scores to reach the specific specialization tiers in a sector are:  
- Industry: T1: 300, T2: 800  
- Population: T1: 400, T2: 1000  
- Space: T1: 450, T2: 700  
- Food: T1: 400, T2: 800  
- Recycle: T1: 300, T2: 800
<br/>
(Different names because of code: Mess Hall -> Refectory, Legislative Center -> LawEnforcement)

# Issues and Questions
If you find any bugs please report it in the issues tab and for any questions simply ask in the [DOLOS A.E.C.](https://discord.gg/UMtuJrSmY3) modding channel (with ping pls) or message me directly (@captnced).
