# MagicFights

## Description
This a helper program to create Mp4 files with hidden shellcode data inside of them. After embedding the data inside a legitimate Mp4 file you will need a loader to run the shellcode from the file. Here is the POC of a loader for the files this spits out - [MagicFightsLoaderC](https://github.com/nickswink/MagicFightsLoaderC)
Large files may take time for it to parse

## Why it could be cool?
It has been whispered that certain AV/EDRs may completely overlook non-executable file types such as Mp4 files. We may see more threat actors start hiding sections of code inside obscure file types such as these.

   PS> .\MagicFights.exe
   Usage: MagicFights.exe <MP4FilePath> <RawDataFilePath> <OutputFilePath>

Creates new Mp4 file with shellcode hidden inside UserData section of the Mp4 file structure. This shouldn't corrupt the Mp4 file.
![image](https://github.com/nickswink/MagicFights/assets/57839593/92e0552c-58b1-483b-b0e8-570c3e325058)

Testing the embedded shellcode with a POC [loader](https://github.com/nickswink/MagicFightsLoaderC)
![MagicFights](https://github.com/nickswink/MagicFights/assets/57839593/35da7b0e-03fe-4280-942e-734413136411)


### Other cool ideas
* Embed/extract using stegonography
* Use NtAPIs to open and read file
* The loading and execution of the actual shellcode could be improved a lot for evasions sake
  
### Credits
* Ekko sleep function copied from @C5Spider [https://github.com/Cracked5pider/Ekko](https://github.com/Cracked5pider/Ekko)
