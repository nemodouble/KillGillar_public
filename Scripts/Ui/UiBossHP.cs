using Boss;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
    public class UiBossHP : MonoBehaviour
    {
        private Slider _slider;

        private void Start()
        {
            _slider = GetComponent<Slider>();
            _slider.maxValue = BossData.Instance.health;
            _slider.value = BossData.Instance.health;
            _slider.transform.Find("HP Text").GetComponent<TextMeshProUGUI>().text = (int)BossData.Instance.health + "";
        }
        
        public void SetHp(float hp)
        {
            _slider.value = (int)hp;
            _slider.transform.Find("HP Text").GetComponent<TextMeshProUGUI>().text = (int)hp + "";
        }
    
    }
}
