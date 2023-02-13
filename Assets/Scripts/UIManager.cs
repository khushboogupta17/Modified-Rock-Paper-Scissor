using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class UIManager : MonoBehaviour
    {
        public TextMeshProUGUI ScoreText;
        public TextMeshProUGUI LevelResult;
        public GameObject PlayerPosition;
        public GameObject ComputerPosition;
        public HorizontalLayoutGroup PlayerOptionsLayout;
        public GameObject AttackModePrefab;

        private List<GameObject> _attackOptions = new List<GameObject>();
        public static UIManager Instance;
        public Timer Timer;

        public delegate void OnPLayerSelectingOptionEvent(AbstractAttackMode abstractAttackMode);
        public static event OnPLayerSelectingOptionEvent OnPlayerSelectingOptionEvent ;
        
        private void Start()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            LevelResult.gameObject.SetActive(false);
            SetScore(0);
            InitialisePlayerOptions();
        }

        void InitialisePlayerOptions()
        {
            foreach (var attackModeInfo in GameDatabase.Instance.AttackModesInfo)
            {
                var obj = SetupAttackModeUI(attackModeInfo, PlayerOptionsLayout.transform);
                obj.GetComponent<Button>().onClick.AddListener(delegate
                {
                    OnClickingAttackOption(obj);
                });
                _attackOptions.Add(obj);
            }
            
        }

        GameObject SetupAttackModeUI(AttackModeInfo attackModeInfo, Transform parentTransform)
        {
            var obj = Instantiate(AttackModePrefab, parentTransform);
            var classType = Type.GetType(attackModeInfo.Name.ToString());
            obj.AddComponent(classType);
            obj.GetComponent<AbstractAttackMode>().SetAttackModeInfo(attackModeInfo);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = attackModeInfo.Name.ToString();
            obj.name = classType.ToString();
            return obj;
        }

        void OnClickingAttackOption(GameObject obj)
        {
            obj.transform.position = PlayerPosition.transform.position;
            SetAttackOptions(false, obj);
            Timer.StopTimer();
            OnPlayerSelectingOptionEvent?.Invoke(obj.GetComponent<AbstractAttackMode>());
        }

        public void SetAttackOptions(bool isEnabled, GameObject exceptionObj=null)
        {
            foreach (var attackOption in _attackOptions)
            {
                if (exceptionObj != null && attackOption == exceptionObj)
                {
                    attackOption.GetComponent<Button>().enabled = isEnabled;
                    continue;
                }
                
                attackOption.GetComponent<Button>().interactable = isEnabled;
                attackOption.GetComponent<Button>().enabled = true;
            }
        }


        public void InstantiateComputerAttackMode(AttackModeInfo attackModeInfo)
        {
            var obj = SetupAttackModeUI(attackModeInfo, ComputerPosition.transform);
            obj.GetComponent<Button>().enabled = false;
            obj.transform.localPosition=Vector3.zero;
        }

        public void ResetUI()
        {
           ResetPlayerOptionPositions();
           LevelResult.gameObject.SetActive(false);
            SetAttackOptions(true);
            if (ComputerPosition.transform.childCount > 1)
            {
                Destroy(ComputerPosition.transform.GetChild(1).gameObject);
            } 
            Timer.StartTimer();
        }

        void ResetPlayerOptionPositions()
        {
            PlayerOptionsLayout.enabled = false;
            PlayerOptionsLayout.enabled = true;

        }

        public void SetLevelResult(string text)
        {
            LevelResult.gameObject.SetActive(true);
            LevelResult.text = text;
        }

        public void SetScore(int score)
        {
            ScoreText.text = "Score: "+ score;
        }

    }
}