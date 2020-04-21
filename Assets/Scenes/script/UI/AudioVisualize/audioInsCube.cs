using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioInsCube : MonoBehaviour
{
    public GameObject _sampleCubePre;
    GameObject[] _sampleCube = new GameObject[8];

    //distance between 2 cubes
    public float distance;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 8; i++)
        {
            GameObject _instanceSampleCube = (GameObject)Instantiate(_sampleCubePre);
            _instanceSampleCube.transform.position = new Vector2 (transform.position.x + distance * (i-4),transform.position.y);
            _instanceSampleCube.transform.parent = transform;            
            _instanceSampleCube.name = "sampleCube" + i;
            _sampleCube[i] = _instanceSampleCube;
            _sampleCube[i].GetComponent<paramCubes>()._band = i;
        }

    }


}
