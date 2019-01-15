//
// autor : OgreZCD
// date : 2015-11-03
//

using UnityEngine;
using System.Collections;
using System.IO;

using System;

public class GameConfig : IExcelBean
{
	#region attribute
	private int m_id;
	//版本号(大版本号（强更），小版本号（建议更），修正版本号（服务器热更））
	private	string m_version_number;
	//非战斗的移动速度
	private	int m_run_speed;
	//每一格怒气的单位
	private	int m_each_rage_point;
	//最大怒气量
	private	int m_max_rage_point;
	//普通攻击怒气增加值
	private	int m_att_rp_inc;
	//被普通攻击怒气增加值
	private	int m_be_att_rp_inc;
	//普通攻击暴击时的增加系数
	private	float m_crit_att_para;
	//被普通攻击暴击时的增加系数
	private	float m_be_crit_att_para;
	//闪避普通攻击后攻击方的怒气增加值
	private	int m_be_dodged_att_rp_inc;
	//闪避普通攻击后被攻击方的怒气增加值
	private	int m_dodge_att_rp_inc;
	//非普通(技能)攻击怒气增加值
	private	int m_skillatt_rp_inc;
	//被非普通(技能)攻击怒气增加值
	private	int m_be_skillatt_rp_inc;
	//非普通(技能)攻击暴击时的增加系数
	private	float m_crit_skillatt_para;
	//被非普通(技能)攻击暴击时的增加系数
	private	float m_be_crit_skillatt_para;
	//闪避非普通(技能)攻击后攻击方的怒气增加值
	private	int m_be_dodged_skillatt_rp_inc;
	//闪避非普通(技能)攻击后被攻击方的怒气增加值
	private	int m_dodge_skillatt_rp_inc;
	//杀死对方1个人时的增加值
	private	int m_kill_rp_inc;
	//被杀死我方1个人时的增加值
	private	int m_be_kill_rp_inc;
	//每波怪物怒气奖励
	private	int m_wave_fury;
	//每损失1%生命值增加怒气
	private	float m_lose_every_1_hp;
	//自身公共CD（毫秒）
	private	int m_personPublicCD;
	//全队公共CD（毫秒）
	private	int m_teamPublicCD;
	//一行显示buff/debuff数量
	private	int m_statusNumberRow;
	//总共显示几行buff/debuff
	private	int m_statusRow;
	//阵营A对A攻击系数百分比
	private	int m_attackCoefficient_AtoA;
	//阵营A对B攻击系数百分比
	private	int m_attackCoefficient_AtoB;
	//阵营A对C攻击系数百分比
	private	int m_attackCoefficient_AtoC;
	//阵营B对A攻击系数百分比
	private	int m_attackCoefficient_BtoA;
	//阵营B对B攻击系数百分比
	private	int m_attackCoefficient_BtoB;
	//阵营B对C攻击系数百分比
	private	int m_attackCoefficient_BtoC;
	//阵营C对A攻击系数百分比
	private	int m_attackCoefficient_CtoA;
	//阵营C对B攻击系数百分比
	private	int m_attackCoefficient_CtoB;
	//阵营C对C攻击系数百分比
	private	int m_attackCoefficient_CtoC;
	//闪避率参数A
	private	float m_DodgeA;
	//闪避率参数B
	private	float m_DodgeB;
	//闪避率参数C
	private	float m_DodgeC;
	//暴击率参数D
	private	float m_CriticalD;
	//暴击率参数E
	private	float m_CriticalE;
	//暴击率参数F
	private	float m_CriticalF;
	//物理伤害参数G
	private	float m_PhysicalAttackG;
	//物理伤害参数H
	private	float m_PhysicalAttackH;
	//物理伤害参数I
	private	float m_PhysicalAttackI;
	//法术伤害参数J
	private	float m_MagicAttackJ;
	//法术伤害参数K
	private	float m_MagicAttackK;
	//法术伤害参数L
	private	float m_MagicAttackL;
	//回复参数M
	private	float m_MagicHealingM;
	//回复参数N
	private	float m_MagicHealingN;
	//回复参数O
	private	float m_MagicHealingO;
	//物理回复参数P
	private	float m_PhysicalHealingP;
	//物理回复参数Q
	private	float m_PhysicalHealingQ;
	//物理回复参数R
	private	float m_PhysicalHealingR;
	//攻击频率参数S
	private	float m_attackFrequencyS;
	//攻击频率参数T-频率下限
	private	float m_attackFrequencyT;
	//攻击频率参数U-频率上限
	private	float m_attackFrequencyU;
	//伤害浮动系数下限V
	private	float m_damageOffloatingMin;
	//伤害浮动系数上限W
	private	float m_damageOffloatingMax;
	//生命恢复参数X
	private	float m_lifeRestoringX;
	//生命恢复参数Y
	private	float m_lifeRestoringY;
	//生命恢复参数Z
	private	float m_lifeRestoringZ;
	//治疗浮动系数下限AA
	private	float m_healOffloatingMin;
	//治疗浮动系数上限AB
	private	float m_healOffloatingMax;
	//命中计算系数X
	private	float m_Dodge_X;
	//命中计算系数a
	private	float m_Dodge_a;
	//命中计算系数b
	private	float m_Dodge_b;
	//命中计算系数c
	private	float m_Dodge_c;
	//命中计算系数k
	private	float m_Dodge_k;
	//命中计算系数p
	private	float m_Dodge_p;
	//暴击计算系数X
	private	float m_Critical_X;
	//暴击计算系数a
	private	float m_Critical_a;
	//暴击计算系数b
	private	float m_Critical_b;
	//暴击计算系数c
	private	float m_Critical_c;
	//暴击计算系数k
	private	float m_Critical_k;
	//暴击计算系数p
	private	float m_Critical_p;
	//暴击基础伤害倍率
	private	float m_Critical_base_power;
	//命中率下限
	private	float m_Dodge_min;
	//暴击率上限
	private	float m_Critical_max;
	//对应monster表deathSkillType中类型为1、2、3、4时对应的效果SkillID
	private	int[] m_deathSkillType;
	//对应monster表deathSkillType中类型为1、2、3、4时对应的shader资源名称
	private	string[] m_deathSKillTypeShader;
	//玩家等级上限
	private	int m_player_max_level;
	//初始活力值上限
	private	int m_initial_ap_upper_limit;
	//初始活力值
	private	int m_initial_ap;
	//活力恢复每1点所需秒数
	private	int m_per_ap_recovery_sec;
	//初始游戏币数量
	private	int m_initial_money;
	//初始充值币数量
	private	int m_initial_diamond;
	//初始英雄上限
	private	int m_initial_hero_packset;
	//初始背包上限
	private	int m_initial_common_item_packset;
	//每次购买英雄上限的扩充数量
	private	int m_hero_packset_per_expand;
	//每次购买背包上限的扩充数量
	private	int m_common_item_packset_per_expand;
	//最大购买英雄上限次数
	private	int m_hero_packset_max_purchase;
	//最大购买背包上限次数
	private	int m_common_item_packset_max_purchase;
	//初始探险行动力上限
	private	int m_initial_ep_upper_limit;
	//探险行动力恢复每1点所需秒数
	private	int m_per_ep_recovery_sec;
	//初始探险行动力
	private	int m_initial_ep;
	//神秘商店出现概率
	private	float m_mysteryprob;
	//特殊关卡出现概率
	private	float m_specificmissionsprob;
	//普通符文2星4条鉴定石消耗
	private	int[] m_normal_rune_2star_expose_cost;
	//普通符文3星4条鉴定石消耗
	private	int[] m_normal_rune_3star_expose_cost;
	//普通符文4星4条鉴定石消耗
	private	int[] m_normal_rune_4star_expose_cost;
	//普通符文5星4条鉴定石消耗
	private	int[] m_normal_rune_5star_expose_cost;
	//特殊符文3星4条鉴定石消耗
	private	int[] m_special_rune_3star_expose_cost;
	//特殊符文4星4条鉴定石消耗
	private	int[] m_special_rune_4star_expose_cost;
	//特殊符文5星4条鉴定石消耗
	private	int[] m_special_rune_5star_expose_cost;
	//购买英雄上限的钻石消耗
	private	int[] m_hero_packset_expand_cost;
	//购买背包上限的钻石消耗
	private	int[] m_common_item_packset_expand_cost;
	//玩家AI检查间隔（毫秒）
	private	int m_player_ai_check;
	//增加vip经验所需充值数量，单位是分
	private	int m_realmoney_to_vipexp;
	//增加vip经验所需花费魔钻数量
	private	int m_gold_to_vipexp;
	//神秘商店出现概率增量
	private	float m_mysteryprob_plus;
	//特殊关卡出现概率增量
	private	float m_specificmissionsprob_plus;
	//神秘商店出现时长（秒）
	private	int m_mystery_duration;
	//特殊关卡出现出现时长（秒）
	private	int m_specificmissions_duration;
	//最大探险队列数
	private	int m_explore_queue;
	//一次刷新出现的任务个数
	private	int m_explore_quest_num;
	//刷新消耗魔钻数量
	private	int m_explore_refresh_cost;
	//探索加速公式的函数参数a*x^2+b*x+c，其中x是分钟数，消耗魔钻向下取整
	private	float m_explore_param_a;
	//探索加速公式的函数参数a*x^2+b*x+c，其中x是分钟数
	private	float m_explore_param_b;
	//探索加速公式的函数参数a*x^2+b*x+c，其中x是分钟数
	private	float m_explore_param_c;
	//遗迹宝藏第1层随机事件几率*100
	private	int m_treasure1_event_prob;
	//遗迹宝藏第2层随机事件几率*100
	private	int m_treasure2_event_prob;
	//遗迹宝藏第3层随机事件几率*100
	private	int m_treasure3_event_prob;
	//遗迹宝藏第4层随机事件几率*100
	private	int m_treasure4_event_prob;
	//遗迹宝藏事件随机魔钻最小值
	private	int m_treasure_event10_min;
	//遗迹宝藏事件随机魔钻最大值
	private	int m_treasure_event10_max;
	//遗迹宝藏单抽消耗魔钻
	private	int m_treasure_single_cost;
	//遗迹宝藏单抽奖励金币
	private	int m_treasure_single_reward;
	//遗迹宝藏十连抽消耗魔钻
	private	int m_treasure_ten_cost;
	//遗迹宝藏十连抽抽奖励金币
	private	int m_treasure_ten_reward;
	//遗迹宝藏刷新消耗魔钻
	private	int m_treasure_refresh_cost;
	//遗迹宝藏刷新奖励金币
	private	int m_treasure_refresh_reward;
	//遗迹宝藏事件描述
	private	string m_treasure_event1_des;
	//遗迹宝藏事件描述
	private	string m_treasure_event2_des;
	//遗迹宝藏事件描述
	private	string m_treasure_event7_des;
	//遗迹宝藏事件描述
	private	string m_treasure_event8_des;
	//遗迹宝藏事件描述
	private	string m_treasure_event9_des;
	//遗迹宝藏免费购买倒计时时间（分钟）
	private	int m_treasure_single_everyday;
	//1星通关经验倍率
	private	float m_stage_1star_reward_pow;
	//2星通关经验倍率
	private	float m_stage_2star_reward_pow;
	//3星通关经验倍率
	private	float m_stage_3star_reward_pow;
	//1经验结晶提供英雄经验数
	private	int m_jingyanjiejing_to_jingyan;
	//熔灵经验折算系数
	private	float m_rongling_conversion_rate;
	//进阶3星英雄初始等级
	private	int m_hero_advanced_level_3;
	//进阶4星英雄初始等级
	private	int m_hero_advanced_level_4;
	//进阶5星英雄初始等级
	private	int m_hero_advanced_level_5;
	//活力补充道具
	private	int[] m_ap_supplement_item;
	//活力补满商品id
	private	int m_ap_supplement_goods;
	//极限试炼最大波数
	private	int m_ultimatetrial_max_wave;
	//极限试炼每5波奖励图标
	private	string[] m_ultimatetrial_5wave_reward_icon;
	//极限试炼每5波奖励数量
	private	int[] m_ultimatetrial_5wave_reward_num;
	//极限试炼勇者证明获得：杀怪#每波
	private	int[] m_ultimatetrial_honestdiploma_get;
	//极限试炼购买消耗勇者证明
	private	int m_ultimatetrial_honestdiploma_cost;
	//极限试炼恢复生命上限比率
	private	float m_ultimatetrial_honestdiploma_num1;
	//极限试炼物理、法术攻击提升率
	private	float m_ultimatetrial_honestdiploma_num2;
	//极限试炼物理、法术防御提升率
	private	float m_ultimatetrial_honestdiploma_num3;
	//极限试炼怒气获得提升比率
	private	float m_ultimatetrial_honestdiploma_num4;
	//极限试炼预约关（波）数
	private	int[] m_ultimatetrial_appointment_wave;
	//极限试炼预约消耗魔钻
	private	int[] m_ultimatetrial_appointment_cost;
	//极限试炼预约奖励圣灵之泉
	private	int[] m_ultimatetrial_appointment_reward;
	//极限试炼在榜奖励所需时间(天)
	private	int[] m_ultimatetrial_on_list_time;
	//极限试炼在榜奖励魔钻数量
	private	int[] m_ultimatetrial_on_list_reward;
	//神秘商店魔钻商品最小数量
	private	int m_mysteriousshop_diamond_commodity_num;
	//神秘商店商品总数
	private	int m_mysteriousshop_commodity_total_num;
	//极限试炼恢复生命特效（-1时无特效）
	private	string m_ultimatetrial_honestdiploma_effect1;
	//开启封印魔盒消耗道具id
	private	int m_open_bossbox_cost_id;
	//开启封印魔盒消耗道具数量（第1,2,3次开启）为0时显示为免费开启
	private	int[] m_open_bossbox_cost_num;
	//扫荡次数重置消耗魔钻(如果填了n个数，第n次后消耗按n次的消耗计算)
	private	int[] m_sweep_reset_cost;
	//缪斯奏曲领取活力时间
	private	string[] m_ap_get_time;
	//缪斯奏曲领取活力数量
	private	int m_ap_get_quantity;
	//祈愿奖励掉落
	private	int m_pray_drop;
	//单抽招募魔钻消耗
	private	int m_single_herorecruit_cost;
	//十连抽招募魔钻消耗
	private	int m_ten_herorecruit_cost;
	//活跃度付费任务抽取数量
	private	int m_activitymission_num1;
	//活跃度20任务抽取数量
	private	int m_activitymission_num2;
	//活跃度15任务抽取数量
	private	int m_activitymission_num3;
	//活跃度10任务抽取数量
	private	int m_activitymission_num4;
	//活跃度奖励档
	private	int[] m_activitymission_reward_level;
	//活跃度奖励掉落包
	private	int[] m_activitymission_reward_drop;
	//连续签到初始库
	private	int m_loginbonus_7day_initial_room;
	//累计签到初始库
	private	int m_loginbonus_28day_initial_room;
	//单抽梦想值奖励
	private	int m_single_herorecruit_dreamvalue;
	//十连抽梦想值奖励
	private	int m_ten_herorecruit_dreamvalue;
	//梦想兑换所需梦想值
	private	int m_dream_need_value;
	//梦想兑换刷新消耗(魔钻)
	private	int m_dream_refresh_cost;
	//免费购买倒计时时间（分钟）
	private	int m_single_herorecruit_time;
	//招募英雄的解封卷轴id
	private	int m_herorecruit_item_id;
	//守望者时间
	private	string[] m_legend_watcher_open_time;
	//传说战时间
	private	string[] m_legend_fight_open_time;
	//守望者CD时间
	private	int m_legeng_watcher_cd_time;
	//传说战CD时间
	private	int m_legend_fight_cd_time;
	//传说战秒CD消耗魔钻
	private	int m_legend_fight_cd_cost;
	//祝福消耗守望之灵
	private	int[] m_legend_wish_cost;
	//守望之灵每日最大用量
	private	int m_legend_wish_max;
	//祝福造成伤害加成
	private	float m_legend_wish_damage_up;
	//祝福受到伤害减免
	private	float m_legend_wish_hurt_down;
	//守望之灵单价（魔钻）
	private	int m_legend_watcher_soul_cost;
	//传说战BOSS初始血量
	private	string m_legend_boss_init_hp;
	//传说战BOSS血量系数A
	private	float m_legend_boss_hp_coefficient_A;
	//传说战BOSS血量系数B
	private	float m_legend_boss_hp_coefficient_B;
	//传说战BOSS血量系数C
	private	float m_legend_boss_hp_coefficient_C;
	//传说战BOSS血量系数D
	private	float m_legend_boss_hp_coefficient_D;
	//守望者战斗奖励系数A
	private	float m_legend_watcher_reward_coefficient_A;
	//守望者战斗奖励系数B
	private	float m_legend_watcher_reward_coefficient_B;
	//守望者战斗奖励系数C
	private	float m_legend_watcher_reward_coefficient_C;
	//守望者战斗奖励系数D
	private	float m_legend_watcher_reward_coefficient_D;
	//传说战奖励系数A
	private	float m_legend_fight_reward_coefficient_A;
	//传说战奖励系数B
	private	float m_legend_fight_reward_coefficient_B;
	//传说战奖励系数C
	private	float m_legend_fight_reward_coefficient_C;
	//传说战奖励系数D
	private	float m_legend_fight_reward_coefficient_D;
	//传说战奖励系数E
	private	float m_legend_fight_reward_coefficient_E;
	//传说战奖励系数F
	private	float m_legend_fight_reward_coefficient_F;
	//传说战击杀boss奖励魔钻
	private	int m_legend_kill_reward1;
	//传说战击杀boss奖励传说之石
	private	int m_legend_kill_reward2;
	//传说战伤害排行奖励掉落包1
	private	int m_legend_rank_reward1;
	//传说战伤害排行奖励掉落包2
	private	int m_legend_rank_reward2;
	//传说战伤害排行奖励掉落包3
	private	int m_legend_rank_reward3;
	//传说战伤害排行奖励掉落包4-5
	private	int m_legend_rank_reward4;
	//传说战伤害排行奖励掉落包6-10
	private	int m_legend_rank_reward5;
	//传说圣殿每日兑换最大次数
	private	int m_legend_excharge_max_num;
	//行动力补充道具
	private	int[] m_ep_supplement_item;
	//行动力补满商品id
	private	int m_ep_supplement_goods;
	//鉴定石商品id
	private	int m_identify_stone_id;
	//探险功能开启前置关卡
	private	int m_explore_open_stage;
	//玩家初始英雄
	private	int m_player_initial_hero;
	//招募初始英雄
	private	int m_recruit_initial_hero;
	//第一次通关1-2（1310102000）必定额外掉落符文的小包id（2星物理防御符文）
	private	int m_newbieguide_config_1;
	//新手引导特殊掉落关卡
	private	int m_newbieguide_config_2;
	//新手引导ID，用于关卡额外掉落判定
	private	int m_newbieguide_config_3;
	//最大属性暴击率上限
	private	int m_max_crit_limit;
	//最大属性闪避率上限
	private	int m_max_hiding_limit;
	//固定未命中率值
	private	int m_fix_nothit_value;
	//攻击力修正系数X
	private	float m_att_cor_para;
	//防御力修正系数Y
	private	float m_def_cor_para;
	//保底值系数
	private	float m_gua_val;
	//对应被动释放的主动技能逻辑参数，前后顺序分别是abcdpqx
	private	int[] m_passive_skill_parameter;
	//打断的生命上限伤害比例阈值
	private	int m_damage_interrupt_life_perc;
	//金币抽取（碎片和道具）必掉一个+1个经验药水
	private	int[] m_gold_number_1;
	//金币必给（碎片）必掉一个+1个经验药水
	private	int[] m_gold_number_10;
	//英雄抽取（碎片和英雄）必掉一个：掉英雄概率小+1个魂石（临时填的改名卡）
	private	int[] m_diamond_number_1;
	//英雄必给（英雄）必掉一个+1个魂石（临时填的改名卡）
	private	int[] m_diamond_number_10;
	//单抽首抽英雄（必掉一个指定英雄）+1个魂石（临时填的改名卡）
	private	int[] m_diamond_first;
	//战斗力攻击系数
	private	float m_combat_attack_factor;
	//战斗力防御系数
	private	float m_combat_defense_factor;
	//战斗力血量系数
	private	float m_combat_blood_factor;
	//单抽金币消耗
	private	int m_gold_consumer_1;
	//10连抽金币消耗
	private	int m_gold_consumer_10;
	//英雄抽取魔钻消耗
	private	int m_diamond_consumer_1;
	//10连抽英雄抽取魔钻消耗
	private	int m_diamond_consumer_10;
	//1号经验药水
	private	int m_item_exp_1;
	//2号经验药水
	private	int m_item_exp_2;
	//3号经验药水
	private	int m_item_exp_3;
	//4号经验药水
	private	int m_item_exp_4;
	//5号经验药水
	private	int m_item_exp_5;
	//6号经验药水
	private	int m_item_exp_6;
	//大秘术经验和小秘术经验
	private	int[] m_ms_exp;
	//英雄转碎片系数
	private	float m_hero_debris_factor;
	//10连抽首抽英雄（必掉一个A资质英雄）+1个魂石（临时填的改名卡）
	private	int[] m_diamond_ten;
	//被动式主动技能1初始释放概率
	private	int m_Passive_Active_Skills_A;
	//被动式主动技能1递增概率
	private	int m_Passive_Active_Skills_B;
	//c为第二个技能的初始几率
	private	int m_Passive_Active_Skills_C;
	//d为第二个技能每次递增几率
	private	int m_Passive_Active_Skills_D;
	//p为第一个技能出现几率a%时的普攻次数
	private	int m_Passive_Active_Skills_P;
	//q为第二个技能出现几率c%时的普攻次数
	private	int m_Passive_Active_Skills_Q;
	//x为普攻周期次数
	private	int m_Passive_Active_Skills_X;
    //金币免费抽取次数;
    private int m_Gold_Free_Times;
    //金币抽取CD（秒）;
    private int m_Gold_Extraction_CD;
    //钻石免费次数;
    private int m_Diamonds_Free_Times;
    //抽奖刷新时间（24小时制，凌晨X点），金币免费抽取次数和钻石免费抽取次数重置;
    private int m_Refresh_Recruit_Time;
	#endregion

