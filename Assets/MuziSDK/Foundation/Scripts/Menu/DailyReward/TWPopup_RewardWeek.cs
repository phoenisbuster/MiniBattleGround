using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Grpc.Core;
using UnityEngine;
using Useractivity.Quest;
using DG.Tweening;
using static Useractivity.Quest.UserQuestService;
using static Useractivity.Quest.UserQuestFilterService;
using Muziverse.Proto.GameContent.Domain;

public class TWPopup_RewardWeek : TWBoard
{
    // Start is called before the first frame update
    public TWPopup_RewardWeek_Item item;

    [SerializeField] private List<TWPopup_RewardWeek_Item> items = new List<TWPopup_RewardWeek_Item>();

    IEnumerator Start()
    {
        base.InitTWBoard();
        GetLoginRewardsList();
        yield return new WaitForSeconds(0.5f);
    }

    async void GetLoginRewardsList()
    {
        while (!FoundationManager.IsConnectedToMuziServer)
            await Task.Delay(100);

        UserQuestFilterServiceClient client = new UserQuestFilterServiceClient(FoundationManager.channel);
        try
        {
            QuestFilterRequest request = new QuestFilterRequest
            {
                Type = QuestType.Login,
                Status = QuestStatus.Open,
                ZoneOffset = FoundationManager.MyTimeZone
            };
            var response = await client.FilterAsync(request, FoundationManager.metadata);

            for (int i = 0; i < response.Quests.Count; i++)
            { 
                items[i].Init(response.Quests[i]);
                items[i].onClaimed += ShiftTomorrowItemMsg;
            }
        }
        catch (RpcException e)
        {
            RpcJSONError j = FoundationManager.GetErrorFromMetaData(e);
            Debug.Log(j.errorMessage);
        }

        try
        {
            UserQuestServiceClient client2 = new UserQuestServiceClient(FoundationManager.channel);
            GetLoginQuestProgressRequest progressRequest = new GetLoginQuestProgressRequest { ZoneOffset = FoundationManager.MyTimeZone };
            var response = await client2.GetLoginQuestProgressAsync(progressRequest, FoundationManager.metadata);
            for (int i = 0; i < items.Count; i++)
            {
                if (i < response.FinishedQuests)        // If finished quests is 1 or more set claimed accordingly 
                    items[i].SetClaimedReward();
                else if (i > response.FinishedQuests)   // If i > number of finished quest = not claimable 
                    items[i].SetItemStatus(false);
                else                                    // If i = number of finished quest, check whether user claimed reward today
                {
                    var isTodayClaimed = response.IsTodayClaimed;
                    items[i].SetItemStatus(!isTodayClaimed);
                    if (isTodayClaimed) items[i].ToggleNextItemMsg(); // If reward claimed toggle msg for tmr reward
                }
            }
        }
        catch (RpcException e)
        {
            RpcJSONError j = FoundationManager.GetErrorFromMetaData(e);
            Debug.Log(j.errorMessage);
        }
    }

    private void ShiftTomorrowItemMsg(TWPopup_RewardWeek_Item claimedItem)
    {
        int nextItemIndex = items.IndexOf(claimedItem) + 1;
        if(nextItemIndex < items.Count) items[nextItemIndex].ToggleNextItemMsg(); // Show next item msg if not exceeded 7 days
    }
  
}
