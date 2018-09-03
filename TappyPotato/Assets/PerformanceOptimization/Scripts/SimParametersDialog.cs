using PerformanceOptimization.Scripts.Assets;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace PerformanceOptimization
{
    public class SimParametersDialog : MonoBehaviour
    {
        [SerializeField] private FloatVariable step;
        [SerializeField] private FloatVariable minDeltaTime;

        [SerializeField]
        private InputField stepText;

        [SerializeField]
        private InputField minDeltaTimeText;

        [SerializeField]
        private Button startBtn;

        private void Start()
        {
            stepText.text = step.Value.ToString();
            minDeltaTimeText.text = minDeltaTime.Value.ToString();
            startBtn.onClick.AddListener(StartBtnClickHandler);
        }

        private void StartBtnClickHandler()
        {
            step.Value = float.Parse(stepText.text);
            minDeltaTime.Value = float.Parse(minDeltaTimeText.text);
            SceneManager.LoadScene("SimMotion");
        }
    }
}