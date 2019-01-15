package chuhan.gsp.exchange;

public class PVerifyAppReceipt extends xdb.Procedure
{
	private final long roleId;
	private final String transactionId;
	private final String receipt;
	public PVerifyAppReceipt(long roleId, String transactionId, String receipt) {
		this.roleId = roleId;
		this.transactionId = transactionId;
		this.receipt = receipt;
	}
	
	@Override
	protected boolean process() throws Exception {
		xdb.Procedure.psend(roleId, new SReplyAppStoreRceipt(transactionId));
		long tid = 0l;
		try
		{
			tid = Long.valueOf(transactionId);
		}
		catch(Exception e)
		{
			ChargeRole.logger.error("TID WRONG. roleid="+roleId+",tid="+transactionId+",receipt="+receipt);
			return false;
		}
		if(tid == 0)
			return false;
		if(xtable.Appreceiptes.get(tid) != null)
			return true;
		xbean.AppReceiptData xreceipt = xbean.Pod.newAppReceiptData();
		xreceipt.setRoleid(roleId);
		xreceipt.setReceipt(receipt);
		xtable.Appreceiptes.insert(tid, xreceipt);
		ChargeRole crole = ChargeRole.getChargeRole(xreceipt.getRoleid(), false);
		return crole.verifyReceipt(tid, receipt);
	}
}
