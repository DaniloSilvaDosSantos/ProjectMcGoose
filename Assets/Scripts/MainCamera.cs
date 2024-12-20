using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    float shakeAmount = 0f;

    private Vector3 targetpos;

    public float shakereduct = 30f;

    void Start()
    {
        targetpos = transform.position;
    }

    void FixedUpdate()
    {
        if (shakeAmount > 0f)
        {
            float  finalShake = shakeAmount - (shakereduct) * Time.deltaTime;

            shakeAmount = Mathf.Clamp(finalShake, 0f, 100f);
        }

        transform.position = Vector3.Slerp(transform.position, targetpos, 0.1f);

        transform.position += new Vector3(Random.Range(-1.0f, 1.0f) * (shakeAmount)/200, Random.Range(-1.0f, 1.0f) * (shakeAmount)/200, 0);
    }

    public void CauseRumble(float amount)
    {
        shakeAmount += amount;
    }
}