	public override void parser(BinaryReader data)
	{
		int id = 0;
		id = data.ReadInt32();
		m_version_number = ReadToString(data);
		id = data.ReadInt32();
		m_run_speed = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_each_rage_point = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_max_rage_point = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_att_rp_inc = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_be_att_rp_inc = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_crit_att_para = ReadToSingle(data);
		id = data.ReadInt32();
		m_be_crit_att_para = ReadToSingle(data);
		id = data.ReadInt32();
		m_be_dodged_att_rp_inc = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_dodge_att_rp_inc = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_skillatt_rp_inc = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_be_skillatt_rp_inc = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_crit_skillatt_para = ReadToSingle(data);
		id = data.ReadInt32();
		m_be_crit_skillatt_para = ReadToSingle(data);
		id = data.ReadInt32();
		m_be_dodged_skillatt_rp_inc = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_dodge_skillatt_rp_inc = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_kill_rp_inc = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_be_kill_rp_inc = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_wave_fury = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_lose_every_1_hp = ReadToSingle(data);
		id = data.ReadInt32();
		m_personPublicCD = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_teamPublicCD = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_statusNumberRow = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_statusRow = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_attackCoefficient_AtoA = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_attackCoefficient_AtoB = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_attackCoefficient_AtoC = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_attackCoefficient_BtoA = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_attackCoefficient_BtoB = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_attackCoefficient_BtoC = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_attackCoefficient_CtoA = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_attackCoefficient_CtoB = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_attackCoefficient_CtoC = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_DodgeA = ReadToSingle(data);
		id = data.ReadInt32();
		m_DodgeB = ReadToSingle(data);
		id = data.ReadInt32();
		m_DodgeC = ReadToSingle(data);
		id = data.ReadInt32();
		m_CriticalD = ReadToSingle(data);
		id = data.ReadInt32();
		m_CriticalE = ReadToSingle(data);
		id = data.ReadInt32();
		m_CriticalF = ReadToSingle(data);
		id = data.ReadInt32();
		m_PhysicalAttackG = ReadToSingle(data);
		id = data.ReadInt32();
		m_PhysicalAttackH = ReadToSingle(data);
		id = data.ReadInt32();
		m_PhysicalAttackI = ReadToSingle(data);
		id = data.ReadInt32();
		m_MagicAttackJ = ReadToSingle(data);
		id = data.ReadInt32();
		m_MagicAttackK = ReadToSingle(data);
		id = data.ReadInt32();
		m_MagicAttackL = ReadToSingle(data);
		id = data.ReadInt32();
		m_MagicHealingM = ReadToSingle(data);
		id = data.ReadInt32();
		m_MagicHealingN = ReadToSingle(data);
		id = data.ReadInt32();
		m_MagicHealingO = ReadToSingle(data);
		id = data.ReadInt32();
		m_PhysicalHealingP = ReadToSingle(data);
		id = data.ReadInt32();
		m_PhysicalHealingQ = ReadToSingle(data);
		id = data.ReadInt32();
		m_PhysicalHealingR = ReadToSingle(data);
		id = data.ReadInt32();
		m_attackFrequencyS = ReadToSingle(data);
		id = data.ReadInt32();
		m_attackFrequencyT = ReadToSingle(data);
		id = data.ReadInt32();
		m_attackFrequencyU = ReadToSingle(data);
		id = data.ReadInt32();
		m_damageOffloatingMin = ReadToSingle(data);
		id = data.ReadInt32();
		m_damageOffloatingMax = ReadToSingle(data);
		id = data.ReadInt32();
		m_lifeRestoringX = ReadToSingle(data);
		id = data.ReadInt32();
		m_lifeRestoringY = ReadToSingle(data);
		id = data.ReadInt32();
		m_lifeRestoringZ = ReadToSingle(data);
		id = data.ReadInt32();
		m_healOffloatingMin = ReadToSingle(data);
		id = data.ReadInt32();
		m_healOffloatingMax = ReadToSingle(data);
		id = data.ReadInt32();
		m_Dodge_X = ReadToSingle(data);
		id = data.ReadInt32();
		m_Dodge_a = ReadToSingle(data);
		id = data.ReadInt32();
		m_Dodge_b = ReadToSingle(data);
		id = data.ReadInt32();
		m_Dodge_c = ReadToSingle(data);
		id = data.ReadInt32();
		m_Dodge_k = ReadToSingle(data);
		id = data.ReadInt32();
		m_Dodge_p = ReadToSingle(data);
		id = data.ReadInt32();
		m_Critical_X = ReadToSingle(data);
		id = data.ReadInt32();
		m_Critical_a = ReadToSingle(data);
		id = data.ReadInt32();
		m_Critical_b = ReadToSingle(data);
		id = data.ReadInt32();
		m_Critical_c = ReadToSingle(data);
		id = data.ReadInt32();
		m_Critical_k = ReadToSingle(data);
		id = data.ReadInt32();
		m_Critical_p = ReadToSingle(data);
		id = data.ReadInt32();
		m_Critical_base_power = ReadToSingle(data);
		id = data.ReadInt32();
		m_Dodge_min = ReadToSingle(data);
		id = data.ReadInt32();
		m_Critical_max = ReadToSingle(data);
		id = data.ReadInt32();
		m_deathSkillType = parserXMLIntArray(ReadToString(data));
		id = data.ReadInt32();
		m_deathSKillTypeShader = parserXMLStringArray(ReadToString(data));
		id = data.ReadInt32();
		m_player_max_level = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_initial_ap_upper_limit = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_initial_ap = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_per_ap_recovery_sec = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_initial_money = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_initial_diamond = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_initial_hero_packset = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_initial_common_item_packset = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_hero_packset_per_expand = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_common_item_packset_per_expand = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_hero_packset_max_purchase = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_common_item_packset_max_purchase = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_initial_ep_upper_limit = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_per_ep_recovery_sec = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_initial_ep = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_mysteryprob = ReadToSingle(data);
		id = data.ReadInt32();
		m_specificmissionsprob = ReadToSingle(data);
		id = data.ReadInt32();
		m_normal_rune_2star_expose_cost = parserXMLIntArray(ReadToString(data));
		id = data.ReadInt32();
		m_normal_rune_3star_expose_cost = parserXMLIntArray(ReadToString(data));
		id = data.ReadInt32();
		m_normal_rune_4star_expose_cost = parserXMLIntArray(ReadToString(data));
		id = data.ReadInt32();
		m_normal_rune_5star_expose_cost = parserXMLIntArray(ReadToString(data));
		id = data.ReadInt32();
		m_special_rune_3star_expose_cost = parserXMLIntArray(ReadToString(data));
		id = data.ReadInt32();
		m_special_rune_4star_expose_cost = parserXMLIntArray(ReadToString(data));
		id = data.ReadInt32();
		m_special_rune_5star_expose_cost = parserXMLIntArray(ReadToString(data));
		id = data.ReadInt32();
		m_hero_packset_expand_cost = parserXMLIntArray(ReadToString(data));
		id = data.ReadInt32();
		m_common_item_packset_expand_cost = parserXMLIntArray(ReadToString(data));
		id = data.ReadInt32();
		m_player_ai_check = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_realmoney_to_vipexp = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_gold_to_vipexp = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_mysteryprob_plus = ReadToSingle(data);
		id = data.ReadInt32();
		m_specificmissionsprob_plus = ReadToSingle(data);
		id = data.ReadInt32();
		m_mystery_duration = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_specificmissions_duration = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_explore_queue = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_explore_quest_num = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_explore_refresh_cost = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_explore_param_a = ReadToSingle(data);
		id = data.ReadInt32();
		m_explore_param_b = ReadToSingle(data);
		id = data.ReadInt32();
		m_explore_param_c = ReadToSingle(data);
		id = data.ReadInt32();
		m_treasure1_event_prob = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_treasure2_event_prob = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_treasure3_event_prob = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_treasure4_event_prob = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_treasure_event10_min = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_treasure_event10_max = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_treasure_single_cost = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_treasure_single_reward = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_treasure_ten_cost = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_treasure_ten_reward = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_treasure_refresh_cost = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_treasure_refresh_reward = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_treasure_event1_des = ReadToString(data);
		id = data.ReadInt32();
		m_treasure_event2_des = ReadToString(data);
		id = data.ReadInt32();
		m_treasure_event7_des = ReadToString(data);
		id = data.ReadInt32();
		m_treasure_event8_des = ReadToString(data);
		id = data.ReadInt32();
		m_treasure_event9_des = ReadToString(data);
		id = data.ReadInt32();
		m_treasure_single_everyday = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_stage_1star_reward_pow = ReadToSingle(data);
		id = data.ReadInt32();
		m_stage_2star_reward_pow = ReadToSingle(data);
		id = data.ReadInt32();
		m_stage_3star_reward_pow = ReadToSingle(data);
		id = data.ReadInt32();
		m_jingyanjiejing_to_jingyan = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_rongling_conversion_rate = ReadToSingle(data);
		id = data.ReadInt32();
		m_hero_advanced_level_3 = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_hero_advanced_level_4 = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_hero_advanced_level_5 = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_ap_supplement_item = parserXMLIntArray(ReadToString(data));
		id = data.ReadInt32();
		m_ap_supplement_goods = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_ultimatetrial_max_wave = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_ultimatetrial_5wave_reward_icon = parserXMLStringArray(ReadToString(data));
		id = data.ReadInt32();
		m_ultimatetrial_5wave_reward_num = parserXMLIntArray(ReadToString(data));
		id = data.ReadInt32();
		m_ultimatetrial_honestdiploma_get = parserXMLIntArray(ReadToString(data));
		id = data.ReadInt32();
		m_ultimatetrial_honestdiploma_cost = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_ultimatetrial_honestdiploma_num1 = ReadToSingle(data);
		id = data.ReadInt32();
		m_ultimatetrial_honestdiploma_num2 = ReadToSingle(data);
		id = data.ReadInt32();
		m_ultimatetrial_honestdiploma_num3 = ReadToSingle(data);
		id = data.ReadInt32();
		m_ultimatetrial_honestdiploma_num4 = ReadToSingle(data);
		id = data.ReadInt32();
		m_ultimatetrial_appointment_wave = parserXMLIntArray(ReadToString(data));
		id = data.ReadInt32();
		m_ultimatetrial_appointment_cost = parserXMLIntArray(ReadToString(data));
		id = data.ReadInt32();
		m_ultimatetrial_appointment_reward = parserXMLIntArray(ReadToString(data));
		id = data.ReadInt32();
		m_ultimatetrial_on_list_time = parserXMLIntArray(ReadToString(data));
		id = data.ReadInt32();
		m_ultimatetrial_on_list_reward = parserXMLIntArray(ReadToString(data));
		id = data.ReadInt32();
		m_mysteriousshop_diamond_commodity_num = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_mysteriousshop_commodity_total_num = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_ultimatetrial_honestdiploma_effect1 = ReadToString(data);
		id = data.ReadInt32();
		m_open_bossbox_cost_id = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_open_bossbox_cost_num = parserXMLIntArray(ReadToString(data));
		id = data.ReadInt32();
		m_sweep_reset_cost = parserXMLIntArray(ReadToString(data));
		id = data.ReadInt32();
		m_ap_get_time = parserXMLStringArray(ReadToString(data));
		id = data.ReadInt32();
		m_ap_get_quantity = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_pray_drop = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_single_herorecruit_cost = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_ten_herorecruit_cost = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_activitymission_num1 = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_activitymission_num2 = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_activitymission_num3 = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_activitymission_num4 = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_activitymission_reward_level = parserXMLIntArray(ReadToString(data));
		id = data.ReadInt32();
		m_activitymission_reward_drop = parserXMLIntArray(ReadToString(data));
		id = data.ReadInt32();
		m_loginbonus_7day_initial_room = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_loginbonus_28day_initial_room = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_single_herorecruit_dreamvalue = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_ten_herorecruit_dreamvalue = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_dream_need_value = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_dream_refresh_cost = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_single_herorecruit_time = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_herorecruit_item_id = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_legend_watcher_open_time = parserXMLStringArray(ReadToString(data));
		id = data.ReadInt32();
		m_legend_fight_open_time = parserXMLStringArray(ReadToString(data));
		id = data.ReadInt32();
		m_legeng_watcher_cd_time = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_legend_fight_cd_time = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_legend_fight_cd_cost = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_legend_wish_cost = parserXMLIntArray(ReadToString(data));
		id = data.ReadInt32();
		m_legend_wish_max = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_legend_wish_damage_up = ReadToSingle(data);
		id = data.ReadInt32();
		m_legend_wish_hurt_down = ReadToSingle(data);
		id = data.ReadInt32();
		m_legend_watcher_soul_cost = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_legend_boss_init_hp = ReadToString(data);
		id = data.ReadInt32();
		m_legend_boss_hp_coefficient_A = ReadToSingle(data);
		id = data.ReadInt32();
		m_legend_boss_hp_coefficient_B = ReadToSingle(data);
		id = data.ReadInt32();
		m_legend_boss_hp_coefficient_C = ReadToSingle(data);
		id = data.ReadInt32();
		m_legend_boss_hp_coefficient_D = ReadToSingle(data);
		id = data.ReadInt32();
		m_legend_watcher_reward_coefficient_A = ReadToSingle(data);
		id = data.ReadInt32();
		m_legend_watcher_reward_coefficient_B = ReadToSingle(data);
		id = data.ReadInt32();
		m_legend_watcher_reward_coefficient_C = ReadToSingle(data);
		id = data.ReadInt32();
		m_legend_watcher_reward_coefficient_D = ReadToSingle(data);
		id = data.ReadInt32();
		m_legend_fight_reward_coefficient_A = ReadToSingle(data);
		id = data.ReadInt32();
		m_legend_fight_reward_coefficient_B = ReadToSingle(data);
		id = data.ReadInt32();
		m_legend_fight_reward_coefficient_C = ReadToSingle(data);
		id = data.ReadInt32();
		m_legend_fight_reward_coefficient_D = ReadToSingle(data);
		id = data.ReadInt32();
		m_legend_fight_reward_coefficient_E = ReadToSingle(data);
		id = data.ReadInt32();
		m_legend_fight_reward_coefficient_F = ReadToSingle(data);
		id = data.ReadInt32();
		m_legend_kill_reward1 = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_legend_kill_reward2 = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_legend_rank_reward1 = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_legend_rank_reward2 = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_legend_rank_reward3 = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_legend_rank_reward4 = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_legend_rank_reward5 = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_legend_excharge_max_num = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_ep_supplement_item = parserXMLIntArray(ReadToString(data));
		id = data.ReadInt32();
		m_ep_supplement_goods = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_identify_stone_id = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_explore_open_stage = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_player_initial_hero = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_recruit_initial_hero = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_newbieguide_config_1 = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_newbieguide_config_2 = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_newbieguide_config_3 = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_max_crit_limit = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_max_hiding_limit = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_fix_nothit_value = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_att_cor_para = ReadToSingle(data);
		id = data.ReadInt32();
		m_def_cor_para = ReadToSingle(data);
		id = data.ReadInt32();
		m_gua_val = ReadToSingle(data);
		id = data.ReadInt32();
		m_passive_skill_parameter = parserXMLIntArray(ReadToString(data));
		id = data.ReadInt32();
		m_damage_interrupt_life_perc = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_gold_number_1 = parserXMLIntArray(ReadToString(data));
		id = data.ReadInt32();
		m_gold_number_10 = parserXMLIntArray(ReadToString(data));
		id = data.ReadInt32();
		m_diamond_number_1 = parserXMLIntArray(ReadToString(data));
		id = data.ReadInt32();
		m_diamond_number_10 = parserXMLIntArray(ReadToString(data));
		id = data.ReadInt32();
		m_diamond_first = parserXMLIntArray(ReadToString(data));
		id = data.ReadInt32();
		m_combat_attack_factor = ReadToSingle(data);
		id = data.ReadInt32();
		m_combat_defense_factor = ReadToSingle(data);
		id = data.ReadInt32();
		m_combat_blood_factor = ReadToSingle(data);
		id = data.ReadInt32();
		m_gold_consumer_1 = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_gold_consumer_10 = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_diamond_consumer_1 = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_diamond_consumer_10 = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_item_exp_1 = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_item_exp_2 = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_item_exp_3 = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_item_exp_4 = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_item_exp_5 = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_item_exp_6 = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_ms_exp = parserXMLIntArray(ReadToString(data));
		id = data.ReadInt32();
		m_hero_debris_factor = ReadToSingle(data);
		id = data.ReadInt32();
		m_diamond_ten = parserXMLIntArray(ReadToString(data));
		id = data.ReadInt32();
		m_Passive_Active_Skills_A = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_Passive_Active_Skills_B = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_Passive_Active_Skills_C = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_Passive_Active_Skills_D = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_Passive_Active_Skills_P = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_Passive_Active_Skills_Q = Int32.Parse(ReadToString(data));
		id = data.ReadInt32();
		m_Passive_Active_Skills_X = Int32.Parse(ReadToString(data));

        //抽奖---相关;
        id = data.ReadInt32();
        m_Gold_Free_Times = Int32.Parse(ReadToString(data));
        id = data.ReadInt32();
        m_Gold_Extraction_CD = Int32.Parse(ReadToString(data));
        id = data.ReadInt32();
        m_Diamonds_Free_Times = Int32.Parse(ReadToString(data));
        id = data.ReadInt32();
        m_Refresh_Recruit_Time = Int32.Parse(ReadToString(data));

	}


