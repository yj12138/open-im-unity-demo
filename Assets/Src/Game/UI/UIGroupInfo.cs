using UnityEngine;
using TMPro;
using UnityEngine.UI;
using SuperScrollView;
using OpenIM.IMSDK;
using OpenIM.Proto;

namespace Dawn.Game.UI
{
    public class UIGroupInfo : UGuiForm
    {
        class MemeberItem
        {
            public Image Icon;
            public TextMeshProUGUI Name;
            public Button Btn;
        }
        TextMeshProUGUI title;
        Button backBtn;
        LoopGridView memberList;
        Button groupChatBtn;
        Button searchHistory;
        Button clearChat;
        Button groupExitBtn;
        TextMeshProUGUI groupExitText;
        IMGroup localGroup;
        IMGroupMember[] membersInfo;
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            title = GetTextPro("Panel/content/top/title");
            backBtn = GetButton("Panel/content/top/back");
            memberList = GetGridView("Panel/content/center/members/list");
            groupChatBtn = GetButton("Panel/content/center/chat/btn");
            searchHistory = GetButton("Panel/content/center/searchhistory/btn");
            clearChat = GetButton("Panel/content/center/clearchat/btn");
            groupExitBtn = GetButton("Panel/content/center/groupexit/btn");
            groupExitText = GetTextPro("Panel/content/center/groupexit/btn/Text (TMP)");

            memberList.InitGridView(0, (list, index, row, rol) =>
            {
                if (index < 0) return null;
                bool isAdd = index == membersInfo.Length;
                var itemNode = isAdd ? list.NewListViewItem("add") : list.NewListViewItem("item");
                if (!itemNode.IsInitHandlerCalled)
                {
                    var parent = itemNode.transform as RectTransform;
                    itemNode.UserObjectData = new MemeberItem()
                    {
                        Icon = GetImage("icon", parent),
                        Btn = GetButton("", parent),
                        Name = GetTextPro("name", parent)
                    };
                    itemNode.IsInitHandlerCalled = true;
                }
                MemeberItem item = itemNode.UserObjectData as MemeberItem;
                if (isAdd)
                {
                    item.Name.text = "";
                    OnClick(item.Btn, () =>
                    {
                        GameEntry.UI.OpenUI("SelectMember", (OnSelectFriends)OnSelectFriends);
                    });
                }
                else
                {
                    var info = membersInfo[index];
                    SetImage(item.Icon, info.FaceURL);
                    item.Name.text = info.Nickname;
                    OnClick(item.Btn, () =>
                    {
                        GameEntry.UI.OpenUI("UserInfo", info.UserID);
                    });
                }
                return itemNode;
            });
        }
        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            localGroup = userData as IMGroup;

            title.text = localGroup.GroupName;

            OnClick(backBtn, () =>
            {
                CloseSelf();
            });
            if (localGroup.OwnerUserID == Player.Instance.UserId)
            {
                groupExitText.text = "解散群组";
            }
            else
            {
                groupExitText.text = "离开群组";
            }
            OnClick(groupExitBtn, () =>
            {
                if (localGroup.OwnerUserID == Player.Instance.UserId)
                {
                    IMSDK.DismissGroup((suc) =>
                    {
                        if (suc)
                        {
                            CloseSelf();
                        }
                    }, localGroup.GroupID);
                }
                else
                {
                    IMSDK.QuitGroup((suc) =>
                    {
                        if (suc)
                        {
                            CloseSelf();
                        }
                    }, localGroup.GroupID);
                }
            });
            OnClick(groupChatBtn, () =>
            {
                IMSDK.GetOneConversation((conversation) =>
                {
                    if (conversation != null)
                    {
                        GameEntry.UI.OpenUI("Chat", conversation);
                    }
                }, SessionType.Single, localGroup.GroupID);
            });

            OnClick(searchHistory, () =>
            {
                GameEntry.UI.Tip("TODO");
            });
            OnClick(clearChat, () =>
            {
                GameEntry.UI.Tip("TODO");
            });

            RefreshUI();
        }
        protected override void OnClose(bool isShutdown, object userData)
        {
            base.OnClose(isShutdown, userData);
        }
        void RefreshUI()
        {
            IMSDK.GetGroupMembers((list) =>
            {
                if (list != null)
                {
                    membersInfo = list;
                    RefreshGrid(memberList, membersInfo.Length + 1);
                }
            }, localGroup.GroupID, 0, 0, 0);
        }

        void OnSelectFriends(IMFriend[] selectUsers)
        {
            if (selectUsers.Length <= 0) return;
            var name = "";
            var members = new string[selectUsers.Length];
            for (int i = 0; i < selectUsers.Length; i++)
            {
                name = name + selectUsers[i].Nickname;
                members[i] = selectUsers[i].FriendUserID;
            }
            IMSDK.InviteUserToGroup((suc) =>
            {
                if (suc)
                {
                    GameEntry.UI.Tip("Invite User Suc");
                }
            }, localGroup.GroupID, "", members);
        }
    }
}

