using UnityEngine;
using System.IO;

public static class _NpcExtend {

    static string[] line;

    public static void TalkToShopKeeper(this Transform self, int SpeechIndex, GUIText guiText)
    {
        //Paths to txt file and reads from it
        var fileText = File.ReadAllText("Assets/Resources/NPCText/ShopSpeech.txt");
        //if line break add to array
        line = fileText.Split("\n"[0]);
        guiText.text = line[SpeechIndex];
    }

    public static void TalkToOldMan(this Transform self, int SpeechIndex, GUIText guiText)
    {
        //Paths to txt file and reads from it
        var fileText = File.ReadAllText("Assets/Resources/NPCText/OldSpeech.txt");
        //if line break add to array
        line = fileText.Split("\n"[0]);
        guiText.text = line[SpeechIndex];
    }

    public static void TalkToFarmer(this Transform self, int SpeechIndex, GUIText guiText)
    {
        //Paths to txt file and reads from it
        var fileText = File.ReadAllText("Assets/Resources/NPCText/FarmerSpeech.txt");
        //if line break add to array
        line = fileText.Split("\n"[0]);
        guiText.text = line[SpeechIndex];
    }

}
