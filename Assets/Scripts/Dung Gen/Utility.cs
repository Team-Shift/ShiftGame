using System.Collections;
using System.Net.Mime;
using UnityEngine;

public static class Utility {

    public static T[] ShuffleArray<T>(T[] array, int seed)
    {
        System.Random prng;

        prng = new System.Random(seed);

//ToDo Enable Random Dungeon at Build Time
//#if UNITY_EDITOR
//        prng = new System.Random(seed);
//#else
//        prng = new System.Random((int)System.DateTime.Now.Ticks);
//#endif

        for (int i = 0; i < array.Length -1; i++)
        {
            int randomIndex = prng.Next(i, array.Length);
            T tempItem = array[randomIndex];
            array[randomIndex] = array[i];
            array[i] = tempItem;
        }

        return array;
    }
}
