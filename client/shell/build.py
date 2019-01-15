#!/bin/sh

#参数判断  
if [ $# != 0 ];then  
    echo “com.DreamFactionGame.DreamHeros”  
    exit    
fi

echo “package name = $packagename channel = $channel”

#游戏程序路径#
PROJECT_PATH=/Users/dreamfactionmac01/DreamHerosIOS
    
/usr/bin/svn update ${PROJECT_PATH}
#/usr/bin/svn revert -R ${PROJECT_PATH}
#/usr/bin/svn resolve --accept theirs-conflict -R  ${PROJECT_PATH}
#/usr/bin/svn resolved -R ${PROJECT_PATH}

#UNITY程序的路径#
UNITY_PATH=/Applications/Unity1/Unity.app/Contents/MacOS/Unity

#生成的Xcode工程路径#

echo "生成的Xcode工程路径  ${PROJECT_PATH}/$channel" 
XCODE_PATH=${PROJECT_PATH}/$channel

  
echo “将unity导出成xcode工程”  
$UNITY_PATH -projectPath $PROJECT_PATH -executeMethod ProjectBuild.BuildForIPhone project-$channel -quit -batchmode 


echo "XCODE工程生成完毕"

   
#打包脚本路径
BUILD_PATH=${PROJECT_PATH}/Assets/buildios.sh
#开始生成ipa 
$BUILD_PATH $PROJECT_PATH/$channel $packagename


echo "ipa生成完毕"

