using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinUI : MonoBehaviour
{
    public GameObject panel;
    public Button continueButton;

    private void Awake()
    {
        panel.SetActive(false);
    }

    private void OnEnable()
    {
        EventManager.Subscribe("ShowWinUI", OnShowWin);
    }

    private void OnDisable()
    {
        EventManager.Unsubscribe("ShowWinUI", OnShowWin);
    }

    private void OnShowWin(object data = null)
    {
        panel.SetActive(true);
        Time.timeScale = 0f; // d?ng m?i th?
    }

    public void OnContinue()
    {
        Time.timeScale = 1f;
        EventManager.Trigger("PlaySFX", new SoundEventData(SoundType.SFX_ClickButton, 1f, duckMusic: true, duckVolume: 0.3f, duckDuration: 0.5f));
        EventManager.Trigger("PlayMusic", new SoundEventData(SoundType.BGM_MainMenu, 0.5f));

        StartCoroutine(LoadSceneDelayed(GameScene.Overworld.GetSceneName(), 0.2f));
    }

    public void OnExit()
    {
        Debug.Log("Quit Game");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    private IEnumerator LoadSceneDelayed(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        EventManager.Trigger("PlayMusic", new SoundEventData(SoundType.BGM_Level, 0.5f));
    }
}
