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
    public float heartSpace;

    int numberOfLives;


    public List<GUITexture> heartFills;
    public List<GUITexture> heartContainers;


    void Start()
    {
        PlaceHeart();
        //AddHearts(startingHealth);
    }

    //void Awake()
    //{
    //    PlaceHearts();
    //    HealthBar();
    //}

    //void HealthBar()
    //{
    //    for (int i = 1; i <= hearts.Count; i++)
    //    {
    //        if (i <= numberOfLives)
    //        {
    //            hearts[i].gameObject.SetActive(true);
    //        }
    //        else
    //        {
    //            hearts[i].gameObject.SetActive(false);
    //        }
    //    }
    //}
    //void Update()
    //{
    //    numberOfLives = Mathf.CeilToInt((currentHealth / startHealth) * hearts.Count);
    //    HealthBar();
    //}

    //void PlaceHearts()
    //{
    //    for (int i = 0; i < hearts.Count; i++)
    //    {
    //        hearts[i].pixelInset = new Rect(Screen.width * (0.1f + (i * heartSpace)), Screen.height * 0.9f, Screen.width * 0.1f, Screen.width * 0.1f);
    //    }
    //}

    public void AddHeartContainer()
    {
        
    }

    public void AddHearts(int n)
    {

        for (int i = 0; i < n; i++)
            {

                GUITexture newHearts = ((GUITexture)Instantiate(heartGUI));
                //newHearts.transform.position += new Vector3(HeartPos.x * i, HeartPos.y, HeartPos.z);

            }

    }

    void PlaceHeart()
    {
        for(int i = 0; i < heartFills.Count; i++)
        {
            heartFills[i].pixelInset = new Rect(Screen.width * (0.1f + (i * heartSpace)), Screen.height * 0.9f, Screen.width * 0.1f, Screen.width * 0.1f);
        }
    }

    ///Menu Stuff

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
