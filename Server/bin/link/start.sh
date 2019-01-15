#!/bin/sh
cd `dirname $0`

ulimit -c unlimited
ulimit -n 32768 
WORKDIR=$PWD

NUM=( `ls glinkd*.conf | wc -l`)
for((i=1; i<=$NUM; i++));
do
#sleep 1
$WORKDIR/daemon -f $WORKDIR/glinkd$i.log -p $WORKDIR/glinkd$i.pid -c $WORKDIR $WORKDIR/glinkd $WORKDIR/glinkd$i.conf &

	echo glinkd$i started
done
