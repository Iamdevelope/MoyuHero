include(`conf.m4')divert(0)changequote(`[[', `]]')#!/bin/sh
cd `dirname $0`

USERNAME=shouyou
IP=123.196.115.20
LOGDIR=/export/logs/
SYNCDIR=shouyou/ZONEID/

rsync -avzP --timeout=1800 --password-file=rsyncadpasswd ${LOGDIR} ${USERNAME}@${IP}::${SYNCDIR} > /dev/null
