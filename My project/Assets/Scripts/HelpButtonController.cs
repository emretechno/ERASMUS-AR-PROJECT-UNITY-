using UnityEngine;
using UnityEngine.UI;

public class HelpButtonController : MonoBehaviour
{
    public GameObject helpPanel; // Yardım paneline referans
    private bool isHelpPanelActive = false;

    public void ToggleHelpPanel()
    {
        isHelpPanelActive = !isHelpPanelActive;
        helpPanel.SetActive(isHelpPanelActive);
    }
}
