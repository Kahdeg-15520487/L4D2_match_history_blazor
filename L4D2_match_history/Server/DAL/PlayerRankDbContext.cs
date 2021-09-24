using L4D2_match_history.Shared;

using Microsoft.EntityFrameworkCore;

namespace L4D2_match_history.Server.DAL
{
    public class PlayerRankDbContext : DbContext
    {
        public DbSet<PlayerRank> PlayerRanks { get; set; }

        public DbSet<PlayerSkillModifier> PlayerSkillModifiers { get; set; }

        public PlayerRankDbContext(DbContextOptions<PlayerRankDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //modelBuilder.Entity<PlayerRank>().ToTable("STATS_PLAYERS");
            modelBuilder.Entity<PlayerRank>(pr =>
            {
                pr.HasNoKey();
                pr.ToView("STATS_VW_PLAYER_RANKS");
            });
            modelBuilder.Entity<PlayerSkillModifier>().ToTable("STATS_SKILLS");

            modelBuilder.Entity<PlayerSkillModifier>().HasKey(psm => psm.name);
        }
    }
}
