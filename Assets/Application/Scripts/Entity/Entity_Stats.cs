using UnityEngine;

public class Entity_Stats : MonoBehaviour
{
    public Stat maxHealth ;

    public Stat_MajorGroup major;

    public Stat_OffensiveGroup offence;
    public Stat_DefenceGroup defence;

    public float GetMaxHealth()
    {
        float baseHp = maxHealth.GetValue();
        float bonusHP = major.vitality.GetValue() * 5;
        return baseHp + bonusHP;
    }
}
