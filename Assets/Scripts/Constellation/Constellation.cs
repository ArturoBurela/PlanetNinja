using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constellation : MonoBehaviour {

    public GameObject trailPrefab;
    GameObject thisTrail;
    Transform lastStar;
    Vector3 startPos;
    Plane objPlane;
    public Transform[] _nextStar;
    float _distanceNextStar;
    BuildConstellation build;
    public int vecesVisitado;
    void Start()
    {
        vecesVisitado = 0;
        objPlane = new Plane(Camera.main.transform.forward * -1, transform.position);
        _distanceNextStar = Vector3.Distance(_nextStar[0].position, transform.position);
        
        //trailPrefab.GetComponent<LineRenderer>().SetPosition(0, transform.position);
        //trailPrefab.GetComponent<LineRenderer>().SetPosition(1, _nextStar[0].position);
    }


    void Update()
    {
        if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) || Input.GetMouseButtonDown(0))
        {
            thisTrail = Instantiate(trailPrefab,transform.position,Quaternion.identity);
            Ray mRay = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            float rayDistance;
            if (objPlane.Raycast(mRay, out rayDistance))
                startPos = mRay.GetPoint(rayDistance);
        }
        else if (((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)|| Input.GetMouseButton(0)))
        {

            Ray mRay = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            float rayDistance;
            if (objPlane.Raycast(mRay, out rayDistance))
                thisTrail.transform.position = mRay.GetPoint(rayDistance);
        }
        else if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) || Input.GetMouseButtonUp(0))
        {
            //print(_distanceNextStar-(_distanceNextStar - Vector3.Distance(thisTrail.transform.position, _nextStar[0].position)));
            if (Vector3.Distance(thisTrail.transform.position, _nextStar[0].position) <= (_distanceNextStar - Vector3.Distance(thisTrail.transform.position, _nextStar[0].position)))
            {
                if (!_nextStar[0].GetComponent<StarProperties>().IsFinalStar() && !gameObject.GetComponent<StarProperties>().IsFinalStar())
                {
                    lastStar = gameObject.transform;
                    thisTrail.transform.parent = gameObject.transform;
                    _nextStar[0].GetComponent<Constellation>().enabled = true;
                    gameObject.GetComponent<StarProperties>().SetConnected(true);
                    _nextStar[0].GetComponent<StarProperties>().SetConnected(true);
                    GetComponent<Constellation>().enabled = false;
                    print("no es la siguiente la final");
                }
                else if (_nextStar[0].GetComponent<StarProperties>().IsFinalStar() && transform.CompareTag("Untagged"))
                {
                    lastStar = gameObject.transform;
                    thisTrail.transform.parent = gameObject.transform;
                    _nextStar[0].GetComponent<Constellation>().enabled = true;
                    gameObject.GetComponent<StarProperties>().SetConnected(true);
                    GetComponent<Constellation>().enabled = false;
                }
                else if (_nextStar[0].GetComponent<StarProperties>().IsFinalStar() && transform.CompareTag("penultimo"))
                {
                    lastStar = gameObject.transform;
                    thisTrail.transform.parent = gameObject.transform;
                    _nextStar[0].GetComponent<Constellation>().enabled = true;
                    gameObject.GetComponent<StarProperties>().SetConnected(true);
                    _nextStar[0].GetComponent<StarProperties>().SetConnected(true);
                    GetComponent<Constellation>().enabled = false;
                }
                if (gameObject.GetComponent<StarProperties>().IsFinalStar()&& _nextStar[0].CompareTag("Untagged"))
                {
                    lastStar = gameObject.transform;
                    thisTrail.transform.parent = gameObject.transform;
                    _nextStar[0].GetComponent<Constellation>().enabled = true;
                    GetComponent<Constellation>().enabled = false;
                }
                else if (gameObject.GetComponent<StarProperties>().IsFinalStar() && lastStar.CompareTag("penultimo"))
                {
                    thisTrail.transform.parent = gameObject.transform;
                    //_nextStar[0].GetComponent<Constellation>().enabled = true;
                    //gameObject.GetComponent<StarProperties>().SetConnected(true);
                    //_nextStar[0].GetComponent<StarProperties>().SetConnected(true);
                    GetComponent<Constellation>().enabled = false;
                }

            }
            else
            {
                Destroy(thisTrail);
            }
        }
    }
}
