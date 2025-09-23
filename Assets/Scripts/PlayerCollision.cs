using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    private PlayerManager playerManager; 
    //private AudioManager audioManager;

    void Start()
    {
        playerManager = PlayerManager.Instance;
        //audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // --- Item Collision ---
        if (other.gameObject.layer == LayerMask.NameToLayer("Item"))
        {
            var item = other.GetComponent<Item>();
            if (item != null)
            {
                item.Activate();
                if (item.getCoinValue > 0)
                    playerManager.AddCoin(item.getCoinValue);
                else playerManager.AddHealth(item.getHealthValue);
            }
        }

        // --- Damage Collision ---
        if (other.gameObject.layer == LayerMask.NameToLayer("Damage"))
        {
            playerManager.TakeDamage(1);
            if (other.CompareTag("Pearl"))
            {
                var pearl = other.GetComponent<Pearl>();
                if (pearl != null) pearl.hitTarget();
                Destroy(other.gameObject);
            }
            else
            {
            }
            // TODO: play damage sound, spawn particle
        }

        if (other.CompareTag("Flag"))
        {
            int currentLevel = playerManager.lastNodeId;
            int highestUnlocked = playerManager.GetHighestUnlockedID();
            playerManager.AddHealth(1);

            if (currentLevel == highestUnlocked)
            {
                playerManager.UnlockNextFrom(currentLevel);
            }

            //playerManager.lastNodeId = playerManager.GetHighestUnlockedID();

            // Load Overworld
            EventManager.Trigger("PlayMusic", new SoundEventData(SoundType.BGM_MainMenu, 0.5f));

            StartCoroutine(LoadSceneDelayed(GameScene.Overworld.GetSceneName(), 0.1f));
            //SceneManager.LoadScene(GameScene.Overworld.GetSceneName());
        }

        if (other.CompareTag("DeathZone"))
        {            
            playerManager.TakeDamage(1);
            EventManager.Trigger("PlayMusic", new SoundEventData(SoundType.BGM_MainMenu, 0.5f));

            StartCoroutine(LoadSceneDelayed(GameScene.Overworld.GetSceneName(), 0.1f));
        }

        if (other.CompareTag("Finish"))
        {
            PlayerManager.Instance.SetWin();
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        // --- Attack Collision ---
        //if (other.CompareTag("Enemy") && playerAttack != null && playerAttack.IsAttacking)
        //{
        //    // Check facing direction
        //    bool facingRight = playerAttack.FacingRight;
        //    bool targetOnRight = other.transform.position.x > transform.position.x;

        //    if ((facingRight && targetOnRight) || (!facingRight && !targetOnRight))
        //    {
        //        var enemy = other.GetComponent<Enemy>();
        //        if (enemy != null) enemy.Reverse();
        //    }
        //}
    }

    //private IEnumerator LoadSceneDelayed(string sceneName, float delay)
    //{
    //    yield return new WaitForSeconds(delay);
    //    SceneManager.LoadScene(sceneName);
    //}

    private IEnumerator LoadSceneDelayed(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        EventManager.Trigger("PlayMusic", new SoundEventData(SoundType.BGM_MainMenu, 0.5f));
    }
}
