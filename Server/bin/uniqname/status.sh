#!/bin/sh
cd `dirname $0`

if [ -f uniqname.pid  ]; then
	PID=`cat uniqname.pid`
	ps -o pid,%cpu,rss,args --no-heading --pid $PID

	CIDS=`ps --ppid $PID | awk '{print $1}' | grep '[0-9]'`
	for cid in $CIDS
	do
		ps -o pid,%cpu,rss,args --no-heading --pid $cid
	done

	exit 0
fi	

PID=`ps awx | grep java | grep uniqname | awk '{print $1}'`
if [ "$PID" = "" ];then
	echo "uniqname not running"; 
	exit 0
fi

echo "warning: no pid file, but uniqname is running !"
ps -o pid,%cpu,rss,args --no-heading --pid $PID
