
package gnet;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __UserInfoRep__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class UserInfoRep extends __UserInfoRep__ {
	@Override
	protected void process() {
		new xdb.Procedure(){

			@Override
			protected boolean process() throws Exception {
				xbean.AUUserInfo info=xbean.Pod.newAUUserInfo();
				info.setRetcode(retcode);
				info.setLoginip(loginip);
				info.setBlisgm(blisgm);
				info.setNicknameOctets(nickname);
				OctetsStream octstream = new OctetsStream(nickname);
				info.setNickname(octstream.unmarshal_String(chuhan.gsp.main.ConfigManager.OCTETS_CHARSET_ANSI));
				info.setUsername(octstream.unmarshal_String(chuhan.gsp.main.ConfigManager.OCTETS_CHARSET_ANSI));
				
				String[] strs = info.getNickname().split("#");
				if(chuhan.gsp.PlatformTypeStr.isQihoo360(strs[0]))
				{
					info.setUsername(strs[1]);//qihoo uin	
				}
				
				
				if(info.getUsername().isEmpty() || info.getUsername().equals(""))
				{
					xdb.Trace.error("账号："+userid+" 名称有误");
					return false;
				}
				
				java.util.Map<Integer,chuhan.gsp.game.sgmconfig> gmcfgs = chuhan.gsp.main.ConfigManager.getInstance().getConf(chuhan.gsp.game.sgmconfig.class);
				for(chuhan.gsp.game.sgmconfig gmcfg : gmcfgs.values())
				{
					if(gmcfg.pt.equals(info.getNickname()) && gmcfg.gm.equals(info.getUsername()))
						info.setBlisgm(1);
				}
				StringBuilder sb = new StringBuilder();
				sb.append("UserInfoRep:").append("userid:").append(userid).append("username:").append(info.getUsername());
				gnet.link.Onlines.logger.info(sb.toString());
			
				//插入会重复
				xtable.Auuserinfo.remove(userid);
				xtable.Auuserinfo.add(userid, info);
				
				xbean.User xuser = xtable.User.get(userid);
				if(xuser == null)
				{
					xuser = xbean.Pod.newUser();
					xtable.User.insert(userid, xuser);
				}
				if(xuser.getUsername().isEmpty())
					xuser.setUsername(info.getUsername());
				gnet.link.User user = gnet.link.Onlines.getInstance().getOnlineUsers().removeAuuserInfoId(userid);
				if(user != null)
					user.sendRoleList();
				
				return true;
			}
			
		}.submit();
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 211;

	public int getType() {
		return 211;
	}

	public int userid;
	public int retcode;
	public int loginip; // 客户端登录ip
	public byte blisgm; // 是否为GM
	public byte gender; // 0-female,1-male,2-unknown
	public com.goldhuman.Common.Octets nickname; // 昵称

	public UserInfoRep() {
		nickname = new com.goldhuman.Common.Octets();
	}

	public UserInfoRep(int _userid_, int _retcode_, int _loginip_, byte _blisgm_, byte _gender_, com.goldhuman.Common.Octets _nickname_) {
		this.userid = _userid_;
		this.retcode = _retcode_;
		this.loginip = _loginip_;
		this.blisgm = _blisgm_;
		this.gender = _gender_;
		this.nickname = _nickname_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(userid);
		_os_.marshal(retcode);
		_os_.marshal(loginip);
		_os_.marshal(blisgm);
		_os_.marshal(gender);
		_os_.marshal(nickname);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		userid = _os_.unmarshal_int();
		retcode = _os_.unmarshal_int();
		loginip = _os_.unmarshal_int();
		blisgm = _os_.unmarshal_byte();
		gender = _os_.unmarshal_byte();
		nickname = _os_.unmarshal_Octets();
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof UserInfoRep) {
			UserInfoRep _o_ = (UserInfoRep)_o1_;
			if (userid != _o_.userid) return false;
			if (retcode != _o_.retcode) return false;
			if (loginip != _o_.loginip) return false;
			if (blisgm != _o_.blisgm) return false;
			if (gender != _o_.gender) return false;
			if (!nickname.equals(_o_.nickname)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += userid;
		_h_ += retcode;
		_h_ += loginip;
		_h_ += (int)blisgm;
		_h_ += (int)gender;
		_h_ += nickname.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(userid).append(",");
		_sb_.append(retcode).append(",");
		_sb_.append(loginip).append(",");
		_sb_.append(blisgm).append(",");
		_sb_.append(gender).append(",");
		_sb_.append("B").append(nickname.size()).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

