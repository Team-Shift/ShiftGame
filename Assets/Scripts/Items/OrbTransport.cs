using UnityEngine;
using System.Collections;

public class OrbTransport : Item, iConsumable
{
    public int sceneToLoad;
    public void OnUse(GameObject g)
    {
        //**********CHANGE TO SAVE PLAYER DATA********************//

        // teleport to town
        g.GetComponent<PlayerCombat>().transform.LoadScene(sceneToLoad);
    }
}
