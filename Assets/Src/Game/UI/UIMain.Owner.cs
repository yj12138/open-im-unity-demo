using UnityEngine;
using TMPro;
using UnityEngine.UI;
using OpenIM.IMSDK;
using Dawn.Game.Event;
using GameFramework.Event;

namespace Dawn.Game.UI
{
    public partial class UIMain
    {
        RectTransform ownerRoot;
        Image ownerIcon;
        TextMeshProUGUI ownerName;
        Button setOwnerInfoBtn;

        Button settingBtn;
        Button logoutBtn;

        void InitOwner()
        {
            ownerRoot = GetRectTransform("Panel/content/center/owner");
            ownerIcon = GetImage("Panel/content/center/owner/top/icon");
            ownerName = GetTextPro("Panel/content/center/owner/top/name");
            setOwnerInfoBtn = GetButton("Panel/content/center/owner/center/setuserinfo");
            settingBtn = GetButton("Panel/content/center/owner/center/setting");
            logoutBtn = GetButton("Panel/content/center/owner/center/logout");
        }

        void OpenOwner()
        {
            OnClick(setOwnerInfoBtn, () =>
            {
                GameEntry.UI.OpenUI("SetSelfInfo");
            });
            OnClick(settingBtn, () =>
            {
                GameEntry.UI.OpenUI("Setting");
            });
            OnClick(logoutBtn, () =>
            {
                IMSDK.Logout((suc) =>
                {
                    if (suc)
                    {
                        GameEntry.Event.Fire(this, new OnLogout());
                    }
                });
            });
            RefreshUserInfo();
            GameEntry.Event.Subscribe(OnSelfInfoChange.EventId, OnSelfInfoChane);
        }
        void RefreshUserInfo()
        {
            ownerName.text = "";
            IMSDK.GetSelfUserInfo((localUser) =>
            {
                if (localUser != null)
                {
                    ownerName.text = localUser.Nickname;
                    SetImage(ownerIcon, localUser.FaceURL);
                }
            });
        }
        void OnSelfInfoChane(object sender, GameEventArgs args)
        {
            RefreshUserInfo();
        }
        void CloseOwner()
        {
            GameEntry.Event.Unsubscribe(OnSelfInfoChange.EventId, OnSelfInfoChane);
        }
    }
}

