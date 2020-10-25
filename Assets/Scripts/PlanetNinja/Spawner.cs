using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float radius;
    public float timeToReach = 30;
    [SerializeField]
    private GameObject[] planets;
    [SerializeField]
    private float delayToSpawn = 3;
    private float timeToSpawn;
    private bool signo;
    public AudioClip[] soundManager;
    private AudioSource player;

    // Use this for initialization
    void Start ()
    {
        player = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(Time.time >= timeToSpawn)
        {
            GameObject newPlanet = Instantiate(planets[planetToSpawn()], positionToSpawn(), transform.rotation) as GameObject;
            newPlanet.transform.parent = transform;
            timeToSpawn = delayToSpawn + Time.time;
        }
	}

    int planetToSpawn()
    {
        return Random.Range(0, planets.Length);
    }

    Vector3 positionToSpawn()
    {
        signo = (Random.value > 0.5f);
        float x = signo ? Random.Range(5f, 10f) : Random.Range(-5f, -10f);
        signo = (Random.value > 0.5f);
        float y = 1.5f + (signo ? Random.Range(8f, 13f) : Random.Range(-8f, -13f));
        signo = (Random.value > 0.5f);
        float z = signo ? Random.Range(4f, 9f) : Random.Range(-4f, -9f);
        return new Vector3(x,y,z);
    }

    public void playExplosion()
    {
        signo = (Random.value > 0.5f);
        if(signo)
        {
            player.PlayOneShot(soundManager[0], 0.5f);
        }
        else
        {
            player.PlayOneShot(soundManager[1], 0.5f);
        }
    }
}
