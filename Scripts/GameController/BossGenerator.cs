using System;
using Boss.Pattern;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

namespace GameController
{
    public class BossGenerator : MonoBehaviour
    {
        public BossPattern[] patterns;
        public GameObject skillDisplayPrefab;
        public Transform skillDisplayParent;
        
        private BossPattern[] _selectedPatterns;

        private GameObject _patternQ;
        private GameObject _patternW;
        private GameObject _patternE;
        private GameObject _patternR;

        private void Start()
        {
            _patternQ = transform.Find("Skill (1)").gameObject;
            _patternW = transform.Find("Skill (2)").gameObject;
            _patternE = transform.Find("Skill (3)").gameObject;
            _patternR = transform.Find("Skill (4)").gameObject;

            foreach (var pattern in patterns)
            {
                Instantiate(skillDisplayPrefab, skillDisplayParent);
            }
        }

        public void FillPattern()
        {
            for (var index = 0; index < patterns.Length; index++)
            {
                var pattern = patterns[index];
                var skillDisplay = skillDisplayParent.transform.GetChild(index);
                skillDisplay.GetComponent<PatternDisplay>().SetBossPattern(pattern);
            }
        }
    }
}