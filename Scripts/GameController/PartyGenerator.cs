using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameController
{
    public class RaidGenerator : MonoBehaviour
    {
        [Header("Raider Prefabs")]
        public GameObject barbarian;
        public GameObject falcon;
        public GameObject hawkEye;
        public GameObject knight;
        public GameObject ranger;
        public GameObject scouter;
        public GameObject thief;
        
        [Header("Raid Icon")]
        public Sprite orderIcon;
        public Sprite partyIcon;
        public Sprite investigatorIcon;
        public Sprite pilgrimIcon;
        public Sprite warriorsPathIcon;
        public Sprite bountyHunterIcon;
        public Sprite magicAcademyIcon;
        public Sprite ancientGuardianIcon;
        
        public enum RaidName
        {
            Order,
            Party,
            Investigator,
            Pilgrim,
            WarriorsPath,
            BountyHunter,
            MagicAcademy,
            AncientGuardian
        }

        public Sprite GetPartyIcon(RaidName raidName)
        {
            switch (raidName)
            {
                case RaidName.Order:
                    return orderIcon;
                case RaidName.Party:
                    return partyIcon;
                case RaidName.Investigator:
                    return investigatorIcon;
                case RaidName.Pilgrim:
                    return pilgrimIcon;
                case RaidName.WarriorsPath:
                    return warriorsPathIcon;
                case RaidName.BountyHunter:
                    return bountyHunterIcon;
                case RaidName.MagicAcademy:
                    return magicAcademyIcon;
                case RaidName.AncientGuardian:
                    return ancientGuardianIcon;
                default:
                    throw new ArgumentOutOfRangeException(nameof(raidName), raidName, null);
            }
        }

        public List<GameObject> GenerateBasicRaid(RaidName raidName)
        {
            var raidList = new List<GameObject>();
            switch (raidName)
            {
                case RaidName.Order:
                    raidList.Add(knight);
                    raidList.Add(knight);
                    break;
                case RaidName.Party:
                    raidList.Add(barbarian);
                    raidList.Add(ranger);
                    // raidList.Add(socerer);
                    // raidList.Add(prist);
                    break;
                case RaidName.Investigator:
                    // raidList.Add(oracle);
                    // raidList.Add(oracle);
                    break;
                case RaidName.Pilgrim:
                    // raidList.Add(prist);
                    // raidList.Add(prist);
                    // raidList.Add(prist);
                    break;
                case RaidName.WarriorsPath:
                    raidList.Add(barbarian);
                    raidList.Add(barbarian);
                    raidList.Add(barbarian);
                    break;
                case RaidName.BountyHunter:
                    raidList.Add(thief);
                    raidList.Add(thief);
                    // raidList.Add(hunter);
                    // raidList.Add(hunter);
                    break;
                case RaidName.MagicAcademy:
                    // raidList.Add(socerer);
                    // raidList.Add(socerer);
                    // raidList.Add(socerer);
                    // raidList.Add(shaman);
                    break;
                case RaidName.AncientGuardian:
                    // raidList.Add(shaman);
                    // raidList.Add(shaman);
                    // raidList.Add(shaman);
                    raidList.Add(barbarian);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return raidList;
        }
        
        public RaidName SelectRandomRaid()
        {
            Array values = Enum.GetValues(typeof(RaidName));
            System.Random random = new System.Random();
            RaidName randomRaid = (RaidName)values.GetValue(random.Next(values.Length));
            return randomRaid;
        }

        public GameObject GenerateRandomRaider()
        {
            var random = UnityEngine.Random.Range(0, 7);
            switch (random)
            {
                case 0:
                    return barbarian;
                case 1:
                    return falcon;
                case 2:
                    return hawkEye;
                case 3:
                    return knight;
                case 4:
                    return ranger;
                case 5:
                    return scouter;
                case 6:
                    return thief;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}