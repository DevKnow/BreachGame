using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EffectItemView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _desc;

    public void SetDesc(string text)
    {
        _desc.text = $"- {text}";
    }

    public float GetPreferredHeight()
    {
        var width = _desc.rectTransform.rect.width;
        var height = _desc.GetPreferredValues(_desc.text, width, float.MaxValue).y;
        return height;
    }
}
