//
// autor : OgreZCD
// date : 2015-07-14
//

using UnityEngine;
using System.Collections;
using System.IO;


public class ChapterinfoTemplate : IExcelBean
{
	#region attribute
	//章节ID
	private	int m_id;
	//章节名称
	private	string m_chapterName;
	//章节内全部关卡通过奖励掉落包
	private	int[] m_chapterDrop;
	//包含的关卡ID
	private	int[] m_stageID;
	//章节背景图片
	private	string m_backgroundPicture;
	//神秘商店出现位置
	private	float[] m_shopposition;
	//奖励所需星星
	private	int[] m_starnum;
	//关卡难度
	private	int[] m_difficult;
	#endregion

	public override void parser(BinaryReader data)
	{
		m_id = data.ReadInt32();
		m_chapterName = ReadToString(data);
		m_chapterDrop = parserXMLIntArray(ReadToString(data));
		m_stageID = parserXMLIntArray(ReadToString(data));
		m_backgroundPicture = ReadToString(data);
		m_shopposition = parserXMLFloatArray(ReadToString(data));
		m_starnum = parserXMLIntArray(ReadToString(data));
		m_difficult = parserXMLIntArray(ReadToString(data));
	}

	public	int	getId()
	{
		return this.m_id;
	}

	public	string	getChapterName()
	{
		return this.m_chapterName;
	}

	public	int[]	getChapterDrop()
	{
		return this.m_chapterDrop;
	}

	public	int[]	getStageID()
	{
		return this.m_stageID;
	}

	public	string	getBackgroundPicture()
	{
		return this.m_backgroundPicture;
	}

	public	float[]	getShopposition()
	{
		return this.m_shopposition;
	}

	public	int[]	getStarnum()
	{
		return this.m_starnum;
	}

	public	int[]	getDifficult()
	{
		return this.m_difficult;
	}


	public override int GetID()
	{
		return this.m_id;
	}
}