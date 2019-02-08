using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
