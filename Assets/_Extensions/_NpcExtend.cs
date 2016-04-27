using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public static class _NpcExtend {

    //public GUITexture TextBox;
    //public GUIText NPCText;

    public static void startPrint(this Transform self, int speech, string text, GUIText guiText)
    {
        FarmerSpeech(speech,text);
        guiText.text = "";
        for(int i = 0; i < text.Length; i++)
        {
            guiText.text = guiText.text + text[i];
        }
    }


    /*static IEnumerator printText(string Text)
    {
        //NPCText.text = "";
        //for (int i = 0; i < Text.Length; i++)
        //{
        //    NPCText.text = NPCText.text + Text[i];
        //    yield return new WaitForSeconds(0.1f);
        //}
    }
    */

    public static void FarmerSpeech(int NPCSpeechIndex, string texts)
    {
        switch (NPCSpeechIndex)
        {
            case 5:
                texts = "Turns out the witch was just loney.";
                break;
            case 4:
                texts = "Wow I'm three dimentional!";
                break;
            case 3:
                texts = "Is that what the color of my shoes look like?";
                break;
            case 2:
                texts = "Thank you for saving us!";
                break;
            case 1:
                texts = "....";
                break;
            default:
                texts = "Hi there player";
                break;
        }
    }

}
