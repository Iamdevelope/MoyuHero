include(`conf.m4')
<project name="gsxbin">
	<property file="gsxdb.properties"/>
	<target name="run">
		<mkdir dir="xdb" /> 
		<mkdir dir="xbackup" />
		<delete file="xdb/xdb.inuse"/>	
		<java fork="true" classname="chuhan.gsp.main.Gs">
			<classpath>
			<pathelement location="gsxdb.jar" />
				<fileset dir="lib">
					<include name="**/*.jar" />
				</fileset>
				<fileset dir="lib2">
					<include name="**/*.jar" />
				</fileset>
			</classpath>
			<jvmarg line="${jvmarg}"/>
			<arg line="-gszoneid GSID -rmiport JMXPORT"/>
		</java>
	</target>
</project>
