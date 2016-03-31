using UnityEngine;
using System.Collections;
using UniRx;
using UniRx.Triggers;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class UIEvents : MonoBehaviour
{

    public int startingHealth;
    public GUITexture heartGUI;

    int numberOfLives;

    private float spacingX;
    private float spacingY;
    private Vector3 HeartPos = new Vector3(0.12f, 0, 0);

    public List<GUITexture> hearts;

    public float startHealth;
    float currentHealth;
    public float heartSpace;

    void Awake()
    {
        PlaceHearts();
        HealthBar();
    }

    void HealthBar()
    {
        for (int i = 1; i <= hearts.Count; i++)
        {
            if (i <= numberOfLives)
            {
                hearts[i].gameObject.SetActive(true);
            }
            else
            {
                hearts[i].gameObject.SetActive(false);
            }
        }
    }
    void Update()
    {
        numberOfLives = Mathf.CeilToInt((currentHealth / startHealth) * hearts.Count);
        HealthBar();
    }

    void PlaceHearts()
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            hearts[i].pixelInset = new Rect(Screen.width * (0.1f + (i * heartSpace)), Screen.height * 0.9f, Screen.width * 0.1f, Screen.width * 0.1f);
        }
    }

    //public void AddHearts(int n)
    //{
    //    //Better way for later
    //    //for (int i = 0; i < n; i++)
    //    //{
    //    //    //Transform newHearts = ((GameObject)Instantiate(heartGUI.gameObject)).transform;
    //    //    GUITexture newHearts = ((GUITexture)Instantiate(heartGUI));
    //    //    newHearts.transform.position += new Vector3(HeartPos.x * i, HeartPos.y, HeartPos.z);
    //    //    heartFillList.Add(newHearts);
    //    //    //newHearts.transform.parent = this.transform.parent;
    //    //}

    //    //Hack
    //}


    //Menu Stuff

    public void StartGame()
    {
        SceneManager.LoadScene("Town");
    }

    public void SettingPopUp()
    {

    }

    public void Quit()
    {

    }

}
