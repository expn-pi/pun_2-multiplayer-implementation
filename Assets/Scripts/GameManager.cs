using UnityEngine;
using UnityEngine.SceneManagement;

using Photon.Pun;
using Photon.Realtime;


namespace Com.ArmI.TestPun2
{
    public class GameManager : MonoBehaviourPunCallbacks
    {
        public static GameManager Instance;

        [Tooltip("The prefab to use for representing the player")]
        public GameObject playerPrefab;

        void Start()
        {
            Instance = this;
            
            if (playerPrefab == null)
            {
                string message = "<Color=Red><a>Missing</a></Color> " + 
                    "playerPrefab Reference. Please set it up in GameObject 'Game Manager'";

                Debug.LogError(message, this);
            }
            else
            {
                if (PlayerManager.LocalPlayerInstance == null)
                {
                    string message = "We are Instantiating LocalPlayer from {0}";
                    Debug.LogFormat(message, SceneManagerHelper.ActiveSceneName);
                    // we're in a room. spawn a character for the local player. 
                    //      it gets synced by using PhotonNetwork.Instantiate
                    PhotonNetwork.Instantiate(
                        this.playerPrefab.name, new Vector3(0f,5f,0f),
                        Quaternion.identity, 0    
                    );
                }
                else
                {
                    Debug.LogFormat(
                        "Ignoring scene load for {0}",
                         SceneManagerHelper.ActiveSceneName
                    );
                }
            }
        }

        /// <summary>
        /// Called when the local player left the room.
        //      We need to load the launcher scene.
        /// </summary>
        public override void OnLeftRoom()
        {
            SceneManager.LoadScene(0);
        }

        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }

        void LoadArena()
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                string s_message = "PhotonNetwork : Trying to Load a level " + 
                    "but we are not the master Client";

                Debug.LogError(s_message);
            }
            Debug.LogFormat(
                "PhotonNetwork : Loading Level : {0}",
                 PhotonNetwork.CurrentRoom.PlayerCount
            );

            string message = "Room for " + PhotonNetwork.CurrentRoom.PlayerCount;
            PhotonNetwork.LoadLevel(message);
        }

        public override void OnPlayerEnteredRoom(Player other)
        {
            // not seen if you're the player connecting
            Debug.LogFormat("OnPlayerEnteredRoom() {0}", other.NickName); 


            if (PhotonNetwork.IsMasterClient)
            {
                Debug.LogFormat(
                    "OnPlayerEnteredRoom IsMasterClient {0}",
                     PhotonNetwork.IsMasterClient
                ); 
                
                // called before OnPlayerLeftRoom
                LoadArena();
            }
        }

        public override void OnPlayerLeftRoom(Player other)
        {
            // seen when other disconnects
            Debug.LogFormat("OnPlayerLeftRoom() {0}", other.NickName); 


            if (PhotonNetwork.IsMasterClient)
            {
                Debug.LogFormat(
                    "OnPlayerLeftRoom IsMasterClient {0}",
                     PhotonNetwork.IsMasterClient
                ); 

                // called before OnPlayerLeftRoom
                LoadArena();
            }
        }
    }
}
