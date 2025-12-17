using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ProgramData
{
    public string id;
    public string nameKo;
    public string nameEn;
    public int defaultMaxIntegrity;
    public int payloadBonus;

    public string GetName()
    {
        return SettingManager.Instance.Language switch
        {
            LANGUAGE.Ko => nameKo,
            LANGUAGE.En => nameEn,
        };
    }

    public string GetInt()
    {
        return defaultMaxIntegrity.ToString();
    }

    public string GetPayloadBonus()
    {
        if(payloadBonus > 0)
        {
            return $"+{payloadBonus}";
        }

        return payloadBonus.ToString();
    }
}