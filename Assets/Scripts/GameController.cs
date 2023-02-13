using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum LevelResultStates
{
    Won,
    Lost,
    Draw,
    TimeUp,
    Default
}

namespace DefaultNamespace
{
    public class GameController : MonoBehaviour
    {
        public Player Player;
        private AttackModeInfo _computerPlayed;
        private AttackModeInfo _playerPlayed;
        private void Start()
        {
            UIManager.OnPlayerSelectingOptionEvent += PlayComputerAndCheckResult;
            Timer.OnTimeIsUpEvent += OnTimeUp;
        }

        private void PlayComputerAndCheckResult(AbstractAttackMode abstractAttackMode)
        {
            _computerPlayed = PlayComputerTurn();
            _playerPlayed = abstractAttackMode.AttackModeInfo;
            
            var result = CalculateResult(_computerPlayed, _playerPlayed);
            AnalyseResult(result);
        }
        
        
        private AttackModeInfo PlayComputerTurn()
        {
            int randInd = Random.Range(0, GameDatabase.Instance.AttackModesInfo.Count - 1);
            UIManager.Instance.InstantiateComputerAttackMode(GameDatabase.Instance.AttackModesInfo[randInd]);
            return GameDatabase.Instance.AttackModesInfo[randInd];
        }

        private LevelResultStates CalculateResult(AttackModeInfo computer, AttackModeInfo player)
        {
            var strengthToAttackOpponent =
                player.OpponentInfos.Find(x => x.OpponentName == computer.Name).AttackedByStrength;
            if ( strengthToAttackOpponent == GameDatabase.Strengths.CantAttack)
            {
                return LevelResultStates.Lost;
           
            }

            return strengthToAttackOpponent == GameDatabase.Strengths.Draw ? LevelResultStates.Draw : LevelResultStates.Won;
        }
        
        private void AnalyseResult(LevelResultStates levelResultState)
        {
            switch (levelResultState)
            {
                case LevelResultStates.Lost:  UIManager.Instance.SetLevelResult("You Lost!");
                    Invoke(nameof(LoadMainMenu),GameDatabase.Instance.LevelResetBreak);
                break;
                
                case LevelResultStates.Won: UIManager.Instance.SetLevelResult("You Won!");
                    IncrementScore(1,Player);
                    Invoke(nameof(ResetUI),GameDatabase.Instance.LevelResetBreak);
                    break;
                
                case LevelResultStates.Draw: UIManager.Instance.SetLevelResult("Hard luck, it's a draw!");
                    Invoke(nameof(ResetUI),GameDatabase.Instance.LevelResetBreak);
                    break;
                
                case LevelResultStates.TimeUp:UIManager.Instance.SetLevelResult("Time's up, gotta be fast!");
                    UIManager.Instance.SetAttackOptions(false);
                    Invoke(nameof(LoadMainMenu),GameDatabase.Instance.LevelResetBreak);
                    break;
                
                case LevelResultStates.Default: Debug.LogError("Something broke, please check");
                    break;
                
            }
            
            
        }

        private static void IncrementScore(int delta, Player player)
        {
            player.IncrementScore(delta);
        }

        private void ResetUI()
        {
            UIManager.Instance.ResetUI();
        }

        private void LoadMainMenu()
        {
            SceneManager.Instance.LoadScene(0);
        }

        private void OnTimeUp()
        {
            AnalyseResult(LevelResultStates.TimeUp);
        }

        private void OnDestroy()
        {
            UIManager.OnPlayerSelectingOptionEvent -= PlayComputerAndCheckResult;
            Timer.OnTimeIsUpEvent -= OnTimeUp;
        }
    }
}