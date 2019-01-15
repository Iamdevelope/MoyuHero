include(`conf.m4')
jvmarg=-Xdebug -Xrunjdwp:transport=dt_socket,server=y,suspend=n,address=eval(STARTPORT+29)  -DJava.net.preferIPv4Stack=true -Dcom.sun.management.jmxremote  -Dcom.sun.management.jmxremote.port=eval(STARTPORT+28)  -Dcom.sun.management.jmxremote.ssl=false   -Dcom.sun.management.jmxremote.password.file=jmxremote.password
args = -rmiport eval(STARTPORT+30) -zoneid ZONEID