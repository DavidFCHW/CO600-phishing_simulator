using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplanationPanelScript : MonoBehaviour {

    private ExplanationScript explanations;
    private int index;

    /*
     * Setter for explanations
     */
    public void SetExplanations(ExplanationScript explanations)
    {
        this.explanations = explanations;
    }

    /*
     * Setter for the index
     */
     public void SetIndex(int index)
    {
        this.index = index;
    }

    public void OnNextClicked()
    {
        this.Unselect();
        explanations.GoToPanel(index + 1);
    }

    public void OnPrevClicked()
    {
        this.Unselect();
        explanations.GoToPanel(index - 1);
    }

    /*
     * Select this panel
     */
    public void Select()
    {
        this.gameObject.SetActive(true);
    }

    /*
     * Unselect this panel
     */
    public void Unselect()
    {
        this.gameObject.SetActive(false);
    }
}
