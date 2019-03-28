using System.Collections;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Coordinates most of the events in the office
 * Such as the manager explanations, which speech it chooses
 * The achievments, the instructions and whatnot
 * Also contains the static class which is responsible for all persistent data between scenes
 * such as the current level or the achievments
 * and saves/fetches progress to local files
 */
namespace Office
{
    public class OfficeScript : MonoBehaviour
    {
        // Unity object references
        public ManagerTalkScript managerDialogue1;
        public ManagerTalkScript managerDialogue2;
        public ManagerTalkScript managerDialogue3;
        public AudioSource backgroundSound;

        public AudioSource clickSound;

        // Achievement related stuff
        public BadgeHoverScript greyedPhisherman;
        public BadgeHoverScript phisherman;
        public BadgeHoverScript greyedKingphisher;
        public BadgeHoverScript kingphisher;
        public BadgeHoverScript greyedPoseidon;
        public BadgeHoverScript poseidon;

        // Manager
        public GameObject managerNeutral;

        public GameObject managerThinking;

        // Hidden jason nurse
        public GameObject jasonNurse;

        // Blinking instructions
        public GameObject instructions;
        public GameObject instructions2; // Look around the office
        private bool _showInstructions;
        private int _instructionsToShow;

        private void Start()
        {
            // Play background sound
            backgroundSound.Play();
            int level = StaticClass.GetAndSetCurrentLevel();
            switch (level)
            {
                case 1:
                {
                    // Show the correct manager
                    managerNeutral.SetActive(true);
                    managerThinking.SetActive(false);
                    // Hide Jason
                    jasonNurse.SetActive(false);
                    // Show instructions
                    _instructionsToShow = 1;
                    _showInstructions = true;
                    // Sort out the dialogue
                    if (!StaticClass.DialogueForCurrentLevelShown())
                    {
                        managerDialogue1.ShowDialogue();
                        StaticClass.SeenDialogueForCurrentLevel();
                    }
                    else ExplanationsDone();
                    break;
                }
                case 2:
                {
                    // Show the correct manager
                    managerNeutral.SetActive(true);
                    managerThinking.SetActive(false);
                    // Hide Jason
                    jasonNurse.SetActive(false);
                    // show instructions
                    _instructionsToShow = 1;
                    _showInstructions = true;
                    if (!StaticClass.DialogueForCurrentLevelShown())
                    {
                        managerDialogue2.ShowDialogue();
                        StaticClass.SeenDialogueForCurrentLevel();
                    }
                    else ExplanationsDone();
                    break;
                }
                case 3:
                {
                    // Show the correct manager
                    managerNeutral.SetActive(false);
                    managerThinking.SetActive(true);
                    // Show Jason
                    jasonNurse.SetActive(true);
                    // Hide instructions
                    _instructionsToShow = 2;
                    _showInstructions = true;
                    if (!StaticClass.DialogueForCurrentLevelShown())
                    {
                        managerDialogue3.ShowDialogue();
                        StaticClass.SeenDialogueForCurrentLevel();
                    }
                    else ExplanationsDone();
                    break;
                }
                default:
                {
                    // Show the correct manager
                    managerNeutral.SetActive(false);
                    managerThinking.SetActive(true);
                    // Show Jason
                    jasonNurse.SetActive(true);
                    // Hide instructions
                    _showInstructions = false;
                    break;
                }
            }

            // Sort out the badges
            greyedPhisherman.gameObject.SetActive(!StaticClass.GotAchievementEasy);
            greyedKingphisher.gameObject.SetActive(!StaticClass.GotAchievementMedium);
            greyedPoseidon.gameObject.SetActive(!StaticClass.PerfectedEasy || !StaticClass.PerfectedMedium);
            phisherman.gameObject.SetActive(StaticClass.GotAchievementEasy);
            kingphisher.gameObject.SetActive(StaticClass.GotAchievementMedium);
            poseidon.gameObject.SetActive(StaticClass.PerfectedEasy && StaticClass.PerfectedMedium);
            // Hide instructions
            instructions.SetActive(false);
            instructions2.SetActive(false);
        }

        /*
         * Manager explanations done
         */
        public void ExplanationsDone()
        {
            // Make badges hoverable
            greyedPhisherman.UnBlock();
            greyedKingphisher.UnBlock();
            greyedPoseidon.UnBlock();
            phisherman.UnBlock();
            kingphisher.UnBlock();
            poseidon.UnBlock();
            // Show instructions
            if (_showInstructions) StartCoroutine(ShowInstructions());
        }

