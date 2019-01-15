/**
 * Class: ClientSocketRequest.java
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

import java.io.ByteArrayInputStream;
import java.util.HashMap;
import java.util.Map;
import javax.xml.parsers.DocumentBuilderFactory;

import org.w3c.dom.Document;
import org.w3c.dom.Element;

import chuhan.gsp.mbean.beans.AddBill;
import chuhan.gsp.mbean.beans.MailItemToPlayer;
import chuhan.gsp.mbean.beans.AddYuanBao;
import chuhan.gsp.mbean.beans.GetChar;
import chuhan.gsp.mbean.beans.MoveRole;
import chuhan.gsp.mbean.beans.SetVipLv;
import chuhan.gsp.mbean.beans.AddExp;
import chuhan.gsp.mbean.beans.SetPassGk;
import chuhan.gsp.mbean.beans.ExpandBag;
import chuhan.gsp.mbean.beans.ExpandHeroBag;
import chuhan.gsp.mbean.beans.AddHero;
import chuhan.gsp.mbean.beans.AddRes;
import chuhan.gsp.mbean.beans.AddDrop;
import chuhan.gsp.mbean.beans.SendMail;
import chuhan.gsp.mbean.beans.SetEndless;
import chuhan.gsp.mbean.beans.UserInfo;
import chuhan.gsp.mbean.beans.Marquee;
import chuhan.gsp.mbean.beans.PassNew;
import chuhan.gsp.mbean.beans.AddHeroExp;
import chuhan.gsp.mbean.beans.MReload;

import com.goldhuman.service.mhsdinterfaces.LogInfo;

/**
 * ClassName:ClientSocketRequest Function: ADD FUNCTION HERE
 *
 * @author yesheng
 * @version
 * @since
 * @Date 2013-8-10 下午4:55:01
 *
 * @see
 */
public class ClientSocketRequest {

	static DocumentBuilderFactory dbf = DocumentBuilderFactory.newInstance();
	static Map<String, IRequestHandler> xmlNameToClassMap = new HashMap<String, IRequestHandler>();
	public static final LogInfo loginfo = new LogInfo(0, "客服平台", "客服平台操作指令");

	public static void init() {
		xmlNameToClassMap.put("GetChar", new GetChar("GetChar"));
		xmlNameToClassMap.put("MailItemToPlayer", new MailItemToPlayer("MailItemToPlayer"));
		xmlNameToClassMap.put("AddYuanBao", new AddYuanBao("AddYuanBao"));
		xmlNameToClassMap.put("SetVipLv", new SetVipLv("SetVipLv"));
		xmlNameToClassMap.put("AddBill", new AddBill("AddBill"));
		xmlNameToClassMap.put("MoveRole", new MoveRole("MoveRole"));
		xmlNameToClassMap.put("AddExp", new AddExp("AddExp"));
		xmlNameToClassMap.put("SetPassGk", new SetPassGk("SetPassGk"));
		xmlNameToClassMap.put("ExpandBag", new ExpandBag("ExpandBag"));
		xmlNameToClassMap.put("ExpandHeroBag", new ExpandHeroBag("ExpandHeroBag"));
		xmlNameToClassMap.put("AddHero", new AddHero("AddHero"));
		xmlNameToClassMap.put("AddRes", new AddRes("AddRes"));
		xmlNameToClassMap.put("AddDrop", new AddDrop("AddDrop"));
		xmlNameToClassMap.put("SendMail", new SendMail("SendMail"));
		xmlNameToClassMap.put("SetEndless", new SetEndless("SetEndless"));
		xmlNameToClassMap.put("UserInfo", new UserInfo("UserInfo"));
		xmlNameToClassMap.put("Marquee", new Marquee("Marquee"));
		xmlNameToClassMap.put("PassNew", new PassNew("PassNew"));
		xmlNameToClassMap.put("AddHeroExp", new AddHeroExp("AddHeroExp"));
		xmlNameToClassMap.put("MReload", new MReload("MReload"));
	}

	/**
	 * parseAndHandleXmlRequest:(这里用一句话描述这个方法的作用)
	 *
	 * @return String
	 * @throws @since
	 */

	public static String parseAndHandleXmlRequest(String xmlString) {

		Document document;
		try {
			document = dbf.newDocumentBuilder().parse(new ByteArrayInputStream(xmlString.getBytes("UTF-8")));
			Element root = document.getDocumentElement();
			String name = root.getAttribute("cmd_data");
			IRequestHandler handler = xmlNameToClassMap.get(name);
			if (handler == null)
				return null;
			String result = handler.process(root);
			return result;
		} catch (Exception e) {
			e.printStackTrace();

		}

		return null;
	}

}
