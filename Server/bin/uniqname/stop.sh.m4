include(`conf.m4')divert(0)changequote(`[[', `]]')#!/bin/sh
cd `dirname $0`

if [ ! -f uniqname.pid ];then
    echo no uniqname process is running
    ps awx | grep java | grep uniqname | awk '{print $1}' | xargs kill -9 2>/dev/null
    exit 0 
fi

echo  Uniqname begin to stop after 0s, then wait for 10s.
java -cp jmxc.jar jmxc controlRole JMXPASSWD 127.0.0.1 eval(JMXPORT+32) shutdownUn					     2>/dev/null
sleep 10


kill -9 `cat uniqname.pid`	2>/dev/null
rm -f uniqname.pid		2>/dev/null
#ps awx | grep java | grep uniqname | awk '{print $1}' | xargs kill -9 2>/dev/null

echo  uniqname stopped
