using UnityEngine;
using UnityEngine.UI;
using TMPro;


namespace Com.ArmI.TestPun2
{
    public class PlayerUI : MonoBehaviour
    {
        [Tooltip("UI Text to display Player's Name")]
        [SerializeField]
        private TextMeshProUGUI playerNameText;

        [Tooltip("UI Slider to display Player's Health")]
        [SerializeField]
        private Slider playerHealthSlider;

        [Tooltip("Pixel offset from the player target")]
        [SerializeField]
        private Vector3 screenOffset = new Vector3(0f,30f,0f);

        float characterControllerHeight = 0f;
        Transform targetTransform;
        Renderer targetRenderer;
        CanvasGroup _canvasGroup;
        Vector3 targetPosition;

        private PlayerManager target;

        
        
        void Awake()
        {
            this.transform.SetParent(
                GameObject.Find("Canvas").GetComponent<Transform>(),
                false
            );

            this._canvasGroup = this.GetComponent<CanvasGroup>();
        }

        public void SetTarget(PlayerManager _target)
        {
            if (_target == null)
            {
                string message = "<Color=Red><a>Missing</a></Color> PlayMakerManager target " 
                    + "for PlayerUI.SetTarget.";

                Debug.LogError(message, this);

                return;
            }

            // Cache references for efficiency
            target = _target;

            targetTransform = this.target.GetComponent<Transform>();
            targetRenderer = this.target.GetComponent<Renderer>();
            CharacterController characterController = 
                _target.GetComponent<CharacterController> ();
            
            // Get data from the Player that won't change during the lifetime of this Component
            if (characterController != null)
            {
                characterControllerHeight = characterController.height;
            }

            if (playerNameText != null)
            {
                playerNameText.text = target.photonView.Owner.NickName;
            }
        }

        void Update()
        {
            // Reflect the Player Health
            if (playerHealthSlider != null)
            {
                playerHealthSlider.value = target.Health;
            }

            // Destroy itself if the target is null, It's a fail safe when Photon is destroying Instances of a Player over the network
            if (target == null)
            {
                Destroy(this.gameObject);
                return;
            }
        }

        void LateUpdate()
        {
            // Do not show the UI if we are not visible to the camera,
            //      thus avoid potential bugs with seeing the UI,
            //      but not the player itself.

            if (targetRenderer!=null)
            {
                this._canvasGroup.alpha = targetRenderer.isVisible ? 1f : 0f;
            }

            // #Critical
            // Follow the Target GameObject on screen.
            if (targetTransform != null)
            {
                targetPosition = targetTransform.position;
                targetPosition.y += characterControllerHeight;
                
                this.transform.position = Camera.main.WorldToScreenPoint (targetPosition) +
                    screenOffset;
            }
        }
    }
}
