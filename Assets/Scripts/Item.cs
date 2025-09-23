using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private int coinValue = 0;
    [SerializeField] private GameObject particleEffectPrefab;
    [SerializeField] private int healthValue = 0;

    public int getCoinValue => coinValue;
    public int getHealthValue => healthValue;

    private void Awake()
    {
    }

    public void Activate()
    {
        if (particleEffectPrefab != null)
        {
            //audioManager.PlaySFX(audioManager.coinClip, 0.4f);
            EventManager.Trigger("PlaySFX", new SoundEventData(SoundType.SFX_Coin, 0.4f));
            GameObject effect = Instantiate(particleEffectPrefab, transform.position, Quaternion.identity);
            Destroy(effect, 0.21f);
        }

        Destroy(gameObject);
    }
}
