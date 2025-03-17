using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using Networking;

public class LinkOpener : MonoBehaviour, IPointerClickHandler
{
    private TMP_Text text;
    private readonly string myHouseLinkString = "house";

    private void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            int linkIndex = TMP_TextUtilities.FindIntersectingLink(text, Input.mousePosition, null);
            if (linkIndex > -1)
            {
                var linkInfo = text.textInfo.linkInfo[linkIndex];
                var linkId = linkInfo.GetLinkID();
                if (linkId.Split(':')[0] == myHouseLinkString)
                {
                    OnClickGoToTheirHouse(linkId.Split(':')[1]);
                }
            }
        }
    }

    void OnClickGoToTheirHouse(string id)
    {
        //NakamaContentManager.instance.NakamaNeededToJoin_userRoomId = id;
        NakamaContentManager.instance.matchInfoNeedToJoin.SetUserRoomIDMatch(id,"");
#if MUZIVERSE_MAIN
        PersonalRoomManager.currentRoomID = id;
#endif
        StartChangeSceneToPersonalRoom();
    }

    async void StartChangeSceneToPersonalRoom()
    {
        await NakamaContentManager.instance.LeaveMatch();
        TW.AddLoading().LoadScene("PersonalRoom");


    }
}
