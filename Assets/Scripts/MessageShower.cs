using System.Collections;
using TMPro;
using UnityEngine;

public class MessageShower : MonoBehaviour
{
    public GameObject MessageObject;
    public TextMeshProUGUI MessageTMP; // Use TextMeshProUGUI for UI text
    public Canvas canvas;

    public Vector3 originalPosition;
    public Vector3 hiddenPosition;

    private void Awake()
    {
        MessageObject = this.gameObject;
        MessageTMP = MessageObject.GetComponent<TextMeshProUGUI>(); // Correct type
        ShowText("Left Click to set Start \n Right Click to set Goal");
    }

    private void Start()
    {
        canvas = GetComponentInParent<Canvas>();
        originalPosition = MessageTMP.transform.localPosition + new Vector3 (0.0f,0.0f,-10.0f);
        hiddenPosition = originalPosition + Vector3.up * 50f; 
    }

    public void ShowText(string message)
    {
        StopAllCoroutines();
        StartCoroutine(ShowTextAnim(message));
    }

    private IEnumerator ShowTextAnim(string txt)
    {
        MessageTMP.text = txt;
        Color c = MessageTMP.color;
        c.a = 0f;
        MessageTMP.color = c;

        // Slide from above
        MessageTMP.transform.localPosition = hiddenPosition;

        float timeIn = 0.5f;
        float timeOut = 0.5f;
        float curTime = 0f;

        // Fade In + Slide In
        while (curTime < timeIn)
        {
            curTime += Time.deltaTime;
            float t = Mathf.Clamp01(curTime / timeIn);

            c.a = t;
            MessageTMP.color = c;

            MessageTMP.transform.localPosition = Vector3.Lerp(hiddenPosition, originalPosition, t);

            yield return null;
        }

        // Hold
        yield return new WaitForSeconds(1f);

        curTime = 0f;

        // Fade Out + Slide Out
        while (curTime < timeOut)
        {
            curTime += Time.deltaTime;
            float t = Mathf.Clamp01(curTime / timeOut);

            c.a = 1 - t;
            MessageTMP.color = c;

            MessageTMP.transform.localPosition = Vector3.Lerp(originalPosition, hiddenPosition, t);

            yield return null;
        }

        MessageTMP.text = " ";

        MessageTMP.transform.localPosition = originalPosition;
    }

}