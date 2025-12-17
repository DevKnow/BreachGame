using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class ProgramCardView : MonoBehaviour
{
    private const string _INT = "integrity";

    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _status;
    [SerializeField] private TMP_Text _pid;
    [SerializeField] private TMP_Text _int;
    [SerializeField] private Image _border;

    private ProgramData _data;
    private bool _locked = false;

    public void Bind(ProgramData data)
    {
        _data = data;
        _name.text = $"{data.GetName()}.exe";
        // TODO
        _pid.text = "├─ PID: 0x3A7F";
        _int.text = $"└─ {DataLoader.GetUIText(_INT).GetName()}: {data.GetInt()}";

        // TODO
        _locked = false;
    }

    public void SetState(bool isSelected, bool isFocused)
    {
        // 텍스트
        _status.text = isSelected ? "[SELECTED]" : "[UNLOCKED]";

        // Border - focused 우선
        if (_locked)
        {
            _status.text = "[LOCKED]";
            _border.color = ColorPalette.LockedBorder;
            return;
        }

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
