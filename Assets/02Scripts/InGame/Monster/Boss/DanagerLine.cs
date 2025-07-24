using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DanagerLine : MonoBehaviour
{
    public Image[] images;
    void Start()
    {
        images = GetComponentsInChildren<Image>();
        for (int i = 0; i < images.Length; i++)
        {
            images[i].color = new Color(1, 0, 0, 0.7f);
        }
        Destroy(gameObject, 2f);
        StartCoroutine(DangerLine());
    }



    IEnumerator DangerLine()
    {
        float j = 5;
        while (j < 80)
        {
            for (int i = 0; i < images.Length; i++)
            {
                images[i].rectTransform.sizeDelta = new Vector2(5, j);
            }
            yield return new WaitForSeconds(0.007f);
            j += 0.5f;
        }
    }
}
