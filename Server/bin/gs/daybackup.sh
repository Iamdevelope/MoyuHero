#!/bin/sh

DATETIME=`date +%Y%m%d`
PATHNAME=${DATETIME}00	# 每日零点备份的目录名为20111212000000这样的名称

mkdir -p /export/everyday
cp -r /export/xbackup/${PATHNAME}* /export/everyday/ 2>/dev/null
