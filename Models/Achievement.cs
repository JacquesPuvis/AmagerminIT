using System.ComponentModel.DataAnnotations;

namespace AmagerminIT.Models;

public class Achievement
{
    [Key]
    public int AchievementId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ICollection<UserAchievement> UserAchievements { get; set; } = new List<UserAchievement>();

}