include(`conf.m4')
[GAuanyServer]
type			=	tcp
port			=	30000
address			=	AU_SERVER_IP
so_sndbuf		=	16384
so_rcvbuf		=	16384
tcp_nodelay		=	0
plattype 		= 	PLATFORM
zoneid 			= 	ZONEID
accumulate		=	268435456

[GLinkServer]
type			=	tcp
port			=	eval(STARTPORT-1)
address			=	0.0.0.0
so_sndbuf		=	16384
so_rcvbuf		=	16384
ibuffermax		=	16384
obuffermax		=	65535
tcp_nodelay		=	0
max_users		=	3000
listen_backlog	=	10
accumulate		=	1310720
urgency_support 	=  	1
version			=	804

[GProviderServer]
type			=	tcp
port			=	eval(STARTPORT+1)
address			=	0.0.0.0
so_sndbuf		=	16384
so_rcvbuf		=	16384
ibuffermax      = 	10485760
obuffermax      = 	10485760
;so_broadcast	=	1
tcp_nodelay		=	0
accumulate		=	268435456


[ThreadPool]
threads				=	(1,2)(100,1)(101,1)(0,1)
max_queuesize		=	1048576
prior_strict		=	1

