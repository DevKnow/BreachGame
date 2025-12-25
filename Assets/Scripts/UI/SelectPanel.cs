using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class SelectPanel<TData, TCardView> : MonoBehaviour
    where TCardView : MonoBehaviour, IBindable<TData>
{
    /// <summary>
    ///
    /// </summary>
    [SerializeField] protected TCardView[] _cards;

    /// <summary>
    ///  All available data
    /// </summary>
    protected List<TData> _dataList;

    protected event Action<TData> OnSelected;
    protected event Action<TData> OnCancled;

    /// <summary>
    /// Scroll position (starts at 0)
    /// </summary>
    protected int _scrollOffset;
    /// <summary>
    /// Currently highlighted index (keyboard/mouse hover)
    /// </summary>
    protected int _focusIndex = -1;

    private int _count = 0;
    public int Count
    {
        get
        {
            if (_count == 0)
            {
                _count = _cards.Length;
            }

            return _count;
        }
    }

    public virtual void Initialize(List<TData> availableDatas, Action<TData> onSelected, Action<TData> onCancled)
    {
        _dataList = availableDatas;

        _scrollOffset = 0;

        OnSelected = onSelected;
        OnCancled = onCancled;
    }

    public void MoveFocus(int direction)
    {
        var newFocus = Mathf.Clamp(_focusIndex + direction, 0, _dataList.Count - 1);
        if (newFocus == _focusIndex)
            return;

        var isChanged = false;
        if (_scrollOffset > newFocus)
        {
            _scrollOffset = newFocus;
            isChanged = true;
        }
        else if (newFocus >= _scrollOffset + Count)
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

    protected void BindCards()
    {
        for (int i = 0, iMax = Count; i < iMax; i++)
        {
            var targetIndex = _scrollOffset + i;

            var data = _dataList[targetIndex];
            _cards[i].Bind(data);
        }
    }

    protected void RefreshCardsState()
    {
        for (int i = 0, iMax = Count; i < iMax; i++)
        {
            var targetIndex = _scrollOffset + i;
            _cards[i].SetState(
               isSelected: IsSelected(targetIndex),
               isFocused: targetIndex == _focusIndex
           );
        }
    }

    protected abstract bool IsSelected(int targetIndex);

    public virtual void ConfirmSelection()
    {
        OnSelected?.Invoke(_dataList[_focusIndex]);

        RefreshCardsState();
    }

    protected void CancleData(TData data)
    {
        OnSelected?.Invoke(data);
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
