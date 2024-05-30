using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    public float forcePower;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnTrampoline(other.gameObject);
        }
    }

    private void OnTrampoline(GameObject obj)
    {
        obj.GetComponent<Rigidbody>().AddForce(Vector2.up*forcePower, ForceMode.Impulse);
    }
}
