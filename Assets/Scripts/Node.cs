using UnityEngine;
using System.Collections.Generic;

public class Node : MonoBehaviour
{
    public int nodeId;
    public GameScene scene;

    [System.Serializable]
    public class Connection
    {
        public Node targetNode;
        public List<Vector3> waypoints;
        public bool unlocked = false;
        public Vector2 direction;
    }

    public List<Connection> connections = new List<Connection>();

    // ==== PATH ====
    public List<Vector3> GetPathTo(Node target)
    {
        var conn = connections.Find(c => c.targetNode == target && c.unlocked);
        if (conn == null) return null;

        List<Vector3> path = new List<Vector3>(conn.waypoints);
        path.Add(target.transform.position);
        return path;
    }

    // ==== MOVEMENT DIRECTION ====
    public Connection GetConnectionByDirection(Vector2 inputDir)
    {
        foreach (var conn in connections)
        {
            if (!conn.unlocked) continue;
            if (Vector2.Dot(conn.direction.normalized, inputDir.normalized) > 0.9f)
                return conn;
        }
        return null;
    }

    // ==== REFRESH CONNECTIONS ====
    public void RefreshConnections()
    {
        foreach (var conn in connections)
        {
            if (conn.targetNode == null) continue;
            conn.unlocked = PlayerManager.Instance.IsUnlocked(conn.targetNode.nodeId);
        }

        RefreshVisibility();
    }

    // ==== SHOW/HIDE NODE ====
    public void RefreshVisibility()
    {
        bool unlocked = PlayerManager.Instance.IsUnlocked(nodeId);
        gameObject.SetActive(unlocked);
    }
}
