using UnityEngine;

public class CaseCode : MonoBehaviour
{
    [Header("Sprites dans l'ordre")]
    public Sprite[] symboles; // glisse tes 4 sprites ici dans l'Inspector

    private int indexActuel = 0;
    private SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        // Supprime cette ligne :
        // sr.sprite = symboles[0]; 
    }

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        // Démarre sur un symbole aléatoire
        indexActuel = Random.Range(0, symboles.Length);
        sr.sprite = symboles[indexActuel];
    }

    void OnMouseDown()
    {
        indexActuel = (indexActuel + 1) % symboles.Length;
        sr.sprite = symboles[indexActuel];
        CadenasManager.instance.VerifierCode();
    }

    public int GetIndex() => indexActuel;
}