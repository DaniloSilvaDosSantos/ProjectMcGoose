using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    public void DestroyHimself()
    {
        Destroy(gameObject);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Object"))
        {
            Destroy(other.gameObject);
        }
        else if(other.gameObject.layer == LayerMask.NameToLayer("Star"))
        {
            StartsHud startsHud = GameObject.Find("Stars").GetComponent<StartsHud>();
            startsHud.starsAll[2] = true;

            Destroy(other.gameObject);
        }
    }
}