	public	string	getVersion_number()
	{
		return this.m_version_number;
	}

	public	int	getRun_speed()
	{
		return this.m_run_speed;
	}

	public	int	getEach_rage_point()
	{
		return this.m_each_rage_point;
	}

	public	int	getMax_rage_point()
	{
		return this.m_max_rage_point;
	}

	public	int	getAtt_rp_inc()
	{
		return this.m_att_rp_inc;
	}

	public	int	getBe_att_rp_inc()
	{
		return this.m_be_att_rp_inc;
	}

	public	float	getCrit_att_para()
	{
		return this.m_crit_att_para;
	}

	public	float	getBe_crit_att_para()
	{
		return this.m_be_crit_att_para;
	}

	public	int	getBe_dodged_att_rp_inc()
	{
		return this.m_be_dodged_att_rp_inc;
	}

	public	int	getDodge_att_rp_inc()
	{
		return this.m_dodge_att_rp_inc;
	}

	public	int	getSkillatt_rp_inc()
	{
		return this.m_skillatt_rp_inc;
	}

	public	int	getBe_skillatt_rp_inc()
	{
		return this.m_be_skillatt_rp_inc;
	}

	public	float	getCrit_skillatt_para()
	{
		return this.m_crit_skillatt_para;
	}

	public	float	getBe_crit_skillatt_para()
	{
		return this.m_be_crit_skillatt_para;
	}

