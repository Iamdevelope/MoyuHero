package chuhan.gsp.exchange;

import org.json.JSONObject;

public class PVerifyGoogleReceipt extends xdb.Procedure {
	private static final String GOOGLE_PUBLICKEY = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAjD7TZ7w5Cp9XKyhE9RYygxNV8/fpNAxJh/g/eJFPnnLZIVQ2ny0sM9+knJTMsnEqucOVaD018RTKZ+ZKXHCubBG95/uf/xipExylxyAIt6kWAmHZMDh7SGYEM5b//quHPRlnrZj5C/mBnz7kdLBjISJXFEzG0KS/b8cT/161EAgNmfQrlKrpM4y6y6TxmsdZOvLwoM+v4kbo0aAIURxa9FZ5xqur0ilLlu10y2tCi5Z84JtER0p6A10uBO7cis0ih54CE3iCO1aA0NJiAeFmkgq+GG7/x3l0zcRayUvvxL5IzkKHj00Npsejb1W7e+GMTkAthlIWHSQmn4NSySDl8wIDAQAB";
	private final long roleId;
	private final String billId;
	private final String packageName;//json格式
	private String productId;
	private final String token;
	public PVerifyGoogleReceipt(long roleId, String billId, String packageName, 
			String productId, String token) {
		this.roleId = roleId;
		this.billId = billId;
		this.packageName = packageName;
		this.productId = productId;
		this.token = token;
	}
	
	@Override
	protected boolean process() throws Exception {
		long bid = Long.valueOf(billId);
		JSONObject json = new JSONObject(packageName);
		this.productId = json.getString("productId");
		xdb.Procedure.psend(roleId, new SReplyGooglePlayRceipt(productId));
		if(xtable.Googlereceiptes.get(bid) != null)
			return true;
		xbean.GoogleReceiptData xreceipt = xbean.Pod.newGoogleReceiptData();
		xreceipt.setRoleid(roleId);
		xreceipt.setPackagename(packageName);
		xreceipt.setProductid(productId);
		xreceipt.setToken(token);
		xtable.Googlereceiptes.insert(bid, xreceipt);
		if(!GoogleSecurity.verifyPurchase(GOOGLE_PUBLICKEY, packageName, token)) {
			return false;
		}
		ChargeRole crole = ChargeRole.getChargeRole(xreceipt.getRoleid(), false);
		crole.confirmChargeByAppStore(bid, xreceipt.getProductid(), 0, json.getString("orderId"));
		return true;
	}
	
}
