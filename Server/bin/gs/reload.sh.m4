include(`conf.m4')divert(0)changequote(`[[', `]]')#!/bin/sh
cd `dirname $0`
java -cp ./lib/jmxc.jar jmxc controlRole JMXPASSWD 127.0.0.1 eval(JMXPORT+GSID) reload