	public	int	getBe_dodged_skillatt_rp_inc()
	{
		return this.m_be_dodged_skillatt_rp_inc;
	}

	public	int	getDodge_skillatt_rp_inc()
	{
		return this.m_dodge_skillatt_rp_inc;
	}

	public	int	getKill_rp_inc()
	{
		return this.m_kill_rp_inc;
	}

	public	int	getBe_kill_rp_inc()
	{
		return this.m_be_kill_rp_inc;
	}

	public	int	getWave_fury()
	{
		return this.m_wave_fury;
	}

	public	float	getLose_every_1_hp()
	{
		return this.m_lose_every_1_hp;
	}

	public	int	getPersonPublicCD()
	{
		return this.m_personPublicCD;
	}

	public	int	getTeamPublicCD()
	{
		return this.m_teamPublicCD;
	}

	public	int	getStatusNumberRow()
	{
		return this.m_statusNumberRow;
	}

	public	int	getStatusRow()
	{
		return this.m_statusRow;
	}

	public	int	getAttackCoefficient_AtoA()
	{
		return this.m_attackCoefficient_AtoA;
	}

	public	int	getAttackCoefficient_AtoB()
	{
		return this.m_attackCoefficient_AtoB;
	}

	public	int	getAttackCoefficient_AtoC()
	{
		return this.m_attackCoefficient_AtoC;
	}

