using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace GNET
{
	public partial class UserLogin: Protocol
	{

        public Octets account;
        public Octets response;
        public Octets challenge;
  
        public const int PROTOCOL_TYPE = 101;

        public UserLogin()
            : base(PROTOCOL_TYPE)
		 {
             
		 } 

		public override object Clone()
		{
            UserLogin obj = new UserLogin();
			return obj; 
		}

		public override OctetsStream marshal(OctetsStream os)
		{
            os.marshal(account);
            os.marshal(response);
            os.marshal(challenge);
			return os;
		}

		public override OctetsStream unmarshal(OctetsStream os)
		{
            account = os.unmarshal_Octets();
            response = os.unmarshal_Octets();
            challenge = os.unmarshal_Octets();
			return os;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process(){}
	}	
}
