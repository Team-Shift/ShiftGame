using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public static class _NpcExtend {

    public static void ShopkeeperSpeech(this Transform self, int NPCSpeechIndex, string texts, GUIText guiText)
    {
        switch (NPCSpeechIndex)
        {
            case 5:
                texts = "Turns out the witch was just lonely.";
                guiText.text = texts;
                break;
            case 4:
                texts = "Wow I'm three dimentional!";
                guiText.text = texts;
                break;
            case 3:
                texts = "Is that what the color of my shoes look like?";
                guiText.text = texts;
                break;
            case 2:
                texts = "Thank you for saving us!";
                guiText.text = texts;
                break;
            case 1:
                texts = "....";
                guiText.text = texts;
                break;
            default:
                texts = "Hi there player";
                guiText.text = texts;
                break;
        }
    }

}
