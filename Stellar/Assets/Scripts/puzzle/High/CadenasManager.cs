using UnityEngine;

public class CadenasManager : MonoBehaviour
{
    public static CadenasManager instance;

    [Header("Les 4 cases dans l'ordre")]
    public CaseCode[] cases; // glisse Case1, Case2, Case3, Case4

    [Header("Coffre")]
    public GameObject coffreFerme;
    public GameObject coffreOuvert;

    // La solution : index 0,1,2,3 dans chaque case (= symbole 1,2,3,4)
    private int[] solution = { 0, 1, 2, 3 };

    void Awake()
    {
        instance = this;
        coffreOuvert.SetActive(false);
    }

    public void VerifierCode()
    {
        for (int i = 0; i < cases.Length; i++)
        {
            if (cases[i].GetIndex() != solution[i])
                return; // pas bon, on sort
        }

        // Bonne combinaison !
        Debug.Log("Cadenas ouvert !");
        coffreFerme.SetActive(false);
        coffreOuvert.SetActive(true);
    }
}