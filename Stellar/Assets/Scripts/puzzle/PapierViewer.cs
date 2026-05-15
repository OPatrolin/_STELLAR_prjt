using UnityEngine;
using UnityEngine.UI;

public class PaperViewer : MonoBehaviour
{
    public static PaperViewer Instance { get; private set; }

    [Header("UI")]
    public Image paperImage;
    public GameObject backButton;

    void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);
        if (backButton != null) backButton.SetActive(false);
    }

    public void Show(Sprite sprite)
    {
        paperImage.sprite = sprite;
        gameObject.SetActive(true);
        if (backButton != null) backButton.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        if (backButton != null) backButton.SetActive(false);
    }
}