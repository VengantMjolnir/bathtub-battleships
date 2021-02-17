using UnityEngine;
using UnityEngine.UI;

namespace RogueEyebrow.Variables
{
    [ExecuteInEditMode]
    public class SliderSetter : MonoBehaviour
    {
        public Slider Slider;
        public FloatVariable Variable;

        private void Update()
        {
            if (Slider != null && Variable != null)
            {
                Slider.value = Variable.Value;

                if (Variable.Value <= 0)
                {
                    Slider.fillRect.gameObject.SetActive(false);
                }
                else if (Slider.fillRect.gameObject.activeSelf == false)
                {
                    Slider.fillRect.gameObject.SetActive(true);
                }
            }
        }
    }
}