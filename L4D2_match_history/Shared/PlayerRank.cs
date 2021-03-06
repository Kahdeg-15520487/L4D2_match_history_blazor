using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace L4D2_match_history.Shared
{
    public class PlayerRank
    {
        public string steam_id { get; set; }
        public string steam_id64 { get; set; }
        public string play_style { get; set; }
        public string last_known_alias { get; set; }
        public string last_known_alias_unicode { get; set; }
        public DateTime last_join_date { get; set; }
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
        public int car_alarm { get; set; }
        public DateTime create_date { get; set; }

        public PlayerRank() { }

        public PlayerRank(string steam_id, string last_known_alias, DateTime last_join_date, int survivor_healed, int survivor_defibed, int survivor_death, int survivor_incapped, int survivor_ff, int weapon_rifle, int weapon_shotgun, int weapon_melee, int weapon_deagle, int weapon_special, int weapon_smg, int weapon_sniper, int infected_killed, int infected_headshot, int boomer_killed, int boomer_killed_clean, int charger_killed, int charger_pummeled, int hunter_killed, int hunter_pounced, int hunter_shoved, int jockey_killed, int jockey_pounced, int jockey_shoved, int jockey_rided, int smoker_killed, int smoker_choked, int smoker_tongue_slashed, int spitter_killed, int witch_killed, int witch_killed_1shot, int witch_harassed, int tank_killed, int tank_melee, DateTime create_date)
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
            this.create_date = create_date;
        }

        public string GetSteamId64()
        {
            var parts = steam_id.Split(":");
            long converted = long.Parse(parts[2]) * 2;
            converted += 76561197960265728;
            converted += int.Parse(parts[1]);
            return converted.ToString();
        }

        public string GetPlayStyle()
        {

            int special = this.weapon_special;
            int melee = this.weapon_melee;
            int deagle = this.weapon_deagle;
            int rifle = this.weapon_rifle;
            int shotgun = this.weapon_shotgun;
            int smg = this.weapon_smg;
            int sniper = this.weapon_sniper;

            string playStyle = "Balancer";

            if (special > melee && special > deagle && special > rifle && special > shotgun && special > smg && special > sniper)
            {
                playStyle = "Specialist";
            }
            else if (melee > special && melee > deagle && melee > rifle && melee > shotgun && melee > smg && melee > sniper)
            {
                playStyle = "Brawler";
            }
            else if (deagle > special && deagle > melee && deagle > rifle && deagle > shotgun && deagle > smg && deagle > sniper)
            {
                playStyle = "Cowboy";
            }
            else if (rifle > special && rifle > melee && rifle > deagle && rifle > shotgun && rifle > smg && rifle > sniper)
            {
                playStyle = "Rifler";
            }
            else if (shotgun > special && shotgun > melee && shotgun > deagle && shotgun > rifle && shotgun > smg && shotgun > sniper)
            {
                playStyle = "Supporter";
            }
            else if (smg > special && smg > melee && smg > deagle && smg > rifle && smg > shotgun && smg > sniper)
            {
                playStyle = "Run'n'Gun";
            }
            else if (sniper > special && sniper > melee && sniper > deagle && sniper > rifle && sniper > shotgun && sniper > smg)
            {
                playStyle = "Markman";
            }

            return $"<div title=\"{PlayStyleNoteVN[playStyle]}\">{playStyle}<div/>";
        }

        private static Dictionary<string, string> PlayStyleNoteVN = new Dictionary<string, string>
                    {
                        {"Balancer", "Không chuyên dùng vũ khí nào." },
                        {"Specialist", "Chuyên dùng M60 hoặc M79." },
                        {"Brawler", "Thích chém giết." },
                        {"Cowboy", "Chỉ cần Deagle là đủ." },
                        {"Rifler", "Quân nhân thực thụ." },
                        {"Supporter", "Chuyên dùng shotgun." },
                        {"Run'n'Gun", "Chạy vòng vòng với SMG." },
                        {"Markman", "Người săn đầu." },
                    };
    }
}
