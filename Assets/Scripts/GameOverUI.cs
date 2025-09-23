using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    public GameObject panel;
    public Button playButton;
    public Button exitButton;

    private static GameOverUI instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        HideGameOver();
    }
    private void OnEnable()
    {
        EventManager.Subscribe("PlayerDied", OnPlayerDied);
    }

    private void OnDisable()
    {
        EventManager.Unsubscribe("PlayerDied", OnPlayerDied);
    }

    //private void Start()
    //{
    //    if (playButton != null)
    //        playButton.onClick.AddListener(OnPlayAgain);

    //    if (exitButton != null)
    //        exitButton.onClick.AddListener(OnExit);
    //}

    private void OnPlayerDied(object data = null)
    {
        ShowGameOver();
    }

    public void ShowGameOver()
    {
        panel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void HideGameOver()
    {
        panel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void OnPlayAgain()
    {
        Debug.Log("Play Again");
        PlayerManager.Instance.ResetStats();
        Time.timeScale = 1f;

        EventManager.Trigger("PlaySFX", new SoundEventData(SoundType.SFX_ClickButton, 1f, duckMusic: true, duckVolume: 0.3f, duckDuration: 0.5f));
        EventManager.Trigger("PlayMusic", new SoundEventData(SoundType.BGM_MainMenu, 0.5f));

        StartCoroutine(LoadSceneDelayed(GameScene.Overworld.GetSceneName(), 0.2f));
        HideGameOver();
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
