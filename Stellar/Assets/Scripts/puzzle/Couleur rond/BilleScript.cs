using UnityEngine;
using System.Collections.Generic;

public class BilleScript : MonoBehaviour
{
    // public int billeIndex;
    //public Placement currentPlacement;

    // public BilleScript billeSelectionnee = null; // bille actuellement sélectionnée

    private Rigidbody2D rb;
    private Vector2 startPosition;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
    }

    private void FixedUpdate()
    {
        // Annule toute vélocité parasite
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
    }





}