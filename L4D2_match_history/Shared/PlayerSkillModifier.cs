using System;

namespace L4D2_match_history.Shared
{
    public class PlayerSkillModifier
    {
        public string name { get; set; }
        public float modifier { get; set; }
        public DateTime update_date { get; set; }

        public PlayerSkillModifier(string name, float modifier, DateTime update_date)
        {
            this.name = name;
            this.modifier = modifier;
            this.update_date = update_date;
        }
    }
}
