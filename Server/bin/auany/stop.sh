#!/bin/sh
cd `dirname $0`

if [ ! -f auany.pid ];then
    echo no auany process is running
    ps awx | grep java | grep auany | awk '{print $1}' | xargs kill -9 2>/dev/null
    exit 0 
fi

echo  auany begin to stop after 0s, then wait for 10s.
java -cp libs/jmxc.jar jmxc controlRole JMXPASSWD 127.0.0.1 2709 shutdownAuany					     2>/dev/null
sleep 10


kill -9 `cat auany.pid`	2>/dev/null
rm -f auany.pid		2>/dev/null
ps awx | grep java | grep auany | awk '{print $1}' | xargs kill -9 2>/dev/null
rm -f xdb/xdb.inuse
echo  auany stopped
