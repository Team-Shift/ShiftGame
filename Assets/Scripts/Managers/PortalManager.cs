using UnityEngine;
using System.Collections;

public class PortalManager : MonoBehaviour {


    void OnTriggerEnter()
    {
        this.transform.LoadScene(2);
    }
}
