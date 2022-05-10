using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;

using System.Collections;


namespace Com.ArmI.TestPun2
{
    /// <summary>
    /// Player name input field. Let the user input his name, will appear above the player in the game.
    /// </summary>
    [RequireComponent(typeof(InputField))]
    public class PlayerNameInputField : MonoBehaviour
    {
        // Store the PlayerPref Key to avoid typos
        const string playerNamePrefKey = "PlayerName";

        /// <summary>
        /// MonoBehaviour method called on GameObject by Unity during initialization phase.
        /// </summary>
        void Start () {

            string default_name = string.Empty;

            InputField _inputField = this.GetComponent<InputField>();

            if (_inputField!=null)
            {
                if (PlayerPrefs.HasKey(playerNamePrefKey))
                {
                    default_name = PlayerPrefs.GetString(playerNamePrefKey);
                    
                    _inputField.text = default_name;
                }
            }

            PhotonNetwork.NickName =  default_name;
        }

        /// <summary>
        /// Sets the name of the player, and save it in the PlayerPrefs for future sessions.
        /// </summary>
        /// <param name="value">The name of the Player</param>
        public void SetPlayerName(string value)
        {
            // #Important
            if (string.IsNullOrEmpty(value)) {

                Debug.LogError("Player Name is null or empty");
            }
            else {
                PhotonNetwork.NickName = value;
                
                PlayerPrefs.SetString(playerNamePrefKey, value);
            }
        }
    }
}
