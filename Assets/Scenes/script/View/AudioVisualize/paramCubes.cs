using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paramCubes : MonoBehaviour
{
    public int _band;
    public float _startScale, _scaleMultiplier;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //float height = GetComponentInParent<audioPeer>()._freqBand[_band];
        transform.localScale = new Vector2(transform.localScale.x, (GetComponentInParent<audioPeer>()._freqBand[_band] * _scaleMultiplier) + _startScale);
    }
}
