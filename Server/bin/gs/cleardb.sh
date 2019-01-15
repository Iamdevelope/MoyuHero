#!/bin/sh
cd `dirname $0`

DBLIB_PATH=.
LIBPATH=lib
STORE_DB_DIR=cleardbstore
DESTDB_HOME=xdb

if [ ! -f "$LIBPATH/clear.jar" ];then
	echo "clear.jar is not found!!!"
	exit -1
fi

if [ ! -d $DESTDB_HOME ];then
	echo $DESTDB_HOME is not found.
	exit -1
fi

if [ ! -d $DESTDB_HOME/dbdata ];then
	echo xdb is empty.
	exit -1
fi

rm -rf $STORE_DB_DIR
if [ ! -d $STORE_DB_DIR ];then
	mkdir $STORE_DB_DIR
fi

echo "begin to clear xdb!!!"
java -Xbootclasspath/a:$LIBPATH/xdb.jar:$LIBPATH/jio.jar:$LIBPATH/xstream-1.4.3.jar:gsxdb.jar -jar $LIBPATH/clear.jar -srcdb $DESTDB_HOME -destdb $STORE_DB_DIR -libpath $DBLIB_PATH

#把优化好的数据表拷贝至原数据库目录中
cp -f $STORE_DB_DIR/dbdata/* $DESTDB_HOME/dbdata
rm -rf $STORE_DB_DIR

echo "finish to clear xdb!!!"
