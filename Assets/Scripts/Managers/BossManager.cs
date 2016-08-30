using UnityEngine;
using System.Collections;

public class BossManager : MonoBehaviour {

	public delegate void BossEventHandler();

	public static event BossEventHandler OnStart;

	public static void startBossFight()
	{
		if (OnStart != null) {
			OnStart ();
		} else {
			Debug.Log ("is null");
		}
	}


}
