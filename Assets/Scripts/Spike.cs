using UnityEngine;

public class Spike : MonoBehaviour
{
    [SerializeField] private Vector2 center;
    [SerializeField] private float radius = 1f;
    [SerializeField] private float speed = 90f;
    [SerializeField] private float startAngle = 0f;
    [SerializeField] private float endAngle = 180f;
    [SerializeField] private bool isFullCircle = false;

    private float angle;
    private int direction = 1;

    public void Initialize(Vector2 c, float r, float s, float start, float end, bool full)
    {
        center = c;
        radius = r;
        speed = s;
        startAngle = start;
        endAngle = end;
        isFullCircle = full;
        angle = startAngle;
        UpdatePosition();
    }

    void Update()
    {
        float dt = Time.deltaTime;
        angle += direction * speed * dt;

        if (!isFullCircle)
        {
            if (angle >= endAngle) direction = -1;
            if (angle <= startAngle) direction = 1;
        }
        else
        {
            angle %= 360f;
        }

        UpdatePosition();
    }

    private void UpdatePosition()
    {
        float rad = angle * Mathf.Deg2Rad;
        
        float x = center.x + Mathf.Cos(rad) * radius;
        float y = center.y - Mathf.Sin(rad) * radius;

        transform.position = new Vector3(x, y, 0f);

        
        Vector2 dir = center - new Vector2(x, y);
        float rotZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ - 90f);
    }
}
