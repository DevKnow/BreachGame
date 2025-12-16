using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LobbyView : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] ProgramSelectPanel _programPanel;
    //[SerializeField] ModuleSelectPanel _modulePanel;
    //[SerializeField] BuildPanel _buildPanel;

    [Header("Data")]
    private SaveManager _saveManager;

    private ProgramData _selectedProgram;

    /// <summary>
    /// 0: Program 1: Module 2: Build
    /// </summary>
    private int _currentPanelIndex = 0;

    public void Initialize(SaveManager saveManager)
    {
        _saveManager = saveManager;
        _currentPanelIndex = 0;

        #region Initialize Program Panel

        var unlockedIds = _saveManager.GetUnlockedPrograms();
        var programList = new List<ProgramData>(unlockedIds.Count);

        for(int i=0, iMax = unlockedIds.Count; i<iMax; i++)
        {
            var data = DataLoader.GetProgramData(unlockedIds[i]);
            programList.Add(data);
        }
        _programPanel.Initialize(programList);

        _programPanel.OnSelected += OnProgramSelected;

        #endregion
    }

    public void OnProgramSelected(ProgramData programData)
    {
        _selectedProgram = programData;
    }
}
