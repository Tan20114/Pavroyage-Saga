public enum DamageCategory
{
    Physical,
    Magic
}

public enum Element
{
    None,
    Fire,
    Ice,
    Lightning,
    Holy,
    Blood,
    DoT
}

public interface IDamageCalculator
{
    int Calculate(DamageContext context);
    int Calculate(DamageContext context, float skillMultiplier);

    bool CriticalCalcuations(CharacterDerivedStat attackerStat);
    float ResistanceCalcuations(CharacterDerivedStat attackerStat, CharacterDerivedStat defenderStat);
    float BlockCalculation(Character defender);    
}
