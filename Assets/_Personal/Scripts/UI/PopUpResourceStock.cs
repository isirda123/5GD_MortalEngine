using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class PopUpResourceStock : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMeshPro;
    [SerializeField] Image image;
    [SerializeField] Image background;

    public void SetImage(ResourcesInfos resourcesInfos)
    {
        image.sprite = resourcesInfos.sprite;
    }

    public void SetText(int stockChange)
    {
        textMeshPro.text = stockChange.ToString();
    }

    private void Start()
    {
        background.color = new Color(background.color.r, background.color.g, background.color.b, 0);
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
        textMeshPro.color = new Color(textMeshPro.color.r, textMeshPro.color.g, textMeshPro.color.b, 0);

        Sequence sequenceBackground = DOTween.Sequence();
        sequenceBackground.Append(background.DOFade(1, 0.5f));
        sequenceBackground.AppendInterval(1f);
        sequenceBackground.Append(background.DOFade(0, 0.25f));

        Sequence sequence = DOTween.Sequence();
        sequence.Append(image.DOFade(1, 0.5f));
        sequence.AppendInterval(1f);
        sequence.Append(image.DOFade(0, 0.25f));

        Sequence sequenceText = DOTween.Sequence();
        sequenceText.Append(textMeshPro.DOFade(1, 0.5f));
        sequenceText.AppendInterval(1f);
        sequenceText.Append(textMeshPro.DOFade(0, 0.25f));

        sequenceBackground.OnComplete(() => Destroy(gameObject));
    }
}
