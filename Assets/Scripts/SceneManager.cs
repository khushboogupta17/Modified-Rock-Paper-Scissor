using System;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

namespace DefaultNamespace
{
    public class SceneManager : MonoBehaviour
    {
        public static SceneManager Instance;

        private void Start()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                DestroyImmediate(this);
                return;
            }
            
            DontDestroyOnLoad(this);
        }

        public void LoadScene(int ind)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(ind);
            
        }
        
        
    }
}