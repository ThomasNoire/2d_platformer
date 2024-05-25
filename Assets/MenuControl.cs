using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuControl : MonoBehaviour
{
    public GameObject menu; 
    private Dictionary<GameObject, bool> originalStates = new Dictionary<GameObject, bool>();

    
    void Start()
    {
        StoreOriginalStates(menu); 
        SetActiveRecursively(menu, false); 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menu.activeSelf)
            {
                SetActiveRecursively(menu, false);
            }
            else
            {
                RestoreOriginalStates(menu);
            }
        }
    }

    void StoreOriginalStates(GameObject obj)
    {
        originalStates[obj] = obj.activeSelf;
        foreach (Transform child in obj.transform)
        {
            StoreOriginalStates(child.gameObject);
        }
    }

    void SetActiveRecursively(GameObject obj, bool state)
    {
        obj.SetActive(state);
        foreach (Transform child in obj.transform)
        {
            SetActiveRecursively(child.gameObject, state);
        }
    }

    void RestoreOriginalStates(GameObject obj)
    {
        if (originalStates.ContainsKey(obj))
        {
            obj.SetActive(originalStates[obj]);
        }
        foreach (Transform child in obj.transform)
        {
            RestoreOriginalStates(child.gameObject);
        }
    }
    
}