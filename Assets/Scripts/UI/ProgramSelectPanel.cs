using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgramSelectPanel : MonoBehaviour
{
    /// <summary>
    ///
    /// </summary>
    [SerializeField] ProgramCardView[] _cards;

    /// <summary>
    ///  All available program data
    /// </summary>
    private List<ProgramData> _programList;
    /// <summary>
    /// Scroll position (starts at 0)
    /// </summary>
    private int _scrollOffset;
    /// <summary>
    /// Currently highlighted index (keyboard/mouse hover)
    /// </summary>
    private int _focusIndex;
    /// <summary>
    /// Confirmed selection index (-1 = none)
    /// </summary>
    private int _selectedIndex = -1;

    public event Action<ProgramData> OnSelected;

    private int _count = 0;
    private int Count
    {
        get
        {
            if(_count == 0)
            {
                _count = _cards.Length;
            }

            return _count;
        }
    }

    public void Initialize(List<ProgramData> programs)
    {
        _programList = programs;

        _selectedIndex = -1;
        _scrollOffset = _focusIndex  = 0;

        BindCards();
        RefreshCardsState();
    }

    private void BindCards()
    {
        for(int i=0, iMax = Count; i<iMax; i++)
        {
            var targetIndex = _scrollOffset + i;
            
            var data = _programList[targetIndex];
            _cards[i].Bind(data);
        }
    }

    private void RefreshCardsState()
    {
        for (int i = 0, iMax = Count; i < iMax; i++)
        {
            var targetIndex = _scrollOffset + i;
            _cards[i].SetState(
               isSelected: targetIndex == _selectedIndex,
               isFocused: targetIndex == _focusIndex
           );
        }
    }

    /// <summary>
    /// Moves the focus highlight up or down.
    /// </summary>
    /// <param name="direction">-1 for up, +1 for down.</param>
    public void MoveFocus(int direction)
    {
        var newFocus = Mathf.Clamp(_focusIndex + direction, 0, _programList.Count - 1);
        if (newFocus == _focusIndex)
            return;

        var isChanged = false;
        if(_scrollOffset > newFocus)
        {
            _scrollOffset = newFocus;
            isChanged = true;
        }
        else if(newFocus >= _scrollOffset + Count)
        {
            _scrollOffset = newFocus - Count + 1;
            isChanged = true;
        }

        if (isChanged)
        {
            BindCards();
        }

        _focusIndex = newFocus;

        RefreshCardsState();
    }

    /// <summary>
    /// Confirms the currently focused item as selected.<br/>
    /// If already selected, deselects it.<br/>
    /// Invokes OnSelected event with the selected program data.
    /// </summary>
    public void ConfirmSelection()
    {
        if(_selectedIndex == _focusIndex)
        {
            _selectedIndex = -1;
            RefreshCardsState();
            return;
        }

        _selectedIndex = _focusIndex;
        
        RefreshCardsState();

        OnSelected?.Invoke(_programList[_selectedIndex]);
    }

    /// <summary>
    /// Called when this panel becomes active.
    /// </summary>
    public void OnEnter()
    {
        _focusIndex = _scrollOffset;
        RefreshCardsState();
    }

    /// <summary>
    /// Called when leaving this panel.
    /// </summary>
    public void OnExit()
    {
        _focusIndex = -1;
        RefreshCardsState();
    }
}
