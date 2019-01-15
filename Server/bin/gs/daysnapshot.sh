#!/bin/sh
cd `dirname $0`

DAYLOG_DIR=$1	# 这是24小时快照日志的输出目录
SCAN_TYPE=$2	# 这个是24小时的快照及排行榜

if [ "$SCAN_TYPE" == "" ];then
   SCAN_TYPE=0
fi

		#扫描类型
		#全部扫描 0
		#生成快照 1
		#角色级别 2
		#角色财产排名 4
		#运营的log 8
		#财富排行榜 16
		#宠物排行榜 32


WORKSPACE=`pwd`
JARFILE=lib2/scaner.jar
CONFFILE=snapshot.conf
rm trace.log -f
mkdir -p $DAYLOG_DIR

mkdir -p /export/chartdata
rm -rf  /export/chartdata/latest
mkdir -p /export/chartdata/latest

java -jar -Dfile.encoding=utf-8 $JARFILE $WORKSPACE $CONFFILE $DAYLOG_DIR $SCAN_TYPE /export/xbackup/ /export/chartdata/latest/

cp -rf /export/chartdata/latest/* /export/chartdata/
exit 0
