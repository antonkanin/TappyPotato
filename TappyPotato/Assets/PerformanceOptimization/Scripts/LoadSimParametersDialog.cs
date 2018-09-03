using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.SceneManagement;

namespace PerformanceOptimization.Scripts.Assets
{
    public class LoadSimParametersDialog : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                SceneManager.LoadScene("InitSimulation");
            }
        }
        
    }
}