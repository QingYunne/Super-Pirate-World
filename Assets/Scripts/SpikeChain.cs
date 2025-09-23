//using UnityEngine;

//public class SpikeChain : MonoBehaviour
//{
//    [SerializeField] private GameObject spikePrefab;
//    [SerializeField] private int spikeCount = 5;
//    [SerializeField] private float gap = 0.5f;

//    private Spike[] spikes;

//    public void Init(Vector2 pos, float radius, float speed, float startAngle, float endAngle, int count, float gap)
//    {
//        this.spikeCount = count;
//        this.gap = gap;

//        spikes = new Spike[count];

//        for (int i = 0; i < count; i++)
//        {
//            GameObject spikeObj = Instantiate(spikePrefab, pos, Quaternion.identity, transform);
//            Spike spike = spikeObj.GetComponent<Spike>();

//            float offsetRadius = radius + i * gap;

//            spike.Init(pos, offsetRadius, speed, startAngle, endAngle);
//            spikes[i] = spike;
//        }
//    }
//}
