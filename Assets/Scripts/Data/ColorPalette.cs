using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName= "ColorPalette", menuName ="Breach/ColorPalette")]
public class ColorPalette : ScriptableObject
{
    [Header("Card background")]
    [SerializeField] Color _cardBackground;
    [SerializeField] Color _patchBackground;

    [Header("Border Colors")]
    [SerializeField] Color _borderDefault;
    [SerializeField] Color _borderSelected;
    [SerializeField] Color _borderHighlightened;
    [SerializeField] Color _borderLocked;

    [Header("Text Colors")]
    [SerializeField] Color _defaultText;
    [SerializeField] Color _textLabel;
    [SerializeField] Color _textMetaInfo;
    [SerializeField] Color _textInt;
    [SerializeField] Color _textPwr;

    static ColorPalette _instance;
    private static ColorPalette Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = Resources.Load<ColorPalette>("ColorPalette");
            }

            return _instance;
        }
    }

    public static Color Selected => Instance._borderSelected;
    public static Color Highlightened => Instance._borderHighlightened;
    public static Color BorderDefault => Instance._borderDefault;
    public static Color LockedBorder => Instance._borderLocked;
}
