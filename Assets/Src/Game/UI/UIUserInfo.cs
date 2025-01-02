using UnityEngine;
using TMPro;
using UnityEngine.UI;
using OpenIM.IMSDK;
using OpenIM.Proto;

namespace Dawn.Game.UI
{

    public class UIUserInfo : UGuiForm
    {
        Button backBtn;
        Image userIcon;
        TextMeshProUGUI userName;
        TMP_InputField reqMsg;

        RectTransform friendTrans;
        Button remarkBtn;
        Button sendMsgBtn;
        Button audioChatBtn;
        RectTransform addTrans;
        Button addBtn;
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            backBtn = GetButton("Panel/content/top/back");
            userIcon = GetImage("Panel/content/center/icon");
            userName = GetTextPro("Panel/content/center/userid");
            friendTrans = GetRectTransform("Panel/content/bottom/friend");
            remarkBtn = GetButton("Panel/content/bottom/friend/remark");
            sendMsgBtn = GetButton("Panel/content/bottom/friend/msg");
            audioChatBtn = GetButton("Panel/content/bottom/friend/audiochat");
            addTrans = GetRectTransform("Panel/content/bottom/add");
            reqMsg = GetInputField("Panel/content/bottom/add/reqmsg");
            addBtn = GetButton("Panel/content/bottom/add/btn");
        }

        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            string userId = userData as string;
            userName.text = userId;
            OnClick(backBtn, () =>
            {
                CloseSelf();
            });
            OnClick(remarkBtn, () =>
            {
                GameEntry.UI.Tip("TODO");
            });
            OnClick(audioChatBtn, () =>
            {
                GameEntry.UI.Tip("TODO");
            });

            userIcon.sprite = null;
            userName.text = "";
            if (userId == Player.Instance.UserId)
            {
                IMSDK.GetSelfUserInfo((userInfo) =>
                {
                    if (userInfo != null)
                    {
                        SetImage(userIcon, userInfo.FaceURL);
                        userName.text = userInfo.Nickname;
                    }
                });
                addTrans.gameObject.SetActive(false);
                friendTrans.gameObject.SetActive(false);
                addTrans.gameObject.SetActive(false);
            }
            else
            {
                IMSDK.GetUsersInfo((list) =>
                {
                    if (list != null)
                    {
                        if (list.Length >= 1)
                        {
                            var userInfo = list[0];
                            SetImage(userIcon, userInfo.FaceURL);
                            userName.text = userInfo.Nickname;
                        }
                    }
                }, new string[1] { userId });

                IMSDK.CheckFriend((list) =>
                {
                    if (list != null && list.Length == 1)
                    {
                        if (list[0].Result == CheckFriendResult.IsFriend)
                        {
                            friendTrans.gameObject.SetActive(true);
                            addTrans.gameObject.SetActive(false);

                            OnClick(sendMsgBtn, () =>
                            {
                                IMSDK.GetOneConversation((conversation) =>
                                {
                                    if (conversation != null)
                                    {
                                        GameEntry.UI.OpenUI("Chat", conversation);
                                    }
                                }, SessionType.Single, userId);
                            });
                        }
                        else
                        {
                            friendTrans.gameObject.SetActive(false);
                            addTrans.gameObject.SetActive(true);
                            OnClick(addBtn, () =>
                            {
                                IMSDK.AddFriend((suc) =>
                                {
                                    if (suc)
                                    {
                                        CloseSelf();
                                    }
                                }, userId, reqMsg.text, "");
                            });
                        }
                    }
                }, new string[] { userId });
            }

        }

        protected override void OnClose(bool isShutdown, object userData)
        {
            base.OnClose(isShutdown, userData);
        }
    }
}

