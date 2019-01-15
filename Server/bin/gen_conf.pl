#!/usr/bin/perl

use strict;
use warnings;

my $fh;
open($fh,"<","conf.m4") or die "open conf.m4 fail:$!";

my $link_number=1;
my $mult_line="false";
while (<$fh>)
{
    if( $_ =~ /LINK_NUMBER.*?(\d+)/){
        $link_number=$1;
    }
}
close($fh);
print "link number=$link_number\n";

system("rm -f link/glinkd*.conf");

for(my $i=1;$i!=$link_number+1;++$i){
	system("m4 -I. -DLINK_ID=$i -DPORT_SEQ=$i link/glinkd.conf.m4 > link/glinkd$i.conf");
}

system("m4 -I. jmxremote.password.m4 > uniqname/jmxremote.password");
system("m4 -I. jmxremote.password.m4 > gs/jmxremote.password");
system("m4 -I. uniqname/uniqname.xdb.xml.m4 > uniqname/uniqname.xdb.xml");
system("m4 -I. uniqname/start.sh.m4 > uniqname/start.sh");
system("m4 -I. uniqname/stop.sh.m4 > uniqname/stop.sh");
system("m4 -I. jmxremote.password.m4 > gs/jmxremote.password");
system("m4 -I. gs/gsx.xdb.xml.m4 > gs/gsx.xdb.xml");
system("m4 -I. gs/gs.xio.xml.m4 > gs/gs.xio.xml");
system("m4 -I. gs/log4j.xml.m4 > gs/log4j.xml");
system("m4 -I. gs/start.sh.m4 > gs/start.sh");
system("m4 -I. gs/stop.sh.m4 > gs/stop.sh");
system("m4 -I. gs/gsxdb.properties.m4 > gs/gsxdb.properties");
system("m4 -I. gs/alive.sh.m4 > gs/alive.sh");
system("m4 -I. gs/onlinenum.sh.m4 > gs/onlinenum.sh");
system("m4 -I. gs/reload.sh.m4 > gs/reload.sh");
system("m4 -I. gs/doexp.sh.m4 > gs/doexp.sh");
system("m4 -I. gs/refreshrank.sh.m4 > gs/refreshrank.sh");
system("m4 -I. gs/getqlen.sh.m4 > gs/getqlen.sh");
system("m4 -I. gs/rsynclog.sh.m4 > gs/rsynclog.sh");
system("m4 -I. gs/rsyncadlog.sh.m4 > gs/rsyncadlog.sh");
system("m4 -I. gs/operlog.properties.m4	 > gs/operlog.properties");
system("m4 -I. gs/build.xml.m4 > gs/build.xml");
system("chmod 755 */*.sh");
system("chmod 600 uniqname/jmxremote.password");
system("chmod 600 gs/jmxremote.password");
system("chmod 755 gs/daemon");
system("chmod 755 gs/bin/ant");
system("chmod 755 link/daemon");
system("chmod 755 link/glinkd");
system("chmod 755 uniqname/daemon");


