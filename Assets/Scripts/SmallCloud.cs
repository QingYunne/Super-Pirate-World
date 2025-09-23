using UnityEngine;

public class SmallCloud : MonoBehaviour
{
    private float speed;
    private float destroyX;

    public void Init(float spd, float destroyX)
    {
        this.speed = spd;
        this.destroyX = destroyX;
    }

    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;

        if (transform.position.x <= destroyX)
        {
            Destroy(gameObject);
        }
    }
}
