using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using SuperScrollView;
using OpenIM.IMSDK;
using OpenIM.Proto;

namespace Dawn.Game.UI
{
    public class UICreateGroup : UGuiForm
    {
        class MemeberItem
        {
            public Image Icon;
            public TextMeshProUGUI Name;
            public Button Btn;
        }
        Button backBtn;
        Button inviteBtn;
        Button createBtn;
        Button faceBtn;
        Image faceIcon;
        TMP_InputField groupNameInput;
        LoopListView2 memberList;
        IMFriend[] selectFriends;
        string selectIcon = "";
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            backBtn = GetButton("Panel/content/top/back");
            groupNameInput = GetInputField("Panel/content/center/groupname/input");
            inviteBtn = GetButton("Panel/content/top/invite");
            faceBtn = GetButton("Panel/content/center/face/icon");
            faceIcon = GetImage("Panel/content/center/face/icon");
            createBtn = GetButton("Panel/content/center/createbtn/btn");
            memberList = GetListView("Panel/content/center/members/list");

            selectFriends = new IMFriend[0];
            memberList.InitListView(0, (list, index) =>
            {
                if (index < 0) return null;
                var itemNode = list.NewListViewItem("item");
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
                var info = selectFriends[index];
                SetImage(item.Icon, info.FaceURL);
                item.Name.text = info.Nickname;
                return itemNode;
            });
        }
        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);

            OnClick(backBtn, () =>
            {
                CloseSelf();
            });
            OnClick(inviteBtn, () =>
            {
                GameEntry.UI.OpenUI("SelectMember", (OnSelectFriends)OnSelectFriends);
            });
            OnClick(faceBtn, () =>
            {
                GameEntry.UI.OpenUI("SelectIcon", (OnSelectHeadIcon)OnSelectHeadIcon);
            });
            OnClick(createBtn, () =>
            {
                if (groupNameInput.text == "")
                {
                    GameEntry.UI.Tip("groupName is empty");
                    return;
                }
                if (selectFriends.Length <= 0)
                {
                    GameEntry.UI.Tip("not select members");
                    return;
                }
                List<string> membersId = new List<string>();
                foreach (var userInfo in selectFriends)
                {
                    membersId.Add(userInfo.FriendUserID);
                }
                var req = new CreateGroupReq()
                {
                    GroupName = groupNameInput.text,
                };
                req.MemberUserIDs.Add(membersId);
                IMSDK.CreateGroup((groupInfo) =>
                {
                    if (groupInfo != null)
                    {
                        GameEntry.UI.Tip("Create Group Success");
                        CloseSelf();
                    }
                }, req);
            });
            selectIcon = "headicon/不知火舞";
            groupNameInput.text = "";
        }
        protected override void OnClose(bool isShutdown, object userData)
        {
            base.OnClose(isShutdown, userData);
        }
        public void OnSelectHeadIcon(string url)
        {
            SetImage(faceIcon, url);
            selectIcon = url;
        }
        void OnSelectFriends(IMFriend[] list)
        {
            if (list.Length <= 0) return;
            selectFriends = list;
            RefreshList(memberList, selectFriends.Length);
        }
    }
}

