using UnityEngine;

public class SpikeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject spikeMainPrefab;
    [SerializeField] private GameObject spikeChainPrefab;

    [SerializeField] private Vector2 center;
    [SerializeField] private float maxRadius = 2f;
    [SerializeField] private float radiusStep = 1f;
    [SerializeField] private float speed = 90f;
    [SerializeField] private float startAngle = 0f;
    [SerializeField] private float endAngle = 180f;
    [SerializeField] private bool isFullCircle = false;


    private void Awake()
    {
        center = transform.position;
    }
    void Start()
    {

        GameObject mainSpike = Instantiate(spikeMainPrefab);
        mainSpike.transform.localScale = Vector3.one;
        mainSpike.GetComponent<Spike>().Initialize(center, maxRadius, speed, startAngle, endAngle, isFullCircle);


        for (float r = 0f; r <= maxRadius; r += radiusStep)
        {
            GameObject chainSpike = Instantiate(spikeChainPrefab);
            chainSpike.transform.localScale = Vector3.one;
            chainSpike.GetComponent<Spike>().Initialize(center, r, speed, startAngle, endAngle, isFullCircle);
        }
    }
}
