using Opsive.UltimateCharacterController.Character.Abilities;

public class PlaySittingIdle : Ability
{
    public override float AbilityFloatData => NetworkObject.abilityData;

    public Networking.NakamaNetworkPlayer NetworkObject
    {
        get
        {
            if (networkObject == null)
            {
                networkObject = GetComponent<Networking.NakamaNetworkPlayer>();
            }
            return networkObject;
        }
    }

    private Networking.NakamaNetworkPlayer networkObject;

    public override void Update()
    {
        if (m_CharacterLocomotion.Moving)
        {
            StopAbility(true);
            return;
        }
    }
}
