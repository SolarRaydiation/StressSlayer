using Unity.VisualScripting;
using UnityEngine;

public class MathCollectible : Collectible
{
    public Sprite[] sprites;
    private int randint;
    private CollectibleType type;

    #region Enums and Static Methods
    public enum CollectibleType
    {
        TimesTwo = 0,
        TimesThree = 1,
        DivideTwo = 2,
    }

    private static float BonusToApply(CollectibleType type, float statToChange)
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
                Debug.LogWarning($"Something went wrong and a {(int)type} was recieved in MathCollectible! Defaulting to double!");
                return statToChange * 2;
        }
    }

    #endregion

    #region Methods
    protected override void ExecuteOtherStartFunctions()
    {
        int randint = GenerateRandomNumber();
        SetString((CollectibleType)randint);
        CollectibleType type = (CollectibleType)randint;

        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();

        switch (type)
        {
            case CollectibleType.TimesTwo:
                sr.sprite = sprites[0];
                break;
            case CollectibleType.TimesThree:
                sr.sprite = sprites[1];
                break;
            case CollectibleType.DivideTwo:
                sr.sprite = sprites[2];
                break;
            default:
                sr.sprite = sprites[0];
                Debug.LogWarning($"Something went wrong and a {(int)type} was recieved in MathCollectible! Defaulting to double");
                break;
                
        }
    }

    public void SetString(CollectibleType type)
    {
        switch (type)
        {
            case CollectibleType.TimesTwo:
                floatingText = "Stats temporarily doubled!";
                break;
            case CollectibleType.TimesThree:
                floatingText = "Stats temporarily tripled!";
                break;
            case CollectibleType.DivideTwo:
                floatingText = "Stats temporarily halved!";
                break;
            default:
                Debug.LogWarning($"Something went wrong and a {(int)type} was recieved in MathCollectible!");
                floatingText = "Collectible collected!";
                break;
        }
    }

    private int GenerateRandomNumber()
    {
        return Random.Range(0, 3);
    }

    protected override void ApplyStatusEffectOnEnemy(GameObject enemy)
    {
        EntityStatsScript ess = enemy.GetComponent<EntityStatsScript>();
        

        ess.IncreaseCurrentAttackDamageTemporarily(
            MathCollectible.BonusToApply(type, ess.GetCurrentAttackDamage()),
            statusEffectDuration
            );
    }

    protected override void ApplyStatusEffectOnPlayer(GameObject player)
    {
        EntityStatsScript ess = player.GetComponent<EntityStatsScript>();

        ess.IncreaseCurrentAttackDamageTemporarily(
            MathCollectible.BonusToApply(type, ess.GetCurrentAttackDamage()),
            statusEffectDuration
            );
    }

    #endregion
}
