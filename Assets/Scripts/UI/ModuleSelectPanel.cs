using System;
using System.Collections.Generic;

public class ModuleSelectPanel : SelectPanel<CommandData, ModuleCardView>
{
    private List<int> _selectedIndices;

    private const int MAX_SELECTION = 2;

    public override void Initialize(List<CommandData> availableDatas, Action<CommandData> onSelected, Action<CommandData> onCancled)
    {
        base.Initialize(availableDatas, onSelected, onCancled);

        _selectedIndices ??= new List<int>(MAX_SELECTION);

        _selectedIndices.Clear();

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
