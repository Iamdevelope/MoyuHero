/**
 * Class: AbstractRequestHandler.java
 * Package: knight.gsp.mbean
 *
 *
 *   ver     date      		author
 * ──────────────────────────────────
 *   		 2013-8-10 		yesheng
 *
 * Copyright (c) 2013, Perfect World All Rights Reserved.
*/

package chuhan.gsp.mbean;

import java.util.HashMap;
import java.util.Map;
import java.util.Map.Entry;

import org.apache.log4j.Logger;
import org.w3c.dom.Element;
import org.w3c.dom.Node;


/**
 * ClassName:AbstractRequestHandler
 * Function: ADD FUNCTION HERE
 *
 * @version  
 * @since    
 * @see 	 
 */
public abstract class AbstractRequestHandler implements IRequestHandler {
	public static final Logger logger = Logger.getLogger(AbstractRequestHandler.class);
	String cmdName;
	static Map<Object, Object> successMsgMap = new HashMap<Object, Object>();
	
	static{
		successMsgMap.put("return", "true");
	}
	/**
	 * Creates a new instance of AbstractRequestHandler.
	 *
	 */

	public AbstractRequestHandler(String name) {
		this.cmdName = name;
	}
	/**
	 * (non-Javadoc)
	 * @see knight.gsp.mbean.IRequestHandler#process(org.w3c.dom.Element)
	 */
	@Override
	public String process(Element root) {

		Map<?, ?> paras = parseParameters(root);
		logger.info("来自客服的cmd:" + this.getClass().getSimpleName() + ",paras=" + paras);
		Map<?, ?> resultMap = handleRequest(paras);
		logger.info("客服的cmd:" + this.getClass().getSimpleName() + "f返回,result=" + resultMap);
		return buildXmlString(resultMap);
	}
	/**
	 * buildXmlString:默认构造xml的方法,子类可以修改
	 *
	 * @param resultMap
	 * @return    
	 * String    
	 * @throws 
	 * @since  　
	*/
	
	protected String buildXmlString(Map<?, ?> resultMap) {

		String head = "<cmd_command cmd_data=\""+cmdName+"\" ";
		StringBuilder result = new StringBuilder(head);
		for (Entry<?, ?> entry : resultMap.entrySet()) {
			String key = String.valueOf(entry.getKey());
			String value = String.valueOf(entry.getValue());
			result.append(key).append("=\"").append(value).append("\" ");
		}
		String tail = "/>";
		result.append(tail);
		return result.toString();
	}
	
	/**
	 * handleRequest:(这里用一句话描述这个方法的作用)
	 *
	 * @param paras
	 * @return    
	 * Map<?,?>    
	 * @throws 
	 * @since  　
	*/
	
	protected abstract Map<Object, Object> handleRequest(Map<?, ?> paras);
	/**
	 * parseParameters:(这里用一句话描述这个方法的作用)
	 *
	 * @param root
	 * @return    
	 * Map<?,?>    
	 * @throws 
	 * @since  　
	*/
	
	protected  Map<Object,Object> parseParameters(Element root) {

		Map<Object,Object> result = new HashMap<Object,Object>();
		int length = root.getAttributes().getLength();
		for (int i = 0; i < length; i++) {
			Node node = root.getAttributes().item(i);
			if (node.getNodeType() == Node.ATTRIBUTE_NODE) 
			   result.put((Object)node.getNodeName(), (Object)node.getNodeValue());
		}
		return result;
	}
	
	protected Map<Object, Object> successMsg(){
		
		return successMsgMap;
	}

	protected Map<Object, Object> failedMsg(String desc){
		Map<Object, Object> result = new HashMap<Object, Object>();
		result.put("return", "false");
		result.put("desc", desc);
		return result;
	}
}

