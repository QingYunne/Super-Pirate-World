using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    private void Awake()
    {
    }

    private void Start()
    {
        EventManager.Trigger("PlayMusic", new SoundEventData(SoundType.BGM_MainMenu, 0.5f, loop: true, startTime: 2));
    }
    public void PlayGame()
    {
        EventManager.Trigger("PlaySFX", new SoundEventData(SoundType.SFX_ClickButton, 1f, duckMusic: true, duckVolume:0.3f, duckDuration:0.5f));
        SceneManager.LoadScene(GameScene.Overworld.GetSceneName()); // Load scene game
    }

    public void ExitGame()
    {
        EventManager.Trigger("PlaySFX", new SoundEventData(SoundType.SFX_ClickButton, 1f, duckMusic: true, duckVolume: 0.3f, duckDuration: 0.5f));
        Application.Quit(); 
    }
}
