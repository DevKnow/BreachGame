using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Main game manager
/// Handles initialization and core game logic
/// </summary>
public class GameManager : Singleton<GameManager>
{
    public SaveManager SaveManager => _saveManager;
    [SerializeField] private SaveManager _saveManager;

    protected override void Awake()
    {
        base.Awake();

        // Load all data at game start
        DataLoader.LoadAll();
    }


#if UNITY_EDITOR

    /// <summary>
    /// 테스트용 전투 시작
    /// </summary>
    [Button("Start Test Battle")]
    public void StartTestBattle()
    {
        var playerAgents = new List<ProgramModel>();
        var enemyAgents = new List<ProgramModel>();

        var playerData = DataLoader.GetProgramData("proc_ghost");
        if (playerData != null)
        {
            var player = new PlayerProgramModel(playerData);
            playerAgents.Add(player);
        }

        var enemyData = DataLoader.GetEnemyData("enemy_firewall");
        if (enemyData != null)
        {
            enemyAgents.Add(new EnemyProgramModel(enemyData, 1));
        }

        if (playerAgents.Count > 0 && enemyAgents.Count > 0)
        {
            // 테스트: 섹터 1 = 레이어 3개
            int testSector = 1;
            int maxLayer = GetMaxLayerBySector(testSector);

            //if (_battleView != null)
            //    _battleView.SetupLayerButtons(maxLayer);

            //_battleManager.StartBattle(playerAgents, enemyAgents);
        }
        else
        {
            Debug.LogWarning("[Game] Test data not found!");
        }
    }

    /// <summary>
    /// 섹터별 최대 레이어 수
    /// </summary>
    private int GetMaxLayerBySector(int sector)
    {
        return sector switch
        {
            1 or 2 => 3,
            3 or 4 => 4,
            5 or 6 => 5,
            7 => 6,
            _ => 3
        };
    }
#endif
}
