using UnityEngine;

public class GuildUI : MonoBehaviour
{
    [SerializeField] GameObject[] leftRightTab;
    [SerializeField] GameObject backTab;
    [SerializeField] GameObject inventoryTab;

    void LateUpdate()
    {
        if (GuildManager.Instance.guildState == GuildState.Room || GuildManager.Instance.guildState == GuildState.MainMenu)
        {
            foreach(GameObject item in leftRightTab)
            {
                item.SetActive(false);
            }

            backTab.SetActive(false);
        }
        else if (GuildManager.Instance.guildState == GuildState.GuildHub)
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

    public void OpenInventoryTab()
    {
        inventoryTab.SetActive(true);
    }

    public void CloseInventoryTab()
    {
        inventoryTab.SetActive(false);
    }
}
