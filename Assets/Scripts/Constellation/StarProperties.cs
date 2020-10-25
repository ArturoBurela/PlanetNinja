using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarProperties : MonoBehaviour {

    bool connected, connectedFinal;
    bool isFinal, isInitial;

	// Use this for initialization
	void Start () {
        connected = false;
        connectedFinal = false;
        if (gameObject.CompareTag("Fin"))
        {
            isFinal = true;
        }
        else
        {
            isFinal = false;
        }
        if (gameObject.CompareTag("Inicio"))
        {
            isInitial = true;
        }
        else
        {
            isInitial = false;
        }
    }

    public bool IsFinalStar()
    {
        return isFinal;
    }
    public bool IsInitialStar()
    {
        return isInitial;
    }

    public void SetConnected(bool con)
    {
        connected = con;
    }
    public void SetConnectedFinal()
    {
        connectedFinal = true;
    }

    public bool IsConnected()
    {
        return connected;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
