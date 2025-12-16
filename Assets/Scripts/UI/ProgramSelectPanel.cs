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

        RefreshCards();
    }

    private void RefreshCards()
    {
        for(int i=0, iMax = Count; i<iMax; i++)
        {
            var targetIndex = _scrollOffset + i;
            
            var data = _programList[targetIndex];
            _cards[i].Bind(data);
            
            if(_selectedIndex == targetIndex)
            {
                _cards[i].SetSelected(true);
                continue;
            }

            _cards[i].SetHighlightened(targetIndex == _focusIndex);
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
            _focusIndex = newFocus;
            RefreshCards();
        }
        else
        {
            _cards[_focusIndex - _scrollOffset].SetHighlightened(false);
            _cards[newFocus - _scrollOffset].SetHighlightened(true);
            _focusIndex = newFocus;
        }
    }

    /// <summary>
    /// Confirms the currently focused item as selected.<br/>
    /// If already selected, deselects it.<br/>
    /// Invokes OnSelected event with the selected program data.
    /// </summary>
    public void ConfirmSelection()
    {
        if(_selectedIndex >= 0 &&
            _selectedIndex >= _scrollOffset && _selectedIndex < _scrollOffset + Count)
        {
            _cards[_selectedIndex - _scrollOffset].SetSelected(false);
        }

        if(_selectedIndex == _focusIndex)
        {
            return;
        }

        _selectedIndex = _focusIndex;
        _cards[_focusIndex - _scrollOffset].SetSelected(true);

        OnSelected?.Invoke(_programList[_selectedIndex]);
    }
}
