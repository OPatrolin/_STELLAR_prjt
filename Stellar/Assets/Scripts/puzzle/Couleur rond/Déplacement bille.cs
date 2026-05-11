using System;
using System.Collections.Generic;
using UnityEngine;

public class Déplacementbille : MonoBehaviour
{
    [Header("Références")]
    [SerializeField] private Placement placementCentral;
    [SerializeField] private Placement[] placementsExterieurs;

    [Header("Seuil de détection")]
    [SerializeField] private float distanceMax = 100f;
    [SerializeField] private float longueurBarre = 3f;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            int billeLayer = LayerMask.GetMask("Bille");
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, Mathf.Infinity, billeLayer);

           

            if (hit.collider != null)
            {
                BilleScript bille = hit.collider.GetComponent<BilleScript>();
                if (bille != null)
                    TenterDeplacement(bille);
            }
        }
    }

   


    private Vector2 GetExtremiteSus()
    {
        return (Vector2)transform.position +
               (Vector2)(transform.rotation * new Vector2(0, longueurBarre));
    }

    private Placement TrouverEmplacementAligne()
    {
        Vector2 extremite = GetExtremiteSus();
        Placement plusProche = null;
        float distanceMin = float.MaxValue;

        foreach (Placement p in placementsExterieurs)
        {
            float dist = Vector2.Distance(extremite, p.transform.position);
            

            if (dist < distanceMin)
            {
                distanceMin = dist;
                plusProche = p;
            }
        }

        
        return distanceMin < distanceMax ? plusProche : null;
    }

    private void TenterDeplacement(BilleScript bille)
    {
        Placement emplacementAligne = TrouverEmplacementAligne();

        if (emplacementAligne == null)
        {
          
            return;
        }

        bool billeAuCentral = placementCentral.CurrentMarble == bille.gameObject;
        bool billeAlignee = emplacementAligne.CurrentMarble == bille.gameObject;

        if (billeAuCentral && emplacementAligne.IsEmpty)
        {
            DeplacerBille(bille.gameObject, emplacementAligne, placementCentral);
          
        }
        else if (billeAlignee && placementCentral.IsEmpty)
        {
            DeplacerBille(bille.gameObject, placementCentral, emplacementAligne);
            
        }
    }

    private void DeplacerBille(GameObject bille, Placement destination, Placement origine)
    {
        bille.transform.position = destination.transform.position;
        destination.ForcerOccupe(bille);
        origine.ForcerVide();
    }
}