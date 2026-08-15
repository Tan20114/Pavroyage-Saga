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
        DrawInfo("Physical Damage", cds.derivedStats.physicalDamage.ToString());
        DrawInfo("Magic Damage", cds.derivedStats.magicDamage.ToString());
        DrawInfo("Critical Chance", cds.derivedStats.criticalChance.ToString());
        DrawInfo("Critical Damage", cds.derivedStats.criticalDamage.ToString());
        DrawInfo("Speed", cds.derivedStats.speed.ToString());
        DrawHeader("Defensive Stat");
        DrawInfo("Max HP", cds.derivedStats.maxHP.ToString());
        DrawInfo("Physical Resistance", cds.derivedStats.physicalResistance.ToString());
        DrawInfo("Magic Resistance", cds.derivedStats.magicResistance.ToString());
        DrawInfo("Dodge Chance", cds.derivedStats.dodgeChance.ToString());
        DrawInfo("Debuff Resistance", cds.derivedStats.debuffResistance.ToString());
        if(cds.gameObject.GetComponent<Player>() != null)
        {
            DrawInfo("Block Damage Reduction", cds.derivedStats.blockDamageReduction.ToString());
            DrawInfo("Aggro Reduction", cds.derivedStats.aggroRateReduction.ToString());
            DrawHeader("Other Stat");
            DrawInfo("Discovery Rate", cds.derivedStats.discoveryRate.ToString());
        }
    }
}
