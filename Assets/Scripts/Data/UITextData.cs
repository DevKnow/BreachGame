using System;

[Serializable]
public class UITextData
{
    public string id;
    public string nameKo;
    public string nameEn;
    public string descKo;
    public string descEn;

    public string GetName()
    {
        return SettingManager.Instance.Language switch
        {
            LANGUAGE.Ko => nameKo,
            LANGUAGE.En => nameEn,
        };
    }
}
