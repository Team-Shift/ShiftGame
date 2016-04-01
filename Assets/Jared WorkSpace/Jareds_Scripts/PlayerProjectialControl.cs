using UnityEngine;
using System.Collections;

public class PlayerProjectialControl : MonoBehaviour
{
    public float lifeSpan = 0;
    public float xOffset = 0, yOffset = 0, zOffset = 0;
    private GameObject[] enemies;
    private Custom2DController playerScript;

    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        playerScript = GameObject.Find("Player").GetComponent<Custom2DController>();
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, lifeSpan);


        foreach (GameObject enemy in enemies)
        {

            Vector3 enemyPos = enemy.transform.position;
            Vector3 thisProjectilePos = gameObject.transform.position;
            Enemy enemyStatus = enemy.GetComponent<Enemy>();

            yOffset = enemyPos.y * -.84f;

            if (playerScript.CameraSwitch == false)
            {
                if ((enemyPos.x <= thisProjectilePos.x + xOffset && enemyPos.x >= thisProjectilePos.x - xOffset) && (enemyPos.z <= (thisProjectilePos.z + yOffset) + zOffset && enemyPos.z >= (thisProjectilePos.z + yOffset) - zOffset))
                {
                    Debug.Log("Enemy Hit: " + enemy.name);
                    Destroy(enemy);
                    Destroy(gameObject);
                }
            }
        }


     }
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            Debug.Log("Enemy was hit");


            Destroy(gameObject);

        }
    }
}
