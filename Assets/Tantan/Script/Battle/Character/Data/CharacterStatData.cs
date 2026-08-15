using UnityEngine;

[CreateAssetMenu(fileName = "CharacterStat", menuName = "Character/Character Base Stat")]
public class CharacterStatData : ScriptableObject
{
    public BaseStats stats = new BaseStats();
}