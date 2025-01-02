using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Dawn.Game.Event;
using OpenIM.IMSDK;
using OpenIM.Proto;

namespace Dawn.Game.UI
{

    public class UISetSelfInfo : UGuiForm
    {
        Button backBtn;
        TextMeshProUGUI userId;
        TMP_InputField nickName;
        Button headIconBtn;
        Image headIcon;
        Button saveBtn;
        IMUser userInfo;
        string headIconURL = "";
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);

            backBtn = GetButton("Panel/top/back");
            userId = GetTextPro("Panel/content/userid/val");
            nickName = GetInputField("Panel/content/nickname/input");
            headIconBtn = GetButton("Panel/content/headicon/icon");
            headIcon = GetImage("Panel/content/headicon/icon");
            saveBtn = GetButton("Panel/save");
        }
        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);

            userId.text = "";
            nickName.text = "";
            headIcon.sprite = null;
            IMSDK.GetSelfUserInfo((user) =>
            {
                this.userInfo = user;
                if (user != null)
                {
                    userId.text = user.UserID;
                    nickName.text = user.Nickname;
                    SetImage(headIcon, user.FaceURL);
                }
            });

            OnClick(backBtn, () =>
            {
                CloseSelf();
            });

            OnClick(headIconBtn, () =>
            {
                GameEntry.UI.OpenUI("SelectIcon", (OnSelectHeadIcon)OnSelectHeadIcon);
            });

            OnClick(saveBtn, () =>
            {
                if (this.userInfo != null)
                {
                    IMSDK.SetSelfInfo((suc) =>
                    {
                        if (suc)
                        {
                            GameEntry.Event.Fire(OnSelfInfoChange.EventId, new OnSelfInfoChange());
                            GameEntry.UI.Tip("Save Success");
                            CloseSelf();
                        }
                    }, new SetSelfInfoReq()
                    {
                        UserID = userInfo.UserID,
                        Nickname = nickName.text,
                        FaceURL = headIconURL,
                    });
                }
            });
        }
        public void OnSelectHeadIcon(string url)
        {
            headIconURL = url;
            SetImage(headIcon, url);
        }

        protected override void OnClose(bool isShutdown, object userData)
        {
            base.OnClose(isShutdown, userData);
            userId.text = "";
            nickName.text = "";
        }

    }
}