	public	int	getAttackCoefficient_BtoA()
	{
		return this.m_attackCoefficient_BtoA;
	}

	public	int	getAttackCoefficient_BtoB()
	{
		return this.m_attackCoefficient_BtoB;
	}

	public	int	getAttackCoefficient_BtoC()
	{
		return this.m_attackCoefficient_BtoC;
	}

	public	int	getAttackCoefficient_CtoA()
	{
		return this.m_attackCoefficient_CtoA;
	}

	public	int	getAttackCoefficient_CtoB()
	{
		return this.m_attackCoefficient_CtoB;
	}

	public	int	getAttackCoefficient_CtoC()
	{
		return this.m_attackCoefficient_CtoC;
	}

	public	float	getDodgeA()
	{
		return this.m_DodgeA;
	}

	public	float	getDodgeB()
	{
		return this.m_DodgeB;
	}

	public	float	getDodgeC()
	{
		return this.m_DodgeC;
	}

	public	float	getCriticalD()
	{
		return this.m_CriticalD;
	}

	public	float	getCriticalE()
	{
		return this.m_CriticalE;
	}

	public	float	getCriticalF()
	{
		return this.m_CriticalF;
	}

	public	float	getPhysicalAttackG()
	{
		return this.m_PhysicalAttackG;
	}

