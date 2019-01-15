
package chuhan.gsp.hero;

import com.goldhuman.Common.Marshal.Marshal;
import com.goldhuman.Common.Marshal.OctetsStream;
import com.goldhuman.Common.Marshal.MarshalException;

public class BeautySkinInfo implements Marshal {
	public byte beautyid;
	public int currentskinid;
	public java.util.LinkedList<Integer> haveskins;

	public BeautySkinInfo() {
		haveskins = new java.util.LinkedList<Integer>();
	}

	public BeautySkinInfo(byte _beautyid_, int _currentskinid_, java.util.LinkedList<Integer> _haveskins_) {
		this.beautyid = _beautyid_;
		this.currentskinid = _currentskinid_;
		this.haveskins = _haveskins_;
	}

	public final boolean _validator_() {
		return true;
	}

	public OctetsStream marshal(OctetsStream _os_) {
		_os_.marshal(beautyid);
		_os_.marshal(currentskinid);
		_os_.compact_uint32(haveskins.size());
		for (Integer _v_ : haveskins) {
			_os_.marshal(_v_);
		}
		return _os_;
	}

	public OctetsStream unmarshal(OctetsStream _os_) throws MarshalException {
		beautyid = _os_.unmarshal_byte();
		currentskinid = _os_.unmarshal_int();
		for (int _size_ = _os_.uncompact_uint32(); _size_ > 0; --_size_) {
			int _v_;
			_v_ = _os_.unmarshal_int();
			haveskins.add(_v_);
		}
		return _os_;
	}

	public boolean equals(Object _o1_) {
		if (_o1_ == this) return true;
		if (_o1_ instanceof BeautySkinInfo) {
			BeautySkinInfo _o_ = (BeautySkinInfo)_o1_;
			if (beautyid != _o_.beautyid) return false;
			if (currentskinid != _o_.currentskinid) return false;
			if (!haveskins.equals(_o_.haveskins)) return false;
			return true;
		}
		return false;
	}

	public int hashCode() {
		int _h_ = 0;
		_h_ += (int)beautyid;
		_h_ += currentskinid;
		_h_ += haveskins.hashCode();
		return _h_;
	}

	public String toString() {
		StringBuilder _sb_ = new StringBuilder();
		_sb_.append("(");
		_sb_.append(beautyid).append(",");
		_sb_.append(currentskinid).append(",");
		_sb_.append(haveskins).append(",");
		_sb_.append(")");
		return _sb_.toString();
	}

}

