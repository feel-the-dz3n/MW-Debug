using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MW_Debug_2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string InfoText = @"Hotkeys:
Insert: Enable/disable task
Delete: Toggle expanded cordlog
Home, End: Switch between tasks
PgUp, PgDown: Select object
(-2 - nearest objs, -1 - all objs, 0 - player, >0 - others)
V: Hide MW Debug overlay or HUD
RCtrl+P: Set normal game speed
RCtrl+]: Slow motion
RCtrl+[: Set game speed to 0
RCtrl+ +/-: Increase/decrease game speed
Alt+V: Toggle showcase camera
Ctrl+Q: Unpause/reset camera
RAlt+N: Force stop car
Thanks to osdever and nlgzrgn for help!";
        public static bool ListFNGList = false;
        public static bool ListFNGListCall = false;
        public static string ListResultRef;
        public static string ListResult = "";
        public List<Grid> ErrorChangable = new List<Grid>();
        public Brush transparentColor = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
        public Brush selectedColor = new SolidColorBrush(Color.FromArgb(40, 255, 255, 255));
        public Brush selectedColorBlack = new SolidColorBrush(Color.FromArgb(40, 0, 0, 0));
        public bool ErrorCantChangeGrid = false;
        public Grid prevGrid = null;
        public Grid prevGridInfo = null;
        public Brush GoodTopBorder;
        public static Version v = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;

        public void InfoWnd(string title, string text, Grid pGrid = null)
        {
            SetActiveGrid(GridInfo);
            prevGridInfo = pGrid;
            InfoBox.Text = text;
            InfoTitle.Text = title;
        }
        public void ErrorWnd(string title, string error, bool isCritical, Grid pGrid = null)
        {
            SetActiveGrid(GridError);
            prevGrid = pGrid;
            ErrorCantChangeGrid = isCritical;
            if (isCritical)
            {
                ButtonsWhenError.Visibility = System.Windows.Visibility.Visible;
                OpacityAnim(ButtonsWhenError);
                ErrorExitButton.Content = "Exit";
                SetStatus("Critical error");
                // ⛔
                ShowLogButton_Copy1.Content = "⛔";
            }
            else
            {
                ErrorExitButton.Content = "OK";
            }
            ErrorTitle.Text = title;
            ErrorBox.Text = error;
        }
        public void ErrorT(string title, string error, bool isCritical, Grid pGrid = null)
        {
            this.Dispatcher.Invoke(DispatcherPriority.Background, new Action(() => ErrorWnd(title, error, isCritical, pGrid)));
        }
        public void AppendLog(string text)
        {
            DC.WriteLine("[MWDBG LOG] " + text);
            LogBox.AppendText(String.Format("{0}[{1:H:mm:ss}] {2}", "\r\n", DateTime.Now, text));
            LogBox.ScrollToEnd();
        }
        public void AppendLogT(string text)
        {
            this.Dispatcher.Invoke(DispatcherPriority.Background, new Action(() => AppendLog(text)));
        }
        public void SetStatus(string status)
        {
            DC.WriteLine("[MWDBG STATUS] " + status);
            WindowTitle.Text = String.Format("MW DEBUG {0}.{1}.{2}.{3} by Dz3n - {4}", v.Major, v.Minor, v.Build, v.Revision, status);
            StatusWelcomeText.Text = status;
        }
        public void SetStatusD(string status)
        {
            this.Dispatcher.Invoke(DispatcherPriority.Background, new Action(() => SetStatus(status)));
        }
        System.Timers.Timer t = new System.Timers.Timer(1000);
        public MainWindow()
        {
            InitializeComponent();
            PlakImage.Visibility = Visibility.Visible; // make plak permanent - osdever

            DC.Init();
            Funcs.ProgCfg.Handle();
            DC.InitFng();
            Funcs.FuncList.Init();
            DC.HookKbd();
            pVehicleInfo.Init();

            foreach (var func in Funcs.FuncList.funcs)
            {
                AvaFuncs.Items.Add(func.Key + ":" + func.Value);
            }


            foreach (var i in Info.pVehicle)
            {
                SettingsSkipFECarBox.Items.Add(i.name);
                SkipFeCarBox.Items.Add(i.name);
            }

            ErrorChangable.Add(GridWelcome);
            ErrorChangable.Add(GridError);
            ErrorChangable.Add(GridSettings);
            ErrorChangable.Add(GridInfo);
            ErrorChangable.Add(GridLog);
            ErrorChangable.Add(GridList);

            SetActiveGrid(null);
            ToggleButtons(false);
            SetStatus("Starting up...");
            // TopBorder.Background = new LinearGradientBrush(Color.FromArgb(255, 48, 48, 48), Color.FromArgb(0, 0, 0, 0), 90.0);
            OpacityAnim(MainGrid);
            LogBox.Text = "- - - - - - - -";
            DragBorder.Visibility = System.Windows.Visibility.Visible;



            Process[] bdcam = Process.GetProcessesByName("bdcam");
            if (bdcam.Length >= 1)
            {
                MessageBox.Show("MW Debug is not compatible with bandicam\n\nBut you can use Bandicam, just start him after MW Debug & game", "MW Debug Compatibility Error");
            }
            if (Funcs.ProgCfg.Settings["firstRun"] == "1")
            {
                Funcs.ProgCfg.Settings["firstRun"] = "0";
                Funcs.ProgCfg.Save();
                Process.Start("https://www.youtube.com/playlist?list=PLldQ4sw27DSkxgmoznGHu0kbp0QHh6FAX");
                Process.Start("https://www.instagram.com/_dz3n_kurwa_");
            }

            MWDBG.StartTrainer(this);

            t.Elapsed += t_Elapsed;
            t.Start();
        }

        void t_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            t.Dispose();
            SetActiveGridT(GridWelcome);
            ToggleButtonsT(true);

        }

        public void UIStartTrainer(bool gotoLog = true)
        {
            GridButtons.Visibility = System.Windows.Visibility.Visible;
            OpacityAnim(GridButtons);
            if (gotoLog) SetActiveGrid(GridLog);
            AppendLog("Start trainer");
        }

        public void ToggleButtons(bool t)
        {
            if (t)
            {
                GridTopColor.Visibility = System.Windows.Visibility.Visible;
                OpacityAnim(GridTopColor);
                GridButtons.Visibility = System.Windows.Visibility.Visible;
                OpacityAnim(GridButtons);
            }
            else
            {
                GridTopColor.Visibility = System.Windows.Visibility.Hidden;
                GridButtons.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        public void SetActiveGridT(Grid grid)
        {
            this.Dispatcher.Invoke(DispatcherPriority.Background, new Action(() => SetActiveGrid(grid)));
        }
        public void ToggleButtonsT(bool t)
        {
            this.Dispatcher.Invoke(DispatcherPriority.Background, new Action(() => ToggleButtons(t)));
        }

        public void ListUpdateGrid(Grid grid)
        {
            if(grid == GridSettings)
            {
                if(ListFNGList == true)
                {
                    SplashFNGBox.Text = ListResult;
                    ListFNGList = false;
                }
            }
            if (grid == GridCallFE)
            {
                if (ListFNGListCall)
                {
                    FngNameBox.Text = ListResult;
                    ListFNGListCall = false;
                }
            }
        }

        public void SetActiveGrid(Grid grid, bool fromList = false)
        {
            if ((ErrorCantChangeGrid && ErrorChangable.Contains(grid)) || !ErrorCantChangeGrid)
            {
                GridInfo.Visibility = System.Windows.Visibility.Hidden;
                GridSettings.Visibility = System.Windows.Visibility.Hidden;
                GridLog.Visibility = System.Windows.Visibility.Hidden;
                GridError.Visibility = System.Windows.Visibility.Hidden;
                GridWelcome.Visibility = System.Windows.Visibility.Hidden;
                GridList.Visibility = Visibility.Hidden;
                GridCallFE.Visibility = Visibility.Hidden;
                GridMoreFunctions.Visibility = Visibility.Hidden;
                GridSetPlayersCar.Visibility = Visibility.Hidden;
                GridSkipFe.Visibility = Visibility.Hidden;
                GridCarSpawn.Visibility = Visibility.Hidden;
                GridMemory.Visibility = Visibility.Hidden;

                if (grid != null)
                {
                    grid.Visibility = System.Windows.Visibility.Visible;
                    if (fromList) ListUpdateGrid(grid);
                    OpacityAnim(grid);
                }
            }
        }

        private void DragBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {
        }

        private void OpacityAnim(Grid grid)
        {
            if (Funcs.ProgCfg.Settings["animations"].StartsWith("1"))
            {
                DoubleAnimation animation = new DoubleAnimation();
                animation.From = 0;
                animation.To = 100;
                animation.SpeedRatio = 0.03;
                animation.FillBehavior = FillBehavior.HoldEnd;
                animation.Completed += (s, a) => { DC.WriteLine("Animation end"); };
                grid.BeginAnimation(UIElement.OpacityProperty, animation);
                animation.Freeze();
            }
            else
            {
                grid.Opacity = 100;
            }
        }


        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Environment.Exit(0);
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }

        private void DragBorder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void CopyLogButton_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(LogBox.Text);
            AppendLog("Copied");
        }

        private void ShowLogButton_Click(object sender, RoutedEventArgs e)
        {
            SetActiveGrid(GridLog);
        }

        private void ShowSettingsGrid_Click(object sender, RoutedEventArgs e)
        {
            UpdateSettingsGrid();
            SetActiveGrid(GridSettings);
        }

        public void UpdateSettingsGrid()
        {
            StartGameWithMWD.IsChecked = false;
            AfterStartingSkipFE.IsChecked = false;
            AnimationsCheckbox.IsChecked = false;

            ReplaySpeedBox.Text = Replays.Double.ToString();
            
            GameFolderBox.Text = Funcs.ProgCfg.Settings["gamePath"];
            SettingsSkipFECarBox.SelectedItem = Funcs.ProgCfg.Settings["skipfePlayerCar"];

            if (Funcs.ProgCfg.Settings["startGame"].StartsWith("1")) StartGameWithMWD.IsChecked = true;
            if (Funcs.ProgCfg.Settings["skipfe"].StartsWith("1")) AfterStartingSkipFE.IsChecked = true;
            if (Funcs.ProgCfg.Settings["animations"].StartsWith("1")) AnimationsCheckbox.IsChecked = true;
            SplashFNGBox.Text = Funcs.ProgCfg.Settings["splashFNG"];

        }

        private void error1_Click(object sender, RoutedEventArgs e)
        {
            ErrorWnd("CRITICAL ERROR", "Generated error", true);
        }

        private void ErrorExitButton_Click(object sender, RoutedEventArgs e)
        {
            if (ErrorCantChangeGrid)
            {
                this.Hide();
                Environment.Exit(0);
            }
            else
            {
                SetActiveGrid(prevGrid);
            }
        }

        private void error1_Copy_Click(object sender, RoutedEventArgs e)
        {
            ErrorWnd("Error #2", "Generated error", false);
        }

        private void GenErrSet_Click(object sender, RoutedEventArgs e)
        {
            ErrorWnd("Error #3", "Lalalalalala error", false, GridSettings);
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            UIStartTrainer();
        }

        private void HelpBbuton_Click(object sender, RoutedEventArgs e)
        {
            SetActiveGrid(GridWelcome);
        }

        private void ClickYouTube_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start("https://www.youtube.com/channel/UCDv-eoYT6vZ9lNQ0L8ZLPEA");
        }

        private void ClickDiscord_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start("https://discord.gg/U7KhDF3");
        }

        private void HelpButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show(InfoText, "MW Debug", MessageBoxButton.OK);
            //InfoWnd("About MW Debug", InfoText, GridWelcome);
        }

        private void ClickYouTube_MouseEnter(object sender, MouseEventArgs e)
        {
            ClickYouTube.Background = selectedColor;
        }

        private void ClickYouTube_MouseLeave(object sender, MouseEventArgs e)
        {

            ClickYouTube.Background = transparentColor;
        }

        private void HelpButton_MouseEnter(object sender, MouseEventArgs e)
        {
            HelpButton.Background = selectedColor;

        }

        private void HelpButton_MouseLeave(object sender, MouseEventArgs e)
        {
            HelpButton.Background = transparentColor;
        }

        private void ClickDiscord_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void ClickDiscord_MouseEnter(object sender, MouseEventArgs e)
        {
            ClickDiscord.Background = selectedColor;
        }

        private void ClickDiscord_MouseLeave(object sender, MouseEventArgs e)
        {
            ClickDiscord.Background = transparentColor;
        }

        private void ShowErrorScreen_Click(object sender, RoutedEventArgs e)
        {
            SetActiveGrid(GridError);
        }

        private void Window_Closing_1(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ExitButton_Click(null, null);
        }

        private void InfoOk_Click(object sender, RoutedEventArgs e)
        {
            SetActiveGrid(prevGridInfo);
        }

        private void DetectHelp_Click(object sender, RoutedEventArgs e)
        {
            InfoWnd("DETECT? WTF?", "If the game is running the program itself will find all that is needed", GridSettings);
        }

        private void AnimationHelp_Click(object sender, RoutedEventArgs e)
        {
            InfoWnd("SMTH IMPORTANT ABOUT ANIMATIONS!", "You can disable animations forever! This may increase the performance!", GridSettings);
        }

        private void SaveSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            Funcs.ProgCfg.Settings["gamePath"] = GameFolderBox.Text;
            try
            {
                Funcs.ProgCfg.Settings["gameDir"] = MWDBG.GetWD(GameFolderBox.Text);
            }
            catch
            {
                ErrorWnd("UNABLE TO SAVE", "Something wrong with game path!", false, GridSettings);
                return;
            }
            Funcs.ProgCfg.Settings["skipfePlayerCar"] = SettingsSkipFECarBox.SelectedItem.ToString();

            if ((bool)StartGameWithMWD.IsChecked) Funcs.ProgCfg.Settings["startGame"] = "1";
            else Funcs.ProgCfg.Settings["startGame"] = "0";
            if ((bool)AfterStartingSkipFE.IsChecked) Funcs.ProgCfg.Settings["skipfe"] = "1";
            else Funcs.ProgCfg.Settings["skipfe"] = "0";
            if ((bool)AnimationsCheckbox.IsChecked) Funcs.ProgCfg.Settings["animations"] = "1";
            else Funcs.ProgCfg.Settings["animations"] = "0";

            Funcs.ProgCfg.Settings["splashFNG"] = SplashFNGBox.Text;
            //if (Funcs.ProgCfg.Settings["loading"] != loading.SelectedIndex.ToString()) MessageBox.Show("To change loading screens restart game", "MW Debug");
            //Funcs.ProgCfg.Settings["loading"] = loading.SelectedIndex.ToString();

            MWDBG.workMin = (bool)WorkMinimizedCheckbox.IsChecked; try
            {
                Replays.Double = int.Parse(ReplaySpeedBox.Text);
            }
            catch
            {
                ErrorWnd("UNABLE TO SAVE", "Something wrong with replay speed!", false, GridSettings);
                return;
            }
            Funcs.ProgCfg.Save();
            InfoWnd("SETTINGS SAVED", "Thanks for cooperation xd", GridSettings);
            UpdateSettingsGrid();
        }

        private void PlusRS_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int speed = int.Parse(ReplaySpeedBox.Text);
                speed++;
                ReplaySpeedBox.Text = speed.ToString();
            }
            catch { }
        }

        private void MinusRS_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int speed = int.Parse(ReplaySpeedBox.Text);
                if (speed > 0)
                {
                    speed--;
                    ReplaySpeedBox.Text = speed.ToString();
                }
            }
            catch { }
        }

        private void ButtonOpenFolderSettings_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog of = new System.Windows.Forms.OpenFileDialog();
            of.Filter = "speed.exe - NFS MW|*.exe";
            if (of.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                GameFolderBox.Text = of.FileName;
            }
        }

        private void DetectSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process p = Process.GetProcessesByName("speed").FirstOrDefault();
                GameFolderBox.Text = p.MainModule.FileName;
            }
            catch { ErrorWnd("AUTODETECT ERROR!", "Cant detect speed.exe", false, GridSettings); }
        }

        private void ShowLogButton_Copy_Click(object sender, RoutedEventArgs e)
        {
            ErrorWnd("ERROR", "UI is not finished yet blyad", false);
        }

        private void ShowLogButton_Copy2_Click(object sender, RoutedEventArgs e)
        {
            SetActiveGrid(GridSkipFe);
        }

        private void ShowLogButton_Copy3_Click(object sender, RoutedEventArgs e)
        {
            SetActiveGrid(GridMemory);
        }

        private void ShowLogButton_Copy1_Click(object sender, RoutedEventArgs e)
        {
            // ⛔
            if (!ErrorCantChangeGrid)
            {
                CallFngResult.Text = "";
                SetActiveGrid(GridMoreFunctions);
            }
            else
            {
                SetActiveGrid(GridError);
            }
        }

        private void CordLogEnabled_Checked(object sender, RoutedEventArgs e)
        {
            MWDBG.CordLogEnabled = (bool)CordLogEnabled.IsChecked;
        }

        private void HelpCordLog_Click(object sender, RoutedEventArgs e)
        {
            InfoWnd("CORDLOG IS DANGEROUS", "CordLog may (should) decrease (totally) performance", GridSettings);
        }

        private void DetectSettingsButton_Copy_Click(object sender, RoutedEventArgs e)
        {
            SplashFNGBox.Text = "MW_LS_Splash.fng";
        }

        private void DetectSettingsButton_Copy1_Click(object sender, RoutedEventArgs e)
        {
            List<string> fngs = new List<string>();
            foreach (var i in Info.FNG.list)
            {
                fngs.Add(i.Value);
            }
            ListFNGList = true;
            ShowListWnd("FNG LIST", fngs, GridSettings);
        }

        public void ShowListWnd(string title, List<string> items, Grid pGrid)
        {
            prevGrid = pGrid;
            ListTitle.Text = title;
            ListBoxList.Items.Clear();
            ListResult = "";
            foreach (var item in items)
            {
                ListBoxList.Items.Add(item);
            }
            SetActiveGrid(GridList);
        }

        private void ListOKBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ListResult = ListBoxList.SelectedItem.ToString();
                SetActiveGrid(prevGrid, true);
            }
            catch { }
        }

        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (var p in Process.GetProcessesByName("speed"))
            {
                p.Kill();
            }
            Process.Start("MW Debug 2.exe");
            Environment.Exit(0);
        }

        private void FngListCallBtn_Click(object sender, RoutedEventArgs e)
        {
            List<string> fngs = new List<string>();
            foreach (var i in Info.FNG.list)
            {
                fngs.Add(i.Value);
            }
            ListFNGListCall = true;
            ShowListWnd("FNG LIST", fngs, GridCallFE);
        }

        private void CallFEBtn_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)fngCallActivate.IsChecked) MWDBG.m.Windows.MainWindow.Activate();
            MWDBG.m.Assembly.Inject("nop", (IntPtr)0x005C50B1);
            MWDBG.m.Assembly.Inject("nop", (IntPtr)0x005C50B2);
            MWDBG.m.Assembly.Inject("nop", (IntPtr)0x005C50B3);

            //MWDBG.m.Assembly.Inject("xor al, al", (IntPtr)0x0057DBB0);
            //MWDBG.m.Assembly.Inject("ret", (IntPtr)0x0057DBB1);

            CallFngResult.Text = "";
            Int32 cfeng = MWDBG.readMem<Int32>((IntPtr)0x91CADC);
            if ((bool)asMessage.IsChecked)
            {
                Int32 callResult = MWDBG.m[(IntPtr)0x516BE0, false].Execute<Int32>(Binarysharp.MemoryManagement.Assembly.CallingConvention.CallingConventions.Thiscall, cfeng, 0, FngNameBox.Text, -4, 255);
                CallFngResult.Text += "FNG Msg: " + callResult;
            }
            else
            {
                if ((bool)switchPackage.IsChecked)
                {
                    Int32 callResult = MWDBG.m[(IntPtr)0x525940, false].Execute<Int32>(Binarysharp.MemoryManagement.Assembly.CallingConvention.CallingConventions.Thiscall, cfeng, FngNameBox.Text, (Int32)0, (uint)0, 0);
                    CallFngResult.Text += "FNG Call 1: " + callResult;
                }
                else if((bool)noControlP.IsChecked)
                {
                    Int32 callResult = MWDBG.m[(IntPtr)0x516990, false].Execute<Int32>(Binarysharp.MemoryManagement.Assembly.CallingConvention.CallingConventions.Thiscall, cfeng, FngNameBox.Text, 0x64);
                    CallFngResult.Text += "FNG Call 2: " + callResult;
                }
                else if ((bool)unkFnk.IsChecked)
                {
                    Int32 callResult = MWDBG.m[(IntPtr)0x5257F0, false].Execute<Int32>(Binarysharp.MemoryManagement.Assembly.CallingConvention.CallingConventions.Thiscall, cfeng, FngNameBox.Text, 0, 0, 0);
                    CallFngResult.Text += "FNG Call 3: " + callResult;
                }
            }
        }

        private void disScript_Click(object sender, RoutedEventArgs e)
        {
            MWDBG.m.Assembly.Inject("xor al, al\nret", (IntPtr)0x514D10);
            CallFngResult.Text += "Scripts disabled";
        }

        private void enScript_Click(object sender, RoutedEventArgs e)
        {
            MWDBG.m.Assembly.Inject("push edi", (IntPtr)0x514D10);
            CallFngResult.Text += "Scripts enabled";
        }

        private void MoreFuncOK_Click(object sender, RoutedEventArgs e)
        {
            if(MoreFuncList.SelectedIndex == 0)
            {
                SetActiveGrid(GridCallFE);
            }
            if(MoreFuncList.SelectedIndex == 1)
            {
                ShowSetPCar();
            }
            if (MoreFuncList.SelectedIndex == 2)
            {
                SetActiveGrid(GridCarSpawn);
            }
        }

        public void ShowSetPCar()
        {
            SetPlayersCarList.Items.Clear();
            foreach(var i in Info.pVehicle)
            {
                SetPlayersCarList.Items.Add(i.name);
            }
            SetActiveGrid(GridSetPlayersCar);
        }

        private void SetPlayersCarOk_Click(object sender, RoutedEventArgs e)
        {
            MWDBG.m.Windows.MainWindow.Activate();
            MWDBG.SetLog("Result: " + MWD.SetPlayerCar(NewCar.Text));
        }

        private void SetPlayersCarList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            NewCar.Text = SetPlayersCarList.SelectedItem.ToString();
        }

        private void FNGBtn_Click(object sender, RoutedEventArgs e)
        {
            SetActiveGrid(GridCallFE);
        }

        private void CarBtn_Click(object sender, RoutedEventArgs e)
        {
            ShowSetPCar();
        }

        private void SkipFeOk_Click(object sender, RoutedEventArgs e)
        {
            MWDBG.m.Windows.MainWindow.Activate();

            string name = (string)SkipFeCarBox.SelectedItem;
            var MemModel = MWDBG.m.Memory.Allocate(name.Length);
            MemModel.WriteString(name);
            MemModel.MustBeDisposed = false;

            MWDBG.writeMem<int>(Info.SkipFE.PlayerCar, MemModel.BaseAddress.ToInt32());

            MWD.SkipFE_Load();
        }

        private void CarSpawnOk_Click(object sender, RoutedEventArgs e)
        {
            CarSpawnerClass.SpawnCar(0x06465EB2, Vector3c.GetVector3(0, 0, 0), Vector3c.GetVector3(0, -1, 0));
            SetActiveGrid(GridLog);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                new Process { StartInfo = { FileName = "Memcall.exe", Arguments = "-adr:" + adrBox.Text + " -proc:speed" } }.Start();
            }
            catch
            {
                InfoWnd("MEMCALL?!", "Can't start Memcall.exe", GridMemory);
            }
        }

        private void AvaFuncs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try {
                adrBox.Text = AvaFuncs.SelectedItem.ToString().Split(':')[1];
            } catch { }
        }

        public void CallMemBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                long num = long.Parse(this.adrBox.Text.Substring(2), System.Globalization.NumberStyles.HexNumber);
                IntPtr ptr = new IntPtr(num);
                MWDBG.SetLog("Calling function: " + this.adrBox.Text);
                int num2 = MWDBG.m[ptr - 0x400000, true].Execute<int>();
                this.retLbl.Text = "> 0x" + num2.ToString("X4");
                MWDBG.SetLog("{FFFF00}" + this.adrBox.Text + " > 0x" + num2.ToString("X4"));
            }
            catch (Exception exception)
            {
                this.retLbl.Text = exception.ToString();
            }
        }
    }
}
