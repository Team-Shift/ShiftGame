using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public static class _AiExtend {

    public static void LoadScene(this Transform self, int SceneIndex)
    {
        SceneManager.LoadScene(SceneIndex);
    }

    public static void PrintText(this Transform self, string Text)
    {

    }

}
