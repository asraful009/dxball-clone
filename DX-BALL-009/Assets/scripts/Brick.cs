using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public int lifes = 1;
    public int points = 10;
    public Material hitMatireal;
    Material _ownMatireal;
    Renderer _ownRender;
    // Start is called before the first frame update
    void Start()
    {
        //hitMatireal = GetComponents<Material>();
        _ownRender = GetComponent<Renderer>();
        _ownMatireal = _ownRender.sharedMaterial;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision) 
    {
        lifes--;

        if (lifes<=0) 
        {
            GameManager.Instance.Score += points;
            Destroy(gameObject);
        }
        if(hitMatireal) 
        {
            _ownRender.sharedMaterial = hitMatireal;
        }
        Invoke("RestoreMaterial", 0.14f);
    }

    void RestoreMaterial() 
    {
        _ownRender.sharedMaterial = _ownMatireal;
    }

}
