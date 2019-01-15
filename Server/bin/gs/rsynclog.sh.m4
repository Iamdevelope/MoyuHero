include(`conf.m4')divert(0)changequote(`[[', `]]')#!/bin/sh
cd `dirname $0`

USERNAME=game2
IP=219.238.234.141
LOGDIR=/export/logs/
SYNCDIR=SLOG/ZONEID/

rsync -avzP --timeout=1800 --password-file=rsyncpasswd ${LOGDIR} ${USERNAME}@${IP}::${SYNCDIR} > /dev/null
