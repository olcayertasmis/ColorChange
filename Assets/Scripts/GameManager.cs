using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject settingsPanel;

    public void SettingsButton()
    {
        settingsPanel.SetActive(!settingsPanel.activeInHierarchy);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}