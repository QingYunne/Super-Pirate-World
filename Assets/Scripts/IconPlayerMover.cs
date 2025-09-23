using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class IconPlayerMover : MonoBehaviour
{
    public float moveSpeed = 6.25f;
    public Node currentNode;
    private bool isMoving;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();

        Node startNode = PlayerManager.Instance.GetLastNode();

        if (startNode != null)
        {
            currentNode = startNode;
            transform.position = currentNode.transform.position;
        }
        else
        {
            Node[] nodes = FindObjectsByType<Node>(FindObjectsSortMode.None);
            foreach (var n in nodes)
            {
                if (n.nodeId == 0)
                {
                    currentNode = n;
                    transform.position = currentNode.transform.position;
                    break;
                }
            }
        }

        Node[] allNodes = FindObjectsByType<Node>(FindObjectsSortMode.None);
        foreach (var n in allNodes)
            n.RefreshConnections();
        EventManager.Trigger("NodeChanged", currentNode.nodeId);
    }

    private void Update()
    {
        if (isMoving || currentNode == null) return;

        // Input
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (input.sqrMagnitude > 0.1f)
        {
            TryMove(input.normalized);
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            PlayerManager.Instance.lastNodeId = currentNode.nodeId;
            EventManager.Trigger("PlaySFX", new SoundEventData(SoundType.SFX_ClickButton, 1f, duckMusic: true, duckVolume: 0.3f, duckDuration: 0.5f));
            EventManager.Trigger("PlayMusic", new SoundEventData(SoundType.BGM_Level, 0.5f));

            StartCoroutine(LoadSceneDelayed(currentNode.scene.GetSceneName(), 0.2f));
        }

        if (!isMoving)
        {
            anim.SetFloat("Horizontal", 0);
            anim.SetFloat("Vertical", 0);
        }
    }

    private void TryMove(Vector2 inputDir)
    {
        var conn = currentNode.GetConnectionByDirection(inputDir);
        if (conn != null && conn.unlocked)
            StartCoroutine(MoveAlongPath(conn));
    }

    private IEnumerator MoveAlongPath(Node.Connection conn)
    {
        isMoving = true;
        anim.SetBool("isMoving", true);

        List<Vector3> path = new List<Vector3>(conn.waypoints);
        path.Add(conn.targetNode.transform.position);

        foreach (var target in path)
        {
            while (Vector3.Distance(transform.position, target) > 0.05f)
            {
                Vector3 oldPos = transform.position;

                transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);

                Vector3 velocity = (transform.position - oldPos) / Time.deltaTime;

                UpdateAnimator(velocity);

                yield return null;
            }
        }

        currentNode = conn.targetNode;
        PlayerManager.Instance.lastNodeId = currentNode.nodeId;

        EventManager.Trigger("NodeChanged", currentNode.nodeId);

        isMoving = false;
        anim.SetBool("isMoving", false);
        anim.SetInteger("Direction", 0);
    }

    private void UpdateAnimator(Vector2 velocity)
    {
        if (velocity.sqrMagnitude < 0.0001f)
        {
            anim.SetInteger("Direction", 0); // Idle
            return;
        }

        if (Mathf.Abs(velocity.x) > Mathf.Abs(velocity.y))
        {
            anim.SetInteger("Direction", velocity.x > 0 ? 4 : 3);
        }
        else
        {
            anim.SetInteger("Direction", velocity.y > 0 ? 1 : 2);
        }
    }

    private IEnumerator LoadSceneDelayed(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        EventManager.Trigger("PlayMusic", new SoundEventData(SoundType.BGM_Level, 0.5f));
    }


}
