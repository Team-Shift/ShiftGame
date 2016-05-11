using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public static class _AiExtend {

    static FadeManager cameraFade = new FadeManager();

    public static void LoadScene(this Transform self, int SceneIndex)
    {
        cameraFade.lerpAlpha();
        SceneManager.LoadScene(SceneIndex);
    }

    public static void PrintText(this Transform self, string Text)
    {

    }

}
