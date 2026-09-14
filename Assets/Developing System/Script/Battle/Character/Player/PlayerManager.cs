using AYellowpaper.SerializedCollections;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializedDictionary("Player id", "Player Data")]
    [SerializeField] SerializedDictionary<int, PlayerData> playerData;

    public PlayerData GetPlayerData(int playerId)
    {
        if (playerData.TryGetValue(playerId, out PlayerData data))
        {
            return data;
        }
        else
        {
            Debug.LogWarning($"Player data not found for playerId: {playerId}");
            return null;
        }
    }
}