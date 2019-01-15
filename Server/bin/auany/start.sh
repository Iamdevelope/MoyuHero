#!/bin/sh
cd `dirname $0`

ulimit -c unlimited
WORKDIR=$PWD

mkdir -p $WORKDIR/xdb
mkdir -p $WORKDIR/xbackup
if [ ! -f auany.pid ]; then
	rm -f $WORKDIR/xdb/xdb.inuse
fi

CLASSPATH=auany.jar
for JARFILE in lib/*.jar
do
	CLASSPATH=${CLASSPATH}:$JARFILE
done

echo classpath is $CLASSPATH

$WORKDIR/daemon -c $WORKDIR -p $WORKDIR/auany.pid java -cp ${CLASSPATH} com.auany.AuthorizeServer gsx.xdb.xml
echo  auany started!
