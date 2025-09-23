using UnityEngine;

public class ChestController : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        EventManager.Subscribe("PlayerWin", OnPlayerWin);
    }

    private void OnDisable()
    {
        EventManager.Unsubscribe("PlayerWin", OnPlayerWin);
    }

    private void OnPlayerWin(object data = null)
    {
        EventManager.Trigger("PlaySFX", new SoundEventData(SoundType.SFX_Victory, 1f, duckMusic: true, duckVolume: 0.3f, duckDuration: 0.5f));
        animator.SetTrigger("Open"); 
        EventManager.Trigger("StopMusic");
        Invoke(nameof(TriggerWinUI), 2f); 
    }

    private void TriggerWinUI()
    {
        EventManager.Trigger("ShowWinUI");
    }
}
