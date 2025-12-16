using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgramCardView : MonoBehaviour
{
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _status;
    [SerializeField] private TMP_Text _pid;
    [SerializeField] private TMP_Text _int;
    [SerializeField] private TMP_Text _pwrText;
    [SerializeField] private Image _border;

    private ProgramData _data;
    private bool _locked = false;

    public void Bind(ProgramData data)
    {
        _data = data;
        // TODO
        _locked = false;
        SetLocked();
    }

    public void SetSelected(bool selected)
    {
        if (selected)
        {
            _border.color = ColorPalette.Selected;
            _status.text = "[SELECTED]";
            return;
        }

        SetLocked();
    }

    public void SetLocked()
    {
        if (_locked)
        {
            _border.color = ColorPalette.LockedBorder;
            _status.text = "[LOCKED]";
            return;
        }

        _border.color = ColorPalette.BorderDefault;
        _status.text = "[UNLOCKED]";
    }
}
