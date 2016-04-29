using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public static class _NpcExtend {

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
