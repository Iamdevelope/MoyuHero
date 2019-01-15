package chuhan.gsp.gm;

public class Cmd_printreceipt extends GMCommand {

	@Override
	boolean exec(String[] args) {
		if(args.length < 1){
			sendToGM("参数格式错误："+usage());
			return false;
		}
		final long tid = Long.valueOf(args[0]); 
		xbean.AppReceiptData appReceiptData = xtable.Appreceiptes.select(tid);
		if(null == appReceiptData) {
			sendToGM("不存在的发票ID");
			return false;
		}
		sendToGM("roleId = " + appReceiptData.getRoleid());
		sendToGM("receipt = " + appReceiptData.getReceipt());
		System.out.println("roleId = " + appReceiptData.getRoleid());
		System.out.println("receipt = " + appReceiptData.getReceipt());
		return true;
	}

	@Override
	String usage() {
		return "//printreceipt 发票ID ";
	}

}
