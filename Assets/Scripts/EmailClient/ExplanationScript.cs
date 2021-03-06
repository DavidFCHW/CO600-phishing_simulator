﻿using UnityEngine;

/*
 * Script that handles the prev/next explanation panels
 */
namespace EmailClient
{
    public class ExplanationScript : MonoBehaviour
    {

        public ExplanationPanelScript[] explanationPanels; // All the explanation panels in order
        private GameScript gameScript;

        /*
         * Setter for gamescript
         */
        public void SetGameScript(GameScript gameScript)
        {
            this.gameScript = gameScript;
        }

        /*
         * Initialisation
         */
        private void Awake()
        {
            // Give an index and a reference to the parent script to every panel and unselect them
            for (int i = 0; i < explanationPanels.Length; i++)
            {
                explanationPanels[i].SetIndex(i);
                explanationPanels[i].SetExplanations(this);
                explanationPanels[i].Unselect();
            }

            // Make the first panel active
            explanationPanels[0].Select();
        }

        /*
         * Go to a specific panel
         * Does not unselect the other panels   
         */
        public void GoToPanel(int panelIndex)
        {
            if (0 <= panelIndex && panelIndex < explanationPanels.Length)
            {

                gameScript.PlayLightClick();
                explanationPanels[panelIndex].Select();
            }
            else if (panelIndex >= explanationPanels.Length)
            {
                gameScript.PlayMeanClick();
                gameScript.ExplanationsDone();
            }
        }

        /*
         * Remove all explanation panels from screen
         */
        public void DestroyExplanations()
        {
            foreach (ExplanationPanelScript explanationPanel in explanationPanels)
            {
                Destroy(explanationPanel.gameObject);
            }

            Destroy(gameObject);
        }
    }
}
