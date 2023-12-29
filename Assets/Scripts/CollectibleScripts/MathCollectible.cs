using Unity.VisualScripting;
using UnityEngine;

public class MathCollectible : Collectible
{
    public enum CollectibleType
    {
        TimesTwo = 0,
        TimesThree = 1,
        DivideTwo = 2,
    }

    public static float BonusToApply(CollectibleType type, float statToChange)
    {
        switch(type)
        {
            case CollectibleType.TimesTwo:
                return statToChange * 2;
            case CollectibleType.TimesThree:
                return statToChange * 3;
            case CollectibleType.DivideTwo:
                return statToChange / 2;
            default:
                Debug.LogWarning($"Something went wrong and a {(int)type} was recieved in MathCollectible!");
                return statToChange;
        }
    }

    private int GenerateRandomNumber()
    {
        return Random.Range(0, 3);
    }
    protected override void ApplyStatusEffectOnEnemy(GameObject enemy)
    {
        EntityStatsScript ess = enemy.GetComponent<EntityStatsScript>();
        CollectibleType type = (CollectibleType)GenerateRandomNumber();
        ess.IncreaseCurrentAttackDamageTemporarily(
            MathCollectible.BonusToApply(type, ess.GetCurrentAttackDamage()),
            statusEffectDuration
            );
    }

    protected override void ApplyStatusEffectOnPlayer(GameObject player)
    {
        EntityStatsScript ess = player.GetComponent<EntityStatsScript>();
        CollectibleType type = (CollectibleType)GenerateRandomNumber();
        ess.IncreaseCurrentAttackDamageTemporarily(
            MathCollectible.BonusToApply(type, ess.GetCurrentAttackDamage()),
            statusEffectDuration
            );
    }
}
