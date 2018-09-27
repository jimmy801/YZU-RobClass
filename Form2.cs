using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Web;
using System.IO;
using System.Collections;
using System.Threading;
using mshtml;

namespace RobClass
{
    public partial class Form2 : Form
    {
        public Form1 f1 = null;
        public WebBrowser Web;
        public bool makeSure = false;
        public NotifyIcon notifyIcon;//
        CookieContainer cookieContainer = new CookieContainer();
        HtmlDocument left, right;
        ArrayList choseData = new ArrayList();
        ArrayList selectedClass = new ArrayList();
        Dictionary<String, String> selectedDept = new Dictionary<String, String>();//
        bool PostBack = false;
        bool first = true;
        bool isSmall = false;
        bool stop = false;
        int sleepTime = 4500;
        int defaultSleep = 4500;
        int bottom = 3000;
        private Thread th;
        string currentRobClassKey;
        string currentRobClassValue;

        #region Mute WebBrowser Import
        [DllImport("winmm.dll")]
        public static extern int waveOutGetVolume(IntPtr h, out uint dwVolume);

        [DllImport("winmm.dll")]
        public static extern int waveOutSetVolume(IntPtr h, uint dwVolume);

        // Constants
        private const int FEATURE_DISABLE_NAVIGATION_SOUNDS = 21;
        private const int SET_FEATURE_ON_THREAD = 0x00000001;
        private const int SET_FEATURE_ON_PROCESS = 0x00000002;
        private const int SET_FEATURE_IN_REGISTRY = 0x00000004;
        private const int SET_FEATURE_ON_THREAD_LOCALMACHINE = 0x00000008;
        private const int SET_FEATURE_ON_THREAD_INTRANET = 0x00000010;
        private const int SET_FEATURE_ON_THREAD_TRUSTED = 0x00000020;
        private const int SET_FEATURE_ON_THREAD_INTERNET = 0x00000040;
        private const int SET_FEATURE_ON_THREAD_RESTRICTED = 0x00000080;

        // Necessary dll import
        [DllImport("urlmon.dll")]
        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        static extern int CoInternetSetFeatureEnabled(
        int FeatureEntry,
        [MarshalAs(UnmanagedType.U4)] int dwFlags,
        bool fEnable);
        #endregion

        public Form2()
        {
            InitializeComponent();
            this.Icon = Icon.FromHandle(Properties.Resources.YZU_logo.GetHicon());
            Initial();//
        }

        private void submit(object DomDomc)
        {
            IHTMLDocument2 vDocument = (IHTMLDocument2)DomDomc;
            vDocument.parentWindow.execScript(@"window.confirm = function (){return true;} ", "javascript");
            vDocument.parentWindow.execScript(@"window.alert = function (){return true;} ", "javaScript");
        }

        private void setSelectedClass()
        {
            HtmlElementCollection tds = right.GetElementById("CosHtmlTable").GetElementsByTagName("td");
            foreach (HtmlElement td in tds)
            {
                string outerhtml = td.OuterHtml;
                if (outerhtml.Contains("cls_res_main_c_sel_l") && !outerhtml.Contains("退選"))
                {
                    foreach (HtmlElement td_a in td.GetElementsByTagName("a"))
                    {
                        string addItem = td_a.GetAttribute("title").Trim();
                        if (!selectedClass.Contains(addItem))
                            selectedClass.Add(addItem);
                    }
                }
            }
        }

