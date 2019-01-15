package chuhan.gsp.main;

import java.io.File;
import java.io.PrintWriter;
import java.util.Calendar;
import java.util.Collection;

import org.apache.log4j.Logger;

import xdb.TTable;
import xdb.Table;
//import xdb.util.UniqName;
import xdb.util.UniqName;

public class XdbModule implements ModuleInterface {
	private static Logger logger = Logger.getLogger(XdbModule.class);
	public static String EXCHANGE_BILL_UID = "EXCHANGE_BILL_UID";
	private xdb.Xdb myxdb;
	@Override
	public void exit() {
		if(myxdb!=null)
			myxdb.stop();
		logger.info("xdb已关闭");
	}

	@Override
	public void init() throws Exception {
		final xdb.XdbConf conf=new xdb.XdbConf("gsx.xdb.xml");
		myxdb=xdb.Xdb.getInstance();
		myxdb.setConf(conf);
		conf.getDbHome().mkdirs();
		conf.getBackupDir().mkdirs();
		UniqName.initialize();
		File inUse=new File("xdb/xdb.inuse");
		if(inUse.exists())
			inUse.delete();
		myxdb.start();
		new PStartServer().submit();
	}

	@Override
	public ReloadResult reload() throws Exception
	{
		return new ReloadResult(false,"module" + this.getClass().getName() + "not support reload");
	}

	public void printTableCacheInfo() {
		try {
		Calendar cal = Calendar.getInstance();
		StringBuilder sb = new StringBuilder();
		  sb.append("Cache");
		  sb.append(String.valueOf(cal.get(Calendar.MONTH)+1)).append("_");
		  sb.append(String.valueOf(cal.get(Calendar.DAY_OF_MONTH))).append("_");
		  sb.append(String.valueOf(cal.get(Calendar.HOUR_OF_DAY))).append("_");
		  sb.append(String.valueOf(cal.get(Calendar.MINUTE))).append("_");
		  PrintWriter fw = null;
		  try {
//			FileOutputStream fos = new FileOutputStream(new File(sb.toString()));
//			BufferedOutputStream bos = new BufferedOutputStream(fos);
			File cacheFilesFolder = new File("cachefiles");
			if (!cacheFilesFolder.exists()) 
				cacheFilesFolder.mkdir();
			if (cacheFilesFolder.list().length>10){
				for (File subFile : cacheFilesFolder.listFiles()) {
					subFile.delete(); //子文件肯定不会是文件夹
				}
			}
			fw = new PrintWriter(new File(cacheFilesFolder,sb.toString()));
			//表头
			fw.write(addBlank("TableName",32)+"\t");
			fw.write(addBlank("Memory",6)+"\t");
			fw.write("Cache Cap"+"\t");
			fw.write("Cache Size"+"\t");
			fw.write("Count Add"+"\t");
			fw.write("Count Add Miss"+"\t");
			fw.write("Count Add Stor"+"\t");
			fw.write("Count Get"+"\t");
			fw.write("Count Get Miss"+"\t");
			fw.write("Count Get Stor"+"\t");
			fw.write("Count RMV"+"\t");
			fw.write("Count RMV Miss"+"\t");
			fw.write("Count RMV Stor"+"\t");
			fw.write("\n");
			Collection<Table> tables = myxdb.getTables().getTables();
			for (Table table : tables) {
				if (!(table instanceof TTable)) 
					continue;
				@SuppressWarnings("rawtypes")
				TTable tTable = (TTable) table;
				fw.write(addBlank(table.getName(), 32)+"\t");
				fw.write(addBlank(String.valueOf(table.getPersistence()),6)+"\t");
				fw.write(addBlank(String.valueOf(tTable.getCacheCapacity()),9)+"\t");
				fw.write(addBlank(String.valueOf(tTable.getCacheSize()),10)+"\t");
				fw.write(addBlank(String.valueOf(tTable.getCountAdd()),9)+"\t");
				fw.write(addBlank(String.valueOf(tTable.getCountAddMiss()),14)+"\t");
				fw.write(addBlank(String.valueOf(tTable.getCountAddStorageMiss()),14)+"\t");
				fw.write(addBlank(String.valueOf(tTable.getCountGet()),9)+"\t");
				fw.write(addBlank(String.valueOf(tTable.getCountGetMiss()),14)+"\t");
				fw.write(addBlank(String.valueOf(tTable.getCountGetStorageMiss()),14)+"\t");
				fw.write(addBlank(String.valueOf(tTable.getCountRemove()),9)+"\t");
				fw.write(addBlank(String.valueOf(tTable.getCountRemoveMiss()),14)+"\t");
				fw.write(addBlank(String.valueOf(tTable.getCountRemoveStorageMiss()),14)+"\t");
				fw.write("\n");
			}
			fw.flush();
		} catch (Exception e) {
			logger.error("print table cache failed", e);
			
		}finally{
			if(fw != null)
				fw.close();
		}
		} catch (Throwable e) {
			logger.error("print table cache failed", e);
		}
	}

	/**
	 * addBlank:(这里用一句话描述这个方法的作用)
	 *
	 * @param string
	 * @return    
	 * String    
	 * @throws 
	 * @since  　
	*/
	
	private String addBlank(String str,int len) {

		StringBuilder sb = new StringBuilder(str);
		while (sb.length()<len) {
			sb.append(" ");
		}
		return sb.toString();
	}
}
