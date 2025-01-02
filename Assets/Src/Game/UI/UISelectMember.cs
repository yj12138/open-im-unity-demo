using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using SuperScrollView;
using OpenIM.IMSDK;
using OpenIM.Proto;

namespace Dawn.Game.UI
{
    public delegate void OnSelectFriends(IMFriend[] selectUsers);
    public class UISelectMember : UGuiForm
    {
        class Item
        {
            public Button Btn;
            public Image Icon;
            public TextMeshProUGUI Name;
            public RectTransform Select;
        }
        LoopListView2 list;
        Button backBtn;
        Button confirmBtn;
        OnSelectFriends onSelectFriends;
        Dictionary<int, IMFriend> selectFriends;
        IMFriend[] friends;
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            backBtn = GetButton("Panel/top/back");
            confirmBtn = GetButton("Panel/top/ok");
            list = GetListView("Panel/content/list");
            list.InitListView(0, (list, index) =>
            {
                if (index < 0) return null;
                var itemNode = list.NewListViewItem("item");
                if (!itemNode.IsInitHandlerCalled)
                {
                    var parent = itemNode.transform as RectTransform;
                    itemNode.UserObjectData = new Item()
                    {
                        Btn = GetButton("", parent),
                        Icon = GetImage("icon", parent),
                        Name = GetTextPro("name", parent),
                        Select = GetRectTransform("default/hot", parent)
                    };
                    itemNode.IsInitHandlerCalled = true;
                }
                var item = itemNode.UserObjectData as Item;
                var friendInfo = friends[index];
                bool hasSelect = selectFriends.ContainsKey(index);
                item.Select.gameObject.SetActive(hasSelect);
                SetImage(item.Icon, friendInfo.FaceURL);
                item.Name.text = friendInfo.Nickname;
                OnClick(item.Btn, () =>
                {
                    if (selectFriends.ContainsKey(index))
                    {
                        selectFriends.Remove(index);
                    }
                    else
                    {
                        selectFriends.Add(index, friendInfo);
                    }
                    RefreshList(this.list, friends.Length);
                });
                return itemNode;
            });

            selectFriends = new Dictionary<int, IMFriend>();
        }
        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            if (userData is OnSelectFriends)
            {
                onSelectFriends = userData as OnSelectFriends;
            }
            OnClick(backBtn, () =>
            {
                CloseSelf();
            });
            OnClick(confirmBtn, () =>
            {
                if (selectFriends.Count > 0 && onSelectFriends != null)
                {
                    var members = new IMFriend[selectFriends.Count];
                    int index = 0;
                    foreach (var member in selectFriends)
                    {
                        members[index] = member.Value;
                        index++;
                    }
                    onSelectFriends(members);
                }
                CloseSelf();
            });

            list.SetListItemCount(0);
            IMSDK.GetFriends((list) =>
            {
                if (list != null)
                {
                    friends = list;
                    RefreshList(this.list, friends.Length);
                }
            }, true);
        }
        protected override void OnClose(bool isShutdown, object userData)
        {
            base.OnClose(isShutdown, userData);
            friends = null;
            selectFriends.Clear();
        }

    }
}

