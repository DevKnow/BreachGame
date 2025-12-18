using UnityEngine;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class ModuleSelectPanel : SelectPanel<CommandData, ModuleCardView>
{
    /// </summary>
    [SerializeField] protected RectTransform _rect;

    private const int MAX_SELECTION = 2;

    private List<int> _selectedIndices;

    public override void Initialize(List<CommandData> availableDatas, Action<CommandData> onSelected, Action<CommandData> onCancled)
    {
        base.Initialize(availableDatas, onSelected, onCancled);

        _selectedIndices ??= new List<int>(MAX_SELECTION);
        _selectedIndices.Clear();

        BindCards();

        RefreshCardsState();
    }

    public override void ConfirmSelection()
    {
        if (IsSelected(_focusIndex))
        {
            CancleData(_dataList[_focusIndex]);
            _selectedIndices.Remove(_focusIndex);
            RefreshCardsState();
            return;
        }

        if (_selectedIndices.Count >= MAX_SELECTION)
        {
            CancleData(_dataList[_selectedIndices[0]]);
            _selectedIndices.RemoveAt(0);
        }

        _selectedIndices.Add(_focusIndex);

        base.ConfirmSelection();
    }

    protected override bool IsSelected(int targetIndex)
    {
        return _selectedIndices.Contains(targetIndex);
    }
}
