using UnityEngine;

public class Entity_Stats : MonoBehaviour
{
    public Stat maxHealth ;

    public Stat_MajorGroup major;

    public Stat_OffensiveGroup offence;
    public Stat_DefenceGroup defence;



    public float GetPhyscicalDamage(out bool isCrit)
    {
        float baseDamage = offence.damage.GetValue();
        float bonousDamage = major.strength.GetValue();

        float totalBaseDamage = baseDamage + bonousDamage;

        float baseCritChance = offence.critChance.GetValue();
        float bonousCritChance = major.agility.GetValue() * 3; // bonous Crit chance from agility  : +0.3% per Agi

        float critChance = baseCritChance + bonousCritChance;

        float baseCritPower = offence.critPower.GetValue();
        float bonousCritPower = major.strength.GetValue() * 5f; // bonous Crit chance from agility  : +0.5% per STR

        float critPower = (baseCritPower + bonousCritPower) / 100; //total crit power as multiplayer (e.g 150/100=1.5f- multiplayer )


         isCrit = Random.Range(0, 100) < critChance;
        float finalDamage = isCrit ? totalBaseDamage * critPower: totalBaseDamage;
        return finalDamage;
    }


    public float GetArmorMitigation()
    {
        float baseArmor = defence.armor.GetValue();
        float bonusAromor = major.vitality.GetValue(); //Bonous armor from vitality : +1 per VIT
        float totalArmor = baseArmor + bonusAromor;

        float mitigation = totalArmor / (totalArmor + 100);
        float mitigationCap = .85f;//max mitigation will be capped at 85%
        float finalMitigation = Mathf.Clamp(mitigation,0,mitigationCap);

        return finalMitigation; 
    }
    public float GetEvation()
    {
        float baseEvation = defence.evasion.GetValue();
        float bonusEvation = major.agility.GetValue() * 0.5f; // Each agility point guves 0.5 % evation

        float totalEvation = baseEvation + bonusEvation;
        float evationCap = 85f;//evation will be capped at 85%

        float finalEvation = Mathf.Clamp(totalEvation,0,evationCap);
        return finalEvation;
    }
    public float GetMaxHealth()
    {
        float baseMaxHealth = maxHealth.GetValue();
        float bonusMaxHealth = major.vitality.GetValue() * 5;

        float finalMaxHealth = baseMaxHealth + bonusMaxHealth;
        return finalMaxHealth;
    }
}
