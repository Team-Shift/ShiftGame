using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float lerpSpeed = 1f;
    int shiftTimer = 0;
    Camera cam;
    public bool isOrthos = true;
    bool isMoving = false;

    //Stuff for Ortho Cam
    private Vector3 posOrtho;
    private Vector3 rotOrtho;

    //Stuff for Persp Cam
    private Vector3 posPersp;
    private Vector3 rotPersp;

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player");
        cam = GetComponent<Camera>();

        //Position and Rotation wanted
        posOrtho = new Vector3(0, 50f, -40f);
        rotOrtho = new Vector3(50f, 0, 0);
        posPersp = new Vector3(0, 10f, -4f);
        rotPersp = new Vector3(20f, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //Let player go from ortho->persp and back
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            isOrthos = !isOrthos;

            /*
            TODO:
            Figure out how to lerp from one position to the other over time
            */
            if (isOrthos)
            {
                //cam.transform.position = Vector3.Lerp(player.transform.position + posOrtho, player.transform.position + posPersp, 0.5f);
                StartCoroutine(SmoothLerp(player.transform.position + posOrtho, player.transform.position + posPersp, lerpSpeed));
                cam.transform.rotation = Quaternion.Euler(rotOrtho);
                cam.orthographic = true;
            }
            else
            {
                //cam.transform.position = Vector3.Lerp(player.transform.position + posPersp, player.transform.position + posOrtho, 0.5f);
                StartCoroutine(SmoothLerp(player.transform.position + posPersp, player.transform.position + posOrtho, lerpSpeed));
                cam.transform.rotation = Quaternion.Euler(rotPersp);
                cam.orthographic = false;
            }
        }
    }

    void LateUpdate()
    {
        //keep up position
        if (isOrthos)
        {
            cam.transform.position = player.transform.position + posOrtho;
        }
        else
        {
            cam.transform.position = player.transform.position + posPersp;
        }
    }

    IEnumerator SmoothLerp(Vector3 pos1 ,Vector3 pos2, float speed)
    {
        Debug.Log("Starting smooth lerp");
        float timeSinceStarted = 0f;
        while(true)
        {
            timeSinceStarted += Time.deltaTime * speed;
            pos1 = Vector3.Lerp(pos1, pos2, timeSinceStarted);
            cam.transform.position = pos1;
            Debug.Log(pos1 + ", " + pos2);
            Debug.Log(cam.transform.position);

            //If pos1 has arrived at new pos, stop the coroutine
            if (pos1 == pos2)
            {
                Debug.Log("Finished smooth lerp");
                yield break;
            }

            //otherwise cont. to next fram
            yield return null;
        }
    }
}
