using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UILevelController : MonoBehaviour
{
    public GameObject levelPanel; 
    public TMP_Text levelText;

    private void OnEnable()
    {
        EventManager.Subscribe("NodeChanged", OnNodeChanged);
    }

    private void OnDisable()
    {
        EventManager.Unsubscribe("NodeChanged", OnNodeChanged);
    }

    private void OnNodeChanged(object value)
    {
        int nodeId = (int)value;

        if (levelPanel != null)
            levelPanel.SetActive(true);

        if (levelText != null)
            levelText.text = "LEVEL " + nodeId;

    }

}
