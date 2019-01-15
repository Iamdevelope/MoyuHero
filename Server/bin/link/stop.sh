#!/bin/sh
cd `dirname $0`

NUM=`ls *.pid 2>/dev/null | wc -l`

if [ $NUM -eq 0 ];then
        echo no glinkd is running !
        exit 0 
fi

# 停一个glinkd,打印一个的信息
echo "it will stop $NUM glinkds"
for (( i=1; i<=$NUM; i++ ));
do
        if [ -f glinkd${i}.pid  ]; then
                kill `cat glinkd${i}.pid`
                echo "stop glinkd${i}"
        else
                echo "glinkd${i} is not running";
        fi
done

rm -rf glinkd*.pid                       2>/dev/null
