include(`conf.m4')
include(`forloop.m4')
define(`CONNECTOR',`<Connector inputBufferSize="131072" outputBufferSize="10485760" receiveBufferSize="16384" remoteIp="LINK_INTERNAL_IP" remotePort="PORT" sendBufferSize="16384" tcpNoDelay="false" keepOutputBuffer="true" />')
define(`CONNECTOR2',`<Connector inputBufferSize="131072" outputBufferSize="10485760" receiveBufferSize="16384" remoteIp="LINK2_INTERNAL_IP" remotePort="PORT" sendBufferSize="16384" tcpNoDelay="false" keepOutputBuffer="true" />')