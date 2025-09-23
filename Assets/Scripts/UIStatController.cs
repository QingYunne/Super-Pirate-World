using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class UIStatController : MonoBehaviour
{
    [Header("Hearts")]
    public GameObject heartPrefab;
    public Transform heartsContainer;
    private List<GameObject> hearts = new List<GameObject>();
    public float heartSpacing = 10f; 

    [Header("Coins")]
    public Image coinIcon;
    public TMP_Text coinText;
    public float coinDisplayTime = 1f;
    private Coroutine coinCoroutine;

    private void Awake()
    {
        if (coinText == null)
            coinText = transform.Find("CoinText")?.GetComponent<TMP_Text>();
        if (heartsContainer == null)
            heartsContainer = transform.Find("HeartsContainer");

        //if (heartsContainer == null)
        //    heartsContainer = transform.Find("HeartsContainer");

        if (heartPrefab == null && heartsContainer != null)
        {
            Transform heartTemplate = heartsContainer.Find("Heart");
            if (heartTemplate != null)
            {
                heartPrefab = heartTemplate.gameObject;
                heartPrefab.SetActive(false); 
            }
        }
    }

    private void Start()
    {
        //StartCoroutine(InitUI());

        SetHearts(PlayerManager.Instance.GetHealth());
        ShowCoins(PlayerManager.Instance.GetCoins());
        EventManager.Subscribe("CoinsChanged", OnCoinsChanged);
        EventManager.Subscribe("HealthChanged", OnHealthChanged);
    }

    private IEnumerator InitUI()
    {
        yield return null; 

        if (PlayerManager.Instance != null)
        {

            //PlayerManager.Instance.OnHealthChanged += SetHearts;
            //PlayerManager.Instance.OnCoinsChanged += ShowCoins;
            Debug.Log("UIStatController subscribed to PlayerManager events");

            EventManager.Subscribe("CoinsChanged", OnCoinsChanged);
            EventManager.Subscribe("HealthChanged", OnHealthChanged);
        }
        else
        {
            Debug.LogError("PlayerManager.Instance is NULL when InitUI!");
        }
    }

    private void OnEnable()
    {
        StartCoroutine(InitUI());

        //EventManager.Subscribe("CoinsChanged", OnCoinsChanged);
        //EventManager.Subscribe("HealthChanged", OnHealthChanged);


        //if (PlayerManager.Instance != null)
        //{
        //    PlayerManager.Instance.OnHealthChanged += SetHearts;
        //    PlayerManager.Instance.OnCoinsChanged += ShowCoins;
        //    Debug.Log("UIStatController subscribed to PlayerManager events");
        //}

        //SetHearts(PlayerManager.Instance.GetHealth());
        //ShowCoins(PlayerManager.Instance.GetCoins());
    }

    private void OnDisable()
    {
        EventManager.Unsubscribe("CoinsChanged", OnCoinsChanged);
        EventManager.Unsubscribe("HealthChanged", OnHealthChanged);
        //if (PlayerManager.Instance != null)
        //{
        //    PlayerManager.Instance.OnHealthChanged -= SetHearts;
        //    PlayerManager.Instance.OnCoinsChanged -= ShowCoins;
        //    Debug.Log("UIStatController unsubscribed from PlayerManager events");
        //}
    }

    // ===== Hearts =====
    public void SetHearts(int amount)
    {
        if (amount <= 0)
            heartPrefab.SetActive(false);
        else             
            heartPrefab.SetActive(true);
        foreach (var heart in hearts)
        {
            if (heart != null) Destroy(heart);
        }
        hearts.Clear();

        RectTransform rt = heartPrefab.GetComponent<RectTransform>();
        float heartWidth = rt.sizeDelta.x;

        for (int i = 0; i < amount; i++)
        {
            GameObject heart = Instantiate(heartPrefab, heartsContainer);
            RectTransform heartRT = heart.GetComponent<RectTransform>();
            heartRT.anchoredPosition = new Vector2(i * (heartWidth + heartSpacing), 0f); 
            hearts.Add(heart);
        }
    }

    // ===== Coins =====
    public void ShowCoins(int amount)
    {

        Debug.Log("UI updating coin text: " + amount);

        if (coinText == null)
        {
            Debug.LogError("coinText is NULL!");
            return;
        }

        coinText.text = amount.ToString();

        if (coinCoroutine != null)
            StopCoroutine(coinCoroutine);
        coinCoroutine = StartCoroutine(DisplayCoin());
    }

    private IEnumerator DisplayCoin()
    {
        coinIcon.enabled = true;
        coinText.enabled = true;
        yield return new WaitForSeconds(coinDisplayTime);
        coinIcon.enabled = false;
        coinText.enabled = false;
    }

    private void OnCoinsChanged(object value)
    {
        ShowCoins((int)value);
    }

    private void OnHealthChanged(object value)
    {
        SetHearts((int)value);
    }

    private void OnDestroy()
    {
        EventManager.Unsubscribe("CoinsChanged", OnCoinsChanged);
        EventManager.Unsubscribe("HealthChanged", OnHealthChanged);
    }
}
