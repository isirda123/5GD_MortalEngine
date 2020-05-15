using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
public class PopUpResourceHarvest : MonoBehaviour
{
    [SerializeField] TextMeshPro textMeshPro;
    [SerializeField] SpriteRenderer image;

    public void SetImage(ResourcesInfos resourcesInfos)
    {
        image.sprite = resourcesInfos.sprite;
    }

    public void SetText(int stockChange)
    {
        textMeshPro.text = "+" + stockChange.ToString();
    }


    void Start()
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
        textMeshPro.color = new Color(textMeshPro.color.r, textMeshPro.color.g, textMeshPro.color.b, 0);

        Sequence sequence = DOTween.Sequence();
        sequence.Append(image.DOFade(1, 0.5f));
        sequence.AppendInterval(1f);
        sequence.Append(image.DOFade(0, 0.25f));

        Sequence sequenceText = DOTween.Sequence();
        sequenceText.Append(textMeshPro.DOFade(1, 0.5f));
        sequenceText.AppendInterval(1f);
        sequenceText.Append(textMeshPro.DOFade(0, 0.25f));

        sequenceText.OnComplete(() => Destroy(gameObject));
    }
}
