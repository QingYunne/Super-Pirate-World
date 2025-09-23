using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class PathTile : MonoBehaviour
{    
    [SerializeField] private int requiredLevel;

    private bool isVisible = false;

   public void RefreshVisibility(bool isUnlocked)
    {
       isVisible = isUnlocked;
        gameObject.SetActive(isVisible);
    }

    void Start()
    {
        RefreshVisibility(PlayerManager.Instance.IsUnlocked(requiredLevel));
    }
    public void Update()
    {
        RefreshVisibility(PlayerManager.Instance.IsUnlocked(requiredLevel));
    }

}
