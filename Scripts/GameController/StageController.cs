using System;
using System.Collections.Generic;
using Raider;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace GameController
{
    public class StageController : MonoBehaviour
    {
        private Canvas _raiderSelectCanvas;
        private Canvas _patternSelectCanvas;

        [FormerlySerializedAs("partyGenerator")] public RaidGenerator raidGenerator;

        private RaidGenerator.RaidName _selectedRaidName;
        private List<GameObject> _selectedParty;
        private bool _spawnTMp;


        private void Start()
        {
            _raiderSelectCanvas = GameObject.Find("RaiderSelectCanvas").GetComponent<Canvas>();
            _raiderSelectCanvas.gameObject.SetActive(true);
            _patternSelectCanvas = GameObject.Find("PatternSelectCanvas").GetComponent<Canvas>();
            _patternSelectCanvas.gameObject.SetActive(false);
            MakeRaiderSelection();
        }

        public void ReRoll()
        {
            var party1 = _raiderSelectCanvas.transform.Find("Raider 1").GameObject();
            var party2 = _raiderSelectCanvas.transform.Find("Raider 2").GameObject();
            var party3 = _raiderSelectCanvas.transform.Find("Raider 3").GameObject();
            
            party1.GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
            party2.GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
            party3.GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
            
            MakeRaiderSelection();
        }
        
        private void MakeRaiderSelection()
        {
            var party1 = _raiderSelectCanvas.transform.Find("Raider 1").GameObject();
            var party2 = _raiderSelectCanvas.transform.Find("Raider 2").GameObject();
            var party3 = _raiderSelectCanvas.transform.Find("Raider 3").GameObject();

            var party1name = raidGenerator.SelectRandomRaid();
            var party2name = raidGenerator.SelectRandomRaid();
            var party3name = raidGenerator.SelectRandomRaid();
            
            // 중복 검사
            while (party1name == party2name)
            {
                party2name = raidGenerator.SelectRandomRaid();
            }
            while (party1name == party3name || party2name == party3name)
            {
                party3name = raidGenerator.SelectRandomRaid();
            }
            
            SetPartySelectButton(party1, party1name);
            SetPartySelectButton(party2, party2name);
            SetPartySelectButton(party3, party3name);
        }

        private void SetPartySelectButton(GameObject partyButton, RaidGenerator.RaidName raidName)
        {
            partyButton.transform.Find("Text (TMP)").GetComponent<TMPro.TextMeshProUGUI>().text = raidName.ToString();
            partyButton.transform.Find("Image").GetComponent<UnityEngine.UI.Image>().sprite = raidGenerator.GetPartyIcon(raidName);
            partyButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => { SelectParty(raidName); });
        }

        private void SelectParty(RaidGenerator.RaidName raidName)
        {
            _selectedParty = raidGenerator.GenerateBasicRaid(raidName);
            _selectedRaidName = raidName;
            while (_selectedParty.Count <= 3)
            {
                _selectedParty.Add(raidGenerator.GenerateRandomRaider());
            }

            DisplayPatternSelectCanvas();
        }
        
        private void DisplayPatternSelectCanvas()
        {
            _raiderSelectCanvas.gameObject.SetActive(false);
            _patternSelectCanvas.gameObject.SetActive(true);
            
            var raiderUi = _patternSelectCanvas.transform.Find("Raider");
            raiderUi.Find("Image").GetComponent<UnityEngine.UI.Image>().sprite 
                = raidGenerator.GetPartyIcon(_selectedRaidName);
            raiderUi.Find("Text (TMP)").GetComponent<TMPro.TextMeshProUGUI>().text 
                = _selectedRaidName.ToString();
        }

        public void LoadRaid()
        {
            DontDestroyOnLoad(this);
            //Load Scene
            UnityEngine.SceneManagement.SceneManager.LoadScene("Stage");
        }

        private void Update()
        {
            // 씬 이름 확인
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Stage" && !_spawnTMp)
            {
                var spawnpos = new Vector2(-5, -3);
                foreach (var raider in _selectedParty)
                {
                    var raiderGO = Instantiate(raider);
                    raiderGO.transform.position = spawnpos;
                    spawnpos.x += 2;
                }

                _spawnTMp = true;
                RaidMaster.Instance.Awake();
            }
        }
    }
}