//
// autor : OgreZCD
// date : 2015-07-14
//

using UnityEngine;
using System.Collections;
using System.IO;

public class NewbieguideTemplate : IExcelBean
{
	#region attribute
	//引导语id
	private	int m_id;
	//引导语内容
	private	string m_guide_word;
	//引导员资源
	private	string m_guide;
	//npc_x
	private	int m_npc_x;
	//npc_y
	private	int m_npc_y;
	//对话框_x
	private	int m_talk_box_re_npc_x;
	//对话框_y
	private	int m_talk_box_re_npc_y;
	//对话框_w
	private	int m_talk_box_w;
	//对话框_h
	private	int m_talk_box_h;
	//背景遮罩
	private	int m_background_shade;
	//高亮区域
	private	int m_highlight_area;
	//高亮区域_x
	private	int m_area_x;
	//高亮区域_y
	private	int m_area_y;
	//高亮区域_w
	private	int m_area_w;
	//高亮区域_h
	private	int m_area_h;
	//高亮区可点
	private	int m_area_t;
	//箭头_x
	private	int m_arrow_x;
	//箭头_y
	private	int m_arrow_y;
	//朝向
	private	int m_direction;
	//结束引导
	private	int m_stop_type;
	//引导语ID跳转
	private	int m_skip_to;
	//点击继续
	private	int m_click_continue;
	//立即前往按钮
	private	int m_go_button;
	//立即前往跳转
	private	string m_go_to;
	//所属场景
	private	int m_scene;
	//跳过
	private	int m_skip_button;
	//跳过_x
	private	int m_skip_x;
	//跳过_y
	private	int m_skip_y;
	//跳过结束ID
	private	int m_skip_end;
	//中断起始ID
	private	int m_disconnect_start;
	//给予奖励（服务器）
	private	int[] m_reward;
	#endregion

    public override void parser(BinaryReader data)
    {
        m_id = data.ReadInt32();
        m_guide_word = ReadToString(data);
        m_guide = ReadToString(data);
        m_npc_x = data.ReadInt32();
        m_npc_y = data.ReadInt32();
        m_talk_box_re_npc_x = data.ReadInt32();
        m_talk_box_re_npc_y = data.ReadInt32();
        m_talk_box_w = data.ReadInt32();
        m_talk_box_h = data.ReadInt32();
        m_background_shade = data.ReadInt32();
        m_highlight_area = data.ReadInt32();
        m_area_x = data.ReadInt32();
        m_area_y = data.ReadInt32();
        m_area_w = data.ReadInt32();
        m_area_h = data.ReadInt32();
        m_area_t = data.ReadInt32();
        m_arrow_x = data.ReadInt32();
        m_arrow_y = data.ReadInt32();
        m_direction = data.ReadInt32();
        m_stop_type = data.ReadInt32();
        m_skip_to = data.ReadInt32();
        m_click_continue = data.ReadInt32();
        m_go_button = data.ReadInt32();
        m_go_to = ReadToString(data);
        m_scene = data.ReadInt32();
        m_skip_button = data.ReadInt32();
        m_skip_x = data.ReadInt32();
        m_skip_y = data.ReadInt32();
        m_skip_end = data.ReadInt32();
        m_disconnect_start = data.ReadInt32();
        m_reward = parserXMLIntArray(ReadToString(data));
    }

	public	int	getId()
	{
		return this.m_id;
	}

	public	string	getGuide_word()
	{
		return this.m_guide_word;
	}

	public	string	getGuide()
	{
		return this.m_guide;
	}

	public	int	getNpc_x()
	{
		return this.m_npc_x;
	}

	public	int	getNpc_y()
	{
		return this.m_npc_y;
	}

	public	int	getTalk_box_re_npc_x()
	{
		return this.m_talk_box_re_npc_x;
	}

	public	int	getTalk_box_re_npc_y()
	{
		return this.m_talk_box_re_npc_y;
	}

	public	int	getTalk_box_w()
	{
		return this.m_talk_box_w;
	}

	public	int	getTalk_box_h()
	{
		return this.m_talk_box_h;
	}

	public	int	getBackground_shade()
	{
		return this.m_background_shade;
	}

	public	int	getHighlight_area()
	{
		return this.m_highlight_area;
	}

	public	int	getArea_x()
	{
		return this.m_area_x;
	}

	public	int	getArea_y()
	{
		return this.m_area_y;
	}

	public	int	getArea_w()
	{
		return this.m_area_w;
	}

	public	int	getArea_h()
	{
		return this.m_area_h;
	}

	public	int	getArea_t()
	{
		return this.m_area_t;
	}

	public	int	getArrow_x()
	{
		return this.m_arrow_x;
	}

	public	int	getArrow_y()
	{
		return this.m_arrow_y;
	}

	public	int	getDirection()
	{
		return this.m_direction;
	}

	public	int	getStop_type()
	{
		return this.m_stop_type;
	}

	public	int	getSkip_to()
	{
		return this.m_skip_to;
	}

	public	int	getClick_continue()
	{
		return this.m_click_continue;
	}

	public	int	getGo_button()
	{
		return this.m_go_button;
	}

	public	string	getGo_to()
	{
		return this.m_go_to;
	}

	public	int	getScene()
	{
		return this.m_scene;
	}

	public	int	getSkip_button()
	{
		return this.m_skip_button;
	}

	public	int	getSkip_x()
	{
		return this.m_skip_x;
	}

	public	int	getSkip_y()
	{
		return this.m_skip_y;
	}

	public	int	getSkip_end()
	{
		return this.m_skip_end;
	}

	public	int	getDisconnect_start()
	{
		return this.m_disconnect_start;
	}

	public	int[]	getReward()
	{
		return this.m_reward;
	}


	public override int GetID()
	{
		return this.m_id;
	}
}