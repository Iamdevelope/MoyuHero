include(`conf.m4')divert(0)changequote(`[[', `]]')#!/bin/sh
cd `dirname $0`

ulimit -c unlimited
WORKDIR=$PWD

mkdir -p $WORKDIR/uqxdb
mkdir -p $WORKDIR/uqxbackup
if [ ! -f uniqname.pid ]; then
	rm -f $WORKDIR/uqxdb/xdb.inuse
fi

$WORKDIR/daemon -f $WORKDIR/uniqname.log -c $WORKDIR -p $WORKDIR/uniqname.pid java  -cp xdb.jar:uniqname.jar:jio.jar com.uniqname.UniqnameServer uniqname.xdb.xml -rmiport eval(JMXPORT+32)

echo  uniqname started!