	public	float	getPhysicalAttackH()
	{
		return this.m_PhysicalAttackH;
	}

	public	float	getPhysicalAttackI()
	{
		return this.m_PhysicalAttackI;
	}

	public	float	getMagicAttackJ()
	{
		return this.m_MagicAttackJ;
	}

	public	float	getMagicAttackK()
	{
		return this.m_MagicAttackK;
	}

	public	float	getMagicAttackL()
	{
		return this.m_MagicAttackL;
	}

	public	float	getMagicHealingM()
	{
		return this.m_MagicHealingM;
	}

	public	float	getMagicHealingN()
	{
		return this.m_MagicHealingN;
	}

	public	float	getMagicHealingO()
	{
		return this.m_MagicHealingO;
	}

	public	float	getPhysicalHealingP()
	{
		return this.m_PhysicalHealingP;
	}

	public	float	getPhysicalHealingQ()
	{
		return this.m_PhysicalHealingQ;
	}

	public	float	getPhysicalHealingR()
	{
		return this.m_PhysicalHealingR;
	}

	public	float	getAttackFrequencyS()
	{
		return this.m_attackFrequencyS;
	}

	public	float	getAttackFrequencyT()
	{
		return this.m_attackFrequencyT;
	}

	public	float	getAttackFrequencyU()
	{
		return this.m_attackFrequencyU;
	}

	public	float	getDamageOffloatingMin()
	{
		return this.m_damageOffloatingMin;
	}

	public	float	getDamageOffloatingMax()
	{
		return this.m_damageOffloatingMax;
	}

	public	float	getLifeRestoringX()
	{
		return this.m_lifeRestoringX;
	}

	public	float	getLifeRestoringY()
	{
		return this.m_lifeRestoringY;
	}

	public	float	getLifeRestoringZ()
	{
		return this.m_lifeRestoringZ;
	}

	public	float	getHealOffloatingMin()
	{
		return this.m_healOffloatingMin;
	}

	public	float	getHealOffloatingMax()
	{
		return this.m_healOffloatingMax;
	}

	public	float	getDodge_X()
	{
		return this.m_Dodge_X;
	}

	public	float	getDodge_a()
	{
		return this.m_Dodge_a;
	}

	public	float	getDodge_b()
	{
		return this.m_Dodge_b;
	}

	public	float	getDodge_c()
	{
		return this.m_Dodge_c;
	}

	public	float	getDodge_k()
	{
		return this.m_Dodge_k;
	}

	public	float	getDodge_p()
	{
		return this.m_Dodge_p;
	}

	public	float	getCritical_X()
	{
		return this.m_Critical_X;
	}

	public	float	getCritical_a()
	{
		return this.m_Critical_a;
	}

	public	float	getCritical_b()
	{
		return this.m_Critical_b;
	}

	public	float	getCritical_c()
	{
		return this.m_Critical_c;
	}

	public	float	getCritical_k()
	{
		return this.m_Critical_k;
	}

	public	float	getCritical_p()
	{
		return this.m_Critical_p;
	}

	public	float	getCritical_base_power()
	{
		return this.m_Critical_base_power;
	}

	public	float	getDodge_min()
	{
		return this.m_Dodge_min;
	}

	public	float	getCritical_max()
	{
		return this.m_Critical_max;
	}

	public	int[]	getDeathSkillType()
	{
		return this.m_deathSkillType;
	}

	public	string[]	getDeathSKillTypeShader()
	{
		return this.m_deathSKillTypeShader;
	}

	public	int	getPlayer_max_level()
	{
		return this.m_player_max_level;
	}

	public	int	getInitial_ap_upper_limit()
	{
		return this.m_initial_ap_upper_limit;
	}

	public	int	getInitial_ap()
	{
		return this.m_initial_ap;
	}

	public	int	getPer_ap_recovery_sec()
	{
		return this.m_per_ap_recovery_sec;
	}

	public	int	getInitial_money()
	{
		return this.m_initial_money;
	}

	public	int	getInitial_diamond()
	{
		return this.m_initial_diamond;
	}

	public	int	getInitial_hero_packset()
	{
		return this.m_initial_hero_packset;
	}

	public	int	getInitial_common_item_packset()
	{
		return this.m_initial_common_item_packset;
	}

	public	int	getHero_packset_per_expand()
	{
		return this.m_hero_packset_per_expand;
	}

	public	int	getCommon_item_packset_per_expand()
	{
		return this.m_common_item_packset_per_expand;
	}

	public	int	getHero_packset_max_purchase()
	{
		return this.m_hero_packset_max_purchase;
	}

	public	int	getCommon_item_packset_max_purchase()
	{
		return this.m_common_item_packset_max_purchase;
	}

	public	int	getInitial_ep_upper_limit()
	{
		return this.m_initial_ep_upper_limit;
	}

	public	int	getPer_ep_recovery_sec()
	{
		return this.m_per_ep_recovery_sec;
	}

	public	int	getInitial_ep()
	{
		return this.m_initial_ep;
	}

	public	float	getMysteryprob()
	{
		return this.m_mysteryprob;
	}

	public	float	getSpecificmissionsprob()
	{
		return this.m_specificmissionsprob;
	}

	public	int[]	getNormal_rune_2star_expose_cost()
	{
		return this.m_normal_rune_2star_expose_cost;
	}

	public	int[]	getNormal_rune_3star_expose_cost()
	{
		return this.m_normal_rune_3star_expose_cost;
	}

	public	int[]	getNormal_rune_4star_expose_cost()
	{
		return this.m_normal_rune_4star_expose_cost;
	}

	public	int[]	getNormal_rune_5star_expose_cost()
	{
		return this.m_normal_rune_5star_expose_cost;
	}

	public	int[]	getSpecial_rune_3star_expose_cost()
	{
		return this.m_special_rune_3star_expose_cost;
	}

	public	int[]	getSpecial_rune_4star_expose_cost()
	{
		return this.m_special_rune_4star_expose_cost;
	}

	public	int[]	getSpecial_rune_5star_expose_cost()
	{
		return this.m_special_rune_5star_expose_cost;
	}

	public	int[]	getHero_packset_expand_cost()
	{
		return this.m_hero_packset_expand_cost;
	}

	public	int[]	getCommon_item_packset_expand_cost()
	{
		return this.m_common_item_packset_expand_cost;
	}

	public	int	getPlayer_ai_check()
	{
		return this.m_player_ai_check;
	}

	public	int	getRealmoney_to_vipexp()
	{
		return this.m_realmoney_to_vipexp;
	}

	public	int	getGold_to_vipexp()
	{
		return this.m_gold_to_vipexp;
	}

	public	float	getMysteryprob_plus()
	{
		return this.m_mysteryprob_plus;
	}

	public	float	getSpecificmissionsprob_plus()
	{
		return this.m_specificmissionsprob_plus;
	}

	public	int	getMystery_duration()
	{
		return this.m_mystery_duration;
	}

	public	int	getSpecificmissions_duration()
	{
		return this.m_specificmissions_duration;
	}

