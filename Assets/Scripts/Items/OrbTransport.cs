using UnityEngine;
using System.Collections;

public class OrbTransport : Item, iConsumable
{
    public int sceneToLoad;
    public void OnUse(GameObject g)
    {
        // teleport to town
        g.GetComponent<PlayerCombat>().transform.LoadScene(sceneToLoad);
    }
}
