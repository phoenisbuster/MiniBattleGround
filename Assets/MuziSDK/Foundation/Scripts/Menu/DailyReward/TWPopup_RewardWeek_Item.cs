using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Useractivity.Quest;
using static Useractivity.Quest.UserQuestService;
using Grpc.Core;
using System.Text;
using System;
using Muziverse.Proto.GameContent.Domain;

public class TWPopup_RewardWeek_Item : MonoBehaviour
{
    //public TMP_Text textM
    public TMP_Text textDay;
    public TMP_Text textReward;

    public Button claimButton;
    public GameObject focused;
    public GameObject nextItemMsg;
    public GameObject cleared;
    public QuestResponse questResponse;

    public Action<TWPopup_RewardWeek_Item> onClaimed;

    public void Init(QuestResponse questResponse)
    {
        this.questResponse = questResponse;
        textDay.text = questResponse.Content.Title;
        StringBuilder sb = new StringBuilder();
        foreach (var reward in questResponse.Rewards)
        {
            sb.Append(GetRewardText(reward.Symbol, reward.Amount));
            sb.Append(" \n");
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
        claimButton.interactable = false;
        focused.SetActive(false);
        cleared.SetActive(true);
    }

    public void SetItemStatus(bool isActive)
    {
        claimButton.interactable = isActive;
        focused.SetActive(isActive);
    }

    public void ToggleNextItemMsg()
    {
        nextItemMsg.SetActive(!nextItemMsg.activeSelf);
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
            
            onClaimed?.Invoke(this);
        }
        catch (RpcException e)
        {
            RpcJSONError j = FoundationManager.GetErrorFromMetaData(e);
            Debug.Log(j.errorMessage);
        }

    }
}
