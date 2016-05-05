using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NpcInteractions : MonoBehaviour {

    public GUITexture TextBox;
    public GUIText NPCText;

    void Start()
    {

    }

    public void startPrint(int speech, string text, GUIText guiText)
    {
        //FarmerSpeech(speech, text);
        StartCoroutine("printText");
    }

    IEnumerator printText(string Text)
    {
        NPCText.text = "";
        for (int i = 0; i < Text.Length; i++)
        {
            NPCText.text = NPCText.text + Text[i];
            yield return new WaitForSeconds(0.1f);
        }
    }

}