        private void auto_click()
        {
            if (Web.Url.ToString().ToUpper().Contains("https://isdna1.yzu.edu.tw/CnStdSel/index.aspx".ToUpper())) ReLogin();
            try
            {
                submit(Web.Document.DomDocument);
                submit(Web.Document.Window.Frames["LeftCosList"].Document.DomDocument);
            }
            catch { }
            try
            {
                string[] kickedList = { "逾時", "logged off", "異常" };
                string[] eventList = { "已選過", "加選訊息", "已達上限", "課程衝堂", "不開放加選", "必須開課系所的學生", "不可再選" };
                string[] info = { "{0} 已選過!", "{0} 加選成功!", "", "{0} 衝堂!", "{0} 不開放加選!", "{0} 不開放加選!", "{0} 已修過" };
                string msg = "";
                try { msg = Web.Document.Window.Frames["frameright"].Document.Body.InnerHtml; }
                catch { return; }
                foreach (var kicked in kickedList)
                {
                    if (msg.Contains(kicked))
                        throw new LogException(kicked);
                }
                for (int i = 0; i < eventList.Length; ++i)
                {
                    if (msg.Contains(eventList[i]))
                    {
                        if (eventList[i] != "已達上限")
                        {
                            RobStatus.Items.Add(String.Format(info[i], currentRobClassKey));
                            remove_listbox(currentRobClassKey, currentRobClassValue);
                            //notifyIcon.ShowBalloonTip(5, "元智自動選課", String.Format(info[i], currentRobClassKey), ToolTipIcon.Info);
                            stop = false;
                        }
                        else if (selectedClass.Contains(currentRobClassKey))
                        {
                            RobStatus.Items.Add(String.Format(info[0], currentRobClassKey));
                            remove_listbox(currentRobClassKey, currentRobClassValue);
                            notifyIcon.ShowBalloonTip(5, "元智自動選課", String.Format(info[0], currentRobClassKey), ToolTipIcon.Info);//
                            stop = false;
                        }
                        if (ClassList.Items.Count == 0)
                        {
                            makeSure = false;
                            th.Abort();
                        }
                        submit(Web.Document.Window.Frames["frameright"].Document.DomDocument);
                        submit(Web.Document.DomDocument);
                        return;
                    }
                }
            }
            catch
            {
                try
                {
                    submit(Web.Document.Window.Frames["frameright"].Document.DomDocument);
                    submit(Web.Document.DomDocument);
                }
                catch { }
                ReLogin();
            }
        }

        private void ReLogin()
        {
            makeSure = false;

            if(th != null)th.Abort();
            RobStatus.Items.Clear();
            this.Hide();
            notifyIcon.Visible = false;
            f1.f2 = false;
            if(!Web.Url.ToString().Contains("isdna1.yzu.edu.tw/CnStdSel/index.aspx"))
            {
                Web.Url = new Uri("https://isdna1.yzu.edu.tw/CnStdSel/index.aspx");
            }
            /**/
            if (this.WindowState == FormWindowState.Minimized)
            {
                f1.WindowState = FormWindowState.Minimized;

                //通知欄顯示Icon
                f1.notifyIcon.Visible = true;
            }
            else
                f1.Show();

            f1.WindowState = this.WindowState;
        }

        private void Web_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            auto_click();
        }

        private void Form2_Shown(object sender, EventArgs e)
        {
            Web = f1.getWeb;
            cookieContainer = f1.cookieContainer;
            Web.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(WebBrowser_DocumentCompleted);
            Web.Navigated += new WebBrowserNavigatedEventHandler(Web_Navigated);

            ArrayList timeData = new ArrayList();
            for (int i = 1; i <= 7; ++i)
                for (int j = 1; j <= 14; ++j)
                    timeData.Add(new DictionaryEntry(String.Format("星期{0}第{1}節", i, j.ToString().PadLeft(2, '0')), i.ToString() + j.ToString()));
            ClassTime.DisplayMember = "Key";
            ClassTime.ValueMember = "Value";
            ClassTime.DataSource = timeData;
            ClassTime.SelectedIndex = -1;
            RobStatus.HorizontalScrollbar = true;
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            form_close();
        }

        private void start_Thread()
        {
            th = new Thread(() => { Start_Rob(); Application.DoEvents(); });
            th.Start();
        }

