using System;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

namespace Boss.Pattern
{
    public class PatternDisplay : MonoBehaviour
    {
        BossPattern bossPattern;
        private Image patternIcon;
        private TextMeshProUGUI patternExplanation;
        
        private void Start()
        {
            // patternIcon = transform.GetChild(0).gameObject.GetComponent<Image>();
            Debug.Log(transform.Find("icon"));
            patternExplanation = transform.Find("Text (TMP)").GetComponentInChildren<TextMeshProUGUI>();
        }

        public void SetBossPattern(BossPattern pattern)
        {
            Start();
            bossPattern = pattern;
            patternIcon.sprite = pattern.icon;
            patternExplanation.text = pattern.explanation;
        }
    }
}