	public	int	getExplore_queue()
	{
		return this.m_explore_queue;
	}

	public	int	getExplore_quest_num()
	{
		return this.m_explore_quest_num;
	}

	public	int	getExplore_refresh_cost()
	{
		return this.m_explore_refresh_cost;
	}

	public	float	getExplore_param_a()
	{
		return this.m_explore_param_a;
	}

	public	float	getExplore_param_b()
	{
		return this.m_explore_param_b;
	}

	public	float	getExplore_param_c()
	{
		return this.m_explore_param_c;
	}

	public	int	getTreasure1_event_prob()
	{
		return this.m_treasure1_event_prob;
	}

	public	int	getTreasure2_event_prob()
	{
		return this.m_treasure2_event_prob;
	}

	public	int	getTreasure3_event_prob()
	{
		return this.m_treasure3_event_prob;
	}

	public	int	getTreasure4_event_prob()
	{
		return this.m_treasure4_event_prob;
	}

	public	int	getTreasure_event10_min()
	{
		return this.m_treasure_event10_min;
	}

	public	int	getTreasure_event10_max()
	{
		return this.m_treasure_event10_max;
	}

	public	int	getTreasure_single_cost()
	{
		return this.m_treasure_single_cost;
	}

	public	int	getTreasure_single_reward()
	{
		return this.m_treasure_single_reward;
	}

	public	int	getTreasure_ten_cost()
	{
		return this.m_treasure_ten_cost;
	}

	public	int	getTreasure_ten_reward()
	{
		return this.m_treasure_ten_reward;
	}

	public	int	getTreasure_refresh_cost()
	{
		return this.m_treasure_refresh_cost;
	}

	public	int	getTreasure_refresh_reward()
	{
		return this.m_treasure_refresh_reward;
	}

	public	string	getTreasure_event1_des()
	{
		return this.m_treasure_event1_des;
	}

	public	string	getTreasure_event2_des()
	{
		return this.m_treasure_event2_des;
	}

	public	string	getTreasure_event7_des()
	{
		return this.m_treasure_event7_des;
	}

	public	string	getTreasure_event8_des()
	{
		return this.m_treasure_event8_des;
	}

	public	string	getTreasure_event9_des()
	{
		return this.m_treasure_event9_des;
	}

	public	int	getTreasure_single_everyday()
	{
		return this.m_treasure_single_everyday;
	}

	public	float	getStage_1star_reward_pow()
	{
		return this.m_stage_1star_reward_pow;
	}

	public	float	getStage_2star_reward_pow()
	{
		return this.m_stage_2star_reward_pow;
	}

	public	float	getStage_3star_reward_pow()
	{
		return this.m_stage_3star_reward_pow;
	}

	public	int	getJingyanjiejing_to_jingyan()
	{
		return this.m_jingyanjiejing_to_jingyan;
	}

	public	float	getRongling_conversion_rate()
	{
		return this.m_rongling_conversion_rate;
	}

	public	int	getHero_advanced_level_3()
	{
		return this.m_hero_advanced_level_3;
	}

	public	int	getHero_advanced_level_4()
	{
		return this.m_hero_advanced_level_4;
	}

	public	int	getHero_advanced_level_5()
	{
		return this.m_hero_advanced_level_5;
	}

	public	int[]	getAp_supplement_item()
	{
		return this.m_ap_supplement_item;
	}

	public	int	getAp_supplement_goods()
	{
		return this.m_ap_supplement_goods;
	}

	public	int	getUltimatetrial_max_wave()
	{
		return this.m_ultimatetrial_max_wave;
	}

	public	string[]	getUltimatetrial_5wave_reward_icon()
	{
		return this.m_ultimatetrial_5wave_reward_icon;
	}

	public	int[]	getUltimatetrial_5wave_reward_num()
	{
		return this.m_ultimatetrial_5wave_reward_num;
	}

	public	int[]	getUltimatetrial_honestdiploma_get()
	{
		return this.m_ultimatetrial_honestdiploma_get;
	}

	public	int	getUltimatetrial_honestdiploma_cost()
	{
		return this.m_ultimatetrial_honestdiploma_cost;
	}

	public	float	getUltimatetrial_honestdiploma_num1()
	{
		return this.m_ultimatetrial_honestdiploma_num1;
	}

	public	float	getUltimatetrial_honestdiploma_num2()
	{
		return this.m_ultimatetrial_honestdiploma_num2;
	}

	public	float	getUltimatetrial_honestdiploma_num3()
	{
		return this.m_ultimatetrial_honestdiploma_num3;
	}

	public	float	getUltimatetrial_honestdiploma_num4()
	{
		return this.m_ultimatetrial_honestdiploma_num4;
	}

	public	int[]	getUltimatetrial_appointment_wave()
	{
		return this.m_ultimatetrial_appointment_wave;
	}

	public	int[]	getUltimatetrial_appointment_cost()
	{
		return this.m_ultimatetrial_appointment_cost;
	}

	public	int[]	getUltimatetrial_appointment_reward()
	{
		return this.m_ultimatetrial_appointment_reward;
	}

	public	int[]	getUltimatetrial_on_list_time()
	{
		return this.m_ultimatetrial_on_list_time;
	}

	public	int[]	getUltimatetrial_on_list_reward()
	{
		return this.m_ultimatetrial_on_list_reward;
	}

	public	int	getMysteriousshop_diamond_commodity_num()
	{
		return this.m_mysteriousshop_diamond_commodity_num;
	}

	public	int	getMysteriousshop_commodity_total_num()
	{
		return this.m_mysteriousshop_commodity_total_num;
	}

	public	string	getUltimatetrial_honestdiploma_effect1()
	{
		return this.m_ultimatetrial_honestdiploma_effect1;
	}

	public	int	getOpen_bossbox_cost_id()
	{
		return this.m_open_bossbox_cost_id;
	}

	public	int[]	getOpen_bossbox_cost_num()
	{
		return this.m_open_bossbox_cost_num;
	}

	public	int[]	getSweep_reset_cost()
	{
		return this.m_sweep_reset_cost;
	}

	public	string[]	getAp_get_time()
	{
		return this.m_ap_get_time;
	}

	public	int	getAp_get_quantity()
	{
		return this.m_ap_get_quantity;
	}

	public	int	getPray_drop()
	{
		return this.m_pray_drop;
	}

	public	int	getSingle_herorecruit_cost()
	{
		return this.m_single_herorecruit_cost;
	}

	public	int	getTen_herorecruit_cost()
	{
		return this.m_ten_herorecruit_cost;
	}

	public	int	getActivitymission_num1()
	{
		return this.m_activitymission_num1;
	}

	public	int	getActivitymission_num2()
	{
		return this.m_activitymission_num2;
	}

	public	int	getActivitymission_num3()
	{
		return this.m_activitymission_num3;
	}

	public	int	getActivitymission_num4()
	{
		return this.m_activitymission_num4;
	}

	public	int[]	getActivitymission_reward_level()
	{
		return this.m_activitymission_reward_level;
	}

	public	int[]	getActivitymission_reward_drop()
	{
		return this.m_activitymission_reward_drop;
	}

	public	int	getLoginbonus_7day_initial_room()
	{
		return this.m_loginbonus_7day_initial_room;
	}

	public	int	getLoginbonus_28day_initial_room()
	{
		return this.m_loginbonus_28day_initial_room;
	}

	public	int	getSingle_herorecruit_dreamvalue()
	{
		return this.m_single_herorecruit_dreamvalue;
	}

	public	int	getTen_herorecruit_dreamvalue()
	{
		return this.m_ten_herorecruit_dreamvalue;
	}

	public	int	getDream_need_value()
	{
		return this.m_dream_need_value;
	}

	public	int	getDream_refresh_cost()
	{
		return this.m_dream_refresh_cost;
	}

	public	int	getSingle_herorecruit_time()
	{
		return this.m_single_herorecruit_time;
	}

	public	int	getHerorecruit_item_id()
	{
		return this.m_herorecruit_item_id;
	}

	public	string[]	getLegend_watcher_open_time()
	{
		return this.m_legend_watcher_open_time;
	}

	public	string[]	getLegend_fight_open_time()
	{
		return this.m_legend_fight_open_time;
	}

	public	int	getLegeng_watcher_cd_time()
	{
		return this.m_legeng_watcher_cd_time;
	}

	public	int	getLegend_fight_cd_time()
	{
		return this.m_legend_fight_cd_time;
	}

	public	int	getLegend_fight_cd_cost()
	{
		return this.m_legend_fight_cd_cost;
	}

