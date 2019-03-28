using System.Collections;
using Office;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
 * Script responsible for every functionality of the title screen
 * ie: showing/moving the credits, quitting the game, loading save data, playing bg music
 */
namespace Title_Screen
{
    public class TitleScript : MonoBehaviour
    {

        public AudioSource clickSound;
        public AudioSource backgroundMusic;
        public GameObject buttonPanel;
        public GameObject creditsPanel;
        public GameObject creditsText;
        public GameObject kentLogo;

        public GameObject areUSurePanel;

        // Variables
        private float _creditsTextYOffset;
        private int _step = 1; // How fast the credits go
        private RectTransform _creditsTextRectTrans;
        private RectTransform _kentLogoRectTransform;
        private int _stepIncrease = 1;

        private void Start()
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            _kentLogoRectTransform = kentLogo.GetComponent<RectTransform>();
            _creditsTextRectTrans = creditsText.GetComponent<RectTransform>();
            // Place the credit panel just below the canvas
            _creditsTextYOffset = -gameObject.GetComponent<RectTransform>().rect.height;
            // Hide credits
            buttonPanel.SetActive(true);
            creditsPanel.SetActive(false);
            creditsText.SetActive(true);
//        kentLogo.SetActive(true);
            // Hide areUSure panel
            areUSurePanel.SetActive(false);
            // Start the music
            backgroundMusic.Play();
            // Load data
            StaticClass.LoadData();
        }

        /*
         * Make the credits go faster
         */
        private void IncreaseStep()
        {
            switch (_stepIncrease)
            {
                case 1:
                    _stepIncrease = 2;
                    _step *= 4;
                    break;
                case 2:
                    _stepIncrease = 3;
                    _step *= 2;
                    break;
                case 3:
                    _stepIncrease = 4;
                    _step *= 2;
                    break;
                case 4:
                    _stepIncrease = 1;
                    _step /= 16;
                    break;
            }
        }

        private void PlayGame()
        {
            SceneManager.LoadScene("Office");
        }

        private void QuitGame()
        {
            StaticClass.SaveData();
            Application.Quit();
        }

        private void PlayCredits()
        {
            // Hide button panel
            buttonPanel.SetActive(false);
            // Show credits panel
            creditsPanel.SetActive(true);
            // Move credits text
            StartCoroutine(MoveCreditsUp());
        }

        private void RemoveCredits()
        {

            // Hide credit panel and show buttons
            creditsPanel.SetActive(false);
            buttonPanel.SetActive(true);
        }

        /*
         * Moves the credits up until out of view
         */
        IEnumerator MoveCreditsUp()
        {
            for (float i = _creditsTextYOffset;
                i < LayoutUtility.GetPreferredSize(_creditsTextRectTrans, 1) + _kentLogoRectTransform.rect.height;
                i += _step)
            {
                _creditsTextRectTrans.offsetMax = new Vector2(0, i);
                yield return null;
            }

            _step = 1;
            yield return new WaitForSeconds(1);
            RemoveCredits();
        }

        /*
         * Play button clicked
         */
        public void PlayClicked()
        {
            clickSound.Play();
            PlayGame();
        }

        /*
         * Credits button clicked
         */
        public void CreditsClicked()
        {
            clickSound.Play();
            PlayCredits();
        }

        /*
         * Quit button clicked
         */
        public void QuitClicked()
        {
            clickSound.Play();
            QuitGame();
        }

        /*
         * Reset button clicked
         */
        public void ResetClicked()
        {
            clickSound.Play();
            areUSurePanel.SetActive(true);
        }

        public void ConfirmReset()
        {
            clickSound.Play();
            areUSurePanel.SetActive(false);
            StaticClass.ResetSavedData();
        }

        public void CancelReset()
        {
            clickSound.Play();
            areUSurePanel.SetActive(false);
        }

        /*
         * Fast forward button clicked
         */
        public void FastForwardClicked()
        {
            clickSound.Play();
            IncreaseStep();
        }
    }
}