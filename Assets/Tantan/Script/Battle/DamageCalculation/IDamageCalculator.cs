public enum DamageCategory
{
    Physical,
    Magic
}

public enum Element
{
    none,
    Fire,
    Ice,
    Lightning,
    Holy,
    Blood
}

public interface IDamageCalculator
{
    float Calculate(DamageContext context);
}