        /*
         * Shows instructions after a delay
         */
        IEnumerator ShowInstructions()
        {
            yield return new WaitForSeconds(3);
            if (_instructionsToShow == 1) instructions.SetActive(true);
            if (_instructionsToShow == 2) instructions2.SetActive(true);
        }

        /*
         * The button in the manager bubble asking you for help
         */
        public void AcceptButtonClicked()
        {
            clickSound.Play();
            SceneManager.LoadScene("Email Client");
        }

        public void QuitGameClicked()
        {
            clickSound.Play();
            SceneManager.LoadScene("Title Screen");
        }
    }

    /*
     * Class that holds persistent data
     */
    public static class StaticClass
    {

        private static int _currentLevel = 1;
        private static int _currentLevelCopy = 1;
        private static bool _dialogueForCurrentLevelShown;
        public static bool GotAchievementEasy;
        public static bool GotAchievementMedium;
        public static bool PerfectedEasy;
        public static bool PerfectedMedium;
        public static int JsonCount = 0;

        private static readonly string SavePath = Application.persistentDataPath + "/savedData.txt";
        private static bool dataLoaded;

        /*
         * Called when a level is completed
         */
        public static void IncreaseLevel()
        {
            if (_currentLevel == 1) GotAchievementEasy = true;
            else if (_currentLevel == 2) GotAchievementMedium = true;
            if (_currentLevelCopy == _currentLevel)
            {
                _currentLevelCopy++;
                _dialogueForCurrentLevelShown = false;
            }

            _currentLevel++;
        }

        public static int GetCurrentLevel()
        {
            return _currentLevel;
        }

        public static int GetAndSetCurrentLevel()
        {
            _currentLevel = _currentLevelCopy;
            return _currentLevel;
        }

        public static void RetryEasyLevel()
        {
            _currentLevel = 1;
            SceneManager.LoadScene("Email Client");
        }

        public static void RetryMediumLevel()
        {
            _currentLevel = 2;
            SceneManager.LoadScene("Email Client");
        }

        public static void PerfectedThisLevel()
        {
            if (_currentLevel == 1) PerfectedEasy = true;
            else if (_currentLevel == 2) PerfectedMedium = true;
        }

        public static bool DialogueForCurrentLevelShown()
        {
            return _dialogueForCurrentLevelShown;
        }

        public static void SeenDialogueForCurrentLevel()
        {
            _dialogueForCurrentLevelShown = true;
        }

        /*
         * Write data to a text file
         */
        public static void SaveData()
        {
            Debug.Log(GotAchievementMedium);

            var strBld = new StringBuilder();
            strBld.AppendFormat("CurrentLevel:{0}\n", _currentLevel);
            strBld.AppendFormat("GotAchievementEasy:{0}\n", GotAchievementEasy ? "true" : "false");
            strBld.AppendFormat("GotAchievementMedium:{0}\n", GotAchievementMedium ? "true" : "false");
            strBld.AppendFormat("PerfectedEasy:{0}\n", PerfectedEasy ? "true" : "false");
            strBld.AppendFormat("PerfectedMedium:{0}\n", PerfectedMedium ? "true" : "false");
            strBld.AppendFormat("JsonCount:{0}\n", JsonCount);

            Debug.Log("Saving " + strBld);

            if (File.Exists(@SavePath)) File.Delete(@SavePath);
            File.WriteAllText(@SavePath, strBld.ToString());
        }

        public static void LoadData()
        {
            if (dataLoaded) return;
            if (!File.Exists(@SavePath)) return;

            var lines = File.ReadAllLines(@SavePath);

            _currentLevel = int.Parse(lines[0].Split(':')[1]);
            _currentLevelCopy = _currentLevel;
            GotAchievementEasy = lines[1].Split(':')[1] == "true";
            GotAchievementMedium = lines[2].Split(':')[1] == "true";
            PerfectedEasy = lines[3].Split(':')[1] == "true";
            PerfectedMedium = lines[4].Split(':')[1] == "true";
            JsonCount = int.Parse(lines[5].Split(':')[1]);

            dataLoaded = true;
        }

        public static void ResetSavedData()
        {
            if (File.Exists(@SavePath)) File.Delete(@SavePath);
            _currentLevel = 1;
            _currentLevelCopy = 1;
            _dialogueForCurrentLevelShown = false;
            GotAchievementEasy = false;
            GotAchievementMedium = false;
            PerfectedEasy = false;
            PerfectedMedium = false;
            JsonCount = 0;
        }
    }
}