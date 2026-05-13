using UnityEngine;
using TMPro;
using System.Collections;

public class NPCDialogue : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TMP_Text dialogueText;
    public float typingSpeed = 0.04f;

    private string[] lines;
    private int currentLine = 0;
    private Coroutine typingCoroutine;
    private bool isTyping = false;

    public virtual string[] GetLines() { return new string[] { }; }

    public void StartDialogue()
    {
        lines = GetLines();
        currentLine = 0;
        dialoguePanel.SetActive(true);
        TypeLine();
    }

    void TypeLine()
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);
        typingCoroutine = StartCoroutine(TypeEffect(lines[currentLine]));
    }

    IEnumerator TypeEffect(string line)
    {
        isTyping = true;
        dialogueText.text = "";
        foreach (char c in line)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
        isTyping = false;
    }

    void Update()
    {
        if (!dialoguePanel.activeSelf) return;

        if (Input.GetMouseButtonDown(0))
        {
            if (isTyping)
            {
                StopCoroutine(typingCoroutine);
                dialogueText.text = lines[currentLine];
                isTyping = false;
            }
            else
            {
                currentLine++;
                if (currentLine >= lines.Length)
                    Close();
                else
                    TypeLine();
            }
        }
    }

    void Close()
    {
        dialoguePanel.SetActive(false);
        FindObjectOfType<CamCam2D>()?.backToNormal();
    }
}