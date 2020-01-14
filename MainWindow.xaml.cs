using IconPacks_Vs_mahapps;
using MahApps.Metro.Controls;
using Shared_Lib.Extention;
using Shared_Lib.LogSystem;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Win32Interop.WinHandles;
using static Shared_Lib.Extention.Windows_Ex;

namespace Test_Repo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow: MetroWindow
    {
        public MainWindow()
        {
            Logger.Initialize(System.IO.Path.GetTempPath());
            InitializeComponent();
        }

     async   private void Test_Click(object sender, RoutedEventArgs e)
        {
            CancellationTokenSource cancelsema = new CancellationTokenSource();
            ThreadWindow win = null;
            var thread = new Thread(new ThreadStart(() =>
            {
                win = new ThreadWindow();
                win.Closed += delegate { cancelsema.Cancel(); };
                win.ShowDialog();
            }));
            thread.SetApartmentState(ApartmentState.STA);
            thread.IsBackground = true;
            thread.Start();

            await Task.Delay(1000); //let the above window show up

            Task t = Task.Factory.StartNew(() =>
            {
                string filename = "";
                mimicsme(mimicsenum.BullZip, cancelsema.Token, ref filename);
                 
            });
        }

        public enum mimicsenum
        {
            dissmiss_AdobPDF_Save,
            Create_Work_Sharing,
            BullZip,
            PDFComplete,
            RasterView,
            CircularRef,
            UnResolvedRef,
            ManagLinks,
            UpgradLoc,
            CopiedCentral,
            AlignTopo
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr FindWindow(string strClassName, string strWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr GetWindow(IntPtr hWnd, GetWindow_Cmd uCmd);

        [DllImport("user32.dll")]
        private static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter,
          string lpszClass, string lpszWindow);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint msg, int wParam, string lParam);

        [DllImport("User32.Dll")]
        private static extern IntPtr PostMessage(IntPtr hWnd, uint msg, int wParam, int lParam);

        public static void mimicsme(mimicsenum mimic, CancellationToken token, ref string text1)
        {
            Logger.Log($"Starting Mimics {mimic} {text1} ", Logger.ErrorType.Info);
            switch (mimic)
            {
                case mimicsenum.dissmiss_AdobPDF_Save:

                    #region Adobe PDF dismisser Save Dialog

                    {
                        again:

                        var dialog = FindWindow(null, "Save PDF File As"); //Get the handle of the window
                                                                           // 1
                        var w = GetWindow(dialog, GetWindow_Cmd.GW_CHILD);
                        // 2
                        w = GetWindow(w, GetWindow_Cmd.GW_CHILD);
                        // 3
                        w = GetWindow(w, GetWindow_Cmd.GW_CHILD);
                        // 4
                        w = GetWindow(w, GetWindow_Cmd.GW_CHILD);

                        var wUid = FindWindowEx(w, IntPtr.Zero, "Edit", "");

                        if (wUid == IntPtr.Zero) goto again;

                        var wSave = FindWindowEx(dialog, IntPtr.Zero, "Button", "&Save");

                        SendMessage(wUid, WM_SETTEXT, 0, text1); //Send password to password edit control
                        PostMessage(wSave, BM_CLICK, 0, 0); //Send left click(0x00f5) to OK button

                        #endregion Adobe PDF dismisser Save Dialog
                    }

                    break;

#if false
                case mimicsenum.Create_Work_Sharing:

                #region Create_Work_Sharing

                    {
                        FilteredWorksetCollector coll
   = new FilteredWorksetCollector(UT_RVTconstants.m_doc);

                        coll.OfKind(WorksetKind.OtherWorkset);

                        string worksetname = "";
                        InputDialog input = new InputDialog();
                        input.WindowTitle = "Type a WorkSet Name";
                        input.MainInstruction = "Please Type WorkSet Name ";

                    again:
                        input.Input = worksetname;
                        input.ShowDialog();
                        worksetname = input.Input;

                        foreach (Workset workset in coll)
                        {
                            if (workset.Name == worksetname)
                            {
                                UT_helper.MsgInfo("This name already Exists, Please type a different name");
                                goto again;
                            }
                        }

                    again1:

                        //Autodesk Revit Debug - [3D View: {3D} - Project1.rvt]
                        string viewtype = UT_tools.getpara("Family", UT_RVTconstants.m_doc.ActiveView).AsValueString();
                        string viewname = UT_RVTconstants.m_doc.ActiveView.Name;
                        string filename = ZlpPathHelper.GetFileNameFromFilePath(UT_RVTconstants.m_doc.PathName);
                        var dialog = FindWindow(null, "Autodesk Revit Debug - [" + viewtype + ": " + viewname + " - " + filename + "]"); //Get the handle of the window

                        IntPtr w = FindWindowEx(dialog, IntPtr.Zero, "msctls_statusbar32", "");
                        // w = GetWindow(w, GetWindow_Cmd.GW_CHILD);

                        IntPtr wUid = FindWindowEx(w, IntPtr.Zero, "Button", "Worksets");

                        PostMessage(wUid, BM_CLICK, 0, 0); //Send left click(0x00f5) to OK button

                        Thread.Sleep(200);
                        //Worksharing
                        dialog = FindWindow(null, "Worksharing"); //Get the handle of the window
                        w = FindWindowEx(dialog, IntPtr.Zero, "Edit", "");
                        w = GetWindow(w, GetWindow_Cmd.GW_HWNDNEXT);
                        w = GetWindow(w, GetWindow_Cmd.GW_HWNDNEXT);

                        SendMessage(w, WM_SETTEXT, 0, worksetname); //create the specified workshare here

                        //Button   OK

                        w = FindWindowEx(dialog, IntPtr.Zero, "Button", "OK");
                        PostMessage(w, BM_CLICK, 0, 0); //Send left click(0x00f5) to OK button

                        dialog = IntPtr.Zero;
                        while (dialog == IntPtr.Zero)
                        {
                            Thread.Sleep(200);
                            //Worksets
                            dialog = FindWindow(null, "Worksets"); //Get the handle of the window
                            goto again1;
                        }

                        var wOK = FindWindowEx(dialog, IntPtr.Zero, "Button", "OK");
                        PostMessage(wOK, BM_CLICK, 0, 0); //Send left click(0x00f5) to OK button

                        if (dialog != IntPtr.Zero && wOK != IntPtr.Zero) return true;
                    }
                    break;

                #endregion Create_Work_Sharing

#endif

                case mimicsenum.BullZip:

                    #region Bullzip

                    {
                        while (true)
                        {
                            if (token.IsCancellationRequested) break;
                            var dialog = TopLevelWindowUtils.FindWindow(wh => wh.GetWindowText().Contains("Bullzip PDF Printer") && wh.GetWindowText().Contains("Create File")).RawPtr;
                            //var dialog = windows.FindWindow("", "Bullzip PDF Printer - Create File");
                            var w = FindWindowEx(dialog, IntPtr.Zero, "TabStripU", "");
                            w = FindWindowEx(w, IntPtr.Zero, "FrameU", "Placeholder");
                            w = FindWindowEx(w, IntPtr.Zero, "FrameU", "Frame1");
                            // 1
                            w = GetWindow(w, GetWindow_Cmd.GW_HWNDNEXT);
                            // 2
                            w = GetWindow(w, GetWindow_Cmd.GW_HWNDNEXT);
                            // 3
                            w = GetWindow(w, GetWindow_Cmd.GW_HWNDNEXT);
                            // 4
                            w = GetWindow(w, GetWindow_Cmd.GW_HWNDNEXT);
                            // 5
                            w = GetWindow(w, GetWindow_Cmd.GW_HWNDNEXT);
                            // 6
                            w = GetWindow(w, GetWindow_Cmd.GW_HWNDNEXT);

                            // 7
                            w = GetWindow(w, GetWindow_Cmd.GW_HWNDNEXT);
                            // 8
                            w = GetWindow(w, GetWindow_Cmd.GW_HWNDNEXT);

                            if (w == IntPtr.Zero)
                            {
                                Thread.Sleep(300);
                                continue;
                            }

                            w = FindWindowEx(w, IntPtr.Zero, "TextBoxU", "");

                            // Allocate correct string length first
                            int length = (int)SendMessage(w, WM_GETTEXTLENGTH, 0, "");
                            StringBuilder sb = new StringBuilder(length + 1);
                            SendMessage(w, WM_GETTEXT, sb.Capacity, sb.ToString());

                            SendMessage(w, WM_SETTEXT, 0, text1); //Send the correct path + file name

                            // uncheck open PDF file after Creation

                            w = GetWindow(w, GetWindow_Cmd.GW_HWNDNEXT);
                            // ***We still need to figureout a way to get the value of Checkedbox or radiobutton...the below statment is not working
                            // if (CheckDlgButton(w, 0, CheckState.Checked) != false)

                            PostMessage(w, BM_CLICK, 0, 0); //send a Left Click on the CheckBox to uncheck it

                            var wSave = FindWindowEx(dialog, IntPtr.Zero, "CommandButtonU", "Save");

                            PostMessage(wSave, BM_CLICK, 0, 0); //Send left click(0x00f5) to OK button
                            break;
                        }

                        break;
                    }

                #endregion Bullzip

                case mimicsenum.PDFComplete:

                    #region PDF Complete

                    {
                        while (true)
                        {
                            if (token.IsCancellationRequested) break;

                            var dialog = FindWindow(null, "PDF Complete Document Creation Options"); //Get the handle of the window
                            if (dialog == IntPtr.Zero) continue;

                            Thread.Sleep(500);

                            // uncheck open PDF file after Creation

                            // ***We still need to figureout a way to get the value of Checkedbox or radiobutton...the below statment is not working
                            // if (CheckDlgButton(w, 0, CheckState.Checked) != false)

                            var w = FindWindowEx(dialog, IntPtr.Zero, "TfrmOptions2.UnicodeClass", "frmOptions2");
                            w = FindWindowEx(w, IntPtr.Zero, "TTntPanel.UnicodeClass", "");
                            w = FindWindowEx(w, IntPtr.Zero, "TTntCheckBox.UnicodeClass", "View PDF after creation");
                            PostMessage(w, BM_CLICK, 0, 0); //send a Left Click on the CheckBox to uncheck it
                            Windows_Ex.MinimizeDialog("PDF Complete Document Creation Options");

                            w = FindWindowEx(dialog, IntPtr.Zero, "ComboBoxEx32", "");
                            w = FindWindowEx(w, IntPtr.Zero, "ComboBox", "");
                            w = FindWindowEx(w, IntPtr.Zero, "Edit", "");

                            if (w == IntPtr.Zero) continue;

                            // Allocate correct string length first
                            int length = (int)SendMessage(w, WM_GETTEXTLENGTH, 0, "");
                            StringBuilder sb = new StringBuilder(length + 1);
                            SendMessage(w, WM_GETTEXT, sb.Capacity, sb.ToString());

                            SendMessage(w, WM_SETTEXT, 0, text1); //Send the correct path + file name
                            Thread.Sleep(300);

                            Thread.Sleep(300);
                            var wSave = FindWindowEx(dialog, IntPtr.Zero, "Button", "&Save");

                            PostMessage(wSave, BM_CLICK, 0, 0); //Send left click(0x00f5) to OK button

                            break;
                        }

                        break;

                        #endregion PDF Complete
                    }
                case mimicsenum.RasterView:
                    /* the below Error are illustrated on upgrading Revit Files
              * \"Printing - Setting Changed for Shaded Views\" \"Circular Link Conflict\" \"Unresolved References\" \"Manage Links\" \"Upgrading Local File\" \"Copied Central Model\"
              */

                    #region RasterView

                    {
                        while (true)
                        {
                            if (token.IsCancellationRequested) break;
                            Task.Delay(200).Wait();

                            var dialog = FindWindow(null, "Printing - Setting Changed for Shaded Views"); //Get the handle of the window

                            //       if(dialog==null)
                            //        dialog = FindWindow(null, "Impression - Paramètre modifié pour les vues ombrées"); //Get the handle of the window

                            var w = FindWindowEx(dialog, IntPtr.Zero, "DirectUIHWND", "");
                            w = FindWindowEx(w, IntPtr.Zero, "CtrlNotifySink", "");

                            // 1
                            w = GetWindow(w, GetWindow_Cmd.GW_HWNDNEXT);
                            // 2
                            w = GetWindow(w, GetWindow_Cmd.GW_HWNDNEXT);
                            // 3
                            w = GetWindow(w, GetWindow_Cmd.GW_HWNDNEXT);
                            // 4
                            w = GetWindow(w, GetWindow_Cmd.GW_HWNDNEXT);
                            // 5
                            w = GetWindow(w, GetWindow_Cmd.GW_HWNDNEXT);
                            // 6
                            w = GetWindow(w, GetWindow_Cmd.GW_HWNDNEXT);

                            w = FindWindowEx(w, IntPtr.Zero, "Button", "&Close");

                            if (w != IntPtr.Zero)
                            {
                                PostMessage(w, BM_CLICK, 0, 0); //send a Left Click on the CheckBox to unchecked it
                            }
                        }

                        break;

                        #endregion RasterView
                    }

                case mimicsenum.UpgradLoc:

                    // start background closing thread
                    while (true)
                    {
                        if (token.IsCancellationRequested) break;
                        //    if (Constants.CloseThread) return;

                        Task.Delay(500).Wait();

                        var dialog = FindWindow(null, "Upgrade Project"); //Get the handle of the window
                                                                          // todo: think baout windowscrapper
                        var w = FindWindowEx(dialog, IntPtr.Zero, "DirectUIHWND", "");

#if true
                        w = FindWindowEx(w, IntPtr.Zero, "CtrlNotifySink", "");

                        // 1
                        w = GetWindow(w, GetWindow_Cmd.GW_HWNDNEXT);
                        // 2
                        w = GetWindow(w, GetWindow_Cmd.GW_HWNDNEXT);
                        // 3
                        w = GetWindow(w, GetWindow_Cmd.GW_HWNDNEXT);
                        // 4
                        w = GetWindow(w, GetWindow_Cmd.GW_HWNDNEXT);
                        // 5
                        w = GetWindow(w, GetWindow_Cmd.GW_HWNDNEXT);
                        // 6
                        w = GetWindow(w, GetWindow_Cmd.GW_HWNDNEXT);
                        // 7
                        w = GetWindow(w, GetWindow_Cmd.GW_HWNDNEXT);
#endif

                        w = FindWindowEx(w, IntPtr.Zero, "Button", "Close");

                        if (w != IntPtr.Zero)
                        {
                            PostMessage(w, BM_CLICK, 0, 0); //send a Left Click on the CheckBox to unchecked it
                        }

                        break;
                    }

                    break;

                case mimicsenum.AlignTopo:
                    {
                        while (true)
                        {
                            Thread.Sleep(200);
                            IntPtr dialog = FindWindowByCaption(IntPtr.Zero, text1);

                            if (dialog != IntPtr.Zero)
                            {
                                Thread.Sleep(200);

                                SetForegroundWindow(dialog);

                                System.Windows.Forms.SendKeys.SendWait("{ESC}");

                                Thread.Sleep(300);
                                break;
                            }
                        }
                        break;
                    }
            }

            Logger.Log($"{mimic} mimic is done", Logger.ErrorType.Info);
        }
    }

    public class mc_test: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public mc_test()
        {
            Items = new ObservableCollection<string>();
            Items.Add("123");
            Items.Add("ddas");
            Items.Add("asda");
            Items.Add("12adas3");
        }

        #region Items

        private ObservableCollection<string> _Items;

        public ObservableCollection<string> Items
        {
            get
            {
                return _Items;
            }
            set { _Items = value; Notify(nameof(Items)); }
        }

        #endregion Items

        public void Notify(string prop)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
    }
}