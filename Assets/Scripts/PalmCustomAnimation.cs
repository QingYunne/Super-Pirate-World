using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PalmCustomAnimation : MonoBehaviour
{
    [Header("Animation Settings")]
    [SerializeField] private string animationName;
    [SerializeField] private float baseSpeed = 6;
    [SerializeField] private int layerIndex = 0;

    private Animator animator;
    public float finalSpeed;

    private void Awake()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
        if (animationName == "")
            animationName = gameObject.name;
    }

    void Start()
    {
        finalSpeed = baseSpeed + Random.Range(-1f, 1f);
        
        animator.speed = finalSpeed / baseSpeed;

        animator.Play(animationName, layerIndex, 0);
        Debug.Log(gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
