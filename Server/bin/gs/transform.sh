#!/bin/sh
cd `dirname $0`

OLD_XML=gsx.xdb.xml
NEW_XML=gsx.xdb.xml.new		# 新的XDB结构定义
SRC_XDB=xdb			# 待转换的XDB表所在的文件夹
DEST_XDB=newxdb
OLD_XDB_JAR=gsxdb.jar		
PID_FILE=gsx.pid			
LIB_DIR=lib			# 转换过程的依赖库
#XDB_BAKDIR=$1			# 备份文件夹的名称


if [ ! -f $OLD_XML ];then 
	exit 0 
fi

diff $OLD_XML $NEW_XML -q
if [ $? -eq 0 ]; then
	exit 0;
fi

if [ ! -d $SRC_XDB ]; then 
	exit 0
fi

if [ ! -f $PID_FILE ]; then 
	rm -f xdb/xdb.inuse
fi


rm -rf 	$DEST_XDB
mkdir 	$DEST_XDB

echo "transform"
sh xtransform.sh $OLD_XDB_JAR $NEW_XML $SRC_XDB $DEST_XDB $LIB_DIR
if [ $? -ne 0 ]; then 
	exit 1
fi

#rm -rf 	$XDB_BAKDIR
#mv $SRC_XDB $XDB_BAKDIR
rm -rf $SRC_XDB
mv $DEST_XDB $SRC_XDB

exit 0
