using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CoinController : MonoBehaviour
{

    [FormerlySerializedAs("RotationSpeed")] public float rotationSpeed;
    

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, Time.deltaTime * rotationSpeed);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().CoinCollected();
            Destroy(gameObject);
        }
    }
}
