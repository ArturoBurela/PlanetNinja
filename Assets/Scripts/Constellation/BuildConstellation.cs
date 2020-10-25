using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildConstellation : MonoBehaviour
{
    public GameObject linePrefab;
    GameObject newGO;
    Transform[] _nextStar,nextStar;
    Transform lastStar;
    bool connected,lineas;
    bool[] connectedStar;
    Dictionary<int, bool> dict;
    Vector3[] allPositionStars;
    void Start()
    {
        connected = false;
        dict = new Dictionary<int, bool>();
        lineas = false;
        _nextStar = gameObject.GetComponentsInChildren<Transform>();
        nextStar = new Transform[_nextStar.Length - 1];
        int i = 0;
        foreach(var star in _nextStar)
        {

            if (!star.CompareTag("Constelacion"))
            {
                nextStar[i] = star;
                i++;
                if (star.CompareTag("Fin"))
                    lastStar = star;
            }
        }
        _nextStar = nextStar;
        allPositionStars = new Vector3[_nextStar.Length + 1];
        print(_nextStar.Length);
        print(allPositionStars.Length);
        print(lastStar.name);
    }
    public Dictionary<int,bool> getDictionary()
    {
        return dict;
    }
    void setLineas()
    {
        lineas = true;
    }
    void Update()
    {
        if (connected==false)
        {
            for (int i=0; i < _nextStar.Length; i++)
            {
                if (_nextStar[i].CompareTag("Fin"))
                {
                    lastStar = _nextStar[i];
                }
                if (!dict.ContainsKey(i))
                {
                    dict.Add(i, _nextStar[i].GetComponent<StarProperties>().IsConnected());
                }
                else
                {
                    dict[i] = _nextStar[i].GetComponent<StarProperties>().IsConnected();
                    print(i+" "+dict[i]);
                }
                
            }
            if (!dict.ContainsValue(false))
            {
                connected = true;
            }
        }

        else
        {
            //print("Osa Mayor");
            if(lineas == false)
            {
                GenerateConstellation();
            }
            else
            {
                //DeleteTrail();

            }
                
        }
    }
    void DeleteTrail()
    {
        for (int i = 0; i < _nextStar.Length; i++)
        {
            _nextStar[i].DetachChildren();
        }
    }
    void GenerateConstellation()
    {
        Vector3[] allPositionStars = new Vector3[_nextStar.Length+1];
        if (allPositionStars.Length >= 2)
        {
            for(int i = 0; i < allPositionStars.Length-1; i++)
            {
                allPositionStars[i] = _nextStar[i].position;
                
            }
            allPositionStars[allPositionStars.Length - 1] = lastStar.position;

            DeleteTrail();
            SpawnLineGenerator(allPositionStars);
            setLineas();
        }
    }




    void SpawnLineGenerator(Vector3[] starPositions)
    {
        GameObject newLine = Instantiate(linePrefab);
        LineRenderer lRend = newLine.GetComponent<LineRenderer>();

        lRend.positionCount = starPositions.Length;
        lRend.loop = false;
        lRend.SetPositions(starPositions);
        lRend.transform.parent = gameObject.transform;
    }
}
