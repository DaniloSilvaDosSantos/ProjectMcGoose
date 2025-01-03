//using System;
//using System.Collections;
//using System.Collections.Generic;
//using Unity.VisualScripting;
using UnityEngine;

public class GunControll : MonoBehaviour
{
    [SerializeField] private FixedJoystick joystick;
    [SerializeField] private GameObject bullet;
    [SerializeField] private string bulletSpawner;
    [SerializeField] private LayerMask wallLayer;
    private LineRenderer lr;
    [SerializeField] private float distanceTrajectoryLine = 1f;
    private Vector2 direction;
    private float angle;
    private bool canShot;
    private bool isAiming;
    private AudioSource audioSource;
    [SerializeField] private AudioClip shot;
    [SerializeField] private AudioClip cockingTheGun;
    private MainCamera mainCamera;
    [SerializeField] private float shakePotency = 100f;
    

    void Start()
    {
        canShot = true;
        isAiming = false;
        lr = GetComponent<LineRenderer>();

        audioSource = GetComponent<AudioSource>();

        joystick = FindAnyObjectByType<FixedJoystick>();

        mainCamera = GameObject.Find("Main Camera").GetComponent<MainCamera>();
    }

    private void Update()
    {
        if(canShot)
        {
            Transform bulletSpawnerTransform = gameObject.transform.Find(bulletSpawner);

            direction = joystick.Direction;

            if(direction.x != 0 || direction.y != 0)
            {
                angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            
                if(angle > -130 && angle < -45) angle = -130; //Original values 180, -45
                if(angle < 80 && angle >= -45) angle = 80;   //90, -45

                //Debug.Log(angle); //  < 80, < -130

                angle += 180f;

                transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

                DrawTrajectoryLine(bulletSpawnerTransform, angle - 10);

                if(isAiming == false) audioSource.PlayOneShot(cockingTheGun) ;

                isAiming = true;
            }
            else
            {
                if(isAiming)
                {
                    GameObject newBullet = Instantiate(bullet, bulletSpawnerTransform.position, bulletSpawnerTransform.rotation);
                    float newBulletAngle = angle + 180f;
                    newBullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, newBulletAngle -10));

                    Debug.Log("atirou");

                    lr.enabled = false;

                    isAiming = false;
                    canShot = false;

                    audioSource.PlayOneShot(shot);

                    mainCamera.CauseRumble(shakePotency);
                }
            }
        }
        /*else
        {
            //faz algo talvez
        }*/
    }

    void DrawTrajectoryLine(Transform originPoint, float angleDegree)
    {
        float angleRadians = angleDegree * Mathf.Deg2Rad;

        float x = Mathf.Cos(angleRadians); 
        float y = Mathf.Sin(angleRadians);

        RaycastHit2D hit = Physics2D.Raycast(originPoint.position, new Vector2(-x, -y), distanceTrajectoryLine, wallLayer);
        //RaycastHit2D hit2;

        if (hit.collider != null)
        {
            lr.SetPosition(0, originPoint.position);
            lr.SetPosition(1, hit.point);
        }
        else
        {
            
            lr.SetPosition(0, originPoint.position);
            lr.SetPosition(1, (Vector2)originPoint.position + new Vector2(-x, -y) * distanceTrajectoryLine);
        }

    }
}
