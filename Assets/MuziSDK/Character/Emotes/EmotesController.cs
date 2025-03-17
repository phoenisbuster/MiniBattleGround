using System.Collections;
using System.Collections.Generic;
using Opsive.UltimateCharacterController.Character;
using UnityEngine;

namespace MuziCharacter
{
    [RequireComponent(typeof(UltimateCharacterLocomotion))]
    [RequireComponent(typeof(Animator))]
    public class EmotesController : MonoBehaviour
    {
        private UltimateCharacterLocomotion _locomotion;
        private WheelPanel _wheelPanel;
        private Emotes _emotesAbility;
        static EmotesController _instance;
        public static EmotesController Inst => _instance;

        private CharacterIK _characterIK;
    
        [SerializeField] private List<string> emotesAnimStr;
        

        private void Awake()
        {
            _locomotion = GetComponent<UltimateCharacterLocomotion>();
            _characterIK = GetComponent<CharacterIK>();
            GetEmotesDataInAnimationController(GetComponent<Animator>().runtimeAnimatorController);
            _instance = this;
        }

        void GetEmotesDataInAnimationController(RuntimeAnimatorController controller)
        {
        }

        public void ShowEmotesWheel()
        {
            _wheelPanel = TW.AddTWByName_s("WheelPanel").GetComponent<WheelPanel>();
            _wheelPanel.SetData(emotesAnimStr, this);
            _wheelPanel.OnEmotesChoice = SetEmoteFloatData;
            EnableHeadIK(false);
        }

        public bool IsShown
        {
            get
            {
                return _wheelPanel != null && _wheelPanel.gameObject != null &&
                       _wheelPanel.gameObject.activeInHierarchy;
            }
        }

        private void SetEmoteFloatData(float floatThresholdEmoteAnim)
        {
            if (_locomotion != null && _emotesAbility == null)
            {
                _emotesAbility = _locomotion.GetAbility<Emotes>();
            }

            if (_emotesAbility == null)
            {
                Debug.LogWarning("Cannot get emote ability");
                return;
            }

            
            _locomotion.TryStopAbility(_emotesAbility);
            // Debug.Log($"<color=red>FLoatData {floatThresholdEmoteAnim}</color>");
            _emotesAbility.FloatData = floatThresholdEmoteAnim;
            
            _locomotion.TryStartAbility(_emotesAbility);
            
            // after start emotes animation, start a checking character is moved again to enable  HEAD IK
            StartCoroutine(MovingChecking());
        }

        private IEnumerator MovingChecking()
        {
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();

            yield return new WaitUntil(() => _locomotion.Moving);
            EnableHeadIK(true);
        }
        void EnableHeadIK(bool enabled)
        {
            if (_characterIK != null)
            {
                _characterIK.LookAtHeadWeight = 0;
            }
        }

        public void HideEmotesWheel()
        {
            if (_wheelPanel != null)
            {
                _wheelPanel.UIFocus(false);
                _wheelPanel.ClickX();
            }
        }
    }
}