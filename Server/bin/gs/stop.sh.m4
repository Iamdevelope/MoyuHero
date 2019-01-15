include(`conf.m4')divert(0)changequote(`[[', `]]')#!/bin/sh
cd `dirname $0`

WTIME=$1
if [ "$WTIME" = "" ];then
	WTIME=0
fi

# 没有pid文件，一般情况是没有运行
if [ ! -f gsx.pid ];then
      echo no gs process is running
      ps awx | grep java | grep gsxdb | awk '{print $1}' | xargs kill -9 2>/dev/null
      exit 0 
fi

echo Gs begin to stop after $WTIME s.
java -cp ./lib/jmxc.jar jmxc controlRole JMXPASSWD 127.0.0.1 eval(JMXPORT+GSID) shutdownGs $WTIME		  		    2>/dev/null

sleep 10
kill -9 `cat gsx.pid`		2>/dev/null
rm -f gsx.pid			2>/dev/null

echo gs stopped
