using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

namespace MultiplayerPlusPlusMod.Controllers
{
    class RoomController : MonoBehaviourSingleton<RoomController>, IConnectionCallbacks, ILobbyCallbacks
    {
        private LoadBalancingClient client;
        public TMP_Text refreshListBtn;
        //Debug
        /*private string debugInfo = "";
        private GUIStyle debugInfolStyle = new GUIStyle();
        public string extraInfo { get; set; } = "";*/

        private void Start()
        {
            client = new LoadBalancingClient();
            client.AddCallbackTarget(this);
            ShowPublicRoomsHiddenButtons();
            refreshListBtn = Array.Find(MultiplayerManager.Instance.menuController.roomList.GetComponentsInChildren<TMP_Text>(), button => button.text == "Refresh List");

            //debugInfolStyle.normal.textColor = Color.green;
        }

        /*private void OnGUI()
        {
            GUI.TextField(new Rect(5, 15, 500, 200), debugInfo, debugInfolStyle);
        }*/

        private void Update()
        {
            if(client.IsConnected) client.Service();
            //UpdateDebugInfo();
        }

        /*private void UpdateDebugInfo()
        {
            string photonInfo = "";
            if (PhotonNetwork.IsConnected)
            {
                photonInfo = "Rooms : " + PhotonNetwork.CountOfRooms
                    + " | Players : " + PhotonNetwork.CountOfPlayers
                    + "\n MasterServerAddress : " + PhotonNetwork.NetworkingClient.MasterServerAddress
                    + " | AppVersion : " + PhotonNetwork.NetworkingClient.AppVersion
                    + " | CloudRegion : " + PhotonNetwork.NetworkingClient.CloudRegion
                    ;
            }

            if (PlayerController.Instance.currentStateEnum == PlayerController.CurrentState.Setup)
                extraInfo = "";

            debugInfo = "Player State : " + PlayerController.Instance.currentStateEnum
                + "\n photonInfo : " + photonInfo
                + "\n extraInfo : " + extraInfo
                ;
        }*/

        private void ShowPublicRoomsHiddenButtons()
        {
            foreach (MultiplayerMainMenu.ButtonVisibilityDef buttonVisibilityDef in MultiplayerManager.Instance.menuController.mainMenu.options)
            {
                if (buttonVisibilityDef.name == "Browse Rooms Button" || buttonVisibilityDef.name == "Create Room Button")
                    buttonVisibilityDef.showOnlyWithDebugCheats = false;

                else if(buttonVisibilityDef.name == "Join Next Map Button")
                {
                    TMP_Text JoinNextMapText = buttonVisibilityDef.buttonGO.GetComponentInChildren<TMP_Text>();
                    if (JoinNextMapText != null) JoinNextMapText.SetText("Browse Rooms");
                }
            }
        }

        public void RefreshRoomListWhileInRoom()
        {
            if (PhotonNetwork.InRoom && !client.IsConnected)
            {
                refreshListBtn.SetText("Searching Rooms...");
                client.AppId = PhotonNetwork.NetworkingClient.AppId;
                client.AppVersion = PhotonNetwork.NetworkingClient.AppVersion;
                client.MasterServerAddress = PhotonNetwork.NetworkingClient.MasterServerAddress;
                client.ConnectToRegionMaster(PhotonNetwork.NetworkingClient.CloudRegion);
            }

        }

        public void OnConnectedToMaster()
        {
            client.OpJoinLobby(MultiplayerManager.TYPEDLOBBY);
        }

        public void OnJoinedLobby()
        {
            client.OpGetGameList(MultiplayerManager.TYPEDLOBBY, "TRUE");
        }

        public void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            if (PhotonNetwork.InRoom)
                roomList.Remove(roomList.Find(room => room.Name == PhotonNetwork.CurrentRoom.Name));

            MultiplayerManager.Instance.roomList = roomList;
            MultiplayerManager.Instance.menuController.roomList.UpdateList();
            client.Disconnect(DisconnectCause.None);
        }

        public void OnDisconnected(DisconnectCause cause)
        {
            refreshListBtn.SetText("Refresh List");
        }

        public void OnConnected()
        {
        }

        public void OnLeftLobby()
        {
        }

        public void OnCustomAuthenticationFailed(string debugMessage)
        {
        }

        public void OnCustomAuthenticationResponse(Dictionary<string, object> data)
        {
        }

        public void OnLobbyStatisticsUpdate(List<TypedLobbyInfo> lobbyStatistics)
        {
        }

        public void OnRegionListReceived(RegionHandler regionHandler)
        {
        }
    }
}
