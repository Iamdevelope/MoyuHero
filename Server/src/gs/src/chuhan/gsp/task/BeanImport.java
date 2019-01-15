
package chuhan.gsp.task;

// {{{ RPCGEN_IMPORT_BEGIN
// {{{ DO NOT EDIT THIS
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

abstract class __BeanImport__ extends xio.Protocol { }

// DO NOT EDIT THIS }}}
// RPCGEN_IMPORT_END }}}

public class BeanImport extends __BeanImport__ {
	@Override
	protected void process() {
		// protocol handle
	}

	// {{{ RPCGEN_DEFINE_BEGIN
	// {{{ DO NOT EDIT THIS
	public static final int PROTOCOL_TYPE = 788033;

	public int getType() {
		return 788033;
	}

	public chuhan.gsp.task.LiangCaoActivity liangcaoactivity;
	public chuhan.gsp.task.QiandaoActivity qiandaoactivity;
	public chuhan.gsp.task.ShengjiActivity shengjiactivity;
	public chuhan.gsp.task.ChargeActivityView chargeactivityview;

	public BeanImport() {
		liangcaoactivity = new chuhan.gsp.task.LiangCaoActivity();
		qiandaoactivity = new chuhan.gsp.task.QiandaoActivity();
		shengjiactivity = new chuhan.gsp.task.ShengjiActivity();
		chargeactivityview = new chuhan.gsp.task.ChargeActivityView();
	}

	public BeanImport(chuhan.gsp.task.LiangCaoActivity _liangcaoactivity_, chuhan.gsp.task.QiandaoActivity _qiandaoactivity_, chuhan.gsp.task.ShengjiActivity _shengjiactivity_, chuhan.gsp.task.ChargeActivityView _chargeactivityview_) {
		this.liangcaoactivity = _liangcaoactivity_;
		this.qiandaoactivity = _qiandaoactivity_;
		this.shengjiactivity = _shengjiactivity_;
		this.chargeactivityview = _chargeactivityview_;
	}

	public final boolean _validator_() {
		if (!liangcaoactivity._validator_()) return false;
		if (!qiandaoactivity._validator_()) return false;
		if (!shengjiactivity._validator_()) return false;
		if (!chargeactivityview._validator_()) return false;
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		_os_.marshal(liangcaoactivity);
		_os_.marshal(qiandaoactivity);
		_os_.marshal(shengjiactivity);
		_os_.marshal(chargeactivityview);
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		liangcaoactivity.unmarshal(_os_);
		qiandaoactivity.unmarshal(_os_);
		shengjiactivity.unmarshal(_os_);
		chargeactivityview.unmarshal(_os_);
		if (!_validator_()) {
			throw new VerifyError("validator failed");
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof BeanImport) {
			BeanImport _o_ = (BeanImport)_o1_;
			if (!liangcaoactivity.equals(_o_.liangcaoactivity)) return false;
			if (!qiandaoactivity.equals(_o_.qiandaoactivity)) return false;
			if (!shengjiactivity.equals(_o_.shengjiactivity)) return false;
			if (!chargeactivityview.equals(_o_.chargeactivityview)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += liangcaoactivity.hashCode();
		_h_ += qiandaoactivity.hashCode();
		_h_ += shengjiactivity.hashCode();
		_h_ += chargeactivityview.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(liangcaoactivity).append(",");
		_sb_.append(qiandaoactivity).append(",");
		_sb_.append(shengjiactivity).append(",");
		_sb_.append(chargeactivityview).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

	// DO NOT EDIT THIS }}}
	// RPCGEN_DEFINE_END }}}

}

