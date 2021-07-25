using UnityEngine;
using UnityEngine.UI;

namespace Snowman
{
    public class SliderUi : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        [SerializeField] private PlayerMovement playerMovement;

        private void Awake()
        {
            slider.minValue = 0;
            slider.maxValue = playerMovement.MaxHitForce;
        }

        private void Update()
        {
            slider.value = playerMovement.GetHitForce();
        }
    }
}