        private void readClass()
        {
            try
            {
                if (System.IO.File.Exists("Class"))
                {
                    StreamReader ClassRead = new StreamReader("Class");
                    string[] set = Encoding.UTF8.GetString(Convert.FromBase64String(ClassRead.ReadToEnd())).Split("::::".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < set.Length; i += 3)
                    {
                        addClassList(set[i], set[i + 1]);
                        addSelectedDept(set[i + 1], set[i + 2]);
                    }
                    ClassRead.Close();
                }
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show(f1.openInSU);
            }
            try
            {
                if (System.IO.File.Exists("Time"))
                {
                    StreamReader SettingRead = new StreamReader("Time");
                    string set = Encoding.UTF8.GetString(Convert.FromBase64String(SettingRead.ReadToEnd()));
                    SettingRead.Close();
                    Regex reg = new Regex("[0-9]+");
                    Match match = reg.Match(set);
                    if (match.Success)
                    {
                        sleepTime = Convert.ToInt32(match.Value);
                        if (sleepTime < bottom) isSmall = true;
                        else selectTime.Text = (Convert.ToDouble(match.Value) / 1000.0).ToString();
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show(f1.openInSU);
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Form.CheckForIllegalCrossThreadCalls = false;

            #region mute webbrowser
            // save the current volume
            uint _savedVolume;
            waveOutGetVolume(IntPtr.Zero, out _savedVolume);

            this.FormClosing += delegate
            {
                form_close();
                // restore the volume upon exit
                waveOutSetVolume(IntPtr.Zero, _savedVolume);
            };

            // mute
            waveOutSetVolume(IntPtr.Zero, 0);
            CoInternetSetFeatureEnabled(FEATURE_DISABLE_NAVIGATION_SOUNDS, SET_FEATURE_ON_PROCESS, true);
            #endregion

            readClass();
        }

        private Tuple<ArrayList, int> optionList(HtmlElementCollection options)
        {
            ArrayList Data = new ArrayList();

            int selectItem = -1;

            for (int i = 0; i < options.Count; ++i)
            {
                Data.Add(new DictionaryEntry(options[i].InnerText, options[i].GetAttribute("value")));
                if (options[i].OuterHtml.Contains("selected"))
                    selectItem = i;
            }

            return Tuple.Create(Data, selectItem);
        }

        private void getCredit()
        {
            OverCredit.Text = right.GetElementById("LabT_OverCredit").InnerText;
            UnderCredit.Text = right.GetElementById("LabT_UnderCredit").InnerText;
            SelCredit.Text = right.GetElementById("LabT_SelCredit").InnerText;

            setSelectedClass();
        }

        public void WebBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            auto_click();
            if (Web.ReadyState == WebBrowserReadyState.Complete)
            {
                if (e.Url.ToString().Contains("https://isdna1.yzu.edu.tw/CnStdSel/SelCurr.aspx"))
                {
                    HtmlWindow CurrentWindow = Web.Document.Window;
                    left = CurrentWindow.Frames["LeftCosList"].Document;
                    right = CurrentWindow.Frames["frameright"].Document;
                    try
                    {
                        DeptDegree.Text = left.GetElementById("Lab_DeptDegree").InnerText.Trim();
                        Student.Text = left.GetElementById("Lab_NameStdno").InnerText.Trim();

                        Tuple<ArrayList, int> ListData = optionList(left.GetElementById("DPL_DeptName").GetElementsByTagName("option"));
                        DeptName.DisplayMember = "Key";
                        DeptName.ValueMember = "Value";
                        DeptName.DataSource = ListData.Item1;
                        DeptName.SelectedIndex = ListData.Item2;

                        ListData = optionList(left.GetElementById("DPL_Degree").GetElementsByTagName("option"));
                        Degree.DisplayMember = "Key";
                        Degree.ValueMember = "Value";
                        Degree.DataSource = ListData.Item1;
                        Degree.SelectedIndex = ListData.Item2;

                        getCredit();

                        className_Set();

                        if (isSmall && first)
                        {
                            selectTime.Text = (Convert.ToDouble(sleepTime) / 1000.0).ToString();
                            first = false;
                        }

                    }
                    catch (Exception) { }
                }
                else if (e.Url.ToString().ToUpper().Contains("https://isdna1.yzu.edu.tw/CnStdSel/index.aspx".ToUpper()))
                {
                    ReLogin();
                }
                else if (PostBack)
                {
                    className_Set();
                    getCredit();
                    PostBack = false;
                }
            }
            else if (e.Url.ToString().ToUpper().Contains("https://isdna1.yzu.edu.tw/CnStdSel/index.aspx".ToUpper()))
            {
                ReLogin();
            }

        }

        private void className_Set()
        {
            try
            {
                ClassName.DataSource = null;
                ArrayList classData = new ArrayList();
                HtmlElementCollection keys = left.GetElementById("CosListTable").GetElementsByTagName("a");
                HtmlElementCollection vals = left.GetElementById("CosListTable").GetElementsByTagName("input");
                for (int i = 0; i < keys.Count; ++i)
                    classData.Add(new DictionaryEntry(keys[i].InnerText, vals[i].Id));
                ClassName.DisplayMember = "Key";
                ClassName.ValueMember = "Value";
                ClassName.DataSource = classData;
            }
            catch { }
        }

        private delegate void Start_RobCallback();
        private void Start_Rob()
        {
            if (!makeSure) return;
            if (Web.InvokeRequired || ClassList.InvokeRequired)
            {
                Start_RobCallback src = new Start_RobCallback(Start_Rob);
                this.Invoke(src);
            }
            else
            {
                string classD, v;
                int k;
                int random, quotient , mod;
                while (ClassList.Items.Count > 0 && !stop)
                {
                    if (!makeSure) return;
                    for (int i = 0; i < ClassList.Items.Count && !stop; i++)
                    {
                        if (!makeSure) return;
                        Application.DoEvents();
                        if (PostBack) { i--; continue; }
                        classD = ClassList.SelectedValue.ToString();
                        /**/
                        try
                        {
                            k = 0;
                            while (left.GetElementsByTagName("input")[classD] == null || String.IsNullOrWhiteSpace(ClassName.Text) || ClassName.SelectedIndex == -1)
                            {
                                if (!makeSure) return;
                                if ((!ClassName.Items.Contains(classD) || String.IsNullOrWhiteSpace(ClassName.Text) || ClassName.SelectedIndex == -1) && k <= 0)
                                {
                                    selectedDept.TryGetValue(classD, out v);
                                    DeptName.Text = v;
                                    Degree.Text = "全部";
                                    DeptName_SelectedIndexChanged(null, null);
                                    Degree_SelectedIndexChanged(null, null);
                                    k = 31;
                                }
                                k--;
                                Application.DoEvents();
                                Thread.Sleep(100);
                                Application.DoEvents();
                                auto_click();
                                if (Web.Url.OriginalString.ToUpper().Contains("https://isdna1.yzu.edu.tw/CnStdSel/index.aspx".ToUpper()))
                                {
                                    ReLogin();
                                    return;
                                }
                            }
                            //Web.Navigate("https://isdna1.yzu.edu.tw/CnStdSel/CurrMainTrans.aspx?mSelType=SelCos&mUrl=" + classD + ",B,");
                            //Web.Document.InvokeScript("TmpSelCos", new object[] { classD + ",B," });
                            left.GetElementsByTagName("input")[classD].InvokeMember("click");
                            currentRobClassKey = ClassList.Text;
                            currentRobClassValue = classD;
                        }
                        catch { auto_click(); }

                        PostBack = true;
                        random = sleepTime > bottom ? new Random().Next(bottom, sleepTime) : new Random().Next(sleepTime, bottom);
                        quotient = random / 100;
                        mod = random % 100;
                        while (quotient >= 0)
                        {
                            if (!makeSure) return;
                            Application.DoEvents();
                            Thread.Sleep(100);
                            quotient--;
                            Application.DoEvents();
                        }
                        if (mod > 0)
                        {
                            Application.DoEvents();
                            Thread.Sleep(mod);
                            Application.DoEvents();
                        }
                        /*Thread.Sleep(new Random().Next(1000, sleepTime));*/
                        if (ClassList.SelectedIndex < ClassList.Items.Count - 1) ClassList.SelectedIndex += 1;
                        else ClassList.SelectedIndex = 0;
                    }
                }
            }
        }

        private void ComboBoxChange(string ElementID)
        {
            ComboBox change_combobox = DeptName;
            if (ElementID == "DPL_Degree") change_combobox = Degree;
            HtmlElement select_list = left.GetElementById(ElementID);
            HtmlElementCollection option = select_list.GetElementsByTagName("option");
            foreach (HtmlElement opt in option)
            {
                if (opt.GetAttribute("value") == change_combobox.SelectedValue.ToString())
                {
                    opt.SetAttribute("selected", "selected");
                    select_list.InvokeMember("onchange");
                    break;
                }
            }
        }

        private void Name_Degree_change(bool dept)
        {
            //auto_click();
            if (!String.IsNullOrEmpty(ClassTime.Text))
            {
                try
                {
                    left.GetElementById("Btn_ShowDept").InvokeMember("click");
                    ClassName.SelectedIndex = -1;
                    ClassTime.SelectedIndex = -1;
                }
                catch { }
            }
            if (!String.IsNullOrEmpty(DeptName.Text) && !String.IsNullOrEmpty(Degree.Text))
            {
                try
                {
                    ComboBoxChange("DPL_DeptName");
                    ComboBoxChange("DPL_Degree");
                    PostBack = true;
                    ClassTime.SelectedIndex = -1;
                    ClassName.SelectedIndex = -1;
                }
                catch { auto_click(); }
            }
        }

        private void DeptName_SelectedIndexChanged(object sender, EventArgs e)
        {
            Name_Degree_change(true);
        }

        private void Degree_SelectedIndexChanged(object sender, EventArgs e)
        {
            Name_Degree_change(false);
        }

        private void ClassTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            //auto_click();
            try
            {
                if (!String.IsNullOrEmpty(ClassTime.Text) && Web.ReadyState == WebBrowserReadyState.Complete)
                {
                    right.InvokeScript("ShowCos", new object[] { ClassTime.SelectedValue.ToString() });
                    PostBack = true;
                    DeptName.SelectedIndex = -1;
                    Degree.SelectedIndex = -1;
                }
                else
                {
                    ClassTime.SelectedIndex = -1;
                }
            }
            catch { }
        }

        private void addClassList(string strClassNameText, string strClassNameValue)
        {
            DictionaryEntry add_data = new DictionaryEntry(strClassNameText, strClassNameValue);
            if (choseData.Contains(add_data)) return;
            choseData.Add(add_data);

            ClassList.DataSource = null;
            ClassList.DisplayMember = "Key";
            ClassList.ValueMember = "Value";
            ClassList.DataSource = choseData;

            StartRob.Enabled = true;
            if (ClassList.SelectedIndex >= 0) RemoveBtn.Enabled = true;
        }

        private void addSelectedDept(string classId, string strDeptName)
        {
            if (!selectedDept.ContainsKey(classId))
            {
                selectedDept.Add(classId, strDeptName);
            }
        }

        private void writeClass(string strClassNameText, string classId, string strDeptName)
        {
            try
            {
                string s = "";
                if (System.IO.File.Exists("Class"))
                {
                    StreamReader ClassRead = new StreamReader("Class");
                    s = Encoding.UTF8.GetString(Convert.FromBase64String(ClassRead.ReadToEnd()));
                    ClassRead.Close();
                }
                if (!s.Contains(strClassNameText))
                {
                    StreamWriter Setting = new StreamWriter("Class");
                    Setting.WriteLine(
                        Convert.ToBase64String(
                        Encoding.UTF8.GetBytes(s + strClassNameText + "::::" + classId + "::::" + strDeptName + "::::")
                        )
                    );
                    Setting.Close();
                }
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show(f1.openInSU);
            }
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            string classId = ClassName.SelectedValue.ToString();
            string strClassNameText = ClassName.Text;
            addClassList(strClassNameText, classId);

            string deptName = DIC.getName(strClassNameText);
            if (!String.IsNullOrWhiteSpace(DeptName.Text))
            {
                addSelectedDept(classId, DeptName.Text);
            }
            else if (deptName != null)
            {
                addSelectedDept(classId, deptName);
            }
            writeClass(strClassNameText, classId, deptName);
        }

        private void RemoveBtn_Click(object sender, EventArgs e)
        {
            remove_listbox(ClassList.Text, ClassList.SelectedValue);
        }

        private void remove_listbox(string key, object value)
        {
            if (ClassList.SelectedIndex == choseData.Count - 1) ClassList.SelectedIndex--;
            choseData.Remove(new DictionaryEntry(key, value));

            try
            {
                string s = "";
                if (System.IO.File.Exists("Class"))
                {
                    StreamReader ClassRead = new StreamReader("Class");
                    s = Encoding.UTF8.GetString(Convert.FromBase64String(ClassRead.ReadToEnd()));
                    ClassRead.Close();
                }
                if (s.Contains(value.ToString()))
                {
                    string endStr;
                    if (selectedDept.TryGetValue(value.ToString(), out endStr))
                    {
                        StreamWriter Setting = new StreamWriter("Class");
                        int intStart = s.IndexOf(key);
                        int intEnd = s.IndexOf(endStr) + endStr.Length;
                        string start = intStart > 0 ? s.Substring(0, intStart) : "";
                        string end = intEnd > 0 ? s.Substring(intEnd) : "";
                        string write = (start + end).Trim(':');
                        Setting.WriteLine(Convert.ToBase64String(Encoding.UTF8.GetBytes(write)));
                        Setting.Close();
                        if (String.IsNullOrWhiteSpace(write) && File.Exists("Class"))
                            File.Delete("Class");
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show(f1.openInSU);
            }

            selectedDept.Remove(value.ToString());

            ClassList.DataSource = null;
            ClassList.DisplayMember = "Key";
            ClassList.ValueMember = "Value";
            ClassList.DataSource = choseData;

            if (ClassList.Items.Count == 0)
            {
                change_status(true);
                StartRob.Enabled = false;
                RemoveBtn.Enabled = false;
            }
        }

        private void StartRob_Click(object sender, EventArgs e)
        {
            change_status(false);
            StartRob.Enabled = false;
            makeSure = true;
            //start_Thread();
            th = new Thread(() => { Start_Rob(); Application.DoEvents(); });
            th.Start();
        }

        private void EndRob_Click(object sender, EventArgs e)
        {
            makeSure = false;
            change_status(true);
            th.Abort();
        }

        private void change_status(bool status)
        {
            stop = status;
            AddBtn.Enabled = status;
            RemoveBtn.Enabled = status;
            ClassTime.Enabled = status;
            DeptName.Enabled = status;
            Degree.Enabled = status;
            ClassName.Enabled = status;
            ClassList.Enabled = status;
            if (ClassList.Items.Count > 0) StartRob.Enabled = true;
            EndRob.Enabled = !status;
        }

        private void form_close()
        {
            makeSure = false;
            try { Environment.Exit(Environment.ExitCode); }
            catch { }
            Application.Exit();
            if (f1 != null)
            {
                try { f1.close(); }
                catch { }
            }
        }

        private void EndSelect_Click(object sender, EventArgs e)
        {
            form_close();
        }

        private void selectTime_TextChanged(object sender, EventArgs e)
        {
            string selectTimeText = selectTime.Text;
            if (String.IsNullOrWhiteSpace(selectTimeText) || selectTimeText.EndsWith(".")) return;
            try
            {
                int t = Convert.ToInt32(Convert.ToDouble(selectTimeText) * 1000);
                if (t <= 0) return;
                if (t < bottom)
                {
                    DialogResult result = MessageBox.Show(String.Format("自動選課時間小於{0}秒將有可能被視作使用外掛程式\n即使如此仍要以每{1}秒作為選課間隔?", Convert.ToDouble(bottom) / 1000.0, selectTimeText),
                        "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        sleepTime = t;
                    }
                    else if (result == DialogResult.No)
                    {
                        selectTime.Text = (Convert.ToDouble(defaultSleep) / 1000.0).ToString();
                        sleepTime = defaultSleep;
                    }
                }
                else sleepTime = t;

                try
                {
                    StreamWriter Setting = new StreamWriter("Time");
                    Setting.WriteLine(
                        Convert.ToBase64String(
                        Encoding.UTF8.GetBytes(sleepTime.ToString())
                        )
                    );
                    Setting.Close();
                }
                catch (UnauthorizedAccessException)
                {
                    MessageBox.Show(f1.openInSU);
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
            catch { MessageBox.Show("輸入格式錯誤"); selectTime.Text = (Convert.ToDouble(sleepTime) / Convert.ToDouble(bottom)).ToString(); }
        }

        // 視窗初始
        private void Initial()
        {
            /**/
            notifyIcon = new NotifyIcon();
            //設定通知欄提示的文字
            notifyIcon.BalloonTipText = "Still running";
            //設定通知欄在滑鼠移至Icon上的要顯示的文字
            notifyIcon.Text = "元智自動選課";
            //決定一個Logo
            notifyIcon.Icon = (System.Drawing.Icon)(Icon.FromHandle(Properties.Resources.YZU_logo.GetHicon()));
            //設定按下Icon發生的事件
            notifyIcon.Click += (sender, e) =>
            {
                //取消再通知欄顯示Icon
                notifyIcon.Visible = false;
                //顯示在工具列
                this.ShowInTaskbar = true;
                this.Show();
                this.TopMost = true;
                //顯示程式的視窗
                this.WindowState = FormWindowState.Normal;
                f1.SHOW = false;
            };

            //設定右鍵選單
            //宣告一個選單的容器
            ContextMenu contextMenu = new ContextMenu();
            //宣告選單項目
            MenuItem notifyIconMenuItem1 = new MenuItem();
            //可以設定是否可勾選
            notifyIconMenuItem1.Checked = true;
            //在NotifyIcon中的頁籤，順序用
            notifyIconMenuItem1.Index = 2;
            //設定顯示的文字，後面的(S&)代表使用者按S鍵也可以觸發Click事件!
            notifyIconMenuItem1.Text = "停止(E&)";
            //設定按下後的事情
            notifyIconMenuItem1.Click += (sender, e) =>
            {
                form_close();
            };

            MenuItem notifyIconMenuItem2 = new MenuItem();
            //可以設定是否可勾選
            notifyIconMenuItem1.Checked = true;
            //在NotifyIcon中的頁籤，順序用
            notifyIconMenuItem2.Index = 1;
            //設定顯示的文字，後面的(S&)代表使用者按S鍵也可以觸發Click事件!
            notifyIconMenuItem2.Text = "開啟(O&)";

            //將MenuItem加入到ContextMenu容器中!
            contextMenu.MenuItems.Add(notifyIconMenuItem1);
            contextMenu.MenuItems.Add(notifyIconMenuItem2);
            //設定notifyIcon的選單內容等於剛剛宣告的選單容器ContextMen;
            notifyIcon.ContextMenu = contextMenu;

        }

        private void Form2_SizeChanged(object sender, EventArgs e)
        {
            /**/
            try
            {
                if (this.WindowState == FormWindowState.Minimized)
                {
                    //讓程式在工具列中隱藏
                    this.ShowInTaskbar = false;
                    //隱藏程式本身的視窗
                    this.Hide();
                    //通知欄顯示Icon
                    notifyIcon.Visible = true;
                    if (!f1.SHOW)
                        //通知欄提示 (顯示時間毫秒，標題，內文，類型)
                        notifyIcon.ShowBalloonTip(5, "元智自動選課", "仍在背景執行中", ToolTipIcon.Info);
                    f1.SHOW = true;
                }
                else if (f1 != null && this.WindowState == FormWindowState.Normal)
                {
                    this.TopMost = true;
                    this.Show();
                    f1.SHOW = false;
                }
            }
            catch { }
        }

        private void Form2_VisibleChanged(object sender, EventArgs e)
        {
            if (ClassList.Items.Count > 0 && th != null && f1.f2)
            {
                makeSure = true;
                start_Thread();
            }
        }

        [Serializable()]
        public class LogException : System.Exception
        {
            public LogException() : base() { }
            public LogException(string message) : base(message) { }
            public LogException(string message, System.Exception inner) : base(message, inner) { }

            // A constructor is needed for serialization when an
            // exception propagates from a remoting server to the client. 
            protected LogException(System.Runtime.Serialization.SerializationInfo info,
                System.Runtime.Serialization.StreamingContext context) { }
        }

        private void ReloginBtn_Click(object sender, EventArgs e)
        {
            ReLogin();
        }

        private static class DIC
        {
            static Dictionary<string, string[]> dic = new Dictionary<string, string[]>
            {
                {"EG", new string[] { "300 工學院" }},
                {"EEA", new string[] { "301 電機系" }},
                {"ME", new string[] { "302 機械系" }},
                {"CH", new string[] { "303 化材系" }},
                {"CS", new string[] { "304 資工系" }},
                {"IE", new string[] { "305 工管系" }},
                {"EEB", new string[] { "307 通訊系" }},
                {"EEC", new string[] { "308 光電系" }},
                {"DE", new string[] { "309 工程英專" }},
                {"EI", new string[] { "310 電通英專" }},
                {"ME+", new string[] { "322 機械碩" }},
                {"CH+", new string[] { "323 化材碩" }},
                {"IE+", new string[] { "325 工管碩" }},
                {"EEA+", new string[] { "326 電機碩" }},
                {"EEB+", new string[] { "327 通訊碩" }},
                {"EEC+", new string[] { "328 光電碩" }},
                {"BI+", new string[] { "329 生技碩" }},
                {"CM", new string[] { "505 管理學院學士班" }},
                {"CM+", new string[] { "530 經營管理碩", "531 財會碩" }},
                {"GM+", new string[] { "532 管理碩專" }},
                {"CM++", new string[] { "554 管理博" }},
                {"HS", new string[] { "600 人社院" }},
                {"FL", new string[] { "601 應外系" }},
                {"CC", new string[] { "602 中語系" }},
                {"AD", new string[] { "603 藝設系" }},
                {"SC", new string[] { "604 社政系" }},
                {"IH", new string[] { "608 人社英專" }},
                {"FL+", new string[] { "621 應外碩" }},
                {"CC+", new string[] { "622 中語碩" }},
                {"AM+", new string[] { "623 藝設碩" }},
                {"SC+", new string[] { "624 社政碩" }},
                {"IP", new string[] { "656 文產博" }},
                {"CI", new string[] { "700 資訊院" }},
                {"IM", new string[] { "701 資管系" }},
                {"IC", new string[] { "702 資傳系" }},
                {"IN", new string[] { "705 資訊英專" }},
                {"IM+", new string[] { "721 資管碩" }},
                {"GI+", new string[] { "722 資傳碩" }},
                {"CS+", new string[] { "724 資工碩" }},
                {"CB+", new string[] { "725 生醫碩" }},
                {"IM++", new string[] { "751 資管博" }},
                {"CL", new string[] { "901 通識" }},
                {"CP", new string[] { "901 通識" }},
                {"FC", new string[] { "901 通識" }},
                {"GN", new string[] { "901 通識" }},
                {"GS", new string[] { "901 通識" }},
                {"ID", new string[] { "901 通識" }},
                {"LE", new string[] { "901 通識" }},
                {"LS", new string[] { "901 通識" }},
                {"MT", new string[] { "903 軍訓室" }},
                {"PL", new string[] { "904 體育室" }},
                {"EL", new string[] { "906 國際語言文化中心" }},
                {"LC", new string[] { "906 國際語言文化中心" }}
            };

            public static string getName(string key)
            {
                Regex regex = new Regex(@"\d+");
                Match match = regex.Match(key);
                string re = "";
                string[] reary = null;
                if (match.Success)
                {
                    int firstN = Int16.Parse(match.Value[0].ToString());
                    regex = new Regex(@"[A-Z]+");
                    Match m2 = regex.Match(key);
                    string tmp = m2.Value;
                    if (firstN > 3) tmp += "+";
                    if (firstN > 6) tmp += "+";
                    if (!dic.TryGetValue(tmp, out reary))
                        if (!dic.TryGetValue(tmp.Substring(0, tmp.Length - 1), out reary))
                            dic.TryGetValue(tmp.Substring(0, tmp.Length - 2), out reary);
                    if (reary.Length > 1)
                    {
                        if (key.Contains("S1") || key.Contains("S2") || key.Contains("S3"))
                            re = reary[1];
                        else re = reary[0];
                    }
                    else re = reary[0];
                }
                return re;
            }
        }

    }
}
