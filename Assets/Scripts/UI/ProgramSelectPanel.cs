using System;
using System.Collections.Generic;
using UnityEngine;

public class ProgramSelectPanel : SelectPanel<ProgramData, ProgramCardView>
{
    /// <summary>
    /// Confirmed selection index (-1 = none)
    /// </summary>
    private int _selectedIndex = -1;

    public override void Initialize(List<ProgramData> availableDatas, Action<ProgramData> onSelected, Action<ProgramData> onCancled)
    {
        base.Initialize(availableDatas, onSelected, onCancled);

        _selectedIndex = -1;
        _scrollOffset = _focusIndex  = 0;

        BindCards();
        RefreshCardsState();
    }

    /// <summary>
    /// Confirms the currently focused item as selected.<br/>
    /// If already selected, deselects it.<br/>
    /// Invokes OnSelected event with the selected program data.
    /// </summary>
    public override void ConfirmSelection()
    {
        if (_selectedIndex == _focusIndex)
        {
            _selectedIndex = -1;
            RefreshCardsState();
            return;
        }

        _selectedIndex = _focusIndex;

        base.ConfirmSelection();
    }

    protected override bool IsSelected(int targetIndex)
    {
        return _selectedIndex == targetIndex;
    }
}
