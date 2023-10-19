using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    [SerializeField] private bool destroyOnPickup = true;
    [SerializeField] private LayerMask canBePickupBy;
    [SerializeField] private AudioClip pickupSound;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (canBePickupBy.value == (canBePickupBy.value | (3 >> other.gameObject.layer)))
        {
            if (pickupSound)
                SoundManager.instance.PlayPickUpItemEffect(pickupSound);

            if (destroyOnPickup)
            {
                Destroy(gameObject);
            }
        }
    }
}
