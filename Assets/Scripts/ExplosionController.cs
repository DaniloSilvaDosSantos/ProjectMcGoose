//using System.Collections;
//using System.Collections.Generic;
//using Unity.VisualScripting;
using Unity.VisualScripting;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip collectingStar;
    [SerializeField] private AudioClip[] pinguimScream;

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
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
            audioSource.PlayOneShot(collectingStar);
            StartsHud startsHud = GameObject.Find("Stars").GetComponent<StartsHud>();
            startsHud.starsAll[2] = true;

            Destroy(other.gameObject);
        }
        else if(other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            audioSource.PlayOneShot(pinguimScream[Random.Range(0, pinguimScream.Length)]);
        }
    }
}
