using UnityEngine;
using System.Collections;

public class Narrative : MonoBehaviour {

    public AudioClip[] narrativeSound;
    int count = 1;
	// Use this for initialization
	void Start () {
        GetComponent<AudioSource>().clip = narrativeSound[0];
        GetComponent<AudioSource>().Play();
        count++;
    }
	
    public void PlayNarrative()
    {
        for(int i = 0; i < count; i++)
        {
            GetComponent<AudioSource>().clip = narrativeSound[i];
            GetComponent<AudioSource>().Play();
        }
        if (count < 5)
        {
            count++;
        }
        
        else
        {
            this.transform.LoadScene(2);
        }
    }
}
