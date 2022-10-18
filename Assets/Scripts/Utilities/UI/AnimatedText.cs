using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Ru1t3rl.Utilities.UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class AnimatedText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textObject;
        [SerializeField] private float updateInterval = 0.5f;
        [SerializeField]
        private string[] loadingTexts = new string[] {
        "Loading",
        "Loading.",
        "Loading..",
        "Loading..." };

        private int currentText = 0;
        private float lastUpdate = 0f;

        private void Awake()
        {
            textObject ??= GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            lastUpdate += Time.deltaTime;

            if (lastUpdate == updateInterval)
            {
                lastUpdate = 0f;
                currentText = (currentText + 1) % loadingTexts.Length;
                textObject.text = loadingTexts[currentText];
            }
        }
    }
}