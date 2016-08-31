using UnityEngine;
using System.Collections;

public class BossManager : MonoBehaviour {

	public delegate void BossEventHandler();

	public static event BossEventHandler OnStart;
	public static event BossEventHandler OnBossDead;

	public static void startBossFight()
	{
		if (OnStart != null) {
			OnStart ();
		} 
	}

	public static void EndBossFight()
	{
		if (OnBossDead != null) {
			OnBossDead ();
		} 
	}
}
