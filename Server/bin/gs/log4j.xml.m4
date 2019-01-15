<?xml version="1.0" ?>
<!DOCTYPE log4j:configuration SYSTEM "log4j.dtd">
include(`conf.m4')
<log4j:configuration xmlns:log4j='http://jakarta.apache.org/log4j/'>

  <appender name="program" class="org.apache.log4j.RollingFileAppender">
    <param name="file" value="gs.ZONEID.log"/>
    <param name="MaxFileSize" value="100MB"/>
    <param name="MaxBackupIndex" value="100"/>
    <layout class="org.apache.log4j.PatternLayout">
      <param name="ConversionPattern" value="%d{yyyy-MM-dd HH:mm:ss} %p %t %c - %m%n"/>
    </layout>
  </appender>
  <appender name="error" class="org.apache.log4j.RollingFileAppender">
      <param name="file" value="gs.ZONEID.error.log"/>
      <param name="MaxFileSize" value="100MB"/>
      <param name="MaxBackupIndex" value="100"/>
      <layout class="org.apache.log4j.PatternLayout">
        <param name="ConversionPattern" value="%d{yyyy-MM-dd HH:mm:ss} %p %t %c - %m%n"/>
      </layout>
      <filter class="org.apache.log4j.varia.LevelRangeFilter" >
	<param name="LevelMin" value="ERROR"/>
	<param name="LevelMax" value="ERROR"/>
      </filter>
</appender>


        <root>
                <priority value ="debug" />
                <appender-ref ref="program" />
		<appender-ref ref="error" />
        </root>

</log4j:configuration>
