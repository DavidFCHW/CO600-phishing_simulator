using UnityEngine;
using UnityEngine.EventSystems;

/*
 * Blocks the continue button if the threshold is not reached
 * Displays a panel
 */
namespace EmailClient
{
    public class ContinueButtonHoverScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private GameScript _gameScript;

        public void SetGameScript(GameScript gameScript)
        {
            _gameScript = gameScript;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _gameScript.ContinueButtonHoverStop();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _gameScript.ContinueButtonHoverStart();
        }
    }
}