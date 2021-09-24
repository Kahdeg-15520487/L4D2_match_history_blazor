using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace L4D2_match_history.Shared
{
    public class PlayerRankView
    {
        public string steam_id { get; set; }
        public string steam_id64 { get; set; }
        public string play_style { get; set; }
        public string last_known_alias { get; set; }
        public string last_known_alias_unicode { get; set; }
        public string last_join_date { get; set; }
        public int survivor_healed { get; set; }
        public int survivor_defibed { get; set; }
        public int survivor_death { get; set; }
        public int survivor_incapped { get; set; }
        public int survivor_ff { get; set; }
        public int weapon_rifle { get; set; }
        public int weapon_shotgun { get; set; }
        public int weapon_melee { get; set; }
        public int weapon_deagle { get; set; }
        public int weapon_special { get; set; }
        public int weapon_smg { get; set; }
        public int weapon_sniper { get; set; }
        public int infected_killed { get; set; }
        public int infected_headshot { get; set; }
        public int boomer_killed { get; set; }
        public int boomer_killed_clean { get; set; }
        public int charger_killed { get; set; }
        public int charger_pummeled { get; set; }
        public int hunter_killed { get; set; }
        public int hunter_pounced { get; set; }
        public int hunter_shoved { get; set; }
        public int jockey_killed { get; set; }
        public int jockey_pounced { get; set; }
        public int jockey_shoved { get; set; }
        public int jockey_rided { get; set; }
        public int smoker_killed { get; set; }
        public int smoker_choked { get; set; }
        public int smoker_tongue_slashed { get; set; }
        public int spitter_killed { get; set; }
        public int witch_killed { get; set; }
        public int witch_killed_1shot { get; set; }
        public int witch_harassed { get; set; }
        public int tank_killed { get; set; }
        public int tank_melee { get; set; }
        public double total_points { get; set; }
        public long rank_num { get; set; }
        public string create_date { get; set; }

        public PlayerRankView() { }

        public PlayerRankView(string steam_id, string last_known_alias, string last_join_date, int survivor_healed, int survivor_defibed, int survivor_death, int survivor_incapped, int survivor_ff, int weapon_rifle, int weapon_shotgun, int weapon_melee, int weapon_deagle, int weapon_special, int weapon_smg, int weapon_sniper, int infected_killed, int infected_headshot, int boomer_killed, int boomer_killed_clean, int charger_killed, int charger_pummeled, int hunter_killed, int hunter_pounced, int hunter_shoved, int jockey_killed, int jockey_pounced, int jockey_shoved, int jockey_rided, int smoker_killed, int smoker_choked, int smoker_tongue_slashed, int spitter_killed, int witch_killed, int witch_killed_1shot, int witch_harassed, int tank_killed, int tank_melee, double total_points, long rank_num, string create_date)
        {
            this.steam_id = steam_id;
            this.last_known_alias = last_known_alias;
            this.last_join_date = last_join_date;
            this.survivor_healed = survivor_healed;
            this.survivor_defibed = survivor_defibed;
            this.survivor_death = survivor_death;
            this.survivor_incapped = survivor_incapped;
            this.survivor_ff = survivor_ff;
            this.weapon_rifle = weapon_rifle;
            this.weapon_shotgun = weapon_shotgun;
            this.weapon_melee = weapon_melee;
            this.weapon_deagle = weapon_deagle;
            this.weapon_special = weapon_special;
            this.weapon_smg = weapon_smg;
            this.weapon_sniper = weapon_sniper;
            this.infected_killed = infected_killed;
            this.infected_headshot = infected_headshot;
            this.boomer_killed = boomer_killed;
            this.boomer_killed_clean = boomer_killed_clean;
            this.charger_killed = charger_killed;
            this.charger_pummeled = charger_pummeled;
            this.hunter_killed = hunter_killed;
            this.hunter_pounced = hunter_pounced;
            this.hunter_shoved = hunter_shoved;
            this.jockey_killed = jockey_killed;
            this.jockey_pounced = jockey_pounced;
            this.jockey_shoved = jockey_shoved;
            this.jockey_rided = jockey_rided;
            this.smoker_killed = smoker_killed;
            this.smoker_choked = smoker_choked;
            this.smoker_tongue_slashed = smoker_tongue_slashed;
            this.spitter_killed = spitter_killed;
            this.witch_killed = witch_killed;
            this.witch_killed_1shot = witch_killed_1shot;
            this.witch_harassed = witch_harassed;
            this.tank_killed = tank_killed;
            this.tank_melee = tank_melee;
            this.total_points = total_points;
            this.rank_num = rank_num;
            this.create_date = create_date;
        }
    }
}
