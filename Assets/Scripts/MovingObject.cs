using UnityEngine;
using UnityEngine.UIElements;

public class MovingObject : MonoBehaviour
{

    [Header("Movement Settings")]
    //[SerializeField] private GameObject targetObject;
    [SerializeField] private Vector2 startPos;
    [SerializeField] private Vector2 endPos;
    [SerializeField] private float speed = 1.5625f;
    [SerializeField] private bool flip;
    [SerializeField] private bool isPlatform;
    [SerializeField] private bool isHorizontal = true;

    //private BoxCollider2D boxTarget;
    //private BoxCollider2D selfBox;
    private Vector2 direction;

    void Awake()
    {
        //if (targetObject != null)
        //{
        //    boxTarget = targetObject.GetComponent<BoxCollider2D>();
        //    selfBox = GetComponent<BoxCollider2D>();
            SetupPositions();
        //}
    }

    void SetupPositions()
    {
        //Bounds targetBounds = boxTarget.bounds;
        //Bounds selfBounds = selfBox.bounds;

        if (isHorizontal)
        {
            //startPos = new Vector2(targetBounds.min.x + (selfBounds.size.x / 2f), targetBounds.center.y);
            //endPos = new Vector2(targetBounds.max.x - (selfBounds.size.x / 2f), targetBounds.center.y);

            transform.position = startPos;

            direction = Vector2.right; 
        }
        else
        {
            //startPos = new Vector2(targetBounds.center.x, targetBounds.max.y - (selfBounds.size.y / 2f));
            //endPos = new Vector2(targetBounds.center.x, targetBounds.min.y + (selfBounds.size.y / 2f));

            transform.position = startPos;

            direction = Vector2.down; 
        }
        //Debug.Log(this.gameObject.name + " " + selfBounds.size.x.ToString() + " " + selfBounds.size.y.ToString());
    }

    void Update()
    {
        //transform.position += (Vector3)(direction * speed * Time.deltaTime);


        //if (isHorizontal)
        //{
        //    if (transform.position.x > endPos.x)
        //    {
        //        direction = Vector2.left;
        //        if (flip) FlipSprite();
        //    }
        //    else if (transform.position.x < startPos.x)
        //    {
        //        direction = Vector2.right;
        //        if (flip) FlipSprite();
        //    }
        //}
        //else
        //{
        //    if (transform.position.y < endPos.y)
        //    {
        //        direction = Vector2.up;
        //        if (flip) FlipSprite();
        //    }
        //    else if (transform.position.y > startPos.y)
        //    {
        //        direction = Vector2.down;
        //        if (flip) FlipSprite();
        //    }
        //}

        transform.position += (Vector3)(direction * speed * Time.deltaTime);

        if (isHorizontal)
        {
            if (transform.position.x > endPos.x)
            {
                direction = Vector2.left;
            }
            else if (transform.position.x < startPos.x)
            {
                direction = Vector2.right;
            }

            if (flip)
                SetFlipScale(direction.x);
        }
        else
        {
            if (transform.position.y < endPos.y)
            {
                direction = Vector2.up;
            }
            else if (transform.position.y > startPos.y)
            {
                direction = Vector2.down;
            }

            if (flip)
                SetFlipScale(direction.y);
        }
    }

    //void FlipSprite()
    //{
    //    Vector3 scale = transform.localScale;
    //    if (isHorizontal)
    //        scale.x *= -1;
    //    else
    //        scale.y *= -1;
    //    transform.localScale = scale;
    //}

    void SetFlipScale(float axis)
    {
        Vector3 scale = transform.localScale;

        if (isHorizontal)
            scale.x = Mathf.Abs(scale.x) * Mathf.Sign(axis); 
        else
            scale.y = Mathf.Abs(scale.y) * Mathf.Sign(axis); 

        transform.localScale = scale;
    }
}
