using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LobbyView : MonoBehaviour
{
    private enum PANEL_INDEX
    {
        PROGRAM = 0,
        MODULE = 1,
        BUILD = 2,
        COUNT
    }

    [Header("Panels")]
    [SerializeField] ProgramSelectPanel _programPanel;
    [SerializeField] ModuleSelectPanel _modulePanel;
    //[SerializeField] BuildPanel _buildPanel;

    [Header("Data")]
    private SaveManager _saveManager;

    private ProgramData _selectedProgram;
    private List<CommandData> _selectedModules = new ();

    /// <summary>
    /// 0: Program 1: Module 2: Build
    /// </summary>
    private PANEL_INDEX _currentPanel = PANEL_INDEX.PROGRAM;

    public void Initialize()
    {
        _saveManager = GameManager.Instance.SaveManager;
        _currentPanel = PANEL_INDEX.PROGRAM;

        #region Initialize Program Panel

        var unlockedProgramIds = _saveManager.GetUnlockedPrograms();
        var programList = new List<ProgramData>(unlockedProgramIds.Count);

        for(int i=0, iMax = unlockedProgramIds.Count; i<iMax; i++)
        {
            var data = DataLoader.GetProgramData(unlockedProgramIds[i]);
            programList.Add(data);
        }
        _programPanel.Initialize(programList, OnProgramSelected, OnProgramCancled);

        #endregion

        #region Initialize Module Panel

        // TODO
        var unlockedModuleIds = _saveManager.GetUnlockedModules();
        var moduleList = new List<CommandData>(unlockedModuleIds.Count);

        for (int i = 0, iMax = unlockedModuleIds.Count; i < iMax; i++)
        {
            var data = DataLoader.GetCommandData(unlockedProgramIds[i]);
            moduleList.Add(data);
        }
#if UNITY_EDITOR
        _modulePanel.Initialize(DataLoader.CommandDatas, OnModuleSelected, OnModuleCancled);
#else
        _modulePanel.Initialize(moduleList, OnModuleSelected, OnModuleCancled);
#endif

#endregion
    }

    private void OnProgramSelected(ProgramData programData)
    {
        _selectedProgram = programData;
    }

    private void OnProgramCancled(ProgramData programData)
    {
        _selectedProgram = null;
    }

    private void OnModuleSelected(CommandData commandData)
    {
        _selectedModules.Add(commandData);
    }

    private void OnModuleCancled(CommandData commandData)
    {
        if (_selectedModules.Contains(commandData))
        {
            _selectedModules.Remove(commandData);
        }
    }

    public void OnNavigate(int direction)
    {
        switch (_currentPanel)
        {
            case PANEL_INDEX.PROGRAM:
                _programPanel.MoveFocus(direction);
                break;
            case PANEL_INDEX.MODULE:
                _modulePanel.MoveFocus(direction);
                break;
            case PANEL_INDEX.BUILD:
                break;
        }
    }

    public void OnSwitchPanel(int direction)
    {
        switch (_currentPanel)
        {
            case PANEL_INDEX.PROGRAM:
                _programPanel.OnExit();
                break;

            case PANEL_INDEX.MODULE:
                _modulePanel.OnExit();
                break;

            case PANEL_INDEX.BUILD:
                break;
        }

        var index = (int)_currentPanel + direction;
        if (index < 0)
        {
            _currentPanel = PANEL_INDEX.BUILD;
        }
        else if (index >= (int)PANEL_INDEX.COUNT)
        {
            _currentPanel = PANEL_INDEX.PROGRAM;
        }
        else
        {
            _currentPanel = (PANEL_INDEX)index;
        }

        switch (_currentPanel)
        {
            case PANEL_INDEX.PROGRAM:
                _programPanel.OnEnter();
                break;

            case PANEL_INDEX.MODULE:
                _modulePanel.OnEnter();
                break;

            case PANEL_INDEX.BUILD:
                break;
        }
    }

    public void OnConfirm()
    {
        switch (_currentPanel)
        {
            case PANEL_INDEX.PROGRAM:
                _programPanel.ConfirmSelection();
                break;
            case PANEL_INDEX.MODULE:
                break;
            case PANEL_INDEX.BUILD:
                break;
        }
    }

    public void OnCancel()
    {
        // TODO
    }
}
