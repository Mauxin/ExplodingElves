using CharacterSystem;
using TMPro;
using UnityEngine;

namespace UI
{
    public class TextTest : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
    
        private const string BASE_TEXT = "Population: \n Blue: {0} \n Black: {1} \n Red: {2} \n White: {3}";
        
        private void Update()
        {
            _text.text = string.Format(BASE_TEXT, CharacterWarehouse.Instance.GetPopulation(CharacterType.Blue),
                CharacterWarehouse.Instance.GetPopulation(CharacterType.Black),
                CharacterWarehouse.Instance.GetPopulation(CharacterType.Red),
                CharacterWarehouse.Instance.GetPopulation(CharacterType.White));
        }
    }
}
