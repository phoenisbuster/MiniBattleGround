using GameAudition;
using Networking;
using Opsive.Shared.Input;
using Opsive.UltimateCharacterController.Character;
using Opsive.UltimateCharacterController.Inventory;
using Opsive.UltimateCharacterController.Traits;
using UnityEngine;
using TMPro;

public class PlayerComponentsManager : MonoBehaviour
{
    [SerializeField] Animator _playerAnimator;

    [Header("Animator Controllers")]
    [SerializeField] private RuntimeAnimatorController _normalAnimator;
    [SerializeField] private RuntimeAnimatorController _dancingHallAnimator;

    [Header("UI")]
    [SerializeField] private TMP_Text _status_Text;
    [SerializeField] private DH_DialogBubble _dialogBubble;
    [SerializeField] private TMP_Text _displayName_Text;

    public void UpdatePlayerComponentsForDancingHall()
    {
        Destroy(GetComponent<AnimatorMonitor>());
        Destroy(GetComponent<MuziLOD>());
#if MUZIVERSE_MAIN
        Destroy(GetComponent<MyMapPlayer>());
#endif

        Destroy(GetComponent<CharacterFootEffects>());
        Destroy(GetComponent<CharacterIK>());
        Destroy(GetComponent<CharacterRespawner>());
        Destroy(GetComponent<CharacterHealth>());
        Destroy(GetComponent<AttributeManager>());
        Destroy(GetComponent<ItemHandler>());
        Destroy(GetComponent<ItemSetManager>());
        Destroy(GetComponent<Inventory>());
        Destroy(GetComponent<UltimateCharacterLocomotionHandler>());
        Destroy(GetComponent<UltimateCharacterLocomotion>());
        Destroy(GetComponent<UnityInput>());
        Destroy(GetComponent<CharacterLayerManager>());
        Destroy(GetComponent<NakamaMyNetworkPlayer>());
        Destroy(GetComponent<NakamaNetworkPlayer>());
#if MUZIVERSE_MAIN
        var dhPlayer = gameObject.AddComponent<DH_Player>();

        dhPlayer._status_Text = _status_Text;
        dhPlayer._dialogBubble = _dialogBubble;
        dhPlayer._displayName_Text = _displayName_Text;
#endif
        _playerAnimator.runtimeAnimatorController = _dancingHallAnimator;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
