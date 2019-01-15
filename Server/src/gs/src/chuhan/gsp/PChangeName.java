package chuhan.gsp;

import java.util.HashMap;

import com.pwrd.op.LogOpChannel;

import xdb.util.UniqName;
import chuhan.gsp.attr.PropRole;
import chuhan.gsp.award.DropManager;
import chuhan.gsp.item.BagTypes;
import chuhan.gsp.item.BasicItem;
import chuhan.gsp.item.ItemColumn;
import chuhan.gsp.item.Module;
import chuhan.gsp.item.PUseItem;
import chuhan.gsp.log.LogManager;
import chuhan.gsp.log.OpLogManager;
import chuhan.gsp.log.RemoteLogID;
import chuhan.gsp.log.RemoteLogParam;
import chuhan.gsp.main.ConfigManager;
import chuhan.gsp.main.GameTime;
import chuhan.gsp.msg.Message;
import chuhan.gsp.util.CheckName;
import chuhan.gsp.util.Conv;
import chuhan.gsp.util.DateUtil;

public class PChangeName extends xdb.Procedure{
	public static int ITEM_ID = 3004;
	final private long roleId;
	final private String name;
	final private int itemKey;
	public PChangeName(long roleId, String rolename,int itemKey) {
		this.roleId = roleId;
		this.name = rolename;
		this.itemKey = itemKey;
	}
	
	@Override
	protected boolean process() throws Exception {
		
		PropRole prole = PropRole.getPropRole(roleId, false);
		if( !prole.getProperties().getRolename().equals("游客") ){
			//消耗物品
			ItemColumn itemcol = Module.getItemColumn(roleId, BagTypes.BAG, false);
			BasicItem item = itemcol.getItem(itemKey);
			if(item == null || item.getAttr().getId() != 1402030009){
				sendError(SChangeName.NO_ITEM);
				return false;
			}
			
			if( !new PUseItem(roleId, BagTypes.BAG,itemKey,1,0).call()){
				sendError(SChangeName.NO_ITEM);
				return false;
			}
		}
		if(name.equals("游客")){
			sendError(SChangeName.INVALID);
			return false;
		}
		
		// 检查用用户名长度是否合理
		if (name.getBytes("GBK").length > LangueVersion.getLangueVersion().getMaxNameLen()) {
			sendError(SChangeName.OVERLEN);
			return false;
		} else if (name.length() < LangueVersion.getLangueVersion().getMinNameLen()) {
			sendError(SChangeName.SHORTLEN);
			return false;
		}
		int resultCode = LangueVersion.getLangueVersion().checkName(name);
		if (resultCode == CheckName.WORD_ILLEGALITY) {
			sendError(SChangeName.INVALID);
			return false;
		} else if (resultCode == CheckName.SPECIAL_WORD_TOO_MANY) {
			sendError(SChangeName.INVALID);
			return false;
		} else if (resultCode == CheckName.NONE_CHARACTER) {
			sendError(SChangeName.INVALID);
			return false;
		} else if (resultCode == CheckName.WORD_ERROR_CHAR) {
			sendError(SChangeName.ERRORCHAR);
			return false;
		}else if (resultCode == CheckName.WORD_SPACE) {
			sendError(SChangeName.HAVESPACE);
			return false;
		}
		
//		OldItemColumn itemcol = Module.getItemColumnByItemId(roleId, ITEM_ID,
//		false);
//if (itemcol.removeItemById(ITEM_ID, 1, 1, 1, "change_name") != 1)
//	return false;
		try {
			if(UniqName.allocate("role", name.toLowerCase()))
			{
				
				String oldName = prole.getProperties().getRolename();
				prole.getProperties().setRolename(name);
				final SChangeName res = new SChangeName();
				res.error = SChangeName.OK;
				res.newname = name;
				gnet.link.Onlines.getInstance().send(roleId, res);
				if( !oldName.toLowerCase().equals(name.toLowerCase()) )
				{
					UniqName.release("role", oldName.toLowerCase());
				}
				//改名后，更改在排行榜上的名称信息
				chuhan.gsp.play.ranking.bossRanking.getInstance().changeName(roleId, name);
				chuhan.gsp.play.ranking.endlessRanking.getInstance().changeName(roleId, name);
				return true;
			}
		} catch (Exception e) {		
			sendError(SChangeName.DUPLICATED);
			e.printStackTrace();
		}	
		sendError(SChangeName.DUPLICATED);
		return false;
				
	}
	private boolean  sendError(int err){
		final SChangeName res=new SChangeName();
		res.error=Conv.toByte(err);
		return gnet.link.Onlines.getInstance().send(roleId, res);
	}
	
}
