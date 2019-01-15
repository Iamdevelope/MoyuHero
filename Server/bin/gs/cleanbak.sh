#!/bin/sh

interval=2
nowtime=`date +%Y%m%d%H%M%S`
logfile=/root/server/backup/clean.log

function checkindays()
{
	for ((i=0; i<=$interval; i++));
	do
		day_date=`date -d "$i days ago" "+%Y%m%d"`
		res=`echo $1 | grep ^$day_date`
		if [ -n "$res" ];then
			return 0
		fi
	done
	return 1
}
	
echo "$nowtime:	clean xbackup dir start" > $logfile 

for d in `ls /export/xbackup` 
do
	zero=`echo $d | grep 0000[0-9][0-9]$`
	if [ -n "$zero" ];then
		echo "ignore: $d"  >> $logfile
		continue
	fi

	checkindays $d
	if [ $? -eq 0 ];then
		echo "ignore: $d" >> $logfile
		continue
	fi

	# echo "need del: $d"
	echo "	delete $d" >> $logfile 
	rm -rf /export/xbackup/$d
done
