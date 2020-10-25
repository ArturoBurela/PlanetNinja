using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class planetToHands : MonoBehaviour
{
    private float radius;
    private Vector3 initialPos;
    private Vector3 endPos;
    private bool signo;
    private float timeToReach;
    private float actualTime = 0;
    


    // Use this for initialization
    void Start ()
    {
        radius = transform.GetComponentInParent<Spawner>().radius;
        timeToReach = transform.GetComponentInParent<Spawner>().timeToReach;
        initialPos = transform.position;
        endPos = positionToHands();
    }
	
	// Update is called once per frame
	void Update ()
    {
        actualTime += Time.deltaTime;
        if(actualTime < timeToReach)
        {
            float transition = (float) actualTime / (float) timeToReach;
            transform.position = Vector3.Lerp(initialPos, endPos, transition);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.layer.ToString() == "Planetas" || col.gameObject.tag == "Mano")
        {
            GetComponent<DissolveEffect>().playDissolve();
            GetComponent<Collider>().enabled = false;
            transform.GetComponentInParent<Spawner>().playExplosion();
            Destroy(gameObject, 4f);
        }
    }

    Vector3 positionToHands()
    {
        signo = (Random.value > 0.5f);
        float x = signo ? Random.Range(0f, radius) : Random.Range(-0f, -radius);
        float y = 0;
        signo = (Random.value > 0.5f);
        float z = signo ? (Mathf.Sqrt(Mathf.Pow(radius, 2) - Mathf.Pow(x, 2)) * -1) : (Mathf.Sqrt(Mathf.Pow(radius, 2) - Mathf.Pow(x, 2)));

        Vector3 direccional = new Vector3(x,y,z) - transform.position;
        return new Vector3(x, y, z) + direccional;
    }
}
