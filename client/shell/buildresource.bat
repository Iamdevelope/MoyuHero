@echo off


cd /d C:\Program Files (x86)\Unity\Editor
  
unity.exe -projectPath G:\DreamHerosIOS\trunk -executeMethod ProjectBuildResource.ExecuteCreateResource project-%1 -quit -batchmode
 

pause