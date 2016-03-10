using UnityEngine;
using System.Collections;

public class CameraShift : MonoBehaviour {
    // need to attach dif cameras in scene
    // or need to tag persp camera with tag in scene
    public GameObject ortho;
    public GameObject persp;

    public bool canShift;
    void Start()
    {
        // changes if unlocked in game
        canShift = true;

        // get cameras if not found
        if(ortho == null)
            ortho = GameObject.FindGameObjectWithTag("MainCamera");
        if(persp == null)
            persp = GameObject.FindGameObjectWithTag("Perspective Camera");
    }

	void FixedUpdate () {
	    if(Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift) && canShift)
        {
            ChangeCamera();
        }
	}
    
    void ChangeCamera()
    {
        if (ortho.activeInHierarchy == true)
        {
            ortho.SetActive(false);
            persp.SetActive(true);
        }
        else
        {
            ortho.SetActive(true);
            persp.SetActive(false);
        }
    }
}
