#!/bin/sh
#author : Cai Jiacheng

function usage()
{
	echo "usage: $0 xbean.jar xml srcdb destdb libpath"
	echo "       xbean.jar :  The Jar of source DB"
	echo "       xml       :  new db definition"
	echo "       srcdb     :  the Source DB Dir"
	echo "       destdb    :  the dest DB Dir( will be clear in the Transform Process )"
	echo "       libpath   :  the lib path of xdb.jar jio.jar .so"
	echo "       Xms       :  the min memory of JVM, default=1024m"
	echo "       Xmx       :  the max memory of JVM, default=2048m"
	exit 1;

}


function clean()
{	
	if [ ! -d $XGEN_OUTPUT ]
	then
		mkdir $XGEN_OUTPUT
	fi
	if [ ! -d $BUILD ]	
	then
		mkdir $BUILD
	fi
	if [ ! -d $DESTDB ]	
	then
		mkdir $DESTDB
	fi
	rm -rf $XGEN_OUTPUT/*
	rm -rf $BUILD/*
}

function verify(){
        if [ $? -ne 0 ]
	then
		echo ""
                echo "Transform fail ....."
	
		exit 1
        fi
}

if [ $# -lt 5 ]
then
  usage
  exit 1
fi

JAR=$1
XML=$2
SRCDB=$3
DESTDB=$4
LIBPATH=$5
XMS=$6
XMX=$7

if [ "$XMS" = "" ]
then
	XMS=1024m
fi

if [ "$XMX" = "" ]
then
	XMX=2048m
fi
	

XGEN_OUTPUT=trnasform.xgen.tmp
BUILD=transform.xgen.classes

clean

echo "begin to Xgean the code of transform......"
#Xgen code of diff Xbean
java -Xbootclasspath/a:$LIBPATH/jio.jar:$JAR:$LIBPATH/xdb.jar -jar $LIBPATH/xdb.jar -output $XGEN_OUTPUT -transform $XML -srcdb $SRCDB -destdb $DESTDB -nowarn
verify

#javac the code of diff Xbean
echo ""
echo "begin to Javac the Transform code ....."
javac -Xbootclasspath/a:$LIBPATH/jio.jar:$LIBPATH/xdb.jar -encoding GBK -sourcepath $XGEN_OUTPUT $XGEN_OUTPUT/Main.java $XGEN_OUTPUT/*/*.java -d $BUILD -g:lines,source -Xlint:unchecked 
verify
#transform the db
echo ""
echo "begin to transform the db from $SRCDB to $DESTDB, -Xmx=$XMX -Xms=$XMS ........"
java -Xbootclasspath/a:$LIBPATH/jio.jar:$LIBPATH/xdb.jar:$BUILD -Xss4m -Xmx$XMX -Xms$XMS Main $LIBPATH
verify

#-----------
#Xgen code of new XML
#echo ""
#echo "begin to xgen the Xbean code of $XML ........"
#clean
#java -Xbootclasspath/a:$LIBPATH/jio.jar:$LIBPATH/xdb.jar -jar xdb.jar -output $XGEN_OUTPUT $XML
#verify
#Javac code
echo ""
#echo "begin to javac the new $JAR ....."
#javac -Xbootclasspath/a:$LIBPATH/jio.jar:$LIBPATH/xdb.jar -encoding GBK -sourcepath $XGEN_OUTPUT $XGEN_OUTPUT/*/*.java -d $BUILD -g:lines,source -Xlint:unchecked 
#verify
#gen new jar
#echo ""
#echo "Gen the new $JAR.new ......"
#if [ -f $JAR.new ]
#then
#	rm -rf $Jar.new
#fi
#jar cf $JAR.new -C $BUILD ./
#verify
#------------

#clear the temp
rm -rf $BUILD
rm -rf $XGEN_OUTPUT

echo ""
echo "Transform OK!!!"

exit 0
