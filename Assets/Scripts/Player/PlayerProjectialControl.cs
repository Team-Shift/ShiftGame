using UnityEngine;
using System.Collections;

public class PlayerProjectialControl : MonoBehaviour
{
    public float lifeSpan;
    public float xOffset, yOffset, zOffset;
    private GameObject[] enemies;
    private Custom2DController playerScript;

    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Custom2DController>();
        //lifeSpan *= 10.0f;
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, lifeSpan);
     }

    void FixedUpdate()
    {
        foreach (GameObject enemy in enemies)
        {
            if (enemy)
            {
                Vector3 enemyPos = enemy.transform.position;
                Vector3 thisProjectilePos = gameObject.transform.position;
                //EnemyHealth enemyStatus = enemy.GetComponent<EnemyHealth>();

                yOffset = enemyPos.y * -.84f;

                if (InputManager.Instance.is2D)
                {
                    if (Mathf.Abs(enemyPos.x - thisProjectilePos.x) <= xOffset && Mathf.Abs(enemyPos.z - thisProjectilePos.z) <= zOffset)
                    {
                        Debug.Log("Enemy Hit: " + enemy.name);
                        Destroy(gameObject);
                    }
                }
            }
        }
    }


    void OnCollisionEnter(Collision col)
    {
        //Debug.Log("Something was hit");
        //if (col.gameObject.tag == "Enemy")
        //{
        //    Debug.Log("Enemy was hit");
        //}
        if (col.gameObject.tag != "Player")
        {
            Destroy(gameObject);
        }
    }
}
