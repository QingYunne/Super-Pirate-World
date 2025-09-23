using UnityEngine;

public class SmallCloudSpawner : MonoBehaviour
{
    [Header("Cloud Settings")]
    [SerializeField] private GameObject[] cloudPrefabs;   
    [SerializeField] private float spawnInterval = 2.5f;  
    [SerializeField] private Vector2 yRange = new Vector2(-2f, 2f);
    [SerializeField] private float startX = 15f;
    [SerializeField] private float destroyX = -15f;       
    [SerializeField] private Vector2 speedRange = new Vector2(0.78f, 1.87f); 

    private float timer;

    void Start()
    {
        
        for (int i = 0; i < 5; i++)
        {
            SpawnCloud(Random.Range(-10f, startX));
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnCloud(startX);
        }
    }

    void SpawnCloud(float xPos)
    {
        
        GameObject prefab = cloudPrefabs[Random.Range(0, cloudPrefabs.Length)];

        
        float y = Random.Range(yRange.x, yRange.y);
        Vector3 pos = new Vector3(xPos, y, 0);

        
        GameObject cloud = Instantiate(prefab, pos, Quaternion.identity, transform);

        
        float spd = Random.Range(speedRange.x, speedRange.y);
        cloud.AddComponent<SmallCloud>().Init(spd, destroyX);
    }
}
