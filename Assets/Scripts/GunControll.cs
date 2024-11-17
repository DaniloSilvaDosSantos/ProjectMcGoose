using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    void Start()
    {
        canShot = true;
        isAiming = false;
        lr = GetComponent<LineRenderer>();
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
                }
            }
        }
        else
        {
            //faz algo talvez
        }
    }

    void DrawTrajectoryLine(Transform originPoint, float angleDegree)
    {
        float angleRadians = angleDegree * Mathf.Deg2Rad;

        float x = Mathf.Cos(angleRadians); 
        float y = Mathf.Sin(angleRadians);

        RaycastHit2D hit = Physics2D.Raycast(originPoint.position, new Vector2(-x, -y), distanceTrajectoryLine * 1000f, wallLayer);
        //RaycastHit2D hit2;

        if (hit.collider != null)
        {
            lr.SetPosition(0, originPoint.position);
            lr.SetPosition(1, hit.point);
            
            if(hit.normal.x != 0) x = -x;
            if(hit.normal.y != 0) y = -y;

            /*hit2 = Physics2D.Raycast(
                hit.point + new Vector2(-x, -y) * 1f, new Vector2(-x, -y), distanceTrajectoryLine - 1f, wallLayer
            );
            
            if(hit2.collider !=  null)
            {
                lr.SetPosition(2, hit2.point);
            }
            else
            {
                lr.SetPosition(2, hit.point + new Vector2(-x, -y) * distanceTrajectoryLine);
            }*/

        }
        else
        {
            
            lr.SetPosition(0, originPoint.position);
            lr.SetPosition(1, (Vector2)originPoint.position + new Vector2(-x, -y) * distanceTrajectoryLine * 1000f);
        }

    }
}
