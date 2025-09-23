using UnityEngine;

public class LargeCloudScroller : MonoBehaviour
{
    [SerializeField] private GameObject cloudPrefab;
    [SerializeField] private int tileCount = 3;          
    [SerializeField] private float speed = 0.78f;

    private GameObject[] clouds;
    private float cloudWidth;

    void Start()
    {
        
        cloudWidth = cloudPrefab.GetComponent<SpriteRenderer>().bounds.size.x;

        
        clouds = new GameObject[tileCount];
        for (int i = 0; i < tileCount; i++)
        {
            Vector3 pos = new Vector3(i * cloudWidth, transform.position.y, 0);
            clouds[i] = Instantiate(cloudPrefab, pos, Quaternion.identity, transform);
        }
    }

    void Update()
    {
        foreach (GameObject cloud in clouds)
        {
            cloud.transform.position += Vector3.left * speed * Time.deltaTime;

            
            if (cloud.transform.position.x <= -cloudWidth)
            {
                float rightMostX = GetRightMostCloudX();
                cloud.transform.position = new Vector3(rightMostX + cloudWidth, cloud.transform.position.y, cloud.transform.position.z);
            }
        }
    }

    float GetRightMostCloudX()
    {
        float maxX = float.MinValue;
        foreach (GameObject cloud in clouds)
        {
            if (cloud.transform.position.x > maxX)
                maxX = cloud.transform.position.x;
        }
        return maxX;
    }
}
