using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

public class ModuleCardView : MonoBehaviour, IBindable<CommandData>
{
    [Header("BorderLine")]
    [SerializeField] private Image _border;
    [SerializeField] private RectTransform _rect;
  
    [SerializeField] private CommandCardView _commandCard;
    [SerializeField] private Transform _patchParent;
    private List<PatchItemView> _patchList = new();

    public void Bind(CommandData data)
    {
        _commandCard.Bind(data);
        ClearPatchList();

        LayoutRebuilder.ForceRebuildLayoutImmediate(_rect);
    }

    public float GetPreferredHeight()
    {
        var commandHeight = _commandCard.GetPreferredHeight();

        var patchHeight = 0f;
        if(_patchList.Count > 0)
        {
            // TODO
        }

        var padding = 20f;  // VLG padding µî

        return commandHeight + patchHeight + padding;
    }

    private void ClearPatchList()
    {
        for (int i = 0, iMax = _patchList.Count; i < iMax; i++)
        {
            PoolManager.Instance.ReleasePatchItem(_patchList[i]);
        }
        _patchList.Clear();
    }

    private void AddPatchItem(PatchModel patch)
    {
        var item = PoolManager.Instance.GetPatchItem();
        item.transform.SetParent(_patchParent, false);

        var effects = patch.Data.effects;
        for (int i = 0, iMax = effects.Length; i < iMax; i++)
        {
            item.SetDesc(effects[i]);
        }

        _patchList.Add(item);
    }

    public void SetState(bool isSelected, bool isFocused)
    {
        if (isFocused)
        {
            _border.color = ColorPalette.Highlightened;
            return;
        }

        _border.color = isSelected
            ? ColorPalette.Selected
            : ColorPalette.BorderDefault;
    }
}
