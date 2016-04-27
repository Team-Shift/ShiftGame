using UnityEngine;
using System.Collections;

public class PortalManager : MonoBehaviour {

    MenuManager sceneShit;

    void Start()
    {
        sceneShit = FindObjectOfType<MenuManager>();
    }

    void OnTriggerEnter()
    {
        sceneShit.ForestDungeon();
    }
}
