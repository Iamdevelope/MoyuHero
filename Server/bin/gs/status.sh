#!/bin/sh
cd `dirname $0`

# 正常情形下
if [ -f gsx.pid  ]; then
	# 父进程
	PID=`cat gsx.pid`
	ps -o pid,%cpu,rss,args --no-heading --pid $PID

	# 子进程
	CIDS=`ps --ppid $PID | awk '{print $1}' | grep '[0-9]'`
	for cid in $CIDS
	do
		ps -o pid,%cpu,rss,args --no-heading --pid $cid
	done

	exit 0
fi	

# pid文件没有，那么试图找出类似的子进程
PID=`ps awx | grep java | grep gsxdb | awk '{print $1}'`
if [ "$PID" = "" ];then
	echo "gs not running"; 
	exit 0
fi

echo "warning: no pid file, but gs is running !"
ps -o pid,%cpu,rss,args --no-heading --pid $PID
