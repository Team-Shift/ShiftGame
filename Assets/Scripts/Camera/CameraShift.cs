using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Camera))]
public class CameraShift : MonoBehaviour {

    public float ProjectionChangeTime = 0.5f;
    public bool ChangeProjection = false;

    private bool _changing = false;
    private float _currentT = 0.0f;

    // player to follow
    public GameObject player;
    private GameObject pivotPoint;

    // for fading in/out
    Color s_fade;                                       // to change alpha
    public GameObject sprite;                           // black sprite
    SpriteRenderer spriteRend;                          // to get/set spriterenderer color
    Camera cam;                                         // camera    
    bool canFade;                                       // used to fade in update
    public bool camActive;                              // false is ortho and true is persp
    float startTime;                                    // for pingPonging fade to start at 0
    public float duration = 0.5f;                       // how long to fade
    public bool isOrthos = true;


    //Shift Stuff
    //public int canShift;                               // if player unlocks shift ability
    public Light DirLight;                              //Change Culling Mask On Lights

    //players pos
    float playerPosX;
    float playerPosZ;
    float playerPosY;

    private Vector3 playerOffset = new Vector3();

    void Start()
    {
        InputManager.Instance.OnShift.AddListener(HandleOnShiftEvent);

        player = GameObject.FindGameObjectWithTag("Player");
        

        foreach(Transform t in player.transform)
        {
            if(t.name == "PivotPoint")
            {
                pivotPoint = t.gameObject;
            }
        }
        camActive = false;  // start in ortho

        // get camera
        cam = gameObject.GetComponent<Camera>();
        // get rend to change
        canFade = false;    // no fading to start
    }

    void Update()
    {

        /*
        Putting this little input here to make it so we can reset to main menu at any time
        */
        if (Input.GetKeyDown(KeyCode.P))
        {
            foreach (var everything in FindObjectsOfType<GameObject>())
            {
                Destroy(everything);
            }
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene("MainMenu");
        }

        // get players pos
        playerPosX = player.transform.position.x;
        playerPosZ = player.transform.position.z;
        playerPosY = player.transform.position.y;

        if (InputManager.Instance.is2D)
        {
            playerOffset = new Vector3(playerPosX, playerPosY + 50, playerPosZ - 40);
            
            transform.rotation = Quaternion.Euler(50f, 0, 0);
        }
        else
        {
            playerOffset = new Vector3(playerPosX, playerPosY + 10.0f, playerPosZ - 4.0f);
            transform.rotation = Quaternion.Euler(20.0f, 0, 0);
        }
        transform.position = playerOffset;
    }

    private void LateUpdate()
    {
        //if (camActive == true)
        if(!InputManager.Instance.is2D)
        {
            float offsetBack = 1;
            
            transform.rotation = (pivotPoint.transform.rotation);
            transform.position = pivotPoint.transform.position + offsetBack * -transform.forward;
        }


        if (!_changing)
        {
            return;
        }

        var currentlyOrthographic = GetComponent<Camera>().orthographic;
        Matrix4x4 orthoMat, persMat;
        if (currentlyOrthographic)
        {
            orthoMat = GetComponent<Camera>().projectionMatrix;

            GetComponent<Camera>().orthographic = false;
            GetComponent<Camera>().ResetProjectionMatrix();
            persMat = GetComponent<Camera>().projectionMatrix;
        }
        else
        {
            persMat = GetComponent<Camera>().projectionMatrix;

            GetComponent<Camera>().orthographic = true;
            GetComponent<Camera>().ResetProjectionMatrix();
            orthoMat = GetComponent<Camera>().projectionMatrix;
        }
        GetComponent<Camera>().orthographic = currentlyOrthographic;

        _currentT += (Time.deltaTime / ProjectionChangeTime);
        if (_currentT < 1.0f)
        {
            if (currentlyOrthographic)
            {
                GetComponent<Camera>().projectionMatrix = MatrixLerp(orthoMat, persMat, _currentT * _currentT);
            }
            else
            {
                GetComponent<Camera>().projectionMatrix = MatrixLerp(persMat, orthoMat, Mathf.Sqrt(_currentT));
            }
        }
        else
        {
            _changing = false;
            GetComponent<Camera>().orthographic = !currentlyOrthographic;
            GetComponent<Camera>().ResetProjectionMatrix();
        }
    }

    private Matrix4x4 MatrixLerp(Matrix4x4 from, Matrix4x4 to, float t)
    {
        t = Mathf.Clamp(t, 0.0f, 1.0f);
        var newMatrix = new Matrix4x4();
        newMatrix.SetRow(0, Vector4.Lerp(from.GetRow(0), to.GetRow(0), t));
        newMatrix.SetRow(1, Vector4.Lerp(from.GetRow(1), to.GetRow(1), t));
        newMatrix.SetRow(2, Vector4.Lerp(from.GetRow(2), to.GetRow(2), t));
        newMatrix.SetRow(3, Vector4.Lerp(from.GetRow(3), to.GetRow(3), t));
        return newMatrix;
    }

    void HandleOnShiftEvent()
    {
        _changing = true;
        _currentT = 0.0f;
    }
}