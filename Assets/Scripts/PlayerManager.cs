using System;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }
    //public UIController ui;

    [Header("Stats")]
    [SerializeField] private int maxHealth = 5;
    [SerializeField] private int coinsForExtraHealth = 100;

    [Header("Damage Settings")]
    [SerializeField] private float invincibleDuration = 0.4f;

    [Header("Level Settings")]
    [SerializeField] public int lastNodeId = 0;
    [SerializeField] private int highestUnlockedID = 0;

    private int health;
    private int coins;

    public bool IsInvincible { get; private set; }
    private float invincibleTimer;
    private bool isWin = false;

    private void Awake()
    {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);

        health = maxHealth;
        coins = 0;
    }

    private void Start()
    {
        EventManager.Trigger("CoinsChanged", coins);
        EventManager.Trigger("HealthChanged", health);
    }

    private void Update()
    {
        if (IsInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer <= 0f)
                IsInvincible = false;
        }
    }

    public void AddCoin(int amount)
    {
        coins += amount;
        Debug.Log("Coins: " + coins);

        while (coins >= coinsForExtraHealth)
        {
            coins -= coinsForExtraHealth;
            AddHealth(1);
        }
        Debug.Log("Event OnCoinsChanged fired! Coins: " + coins);
        EventManager.Trigger("CoinsChanged", coins);
    }


    public void AddHealth(int amount)
    {
        health = health + amount;
        Debug.Log("Health: " + health);
        EventManager.Trigger("HealthChanged", health);
    }

    public void TakeDamage(int amount)
    {
        if (IsInvincible) return;
        EventManager.Trigger("PlaySFX", new SoundEventData(SoundType.SFX_Damage));

        health -= amount;
        EventManager.Trigger("HealthChanged", health);

        if (health <= 0)
        {
            Die();
        }
        else
        {
            IsInvincible = true;
            invincibleTimer = invincibleDuration;
        }
    }

    private void Die()
    {
        Debug.Log("Player Dead!");
        EventManager.Trigger("PlayerDied");

    }

    public void SetCurrentNode(int nodeId)
    {
        lastNodeId = nodeId;

        if (highestUnlockedID < nodeId + 1)
            highestUnlockedID = nodeId + 1;

        Node[] nodes = FindObjectsByType<Node>(FindObjectsSortMode.None);
        foreach (var n in nodes)
            n.RefreshConnections();
    }


    public void UnlockNextFrom(int nodeId)
    {
        if (highestUnlockedID < nodeId + 1)
            highestUnlockedID = nodeId + 1;

        Node[] nodes = FindObjectsByType<Node>(FindObjectsSortMode.None);
        foreach (var n in nodes)
            n.RefreshConnections();
    }

    public Node GetLastNode()
    {
        Node[] nodes = FindObjectsByType<Node>(FindObjectsSortMode.InstanceID);
        foreach (var n in nodes)
        {
            if (n.nodeId == lastNodeId)
                return n;
        }
        return null;
    }

    public void ResetStats()
    {
        isWin = false;
        health = maxHealth;
        coins = 0;
        lastNodeId = 0;
        highestUnlockedID = 0;

        IsInvincible = false;
        invincibleTimer = 0f;

        EventManager.Trigger("CoinsChanged", coins);
        EventManager.Trigger("HealthChanged", health);

        Debug.Log("Player stats reset!");
    }

    public void SetWin()
    {
        isWin = true;
        EventManager.Trigger("PlayerWin"); 
    }

    public bool IsUnlocked(int nodeId) => nodeId <= highestUnlockedID;

    public int GetHealth() => health;
    public int GetCoins() => coins;

    public int GetHighestUnlockedID() => highestUnlockedID;
}
