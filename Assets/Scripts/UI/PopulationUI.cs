using CharacterSystem;
using TMPro;
using UnityEngine;

namespace UI
{
    public class PopulationUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
    
        private const string BASE_TEXT = "Population: {0} \n Blue: {1} \n Black: {2} \n Red: {3} \n White: {4}";
        
        private void Update()
        {
            _text.text = string.Format(BASE_TEXT,
                CharacterWarehouse.Instance.TotalPopulation,
                CharacterWarehouse.Instance.GetPopulation(CharacterType.Blue),
                CharacterWarehouse.Instance.GetPopulation(CharacterType.Black),
                CharacterWarehouse.Instance.GetPopulation(CharacterType.Red),
                CharacterWarehouse.Instance.GetPopulation(CharacterType.White));
        }
    }
}
