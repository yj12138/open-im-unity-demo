using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using SuperScrollView;
using OpenIM.IMSDK;
using Dawn.Game.Event;
using GameFramework.Event;
using OpenIM.Proto;

namespace Dawn.Game.UI
{
    public class UIGroupList : UGuiForm
    {
        public class GroupItem
        {
            public Image Icon;
            public TextMeshProUGUI Name;
            public SwipeButton SwipeBtn;
            public RectTransform Rect;
            public Button DeleteBtn;
        }
        Button backBtn;
        Button createGroupBtn;
        LoopListView2 groupList;
        IMGroup[] groupListInfo;
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            backBtn = GetButton("Panel/content/top/back");
            createGroupBtn = GetButton("Panel/content/top/create");
            groupList = GetListView("Panel/content/list");
            groupList.InitListView(0, (list, index) =>
            {
                if (index < 0) return null;
                if (groupListInfo.Length <= index) return null;
                LoopListViewItem2 itemNode = list.NewListViewItem("item");
                if (!itemNode.IsInitHandlerCalled)
                {
                    var parent = itemNode.transform as RectTransform;
                    itemNode.UserObjectData = new GroupItem()
                    {
                        Rect = parent,
                        SwipeBtn = GetControl(typeof(SwipeButton), "", parent) as SwipeButton,
                        Icon = GetImage("icon", parent),
                        Name = GetTextPro("name", parent),
                        DeleteBtn = GetButton("menu/delete", parent),
                    };
                    itemNode.IsInitHandlerCalled = true;
                }
                var info = groupListInfo[index];
                var item = itemNode.UserObjectData as GroupItem;
                item.Name.text = info.GroupName;
                SetImage(item.Icon, info.FaceURL);
                item.SwipeBtn.OnSwipe.RemoveAllListeners();
                item.SwipeBtn.OnSwipe.AddListener((dx, dy) =>
                {
                    var pos = item.Rect.anchoredPosition;
                    pos.x += dx;
                    pos.x = Mathf.Clamp(pos.x, -300, 0);
                    item.Rect.anchoredPosition = pos;
                });
                item.SwipeBtn.OnClick.RemoveAllListeners();
                item.SwipeBtn.OnClick.AddListener(() =>
                {
                    GameEntry.UI.OpenUI("GroupInfo", info);
                });
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
            OnClick(createGroupBtn, () =>
            {
                GameEntry.UI.OpenUI("CreateGroup");
            });
            groupListInfo = new IMGroup[0];
            RefreshGroupList();
            GameEntry.Event.Subscribe(OnGroupChange.EventId, handleGroupChange);

        }
        protected override void OnClose(bool isShutdown, object userData)
        {
            base.OnClose(isShutdown, userData);
            GameEntry.Event.Unsubscribe(OnGroupChange.EventId, handleGroupChange);
        }

        void RefreshGroupList()
        {
            IMSDK.GetJoinedGroups((list) =>
            {
                if (list != null)
                {
                    groupListInfo = list;
                    RefreshList(groupList, groupListInfo.Length);
                }
            });
        }

        void handleGroupChange(object sender, GameEventArgs e)
        {
            var args = e as OnGroupChange;
            if (args.IsGroupCountChange())
            {
                RefreshGroupList();
            }
        }
    }
}

