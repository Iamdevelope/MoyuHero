package chuhan.gsp.main;

import gnet.link.Onlines;
import gnet.link.Onlines.Handle;
import gnet.link.RoleLinkBrokenHanle;

import javax.xml.parsers.DocumentBuilderFactory;

import org.apache.log4j.Logger;
import org.w3c.dom.Document;

import xdb.Procedure;


public class OnlinesModule implements ModuleInterface {
	private static Logger logger = Logger.getLogger(OnlinesModule.class);

	@Override
	public void exit() {
		logger.info("正在关闭网络");
		xio.Engine.getInstance().close();
		logger.info("网络已关闭");
	}

	@Override
	public void init() throws Exception {
		try {
			final Document doc = DocumentBuilderFactory.newInstance()
					.newDocumentBuilder().parse("gs.xio.xml");
			xio.Engine.getInstance().register(
					new xio.XioConf(doc.getDocumentElement()));
		} catch (final ClassNotFoundException ex) {
			logger.error("xio文件与jar包不匹配,请重新执行rpcgen.jar", ex);
			throw ex;
		}
		final gnet.link.Onlines onlines = Onlines.getInstance();
		Procedure.setOlines(onlines);
		final Handle brok = new RoleLinkBrokenHanle();
		onlines.setHandle(brok);
	}
	@Override
	public ReloadResult reload() throws Exception
	{
		return new ReloadResult(false,"module" + this.getClass().getName() + "not support reload");
	}

}
