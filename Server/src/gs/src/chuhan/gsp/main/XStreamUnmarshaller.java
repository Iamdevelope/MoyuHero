package chuhan.gsp.main;

import com.thoughtworks.xstream.XStream;

public class XStreamUnmarshaller {
	private final XStream xstream;
	
	XStreamUnmarshaller(){
		xstream=new XStream();
	}
	
	public Object unmarshal(java.io.InputStream input) {		
		return xstream.fromXML(input);
	}

}
