using UnityEditor;
using UnityEngine.UIElements;

[CustomEditor(typeof(CharacterDerivedStat))]
public class DerivedStatInspectorGUI : BaseInspector
{
    CharacterDerivedStat cds;

    private void OnEnable()
    {
        cds = (CharacterDerivedStat)target;
    }

    public override void OnInspectorGUI()
    {
        DrawHeader("Offensive Stat");
        DrawInfo("Physical Damage", cds.GetDerived(DerivedType.PhysicalDamage).ToString());
        DrawInfo("Magic Damage", cds.GetDerived(DerivedType.MagicDamage).ToString());
        DrawInfo("Armour Pierce", cds.GetDerived(DerivedType.ArmorPierce).ToString());
        DrawInfo("Critical Chance", cds.GetDerived(DerivedType.CriticalChance).ToString());
        DrawInfo("Critical Damage", cds.GetDerived(DerivedType.CriticalDamage).ToString());
        DrawInfo("Speed", cds.GetDerived(DerivedType.Speed).ToString());

        DrawHeader("Defensive Stat");
        DrawInfo("Max HP", cds.GetDerived(DerivedType.MaxHp).ToString());
        DrawInfo("Physical Resistance", cds.GetDerived(DerivedType.PhysicalResistance).ToString());
        DrawInfo("Magic Resistance", cds.GetDerived(DerivedType.MagicResistance).ToString());
        DrawInfo("Dodge Chance", cds.GetDerived(DerivedType.DodgeChance).ToString());
        DrawInfo("Debuff Resistance", cds.GetDerived(DerivedType.DebuffResistance).ToString());

        if (cds.GetComponent<Player>() != null)
        {
            DrawInfo("Block Damage Reduction", cds.GetDerived(DerivedType.BlockDamageReduction).ToString());
            DrawInfo("Aggro Reduction", cds.GetDerived(DerivedType.AggroRateReduction).ToString());

            DrawHeader("Other Stat");
            DrawInfo("Discovery Rate", cds.GetDerived(DerivedType.DiscoveryRate).ToString());
        }
    }
}
