using FMUtils.KeyboardHook;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MW_Debug_2
{
    public class DC
    {
        private static StreamWriter writer;
        public static void Init()
        {
            writer = new StreamWriter("mwdebug_log.log");
            writer.AutoFlush = true;
            WriteLine("Init");
#if DEBUG
#endif
        }

        public static void Write(object o)
        {
            writer.Write(o); 
#if DEBUG
            Console.Write(o);
#endif
        }

        public static void WriteLine(object o, bool debugTxt = true)
        {
            writer.WriteLine(DateTime.Now + ": " + o);
#if DEBUG
            if (debugTxt) Console.Write("[DEBUG] ");
            Console.WriteLine(o);
#endif
        }
        static Hook KeyboardHook;

        public static void HookKbd()
        {
            KeyboardHook = new Hook("Global Action Hook");
            KeyboardHook.KeyUpEvent += MWDBG.KeyUp;
        }


        public static void InitFng()
        {
            Info.FNG.list.Add((IntPtr)0x893ab8, "Infractions.fng");
            Info.FNG.list.Add((IntPtr)0x8948da, " AIII=BLACK_BACKGROUND.fng");
            Info.FNG.list.Add((IntPtr)0x8948f8, "BUSTED_OVERLAY.fng");
            Info.FNG.list.Add((IntPtr)0x89490c, "BLACK_FADE_OUT.fng");
            Info.FNG.list.Add((IntPtr)0x894920, "FLASHERS.fng");
            Info.FNG.list.Add((IntPtr)0x89718c, "Pause_PCDisplayOptions.fng");
            Info.FNG.list.Add((IntPtr)0x8971a8, "Pause_PC_Controller.fng");
            Info.FNG.list.Add((IntPtr)0x8971c0, "InGameRaceSheet.fng");
            Info.FNG.list.Add((IntPtr)0x8971d4, "InGameBounty.fng");
            Info.FNG.list.Add((IntPtr)0x8971e8, "InGameMilestones.fng");
            Info.FNG.list.Add((IntPtr)0x897210, "PostRace_Pursuit.fng");
            Info.FNG.list.Add((IntPtr)0x897228, "PostRace_MilestoneRewards.fng");
            Info.FNG.list.Add((IntPtr)0x897248, "WS_MW_LS_AttractFMV.fng");
            Info.FNG.list.Add((IntPtr)0x897260, "WS_LS_IntroFMV.fng");
            Info.FNG.list.Add((IntPtr)0x897274, "WS_LS_PSA.fng");
            Info.FNG.list.Add((IntPtr)0x897284, "WS_LS_EA_hidef.fng");
            Info.FNG.list.Add((IntPtr)0x897298, "WS_LS_EALogo.fng");
            Info.FNG.list.Add((IntPtr)0x8972ac, "MW_LS_AttractFMV.fng");
            Info.FNG.list.Add((IntPtr)0x8972c4, "MW_LS_IntroFMV.fng");
            Info.FNG.list.Add((IntPtr)0x8972d8, "LS_PSA.fng");
            Info.FNG.list.Add((IntPtr)0x8972e4, "LS_EA_hidef.fng");
            Info.FNG.list.Add((IntPtr)0x8972f4, "LS_EALogo.fng");
            Info.FNG.list.Add((IntPtr)0x897304, "FEAnyTutorial.fng");
            Info.FNG.list.Add((IntPtr)0x897318, "WS_FEAnyMovie.fng");
            Info.FNG.list.Add((IntPtr)0x89732c, "FEAnyMovie.fng");
            Info.FNG.list.Add((IntPtr)0x89733c, "InGameAnyTutorial.fng");
            Info.FNG.list.Add((IntPtr)0x897354, "WS_InGameAnyMovie.fng");
            Info.FNG.list.Add((IntPtr)0x89736c, "InGameDialog.fng");
            Info.FNG.list.Add((IntPtr)0x897380, "InGame_MC_Main.fng");
            Info.FNG.list.Add((IntPtr)0x897394, "Pause_Performance_Tuning.fng");
            Info.FNG.list.Add((IntPtr)0x8973b4, "InGameAnyMovie.fng");
            Info.FNG.list.Add((IntPtr)0x8973c8, "InGame_MC_Main_GC.fng");
            Info.FNG.list.Add((IntPtr)0x8973e0, "SixDaysLater.fng");
            Info.FNG.list.Add((IntPtr)0x8973f4, "SMS_Message.fng");
            Info.FNG.list.Add((IntPtr)0x897404, "FadeScreen.fng");
            Info.FNG.list.Add((IntPtr)0x897414, "EA_Trax.fng");
            Info.FNG.list.Add((IntPtr)0x897420, "InGamePhotoMaster.fng");
            Info.FNG.list.Add((IntPtr)0x897438, "Pause_Controller.fng");
            Info.FNG.list.Add((IntPtr)0x897450, "Pause_Options.fng");
            Info.FNG.list.Add((IntPtr)0x897464, "Pause_Main.fng");
            Info.FNG.list.Add((IntPtr)0x89ae0c, "OL_LobbyRoom.fng");
            Info.FNG.list.Add((IntPtr)0x89ae20, "OL_GameRoom.fng");
            Info.FNG.list.Add((IntPtr)0x89ae38, "UI_PC_LAN_ServerSelect.fng");
            Info.FNG.list.Add((IntPtr)0x89ae54, "MainMenu.fng");
            Info.FNG.list.Add((IntPtr)0x89ae64, "OL_UseExisting.fng");
            Info.FNG.list.Add((IntPtr)0x89ae78, "UI_OLAgeVerif.fng");
            Info.FNG.list.Add((IntPtr)0x89ae8c, "OL_PC_REGISTRATION.fng");
            Info.FNG.list.Add((IntPtr)0x89aea4, "OL_Create_Account_Country.fng");
            Info.FNG.list.Add((IntPtr)0x89aec4, "UI_OLX_Info_Sharing.fng");
            Info.FNG.list.Add((IntPtr)0x89aedc, "OL_Create_Account.fng");
            Info.FNG.list.Add((IntPtr)0x89aef4, "OL_SelectPersona.fng");
            Info.FNG.list.Add((IntPtr)0x89af0c, "UI_OL_WebOffer2.fng");
            Info.FNG.list.Add((IntPtr)0x89af20, "OLEALogin.fng");
            Info.FNG.list.Add((IntPtr)0x89af30, "UI_OLAuthDNAS.fng");
            Info.FNG.list.Add((IntPtr)0x89af44, "UI_OLISPConnect.fng");
            Info.FNG.list.Add((IntPtr)0x89af58, "UI_OLNetSelect.fng");
            Info.FNG.list.Add((IntPtr)0x89af6c, "GenericDialog_MED.fng");
            Info.FNG.list.Add((IntPtr)0x89af84, "SafeHouseReputationOverview.fng");
            Info.FNG.list.Add((IntPtr)0x89afa4, "SafeHouseRivalBio.fng");
            Info.FNG.list.Add((IntPtr)0x89afbc, "MC_Main.fng");
            Info.FNG.list.Add((IntPtr)0x89afc8, "SafeHouseRegionUnlock.fng");
            Info.FNG.list.Add((IntPtr)0x89afe4, "SafeHouseMarkers.fng");
            Info.FNG.list.Add((IntPtr)0x89affc, "SafeHouseRivalChallenge.fng");
            Info.FNG.list.Add((IntPtr)0x89b044, "OL_MAIN.fng");
            Info.FNG.list.Add((IntPtr)0x89b2c8, "GarageMain.fng");
            Info.FNG.list.Add((IntPtr)0x89b2d8, "EA_TRAX.fng");
            Info.FNG.list.Add((IntPtr)0x89b2e4, "ScreenPrintf.fng");
            Info.FNG.list.Add((IntPtr)0x89b5c4, "EA_Trax_Jukebox.fng");
            Info.FNG.list.Add((IntPtr)0x89b5d8, "OL_MAIN.FNG");
            Info.FNG.list.Add((IntPtr)0x89b72c, "SMS_MailBoxes.fng");
            Info.FNG.list.Add((IntPtr)0x89b760, "Autosave_Overlay.fng");
            Info.FNG.list.Add((IntPtr)0x89b810, "MC_List.fng");
            Info.FNG.list.Add((IntPtr)0x89cbf0, "ControllerUnplugged.fng");
            Info.FNG.list.Add((IntPtr)0x89cc28, "InGameBackground.fng");
            Info.FNG.list.Add((IntPtr)0x89cca0, "MC_DeleteProfile.fng");
            Info.FNG.list.Add((IntPtr)0x89ce1c, "ChallengeSeries.fng");
            Info.FNG.list.Add((IntPtr)0x89d1f0, "MainMenu_Sub.fng");
            Info.FNG.list.Add((IntPtr)0x89d370, "RapSheetMain.fng");
            Info.FNG.list.Add((IntPtr)0x89d384, "RapSheetLogin2.fng");
            Info.FNG.list.Add((IntPtr)0x89d3a0, "RapSheetRankings.fng");
            Info.FNG.list.Add((IntPtr)0x89d3b8, "RapSheetTEP.fng");
            Info.FNG.list.Add((IntPtr)0x89d3c8, "RapSheetCTS.fng");
            Info.FNG.list.Add((IntPtr)0x89d3d8, "RapSheetUS.fng");
            Info.FNG.list.Add((IntPtr)0x89d3e8, "RapSheetVD.fng");
            Info.FNG.list.Add((IntPtr)0x89d3f8, "RapSheetRS.fng");
            Info.FNG.list.Add((IntPtr)0x89d510, "RapSheetPD.fng");
            Info.FNG.list.Add((IntPtr)0x89d998, "Car_Select.fng");
            Info.FNG.list.Add((IntPtr)0x89d9a8, "RapSheetLogin.fng");
            Info.FNG.list.Add((IntPtr)0x89d9bc, "SafehouseReputationOverview.fng");
            Info.FNG.list.Add((IntPtr)0x89da8c, "RapSheetMain_ENDGAME.fng");
            Info.FNG.list.Add((IntPtr)0x89daa8, "RapSheetLogin2_ENDGAME.fng");
            Info.FNG.list.Add((IntPtr)0x89dac4, "RapSheetLogin_ENDGAME.fng");
            Info.FNG.list.Add((IntPtr)0x89dae0, "Credits.fng");
            Info.FNG.list.Add((IntPtr)0x89dc18, "OL_ForgotAccountName.fng");
            Info.FNG.list.Add((IntPtr)0x89dc44, "UI_DateEntry.fng");
            Info.FNG.list.Add((IntPtr)0x89dc98, "OL_Find_Session.fng");
            Info.FNG.list.Add((IntPtr)0x89dcd4, "UI_OLPassword.fng");
            Info.FNG.list.Add((IntPtr)0x89dd80, "OL_GameRoom_Player_Details.fng");
            Info.FNG.list.Add((IntPtr)0x89ddf4, "OL_EAMessenger.fng");
            Info.FNG.list.Add((IntPtr)0x89ded4, "OL_Quickrace_Main.fng");
            Info.FNG.list.Add((IntPtr)0x89deec, "UI_OLX_OptiMatch_Filters.fng");
            Info.FNG.list.Add((IntPtr)0x89dff8, "OL_Rankings.fng");
            Info.FNG.list.Add((IntPtr)0x89e008, "OL_Rankings_Personal.fng");
            Info.FNG.list.Add((IntPtr)0x89e024, "OL_Rankings_Leaderboard.fng");
            Info.FNG.list.Add((IntPtr)0x89e158, "OLX_FindResults.fng");
            Info.FNG.list.Add((IntPtr)0x89e17c, "OL_FriendDialogue.fng");
            Info.FNG.list.Add((IntPtr)0x89e234, "OLX_Message.fng");
            Info.FNG.list.Add((IntPtr)0x89e244, "OL_Challenge.fng");
            Info.FNG.list.Add((IntPtr)0x89e3a8, "FadeScreenNoLoadingBar.fng");
            Info.FNG.list.Add((IntPtr)0x89e418, "MC_ProfileManager.fng");
            Info.FNG.list.Add((IntPtr)0x89e540, "Options_PC_Controller.fng");
            Info.FNG.list.Add((IntPtr)0x89e55c, "Options.fng");
            Info.FNG.list.Add((IntPtr)0x89e568, "OptionsPCDisplay.fng");
            Info.FNG.list.Add((IntPtr)0x89e580, "Dialog.fng");
            Info.FNG.list.Add((IntPtr)0x89e778, "PostRace_Results.fng");
            Info.FNG.list.Add((IntPtr)0x89e790, "WS_Loading.fng");
            Info.FNG.list.Add((IntPtr)0x89e7a0, "Loading.fng");
            Info.FNG.list.Add((IntPtr)0x89e808, "OL_News_and_Terms.fng");
            Info.FNG.list.Add((IntPtr)0x89e8e4, "OL_Rankings_Personal_Overlay.fng");
            Info.FNG.list.Add((IntPtr)0x89eb04, "RapSheetRankingsDetail.fng");
            Info.FNG.list.Add((IntPtr)0x89eb30, "InGameReputationOverview.fng");
            Info.FNG.list.Add((IntPtr)0x89eb50, "Showcase.fng");
            Info.FNG.list.Add((IntPtr)0x89ec10, "UIOLAgeVerif.fng");
            Info.FNG.list.Add((IntPtr)0x89ec54, "UI_OLFilters.fng");
            Info.FNG.list.Add((IntPtr)0x89ece8, "OL_Feedback.fng");
            Info.FNG.list.Add((IntPtr)0x89edec, "MyCarsManager.fng");
            Info.FNG.list.Add((IntPtr)0x89ee38, "InGameRivalBio.fng");
            Info.FNG.list.Add((IntPtr)0x89ee4c, "SafeHouseBounty.fng");
            Info.FNG.list.Add((IntPtr)0x89ee60, "SafeHouseMilestones.fng");
            Info.FNG.list.Add((IntPtr)0x89ee78, "SafeHouseRaceSheet.fng");
            Info.FNG.list.Add((IntPtr)0x89ee90, "InGameRivalChallenge.fng");
            Info.FNG.list.Add((IntPtr)0x89ef34, "UI_OLViewTrack.fng");
            Info.FNG.list.Add((IntPtr)0x89ef48, "OL_GameRoom_Edit_Race.fng");
            Info.FNG.list.Add((IntPtr)0x89ef64, "OL_GameRoom_Car_Select.fng");
            Info.FNG.list.Add((IntPtr)0x89f018, "OL_VoiceChat.fng");
            Info.FNG.list.Add((IntPtr)0x89f054, "UI_PC_LAN.fng");
            Info.FNG.list.Add((IntPtr)0x89f170, "PC_OL_SEARCH.fng");
            Info.FNG.list.Add((IntPtr)0x89f1c0, "SMS_Mailboxes.fng");
            Info.FNG.list.Add((IntPtr)0x89f720, "MC_Main_GC.fng");
            Info.FNG.list.Add((IntPtr)0x89f730, "MC_Bootup_GC.fng");
            Info.FNG.list.Add((IntPtr)0x89f744, "MC_Deleteprofile.fng");
            Info.FNG.list.Add((IntPtr)0x89f75c, "OLX_Connecting2XBL.fng");
            Info.FNG.list.Add((IntPtr)0x89f774, "UI_OLX_OptiMatch_Available.fng");
            Info.FNG.list.Add((IntPtr)0x89f794, "OL_Dialog_Stacked_Buttons.fng");
            Info.FNG.list.Add((IntPtr)0x89f7b4, "OL_Dialog.fng");
            Info.FNG.list.Add((IntPtr)0x89f7c4, "UI_OL_Disconnect_BG.fng");
            Info.FNG.list.Add((IntPtr)0x89f7dc, "PostBusted.fng");
            Info.FNG.list.Add((IntPtr)0x89f7ec, "LS_LangSelect.fng");
            Info.FNG.list.Add((IntPtr)0x89f800, "loading_boot.fng");
            Info.FNG.list.Add((IntPtr)0x89f814, "Loading_Tips.fng");
            Info.FNG.list.Add((IntPtr)0x89f828, "WS_MW_LS_Splash.fng");
            Info.FNG.list.Add((IntPtr)0x89f83c, "LS_THXMovie.fng");
            Info.FNG.list.Add((IntPtr)0x89f84c, "Keyboard_GC.fng");
            Info.FNG.list.Add((IntPtr)0x89f85c, "Chyron_IG.fng");
            Info.FNG.list.Add((IntPtr)0x89f86c, "GameOver.fng");
            Info.FNG.list.Add((IntPtr)0x89f87c, "GenericDialog_ThreeButton.fng");
            Info.FNG.list.Add((IntPtr)0x89f89c, "DiscError.fng");
            Info.FNG.list.Add((IntPtr)0x89f8ac, "CustomizePerformance_BACKROOM.fng");
            Info.FNG.list.Add((IntPtr)0x89f8d0, "CustomizePerformance.fng");
            Info.FNG.list.Add((IntPtr)0x89f8ec, "Spoilers_BACKROOM.fng");
            Info.FNG.list.Add((IntPtr)0x89f904, "Spoilers.fng");
            Info.FNG.list.Add((IntPtr)0x89f914, "Rims_BACKROOM.fng");
            Info.FNG.list.Add((IntPtr)0x89f928, "Rims.fng");
            Info.FNG.list.Add((IntPtr)0x89f934, "Paint_BACKROOM.fng");
            Info.FNG.list.Add((IntPtr)0x89f948, "Paint.fng");
            Info.FNG.list.Add((IntPtr)0x89f954, "Numbers.fng");
            Info.FNG.list.Add((IntPtr)0x89f960, "Decals_BACKROOM.fng");
            Info.FNG.list.Add((IntPtr)0x89f974, "Decals.fng");
            Info.FNG.list.Add((IntPtr)0x89f980, "CustomHUDColor_BACKROOM.fng");
            Info.FNG.list.Add((IntPtr)0x89f99c, "CustomHUDColor.fng");
            Info.FNG.list.Add((IntPtr)0x89f9b0, "CustomHUD_BACKROOM.fng");
            Info.FNG.list.Add((IntPtr)0x89f9c8, "CustomHUD.fng");
            Info.FNG.list.Add((IntPtr)0x89f9d8, "CustomizeParts_BACKROOM.fng");
            Info.FNG.list.Add((IntPtr)0x89f9f4, "CustomizeParts.fng");
            Info.FNG.list.Add((IntPtr)0x89fa08, "ShoppingCart_BACKROOM.fng");
            Info.FNG.list.Add((IntPtr)0x89fa24, "ShoppingCart_QR.fng");
            Info.FNG.list.Add((IntPtr)0x89fa38, "ShoppingCart.fng");
            Info.FNG.list.Add((IntPtr)0x89fa4c, "CustomizeGenericTop_BACKROOM.fng");
            Info.FNG.list.Add((IntPtr)0x89fa70, "CustomizeGenericTop.fng");
            Info.FNG.list.Add((IntPtr)0x89fa88, "CustomizeCategory_BACKROOM.fng");
            Info.FNG.list.Add((IntPtr)0x89faa8, "CustomizeCategory.fng");
            Info.FNG.list.Add((IntPtr)0x89fac0, "CustomizeMain.fng");
            Info.FNG.list.Add((IntPtr)0x89fad4, "UI_DebugCarCustomize.fng");
            Info.FNG.list.Add((IntPtr)0x89faf0, "GameWon.fng");
            Info.FNG.list.Add((IntPtr)0x89fafc, "SafehouseMarkers.fng");
            Info.FNG.list.Add((IntPtr)0x89fb14, "SafehouseBounty.fng");
            Info.FNG.list.Add((IntPtr)0x89fb28, "SafehouseRegionUnlock.fng");
            Info.FNG.list.Add((IntPtr)0x89fb44, "SafehouseMilestones.fng");
            Info.FNG.list.Add((IntPtr)0x89fb5c, "SafehouseRivalBio.fng");
            Info.FNG.list.Add((IntPtr)0x89fb74, "SafehouseRivalChallenge.fng");
            Info.FNG.list.Add((IntPtr)0x89fb90, "OPM_SafehouseRaceSheet.fng");
            Info.FNG.list.Add((IntPtr)0x89fbac, "SafehouseRaceSheet.fng");
            Info.FNG.list.Add((IntPtr)0x89fbc4, "EngageEventDialog.fng");
            Info.FNG.list.Add((IntPtr)0x89fbdc, "HUD_Drag.fng");
            Info.FNG.list.Add((IntPtr)0x89fbec, "HUD_SingleRace.fng");
            Info.FNG.list.Add((IntPtr)0x89fc00, "WorldMapMain.fng");
            Info.FNG.list.Add((IntPtr)0x89fc14, "PressStart.fng");
            Info.FNG.list.Add((IntPtr)0x89fc24, "Track_Options.fng");
            Info.FNG.list.Add((IntPtr)0x89fc38, "Track_Select.fng");
            Info.FNG.list.Add((IntPtr)0x89fc4c, "Quick_Race_Brief.fng");
            Info.FNG.list.Add((IntPtr)0x8a0104, "DiscErrorPC.fng");
            Info.FNG.list.Add((IntPtr)0x8a0114, "MW_LS_Splash.fng");
            Info.FNG.list.Add((IntPtr)0x8a0128, "LS_EAlogo.fng");
            Info.FNG.list.Add((IntPtr)0x8a0138, "MC_Bootup.fng");
            Info.FNG.list.Add((IntPtr)0x8a0148, "Keyboard.fng");
            Info.FNG.list.Add((IntPtr)0x8a0568, "HUD_Drag_Player2.fng");
            Info.FNG.list.Add((IntPtr)0x8a0580, "HUD_Drag_Player1.fng");
            Info.FNG.list.Add((IntPtr)0x8a0598, "HUD_Player2.fng");
            Info.FNG.list.Add((IntPtr)0x8a05a8, "HUD_Player1.fng");
            Info.FNG.list.Add((IntPtr)0x8a0678, "PC_menubar.fng");
            Info.FNG.list.Add((IntPtr)0x8a12b0, "LOADING.FNG");
            Info.FNG.list.Add((IntPtr)0x8a12bc, "MW_LS_SPLASH.FNG");
            Info.FNG.list.Add((IntPtr)0x8a12d0, "MW_LS_ATTRACTFMV.FNG");
            Info.FNG.list.Add((IntPtr)0x8a12e8, "LS_EALOGO.FNG");
            Info.FNG.list.Add((IntPtr)0x8a12f8, "LS_PSA.FNG");
            Info.FNG.list.Add((IntPtr)0x8a1304, "FEANYMOVIE.FNG");
            Info.FNG.list.Add((IntPtr)0x8a1314, "MC_MAIN.FNG");
            Info.FNG.list.Add((IntPtr)0x8a1320, "EA_TRAX_JUKEBOX.FNG");
            Info.FNG.list.Add((IntPtr)0x8a1d40, "LS_Demo_Marketing.fng");
            Info.FNG.list.Add((IntPtr)0x8a749c, "Marketing1.fng");
            Info.FNG.list.Add((IntPtr)0x8a7acc, "Loading_Controller.fng");
            Info.FNG.list.Add((IntPtr)0x8a7ae4, "WS_Loading_Controller.fng");
            Info.FNG.list.Add((IntPtr)0x8a85f8, "PC_Loading.fng");
            Info.FNG.list.Add((IntPtr)0x8b7658, "UI_QRModeOptions.fng");
            WriteLine("FNG list: " + Info.FNG.list.Count, true);
        }



    }
}