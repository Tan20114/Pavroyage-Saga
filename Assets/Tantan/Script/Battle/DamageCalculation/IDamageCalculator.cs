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

    bool CriticalCalcuations(CharacterDerivedStat attackerStat);
    float ResistanceCalcuations(CharacterDerivedStat attackerStat, CharacterDerivedStat defenderStat);
    float BlockCalculation(Character defender);    
}
