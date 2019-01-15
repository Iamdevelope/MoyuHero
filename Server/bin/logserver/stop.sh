#!/bin/sh

cd `dirname $0`
killall ./logservice
ps awx | grep logservice | awk '{print $1}' | xargs kill -9 2>/dev/null