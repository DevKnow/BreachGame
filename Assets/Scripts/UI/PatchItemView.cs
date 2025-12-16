using UnityEngine;
using TMPro;
using NaughtyAttributes;
using UnityEngine.UI;

public class PatchItemView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _desc;
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private LayoutElement _layoutElement;

#if UNITY_EDITOR
   [SerializeField] private string _test;

    public void Awake()
    {
        SetDesc();
    }

    [Button("Set Desc")]
    public void SetDesc()
    {
        _desc.text = _test;
        SetSize();
    }
#endif

    public void SetDesc(string desc)
    {
        _desc.text = desc;
        SetSize();
    }

    private void SetSize()
    {
        _desc.ForceMeshUpdate();
        var height = _desc.preferredHeight + _desc.margin.y + _desc.margin.w;
        _rectTransform.sizeDelta = new Vector2(
            _rectTransform.sizeDelta.x,
            height
        );
        _layoutElement.preferredHeight = height;
    }
}