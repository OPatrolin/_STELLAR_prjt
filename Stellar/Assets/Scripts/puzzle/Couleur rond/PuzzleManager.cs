using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField] private Placement[] slots; // Glisse tes 7 emplacements ici



    [Header("Emplacements")]
    [SerializeField] private Placement placement1;
    [SerializeField] private Placement placement2;
    [SerializeField] private Placement placement3;
    [SerializeField] private Placement placement4;
    [SerializeField] private Placement placement5;
    [SerializeField] private Placement placement6;

    [Header("Billes")]
    [SerializeField] private GameObject billeBleu;   
    [SerializeField] private GameObject billeVert; 
    [SerializeField] private GameObject billeViolet; 
    [SerializeField] private GameObject billeJaune;  
    [SerializeField] private GameObject billeRose;

    [Header("Victoire")]
    [SerializeField] private Item objetVictoire;
    [SerializeField] private itemS0 itemADonner; // L'itemS0 à donner au joueur



    private void Update()
    {
        if ( VerifierVictoire()== true)
        {
            
            Debug.Log("PUZZLE RÉSOLU !");

            // Apparaît dans la scène pour que le joueur le ramasse
            objetVictoire.gameObject.SetActive(true);
        }
    }



    private bool VerifierVictoire()
    {
        return placement1.CurrentMarble == billeViolet &&
               placement2.CurrentMarble == billeRose &&
               placement3.IsEmpty &&
               placement4.CurrentMarble == billeVert &&
               placement5.CurrentMarble == billeBleu &&
               placement6.CurrentMarble == billeJaune;
    }


    public int EmptySlotsCount()
    {
        int count = 0;
        foreach (var slot in slots)
            if (slot.IsEmpty) count++;
        return count; // Doit retourner 2 (7 emplacements - 5 billes)
    }

    public bool AreAllMarblesPlaced()
    {
        // Puzzle résolu si seulement 2 emplacements vides
        return EmptySlotsCount() == 2;
    }
}
