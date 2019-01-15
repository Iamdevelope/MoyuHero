::全打表时：-Dfiles="all"
::单打表时，文件名间用逗号分隔，例：-Dfiles="z战斗NPC_剧情_29xxx.xlsx,技能相关表/c持续性buff表.xlsx"

call bin\ant.bat genxml -Dfiles="all"

pause