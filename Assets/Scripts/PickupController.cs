using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{
    public AudioClip pickupClip;
    void OnTriggerEnter2D(Collider2D other)
    {
        BirdController bird = other.GetComponent<BirdController>();
        if (bird != null)
        {
            bird.ChangeScore(1);
            Destroy(gameObject);
            bird.PlaySound(pickupClip);

        }
    }
}
