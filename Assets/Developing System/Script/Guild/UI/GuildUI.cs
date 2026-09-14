using UnityEngine;

public class GuildUI : MonoBehaviour
{
    [SerializeField] GameObject[] leftRightTab;
    [SerializeField] GameObject backTab;

    void LateUpdate()
    {
        if (GuildManager.Instance.guildState == GuildState.GuildHub)
        {
            foreach(GameObject item in leftRightTab)
            {
                item.SetActive(true);
            }

            backTab.SetActive(false);
        }
        else
        {
            foreach (GameObject item in leftRightTab)
            {
                item.SetActive(false);
            }

            backTab.SetActive(true);
        }
    }
}
