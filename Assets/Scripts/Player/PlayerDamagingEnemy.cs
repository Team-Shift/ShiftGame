using UnityEngine;
using System.Collections;

public class PlayerDamagingEnemy : MonoBehaviour {

    //public float lifeSpan = 0;
    public float xOffset = 0, yOffset = 0, zOffset = 0;
    private GameObject[] enemies;
    private PlayerCombat playerScript;
    private GameObject player;

    // NPC Interaction Stuff
    public GUITexture GuiTexture;
    public GUIText GuiText;
    public int Speechindex = 1;
    int talkCount = 0;
    //public EnemyHealth eh;

    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<PlayerCombat>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        foreach (GameObject enemy in enemies)
        {
            if (enemy)
            {
                Vector3 enemyPos = enemy.transform.position;
                Vector3 playerSword = gameObject.transform.position;
                EnemyHealth enemyStatus = enemy.GetComponent<EnemyHealth>();

                yOffset = enemyPos.y * -.5f;
                if (player.GetComponent<Custom2DController>().CameraSwitch == false && playerScript.melee == false)
                {
                    //if ((enemyPos.x <= thisProjectilePos.x + xOffset && enemyPos.x >= thisProjectilePos.x - xOffset) && (enemyPos.z <= (thisProjectilePos.z + yOffset) + zOffset && enemyPos.z >= (thisProjectilePos.z + yOffset) - zOffset))
                    if (Mathf.Abs(enemyPos.x - playerSword.x) <= xOffset && Mathf.Abs(enemyPos.z - (playerSword.z + yOffset)) <= zOffset)
                    {
                        Debug.Log("Enemy Hit: " + enemy.name);
                        if (enemy.GetComponent<EnemyHealth>())
                        {
                            enemy.GetComponent<EnemyHealth>().TakeDamage();
                        }//if enemy health script exists
                    }//collision check
                }//if player is in 2D and attacking
            }//if enemy exists
        }//Forloop
    }//End of fixed update

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "NPC")
        { 
            this.transform.ShopkeeperSpeech(Speechindex, "", GuiText);
            GuiTexture.gameObject.SetActive(true);
            talkCount++;
            if(talkCount == 2)
            {
                GuiTexture.gameObject.SetActive(false);
                talkCount = 0;
                //Speechindex++;
            }
        }
    }


    /*IEnumerator AnimateText(string strComplete)
    {
        int i = 0;
        str = "";
        while (i < strComplete.Length)
        {
            str += strComplete[i++];
            yield return new WaitForSeconds(0.5F);
        }
    }*/

    /*void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            Debug.Log(col.gameObject.name + " was hit");
        }
    }
    */

}
