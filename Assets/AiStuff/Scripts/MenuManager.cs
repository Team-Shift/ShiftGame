using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public void TownScene()
    {
        SceneManager.LoadScene(1);
    }

    public void ForestDungeon()
    {
        SceneManager.LoadScene(2);
    }
}
