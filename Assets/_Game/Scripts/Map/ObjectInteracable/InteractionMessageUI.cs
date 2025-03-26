using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionMessageUI : MonoBehaviour
{
    public GameObject messageUI;
    public TMPro.TextMeshProUGUI messageText;
    public float messageDuration = 3f;

    private void Start()
    {
        messageUI.SetActive(false);
    }
    public void ShowMessage(string message)
    {
        messageText.text = message;
        messageUI.SetActive(true);
        StartCoroutine(HideMessage());
    }

    public IEnumerator HideMessage()
    {
        yield return new WaitForSeconds(messageDuration);
        messageUI.SetActive(false);
    }
}
