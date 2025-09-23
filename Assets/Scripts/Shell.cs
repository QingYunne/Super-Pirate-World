using System.Collections;
using UnityEngine;

public class Shell : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Transform player;
    [SerializeField] private GameObject pearlPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private bool reverse = false;
    [SerializeField] private float shootCooldown = 3f;
    [SerializeField] private float detectRange = 7.8125f;
    [SerializeField] private float levelRange = 0.47f;
    [SerializeField] private bool canShoot;
    [SerializeField] private int bulletDirection;

    private Animator animator;

    private Vector2 shellPos;
    private Vector2 playerPos;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        bulletDirection = reverse ? -1 : 1;
        shellPos = transform.position;
    }

    private void Start()
    {
        canShoot = true;
        if (reverse)
        {
            Vector3 scale = transform.localScale;
            scale.x = -1;
            transform.localScale = scale;
        }
    }

    private void Update()
    {
        TryShoot();
    }

    private void TryShoot()
    {
        if (IsPlayerVisible())
        {
            print("Player Detected");
            animator.SetTrigger("Fire");
            StartCoroutine(ShootRoutine());
        }
    }

    private bool IsPlayerVisible()
    {
        if (player == null) return false;
        playerPos = player.transform.position;

        bool playerNear = Mathf.Abs(shellPos.x - playerPos.x) < detectRange;
        bool playerFront = bulletDirection > 0 ? shellPos.x < playerPos.x : shellPos.x > playerPos.x;
        bool playerLevel = Mathf.Abs(shellPos.y - playerPos.y) < levelRange;

        return playerNear && playerFront && playerLevel && canShoot;
    }

    private IEnumerator ShootRoutine()
    {
        canShoot = false;
        yield return new WaitForSeconds(shootCooldown);
        canShoot = true;
    }

    public void ShootPearl()
    {
        if (pearlPrefab != null && firePoint != null)
        {
            GameObject pearl = Instantiate(pearlPrefab, firePoint.position, Quaternion.identity);
            pearl.GetComponent<Pearl>().Setup(bulletDirection);
        }
    }

    //[Header("Settings")]
    //[SerializeField] private float shootDistance = 5f;
    //[SerializeField] private float playerLevelTolerance = 0.3f;
    //[SerializeField] private bool reverse = false;
    //[SerializeField] private GameObject pearlPrefab;
    //[SerializeField] private float shootCooldown = 3f;

    //private Transform player;
    //private int bulletDirection;
    //private float timer = 0f;
    //private Animator animator;

    //private void Awake()
    //{
    //    bulletDirection = reverse ? -1 : 1;
    //    animator = GetComponent<Animator>();
    //}

    //public void SetPlayer(Transform playerTransform)
    //{
    //    player = playerTransform;
    //}

    //private void Update()
    //{
    //    if (player == null) return;

    //    timer += Time.deltaTime;

    //    if (CanShoot())
    //    {
    //        animator.SetTrigger("Fire"); 
    //        timer = 0f; 
    //    }
    //}

    //private bool CanShoot()
    //{
    //    Vector2 shellPos = transform.position;
    //    Vector2 playerPos = player.position;

    //    bool playerNear = Mathf.Abs(shellPos.x - playerPos.x) < shootDistance;
    //    bool playerFront = bulletDirection > 0 ? shellPos.x < playerPos.x : shellPos.x > playerPos.x;
    //    bool playerLevel = Mathf.Abs(shellPos.y - playerPos.y) < playerLevelTolerance;

    //    return playerNear && playerFront && playerLevel && timer >= shootCooldown;
    //}


    //public void SpawnPearl()
    //{
    //    if (pearlPrefab == null) return;

    //    Vector3 spawnPos = transform.position + Vector3.right * 0.5f * bulletDirection;
    //    GameObject pearlObj = Instantiate(pearlPrefab, spawnPos, Quaternion.identity);
    //    pearlObj.GetComponent<Pearl>().SetDirection(bulletDirection);
    //}
}
