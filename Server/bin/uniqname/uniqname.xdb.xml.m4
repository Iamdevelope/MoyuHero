<?xml version="1.0" encoding="gbk"?>
include(`conf.m4')
<xdb xgenOutput="src" trace="info" traceTo=":file" corePoolSize="5" dbhome="uqxdb" logpages="4096" backupDir="uqxbackup" flushFatalTime="1000" flushPeriod="5000" checkpointPeriod="60000" backupIncPeriod="600000" backupFullPeriod="3600000" angelPeriod="5000">
	<ProcedureConf executionTime="300" retryTimes="3" retryDelay="100"/>
	<xbean name="NameState">
		<enum name="STATE_ALLOCATE" value="0"/>
		<enum name="STATE_CONFIRM"  value="1"/>

		<variable name="state"   type="int"/>    ���ַ���״̬
		<variable name="localId" type="int"/>    ���ַ����Զ�˷�����
		<variable name="peerIp"  type="string" capacity="32"/> ���ַ����Զ�˷�������ַ
		<variable name="time"    type="long"/>   ���ַ����ʱ��
	</xbean>

	<table name="role" key="string" value="NameState" cacheCapacity="10240" cachehigh="128" cachelow="64"/>

	<table name="family" key="string" value="NameState" cacheCapacity="10240" cachehigh="128" cachelow="64"/>

	<table name="faction" key="string" value="NameState" cacheCapacity="10240" cachehigh="128" cachelow="64"/>

	<xbean name="IdState">
		<variable name="nextId"  type="long"/>   ID������0���ѷ��䣻����0����������ָ����һ������id��
		<variable name="localId" type="int"/>    ID�����Զ�˷�����
		<variable name="peerIp"  type="string" capacity="32"/> ID�����Զ�˷�������ַ
		<variable name="time"    type="long"/>   ID�����ʱ��
	</xbean>

	<!--
	ΨһID�����������0��1��������
	min��id��Χ��ʼ������Ϊ 2��
	max��id��Χ������Ĭ��Ϊ Long.MAX_VALUE��
	<table name="familyid" idmin="10000" idmax="1000000"
		key="long" value="IdState" cacheCapacity="10240" cachehigh="128" cachelow="64"/>
	-->

	<UniqNameConf localId="-1">
		<XioConf name="xdb.util.UniqName">
			<Manager name="Server" maxSize="256">
				<Coder>
					<Rpc class="xdb.util.UniqName$Allocate"   onServer="com.uniqname.Allocate"/>
					<Rpc class="xdb.util.UniqName$Confirm"    onServer="com.uniqname.Confirm"/>
					<Rpc class="xdb.util.UniqName$Release"    onServer="com.uniqname.Release"/>
					<Rpc class="xdb.util.UniqName$Exist"      onServer="com.uniqname.Exist"/>
					<Rpc class="xdb.util.UniqName$AllocateId" onServer="com.uniqname.AllocateId"/>
					<Rpc class="xdb.util.UniqName$ReleaseId"  onServer="com.uniqname.ReleaseId"/>
				</Coder>
				<Acceptor localIp="0.0.0.0" localPort="eval(STARTPORT+32)" backlog="32"
					sendBufferSize="131072" receiveBufferSize="131072" tcpNoDelay="false"
					inputBufferSize="131072" outputBufferSize="131072"/>
			</Manager>
		</XioConf>
	</UniqNameConf>
</xdb>
