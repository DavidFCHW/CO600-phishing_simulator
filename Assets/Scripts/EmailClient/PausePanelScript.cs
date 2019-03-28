using UnityEngine;

/*
 * Simple script attached to the pause panel
 * Doesn't actually cover the logic of it all, that's in the gamescript
 */
namespace EmailClient
{
    public class PausePanelScript : MonoBehaviour
    {
        private GameScript gameScript;

        public void SetGameScript(GameScript gameScript)
        {
            this.gameScript = gameScript;
        }

        public void ResumeClicked()
        {
            gameScript.UnPause();
        }

        public void QuitClicked()
        {
            gameScript.QuitButtonPressed();
        }
    }
}