include(`conf.m4')divert(0)changequote(`[[', `]]')#!/bin/sh
cd `dirname $0`
ulimit -c unlimited
WORKDIR=$PWD
# GSID 请不要改动
LOGFILE=gsx.GSID.log
if [ ! -f $WORKDIR/gsx.pid ]; then
	rm -f $WORKDIR/xdb/xdb.inuse
fi

$WORKDIR/daemon -f $LOGFILE -p gsx.pid  bin/ant run
#crontab cron.rsynclog
echo gs started


