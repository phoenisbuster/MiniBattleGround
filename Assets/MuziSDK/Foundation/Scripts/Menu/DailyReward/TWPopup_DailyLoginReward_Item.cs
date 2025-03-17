using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Useractivity.Quest;
using static Useractivity.Quest.UserQuestService;
using Grpc.Core;
using System.Text;
using Muziverse.Proto.GameContent.Domain;

public class TWPopup_DailyLoginReward_Item : MonoBehaviour
{
    //public TMP_Text textM
    public TMP_Text textDay;
    public TMP_Text textReward;
    public TMP_Text textClaimButton;
    public Button itemButton;
    public Button claimButton;
    public QuestResponse questResponse;
    public Sprite claimedButtonSprite;

    public void Init(QuestResponse questResponse)
    {
        this.questResponse = questResponse;
        textDay.text = questResponse.Content.Title;
        StringBuilder sb = new StringBuilder();
        foreach(var reward in questResponse.Rewards)
        {
            sb.Append(GetRewardText(reward.Symbol, reward.Amount));
            sb.Append("    ");
        }
        textReward.text = sb.ToString();
    }

    string GetRewardText(string symbol, string amount)
    {
        string value = symbol switch
        {
            "MUZI-GOLD" => "<sprite index=0> ",
            "MUZI-DIAMOND" => "<sprite index=1> ",
            "MUZI-EXP" => "<sprite index=2> ",
            _ => ""
        };

        value += amount.ToString();
        return value;
    }

    public void SetClaimedReward()
    {
        itemButton.interactable = true;
        claimButton.interactable = false;
        claimButton.image.sprite = claimedButtonSprite;
        textClaimButton.text = "Complete";
    }
    public void ToggleItem(bool isActive)
    {
        itemButton.interactable = isActive;
        claimButton.interactable = isActive;
    }

    public async void OnClick()
    {
        claimButton.interactable = false;
        UserQuestServiceClient client = new UserQuestServiceClient(FoundationManager.channel);
        try
        {
            ClaimRewardsRequest quest = new ClaimRewardsRequest
            {
                QuestId = questResponse.Id,
                ZoneOffset = FoundationManager.MyTimeZone
            };
            var response = await client.ClaimRewardsAsync(quest, FoundationManager.metadata);
            WalletManager.instance.GetWalletInfo();
            TW.I.AddWarning("", "Claimed Login Reward Successfully!");
            SetClaimedReward();
        }
        catch (RpcException e)
        {
            RpcJSONError j = FoundationManager.GetErrorFromMetaData(e);
            Debug.Log(j.errorMessage);
        }

    }
}
