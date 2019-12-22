using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaReplayConsole
{
    public class Chat
    {
        public int time { get; set; }
        public string unit { get; set; }
        public string key { get; set; }
        public int slot { get; set; }
        public int player_slot { get; set; }
    }

    public class Cosmetics
    {
    }

    public class DraftTiming
    {
        public int order { get; set; }
        public bool pick { get; set; }
        public int active_team { get; set; }
        public int hero_id { get; set; }
        public int player_slot { get; set; }
        public int extra_time { get; set; }
        public int total_time_taken { get; set; }
    }

    public class Objectives
    {
    }

    public class PicksBans
    {
    }

    public class RadiantGoldAdv
    {
    }

    public class RadiantXpAdv
    {
    }

    public class Teamfights
    {
    }

    public class RadiantTeam
    {
    }

    public class DireTeam
    {
    }

    public class League
    {
    }

    public class AbilityUses
    {
    }

    public class AbilityTargets
    {
    }

    public class DamageTargets
    {
    }

    public class Actions
    {
    }

    public class AdditionalUnits
    {
    }

    public class BuybackLog
    {
        public int time { get; set; }
        public int slot { get; set; }
        public int player_slot { get; set; }
    }

    public class ConnectionLog
    {
        public int time { get; set; }
        public string @event { get; set; }
        public int player_slot { get; set; }
    }

    public class Damage
    {
    }

    public class DamageInflictor
    {
    }

    public class DamageInflictorReceived
    {
    }

    public class DamageTaken
    {
    }

    public class GoldReasons
    {
    }

    public class HeroHits
    {
    }

    public class ItemUses
    {
    }

    public class KillStreaks
    {
    }

    public class Killed
    {
    }

    public class KilledBy
    {
    }

    public class KillsLog
    {
        public int time { get; set; }
        public string key { get; set; }
    }

    public class LanePos
    {
    }

    public class LifeState
    {
    }

    public class MaxHeroHit
    {
    }

    public class MultiKills
    {
    }

    public class Obs
    {
    }

    public class ObsLeftLog
    {
    }

    public class ObsLog
    {
    }

    public class PermanentBuff
    {
    }

    public class Purchase
    {
    }

    public class PurchaseLog
    {
        public int time { get; set; }
        public string key { get; set; }
    }

    public class Runes
    {
        public int property1 { get; set; }
        public int property2 { get; set; }
    }

    public class RunesLog
    {
        public int time { get; set; }
        public int key { get; set; }
    }

    public class Sen
    {
    }

    public class SenLeftLog
    {
    }

    public class SenLog
    {
    }

    public class XpReasons
    {
    }

    public class PurchaseTime
    {
    }

    public class FirstPurchaseTime
    {
    }

    public class ItemWin
    {
    }

    public class ItemUsage
    {
    }

    public class PurchaseTpscroll
    {
    }

    public class Benchmarks
    {
    }

    public class Player
    {
        public int match_id { get; set; }
        public int player_slot { get; set; }
        public List<int> ability_upgrades_arr { get; set; }
        public AbilityUses ability_uses { get; set; }
        public AbilityTargets ability_targets { get; set; }
        public DamageTargets damage_targets { get; set; }
        public int account_id { get; set; }
        public Actions actions { get; set; }
        public AdditionalUnits additional_units { get; set; }
        public int assists { get; set; }
        public int backpack_0 { get; set; }
        public int backpack_1 { get; set; }
        public int backpack_2 { get; set; }
        public List<BuybackLog> buyback_log { get; set; }
        public int camps_stacked { get; set; }
        public List<ConnectionLog> connection_log { get; set; }
        public int creeps_stacked { get; set; }
        public Damage damage { get; set; }
        public DamageInflictor damage_inflictor { get; set; }
        public DamageInflictorReceived damage_inflictor_received { get; set; }
        public DamageTaken damage_taken { get; set; }
        public int deaths { get; set; }
        public int denies { get; set; }
        public List<int> dn_t { get; set; }
        public int gold { get; set; }
        public int gold_per_min { get; set; }
        public GoldReasons gold_reasons { get; set; }
        public int gold_spent { get; set; }
        public List<int> gold_t { get; set; }
        public int hero_damage { get; set; }
        public int hero_healing { get; set; }
        public HeroHits hero_hits { get; set; }
        public int hero_id { get; set; }
        public int item_0 { get; set; }
        public int item_1 { get; set; }
        public int item_2 { get; set; }
        public int item_3 { get; set; }
        public int item_4 { get; set; }
        public int item_5 { get; set; }
        public ItemUses item_uses { get; set; }
        public KillStreaks kill_streaks { get; set; }
        public Killed killed { get; set; }
        public KilledBy killed_by { get; set; }
        public int kills { get; set; }
        public List<KillsLog> kills_log { get; set; }
        public LanePos lane_pos { get; set; }
        public int last_hits { get; set; }
        public int leaver_status { get; set; }
        public int level { get; set; }
        public List<int> lh_t { get; set; }
        public LifeState life_state { get; set; }
        public MaxHeroHit max_hero_hit { get; set; }
        public MultiKills multi_kills { get; set; }
        public Obs obs { get; set; }
        public List<ObsLeftLog> obs_left_log { get; set; }
        public List<ObsLog> obs_log { get; set; }
        public int obs_placed { get; set; }
        public int party_id { get; set; }
        public List<PermanentBuff> permanent_buffs { get; set; }
        public int pings { get; set; }
        public Purchase purchase { get; set; }
        public List<PurchaseLog> purchase_log { get; set; }
        public int rune_pickups { get; set; }
        public Runes runes { get; set; }
        public List<RunesLog> runes_log { get; set; }
        public Sen sen { get; set; }
        public List<SenLeftLog> sen_left_log { get; set; }
        public List<SenLog> sen_log { get; set; }
        public int sen_placed { get; set; }
        public int stuns { get; set; }
        public List<int> times { get; set; }
        public int tower_damage { get; set; }
        public int xp_per_min { get; set; }
        public XpReasons xp_reasons { get; set; }
        public List<int> xp_t { get; set; }
        public string personaname { get; set; }
        public string name { get; set; }
        public object last_login { get; set; }
        public bool radiant_win { get; set; }
        public int start_time { get; set; }
        public int duration { get; set; }
        public int cluster { get; set; }
        public int lobby_type { get; set; }
        public int game_mode { get; set; }
        public int patch { get; set; }
        public int region { get; set; }
        public bool isRadiant { get; set; }
        public int win { get; set; }
        public int lose { get; set; }
        public int total_gold { get; set; }
        public int total_xp { get; set; }
        public int kills_per_min { get; set; }
        public int kda { get; set; }
        public int abandons { get; set; }
        public int neutral_kills { get; set; }
        public int tower_kills { get; set; }
        public int courier_kills { get; set; }
        public int lane_kills { get; set; }
        public int hero_kills { get; set; }
        public int observer_kills { get; set; }
        public int sentry_kills { get; set; }
        public int roshan_kills { get; set; }
        public int necronomicon_kills { get; set; }
        public int ancient_kills { get; set; }
        public int buyback_count { get; set; }
        public int observer_uses { get; set; }
        public int sentry_uses { get; set; }
        public int lane_efficiency { get; set; }
        public int lane_efficiency_pct { get; set; }
        public int lane { get; set; }
        public int lane_role { get; set; }
        public bool is_roaming { get; set; }
        public PurchaseTime purchase_time { get; set; }
        public FirstPurchaseTime first_purchase_time { get; set; }
        public ItemWin item_win { get; set; }
        public ItemUsage item_usage { get; set; }
        public PurchaseTpscroll purchase_tpscroll { get; set; }
        public int actions_per_min { get; set; }
        public int life_state_dead { get; set; }
        public int rank_tier { get; set; }
        public List<int> cosmetics { get; set; }
        public Benchmarks benchmarks { get; set; }
    }

    public class AllWordCounts
    {
    }

    public class MyWordCounts
    {
    }

    public class MatchDetail
    {
        public long match_id { get; set; }
        public int barracks_status_dire { get; set; }
        public int barracks_status_radiant { get; set; }
        public List<Chat> chat { get; set; }
        public int cluster { get; set; }
        public Cosmetics cosmetics { get; set; }
        public int dire_score { get; set; }
        public List<DraftTiming> draft_timings { get; set; }
        public int duration { get; set; }
        public int engine { get; set; }
        public int first_blood_time { get; set; }
        public int game_mode { get; set; }
        public int human_players { get; set; }
        public int leagueid { get; set; }
        public int lobby_type { get; set; }
        public int match_seq_num { get; set; }
        public int negative_votes { get; set; }
        public Objectives objectives { get; set; }
        public PicksBans picks_bans { get; set; }
        public int positive_votes { get; set; }
        public RadiantGoldAdv radiant_gold_adv { get; set; }
        public int radiant_score { get; set; }
        public bool radiant_win { get; set; }
        public RadiantXpAdv radiant_xp_adv { get; set; }
        public int start_time { get; set; }
        public Teamfights teamfights { get; set; }
        public int tower_status_dire { get; set; }
        public int tower_status_radiant { get; set; }
        public int version { get; set; }
        public int replay_salt { get; set; }
        public int series_id { get; set; }
        public int series_type { get; set; }
        public RadiantTeam radiant_team { get; set; }
        public DireTeam dire_team { get; set; }
        public League league { get; set; }
        public int skill { get; set; }
        public List<Player> players { get; set; }
        public int patch { get; set; }
        public int region { get; set; }
        public AllWordCounts all_word_counts { get; set; }
        public MyWordCounts my_word_counts { get; set; }
        public int @throw { get; set; }
        public int comeback { get; set; }
        public int loss { get; set; }
        public int win { get; set; }
        public string replay_url { get; set; }
    }
}
