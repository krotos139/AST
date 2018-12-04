using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {
    public AudioClip musicLevel1_1;
    public AudioClip musicLevel1_2;
    public AudioClip musicLevel2_1;
    public AudioClip musicLevel2_2;

    // Use this for initialization
    void Start () {

		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartLevel(int level)
    {
        AudioSource audio = GetComponent<AudioSource>();
        if (level == 1)
        {
            if (Random.Range(0,99)<50)
            {
                audio.clip = musicLevel1_1;
            } else {
                audio.clip = musicLevel1_2;
            }
        }
        if (level == 2)
        {
            if (Random.Range(0, 99) < 50)
            {
                audio.clip = musicLevel2_1;
            }
            else {
                audio.clip = musicLevel2_2;
            }
        }
        audio.Play();
    }

}
