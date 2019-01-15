include(`conf.m4')

#是否是开发环境，true则是，不传日志。false则不是，传日志
IS_DEV=false

#是否抛出异常, true则抛出，false则不抛出
IS_THROW=true

#文件传输方式  AUTO:脚本自动  FTP  HTTP
TRAN_TYPE=AUTO

#游戏名称：YH
KEY_GAME=CH

#服务器标志如：T1
KEY_SERVER=eval(ZONEID)

#本地主日志文件（六种固定日志）目录
URI_MAIN=/log/op/main
#URI_MAIN=c:\\logOp\\main

#本地定制日志文件目录
URI_CONFIG=/log/op/consume
#URI_CONFIG=c:\\logOp\\consume

#远程服务器的地址和端口（ftp或http）
#HOSTNAME=58.215.105.110
#PORT=21
HOSTNAME=58.215.105.110
PORT=21

#ftp两个分别的用户名和密码
MAIN_USERNAME=roe_admin
MAIN_PASSWORD=user_admin@roe
CONFIG_USERNAME=roe_admin
CONFIG_PASSWORD=user_admin@roe

#FTP加密方式，普通还是TLS
IS_SECURITY=

#ftp主日志文件远程目录
REMOTE_URI_MAIN=main

#ftp定制日志文件远程目录
REMOTE_URI_CONFIG=consume

#HTTP服务器ip
#HTTP_HOSTNAME=localhost
HTTP_HOSTNAME=58.215.105.110

#HTTP服务器商品
#HTTP_PORT=8080
HTTP_PORT=8880