	public	int[]	getLegend_wish_cost()
	{
		return this.m_legend_wish_cost;
	}

	public	int	getLegend_wish_max()
	{
		return this.m_legend_wish_max;
	}

	public	float	getLegend_wish_damage_up()
	{
		return this.m_legend_wish_damage_up;
	}

	public	float	getLegend_wish_hurt_down()
	{
		return this.m_legend_wish_hurt_down;
	}

	public	int	getLegend_watcher_soul_cost()
	{
		return this.m_legend_watcher_soul_cost;
	}

	public	string	getLegend_boss_init_hp()
	{
		return this.m_legend_boss_init_hp;
	}

	public	float	getLegend_boss_hp_coefficient_A()
	{
		return this.m_legend_boss_hp_coefficient_A;
	}

	public	float	getLegend_boss_hp_coefficient_B()
	{
		return this.m_legend_boss_hp_coefficient_B;
	}

	public	float	getLegend_boss_hp_coefficient_C()
	{
		return this.m_legend_boss_hp_coefficient_C;
	}

	public	float	getLegend_boss_hp_coefficient_D()
	{
		return this.m_legend_boss_hp_coefficient_D;
	}

	public	float	getLegend_watcher_reward_coefficient_A()
	{
		return this.m_legend_watcher_reward_coefficient_A;
	}

	public	float	getLegend_watcher_reward_coefficient_B()
	{
		return this.m_legend_watcher_reward_coefficient_B;
	}

	public	float	getLegend_watcher_reward_coefficient_C()
	{
		return this.m_legend_watcher_reward_coefficient_C;
	}

	public	float	getLegend_watcher_reward_coefficient_D()
	{
		return this.m_legend_watcher_reward_coefficient_D;
	}

	public	float	getLegend_fight_reward_coefficient_A()
	{
		return this.m_legend_fight_reward_coefficient_A;
	}

	public	float	getLegend_fight_reward_coefficient_B()
	{
		return this.m_legend_fight_reward_coefficient_B;
	}

	public	float	getLegend_fight_reward_coefficient_C()
	{
		return this.m_legend_fight_reward_coefficient_C;
	}

	public	float	getLegend_fight_reward_coefficient_D()
	{
		return this.m_legend_fight_reward_coefficient_D;
	}

	public	float	getLegend_fight_reward_coefficient_E()
	{
		return this.m_legend_fight_reward_coefficient_E;
	}

	public	float	getLegend_fight_reward_coefficient_F()
	{
		return this.m_legend_fight_reward_coefficient_F;
	}

	public	int	getLegend_kill_reward1()
	{
		return this.m_legend_kill_reward1;
	}

	public	int	getLegend_kill_reward2()
	{
		return this.m_legend_kill_reward2;
	}

	public	int	getLegend_rank_reward1()
	{
		return this.m_legend_rank_reward1;
	}

	public	int	getLegend_rank_reward2()
	{
		return this.m_legend_rank_reward2;
	}

	public	int	getLegend_rank_reward3()
	{
		return this.m_legend_rank_reward3;
	}

	public	int	getLegend_rank_reward4()
	{
		return this.m_legend_rank_reward4;
	}

	public	int	getLegend_rank_reward5()
	{
		return this.m_legend_rank_reward5;
	}

	public	int	getLegend_excharge_max_num()
	{
		return this.m_legend_excharge_max_num;
	}

	public	int[]	getEp_supplement_item()
	{
		return this.m_ep_supplement_item;
	}

	public	int	getEp_supplement_goods()
	{
		return this.m_ep_supplement_goods;
	}

	public	int	getIdentify_stone_id()
	{
		return this.m_identify_stone_id;
	}

	public	int	getExplore_open_stage()
	{
		return this.m_explore_open_stage;
	}

	public	int	getPlayer_initial_hero()
	{
		return this.m_player_initial_hero;
	}

	public	int	getRecruit_initial_hero()
	{
		return this.m_recruit_initial_hero;
	}

	public	int	getNewbieguide_config_1()
	{
		return this.m_newbieguide_config_1;
	}

	public	int	getNewbieguide_config_2()
	{
		return this.m_newbieguide_config_2;
	}

	public	int	getNewbieguide_config_3()
	{
		return this.m_newbieguide_config_3;
	}

	public	int	getMax_crit_limit()
	{
		return this.m_max_crit_limit;
	}

	public	int	getMax_hiding_limit()
	{
		return this.m_max_hiding_limit;
	}

	public	int	getFix_nothit_value()
	{
		return this.m_fix_nothit_value;
	}

	public	float	getAtt_cor_para()
	{
		return this.m_att_cor_para;
	}

	public	float	getDef_cor_para()
	{
		return this.m_def_cor_para;
	}

	public	float	getGua_val()
	{
		return this.m_gua_val;
	}

	public	int[]	getPassive_skill_parameter()
	{
		return this.m_passive_skill_parameter;
	}

	public	int	getDamage_interrupt_life_perc()
	{
		return this.m_damage_interrupt_life_perc;
	}

	public	int[]	getGold_number_1()
	{
		return this.m_gold_number_1;
	}

	public	int[]	getGold_number_10()
	{
		return this.m_gold_number_10;
	}

	public	int[]	getDiamond_number_1()
	{
		return this.m_diamond_number_1;
	}

	public	int[]	getDiamond_number_10()
	{
		return this.m_diamond_number_10;
	}

	public	int[]	getDiamond_first()
	{
		return this.m_diamond_first;
	}

	public	float	getCombat_attack_factor()
	{
		return this.m_combat_attack_factor;
	}

	public	float	getCombat_defense_factor()
	{
		return this.m_combat_defense_factor;
	}

	public	float	getCombat_blood_factor()
	{
		return this.m_combat_blood_factor;
	}

	public	int	getGold_consumer_1()
	{
		return this.m_gold_consumer_1;
	}

	public	int	getGold_consumer_10()
	{
		return this.m_gold_consumer_10;
	}

	public	int	getDiamond_consumer_1()
	{
		return this.m_diamond_consumer_1;
	}

	public	int	getDiamond_consumer_10()
	{
		return this.m_diamond_consumer_10;
	}

	public	int	getItem_exp_1()
	{
		return this.m_item_exp_1;
	}

	public	int	getItem_exp_2()
	{
		return this.m_item_exp_2;
	}

	public	int	getItem_exp_3()
	{
		return this.m_item_exp_3;
	}

	public	int	getItem_exp_4()
	{
		return this.m_item_exp_4;
	}

	public	int	getItem_exp_5()
	{
		return this.m_item_exp_5;
	}

	public	int	getItem_exp_6()
	{
		return this.m_item_exp_6;
	}

	public	int[]	getMs_exp()
	{
		return this.m_ms_exp;
	}

	public	float	getHero_debris_factor()
	{
		return this.m_hero_debris_factor;
	}

	public	int[]	getDiamond_ten()
	{
		return this.m_diamond_ten;
	}

	public	int	getPassive_Active_Skills_A()
	{
		return this.m_Passive_Active_Skills_A;
	}

	public	int	getPassive_Active_Skills_B()
	{
		return this.m_Passive_Active_Skills_B;
	}

	public	int	getPassive_Active_Skills_C()
	{
		return this.m_Passive_Active_Skills_C;
	}

	public	int	getPassive_Active_Skills_D()
	{
		return this.m_Passive_Active_Skills_D;
	}

	public	int	getPassive_Active_Skills_P()
	{
		return this.m_Passive_Active_Skills_P;
	}

	public	int	getPassive_Active_Skills_Q()
	{
		return this.m_Passive_Active_Skills_Q;
	}

	public	int	getPassive_Active_Skills_X()
	{
		return this.m_Passive_Active_Skills_X;
	}

    public int getLotteryGoldFreeTimes()
    {
        return m_Gold_Free_Times;
    }

    public int getLotteryGoldExtractionCD()
    {
        return m_Gold_Extraction_CD;
    }

    public int getLotteryDiamondFreeTimes()
    {
        return m_Diamonds_Free_Times;
    }

    public int getLotteryRefreshRecruitTime()
    {
        return m_Refresh_Recruit_Time;
    }

	public override int GetID()
	{
		return this.m_id;
	}

	public void LoadBinary(string fileName, byte[] array)
	{
		MemoryStream nStream = new MemoryStream(array);
		BinaryReader nReader = new BinaryReader(nStream);
		try
		{
			parser(nReader);
		}
		catch (EndOfStreamException ex)
		{
			// 读到末尾
			Debug.LogError(ex.Message + fileName);
		}
		finally
		{
			nStream.Close();
			nReader.Close();
		}
	}
}