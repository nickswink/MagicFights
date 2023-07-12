# MagicFights

## Description
This project is supposed to be a POC of a malware loader that reads files disguised as other file types and extracts and runs embedded code.

### To DO
* Parse shellcode embedded into MP4 files
* Make file remain valid MP4
* Obfuscate shellcode inside of fake file

Some stealth considerations:
* Embed using stegonography
* Use NtAPIs to open and allocate file
* 
