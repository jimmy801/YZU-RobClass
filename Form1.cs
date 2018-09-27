using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Tesseract;
using System.Net;
using System.Web;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using mshtml;
using System.Threading;

namespace RobClass
{
    public partial class Form1 : Form
    {
        public NotifyIcon notifyIcon;//
        bool cracked = false;
        bool refreshWeb = false;
        bool wrongAccountOrPassword = false;
        public bool f2 = false;
        public bool SHOW = false;
        bool init = false;
        bool notGetClassNumYet = false;
        int t = 5;
        public CookieContainer cookieContainer = new CookieContainer();
        Form2 NewForm2 = new Form2();
        WebBrowser webBrowser1 = new WebBrowser();
        //string account_text;
        //string password_test;
        string systemNum;
        public string openInSU = "程式安裝於系統路徑，請以系統管理員身分執行!";

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

        public Form1()
        {
            //AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
            this.Icon = Icon.FromHandle(Properties.Resources.YZU_logo.GetHicon());
            InitializeComponent();
            Initial();
        }

        /*System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            string dllName = args.Name.Contains(",") ? args.Name.Substring(0, args.Name.IndexOf(',')) : args.Name.Replace(".dll", "");

            dllName = dllName.Replace(".", "_");

            if (dllName.EndsWith("_resources")) return null;

            System.Resources.ResourceManager rm = new System.Resources.ResourceManager(GetType().Namespace + ".Properties.Resources",
                System.Reflection.Assembly.GetExecutingAssembly());

            byte[] bytes = (byte[])rm.GetObject(dllName);

            return System.Reflection.Assembly.Load(bytes);
        }
        */
        public WebBrowser getWeb
        {
            get { return webBrowser1; }
        }

        // 視窗初始
        private void Initial()
        {
            webBrowser1.Url = new Uri("https://isdna1.yzu.edu.tw/CnStdSel/index.aspx");
            webBrowser1.Navigated += webBrowser1_Navigated;
            webBrowser1.DocumentCompleted += webBrowser1_DocumentCompleted;
            /**/
            notifyIcon = new NotifyIcon();
            //設定通知欄提示的文字
            notifyIcon.BalloonTipText = "Still running";
            //設定通知欄在滑鼠移至Icon上的要顯示的文字
            notifyIcon.Text = "元智自動選課-登入中...";
            //決定一個Logo
            notifyIcon.Icon = (System.Drawing.Icon)(Icon.FromHandle(Properties.Resources.YZU_logo.GetHicon()));
            //設定按下Icon發生的事件
            notifyIcon.Click += (sender, e) =>
            {
                //顯示程式的視窗
                this.Show();
                //取消再通知欄顯示Icon
                notifyIcon.Visible = false;
                //顯示在工具列
                this.ShowInTaskbar = true;
                this.WindowState = FormWindowState.Normal;
                SHOW = false;
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
                try { Environment.Exit(Environment.ExitCode); }
                catch { }
                Application.Exit();
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

        // 視窗是否為最小化
        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            /**/
            if (this.WindowState == FormWindowState.Minimized)
            {
                //讓程式在工具列中隱藏
                this.ShowInTaskbar = false;
                //隱藏程式本身的視窗
                this.Hide();
                //通知欄顯示Icon
                notifyIcon.Visible = true;

                if (!SHOW)
                    //通知欄提示 (顯示時間毫秒，標題，內文，類型)
                    notifyIcon.ShowBalloonTip(5, "元智自動選課", "仍在背景執行中", ToolTipIcon.Info);
                SHOW = true;
            }
            else if (this.WindowState == FormWindowState.Normal) SHOW = false;
        }

        #region Verification code
        // 驗證碼二元化
        private Image convert2GrayScale(Image img)
        {
            Bitmap bitmap = new Bitmap(img);

            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    Color pixelColor = bitmap.GetPixel(i, j);
                    byte r = pixelColor.R;
                    byte g = pixelColor.G;
                    byte b = pixelColor.B;

                    byte gray = (byte)(0.299 * (float)r + 0.587 * (float)g + 0.114 * (float)b);
                    r = g = b = gray;
                    pixelColor = Color.FromArgb(r, g, b);

                    bitmap.SetPixel(i, j, pixelColor);
                }
            }

            Image grayImage = Image.FromHbitmap(bitmap.GetHbitmap());
            bitmap.Dispose();
            return grayImage;
        }

        // 取得噪點
        public class NoisePoint
        {
            public int X { get; set; }
            public int Y { get; set; }
        }

        // 去噪線
        private Image RemoveNoiseLine(Image img)
        {
            Bitmap BmpSource = new Bitmap(img);
            for (int i = 0; i < BmpSource.Height; i++)
            {
                for (int j = 0; j < BmpSource.Width; j++)
                {
                    int grayValue = BmpSource.GetPixel(j, i).R;
                    if (grayValue <= 255 && grayValue >= 160)
                        BmpSource.SetPixel(j, i, Color.FromArgb(255, 255, 255));
                }
            }
            Image removeLine = Image.FromHbitmap(BmpSource.GetHbitmap());
            BmpSource.Dispose();
            return removeLine;
        }

        // 去噪點
        private Image RemoveNoisePoint(Image img)
        {
            Bitmap BmpSource = new Bitmap(img);
            // 去噪點
            List<NoisePoint> points = new List<NoisePoint>();

            for (int k = 0; k < 5; k++)
            {
                for (int i = 0; i < BmpSource.Height; i++)
                    for (int j = 0; j < BmpSource.Width; j++)
                    {
                        int flag = 0;
                        int grayVal = 255;
                        // 檢查上相鄰像素
                        if (i - 1 > 0 && BmpSource.GetPixel(j, i - 1).R != grayVal) flag++;
                        if (i + 1 < BmpSource.Height && BmpSource.GetPixel(j, i + 1).R != grayVal) flag++;
                        if (j - 1 > 0 && BmpSource.GetPixel(j - 1, i).R != grayVal) flag++;
                        if (j + 1 < BmpSource.Width && BmpSource.GetPixel(j + 1, i).R != grayVal) flag++;
                        if (i - 1 > 0 && j - 1 > 0 && BmpSource.GetPixel(j - 1, i - 1).R != grayVal) flag++;
                        if (i + 1 < BmpSource.Height && j - 1 > 0 && BmpSource.GetPixel(j - 1, i + 1).R != grayVal) flag++;
                        if (i - 1 > 0 && j + 1 < BmpSource.Width && BmpSource.GetPixel(j + 1, i - 1).R != grayVal) flag++;
                        if (i + 1 < BmpSource.Height && j + 1 < BmpSource.Width && BmpSource.GetPixel(j + 1, i + 1).R != grayVal) flag++;

                        if (flag < 3)
                            points.Add(new NoisePoint() { X = j, Y = i });
                    }
                foreach (NoisePoint point in points)
                    BmpSource.SetPixel(point.X, point.Y, Color.FromArgb(255, 255, 255));

            }
            Image removePoint = Image.FromHbitmap(BmpSource.GetHbitmap());
            BmpSource.Dispose();
            return removePoint;
        }

        // 去掉邊框
        private Image ClearPictureBorder(Image img)
        {
            // 清除驗證碼邊界
            Bitmap BmpSource = new Bitmap(img);
            for (int i = 0; i < BmpSource.Height; i++)
            {
                for (int j = 0; j < BmpSource.Width; j++)
                {
                    if (i < 3 || j < 3 || j > BmpSource.Width - 2 || i > BmpSource.Height - 2)
                        BmpSource.SetPixel(j, i, Color.FromArgb(255, 255, 255));
                }
            }
            Image clearBoard = Image.FromHbitmap(BmpSource.GetHbitmap());
            BmpSource.Dispose();
            return clearBoard;
        }

        // 去掉不重要的旁邊空白
        private Image ConvertBmpValidRange(Image img, int pCharsCount)
        {
            // 清除驗證碼邊界
            Bitmap BmpSource = new Bitmap(img);
            // 圖片最大 X, Y，處理後變成起始 X, Y
            int posX1 = BmpSource.Width, posY1 = BmpSource.Height;
            // 圖片起始 X, Y，處理後變成最大 X, Y
            int posX2 = 0, posY2 = 0;
            int grayVal = 255;

            // 取得有效範圍區域
            for (int i = 0; i < BmpSource.Height; i++)
            {
                for (int j = 0; j < BmpSource.Width; j++)
                {
                    int pixelVal = BmpSource.GetPixel(j, i).R;
                    if (pixelVal < grayVal) // 如像該素值低於指定灰階值則進行縮小區域
                    {
                        if (posX1 > j) posX1 = j; // 如 X2 像素位置大於圖片寬度則縮小寬度
                        if (posY1 > i) posY1 = i; // 如 Y2 像素位置大於圖片高度則縮小高度
                        if (posX2 < j) posX2 = j; // 如 X1 像素位置小於圖片寬度則縮小寬度
                        if (posY2 < i) posY2 = i; // 如 Y1 像素位置小於圖片寬度則縮小寬度
                    }
                }
            }

            // 確保圖片可以平均切割圖片
            if (pCharsCount > 0)
            {
                int span = pCharsCount - (posX2 - posX1 + 1) % pCharsCount;
                if (span < pCharsCount)
                {
                    int leftSpan = span / 2;
                    if (posX1 > leftSpan)
                        posX1 = posX1 - leftSpan;
                    if (posX2 + span - leftSpan < BmpSource.Width)
                        posX2 = posX2 + span - leftSpan;
                }
            }
            // 產生變更後的圖片
            Rectangle cloneRect = new Rectangle(posX1, posY1, posX2 - posX1 + 1, posY2 - posY1 + 1);
            BmpSource = BmpSource.Clone(cloneRect, BmpSource.PixelFormat);
            Image ValidRange = Image.FromHbitmap(BmpSource.GetHbitmap());
            BmpSource.Dispose();
            return ValidRange;
        }

        // 辨識驗證碼
        private string parseCaptchaStr(Image image)
        {
            Tesseract.TesseractEngine ocr = new Tesseract.TesseractEngine(@"tessdata\", "yzufont");
            ocr.SetVariable("tessedit_char_whitelist", "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ");
            Page tmpPage = ocr.Process(new Bitmap(image), pageSegMode: ocr.DefaultPageSegMode);
            return Regex.Replace(tmpPage.GetText(), @"\s+", String.Empty);
        }

        // 判斷驗證碼正確性
        private void Check_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return) Login_Click(sender, e);
        }
        #endregion

        private void Reset()
        {
            cracked = refreshWeb = false;
            t = 5;
            Check.Text = "";
            CheckPicture.Image = null;
        }

        private void Login_Click(object sender, EventArgs e)
        {
            if (!init) return;

            #region Get Setting
            if (Remember.Checked)
            {
                try
                {
                    StreamWriter Setting = new StreamWriter("Setting");
                    Setting.WriteLine(Convert.ToBase64String(Encoding.UTF8.GetBytes(Account.Text + "::::" + Password.Text + "::::" + systemNum)));
                    Setting.Close();
                }
                catch (UnauthorizedAccessException)
                {
                    DialogResult re = MessageBox.Show(openInSU, "", MessageBoxButtons.OK);
                    if (re == DialogResult.OK) close();
                }
            }
            else
                File.Delete("Setting");
            #endregion

            /*if (Remember.Checked && validUser())
            {
                account_text = Account.Text;
                password_test = Password.Text;
            }*/

            InputLoginDetail();
            HtmlElement btnSubmit = webBrowser1.Document.GetElementById("btnOK");
            btnSubmit.InvokeMember("click");
            Reset();
        }

        private bool validUser()
        {
            return !String.IsNullOrWhiteSpace(Account.Text) && !String.IsNullOrWhiteSpace(Password.Text);
        }

        // 初始視窗
        private void Form1_Shown(object sender, EventArgs e)
        {
            this.ActiveControl = Account;

            Reset();

            DialogResult result = MessageBox.Show("本程式僅供學術研究，若因使用本程式而被學校處分，開發人員一概不負責。", "Warning", MessageBoxButtons.OKCancel);

            if (result == DialogResult.OK)
            {
                #region Get Account Setting
                /*
            if (!String.IsNullOrWhiteSpace(account_text) && !String.IsNullOrWhiteSpace(password_test))
            {
                Account.Text = account_text;
                Password.Text = password_test;
            }*/

                bool start = false;

                try
                {
                    if (System.IO.File.Exists("Setting"))
                    {
                        string s;
                        StreamReader Setting = new StreamReader("Setting");
                        s = Setting.ReadToEnd();
                        string[] set = Regex.Split(Encoding.UTF8.GetString(Convert.FromBase64String(s)), "::::", RegexOptions.IgnoreCase);
                        Account.Text = set[0];
                        Password.Text = set[1];
                        systemNum = set[2];
                        Remember.Checked = true;
                        Setting.Close();
                        start = true;
                    }
                }
                catch (UnauthorizedAccessException)
                {
                    DialogResult re = MessageBox.Show(openInSU, "", MessageBoxButtons.OK);
                    if (re == DialogResult.OK) close();
                }
                #endregion

                if (start && validUser())
                {
                    if (notGetClassNumYet) GetClassSystem();
                    if (Check.Text.Length != 4 || CheckPicture.Image == null) t = 0;
                    timer1.Start();
                }
                else ReloadWeb();
            }
            else close();

            init = true;
        }

        // 5秒自動進入
        private void timer1_Tick(object sender, EventArgs e)
        {
            Count.Text = t.ToString();
            if (t-- == 0) { timer1.Stop(); t = 5; Count.Text = ""; Login_Click(sender, e); }
        }

        private void GetClassSystem()
        {
            HtmlElementCollection g = webBrowser1.Document.GetElementsByTagName("option");
            foreach (HtmlElement optionName in g)
            {
                if (optionName.GetAttribute("value") != "00")
                {
                    ClassSystemID.Items.Add(optionName.GetAttribute("value"));
                    ClassSystem.Items.Add(optionName.InnerText);
                    Regex reg = new Regex(@"[0-9]+");
                    Match match = reg.Match(optionName.InnerText);
                    if (match.Success)
                    {
                        string num = match.Value;
                        if (systemNum != num)
                        {
                            if (System.IO.File.Exists("Class"))
                                File.Delete("Class");
                            systemNum = num;
                        }
                    }
                }
            }
            ClassSystem.SelectedIndex = 0;
        }

        private Image GetCheckImage(HtmlElement ImageTag)
        {
            HTMLDocument doc = (HTMLDocument)webBrowser1.Document.DomDocument;

            HTMLBody body = (HTMLBody)doc.body;

            IHTMLControlRange rang = (IHTMLControlRange)body.createControlRange();

            IHTMLControlElement Img = (IHTMLControlElement)ImageTag.DomElement; //圖片地址

            bool containImg = Clipboard.ContainsImage();
            bool containText = Clipboard.ContainsText();

            Image oldImage = Clipboard.GetImage();
            string oldText = Clipboard.GetText();

            rang.add(Img);

            rang.execCommand("Copy", false, null); //拷貝到內存

            Image numImage = Clipboard.GetImage(); //從Clipboard中取圖

            if (containImg)
                try { Clipboard.SetImage(oldImage); } //還原 
                catch { }
            else if (containText)
                try { Clipboard.SetText(oldText); } //還原 
                catch { }

            return numImage;
        }

        private void ReloadWeb()
        {
            Check.Text = "";
            CheckPicture.Image = null;
            refreshWeb = true;
            t = 0;
            timer1.Start();
        }

        private void InputLoginDetail()
        {
            if (ClassSystemID.SelectedItem == null) return;
            if (webBrowser1.Document.GetElementById("DPL_SelCosType") == null)
            {
                Thread.Sleep(100);
                Application.DoEvents();
            }
            else if (webBrowser1.Document.GetElementById("DPL_SelCosType").GetElementsByTagName("option") == null)
            {
                Thread.Sleep(100);
                Application.DoEvents();
            }
            HtmlElementCollection option = webBrowser1.Document.GetElementById("DPL_SelCosType").GetElementsByTagName("option");
            foreach (HtmlElement opt in option)
            {
                if (opt.GetAttribute("value") == ClassSystemID.SelectedItem.ToString())
                {
                    opt.SetAttribute("selected", "selected");
                    break;
                }
            }
            HtmlElement contain = webBrowser1.Document.GetElementById("Txt_User");
            contain.SetAttribute("value", Account.Text);
            contain = webBrowser1.Document.GetElementById("Txt_Password");
            contain.SetAttribute("value", Password.Text);
            contain = webBrowser1.Document.GetElementById("Txt_CheckCode");
            contain.SetAttribute("value", Check.Text);
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            IHTMLDocument2 vDocument = (IHTMLDocument2)webBrowser1.Document.DomDocument;
            vDocument.parentWindow.execScript(@"window.confirm = function (){return true;} ", "javascript");
            vDocument.parentWindow.execScript(@"window.alert = function (){return true;} ", "javaScript");
            webBrowser1.ScriptErrorsSuppressed = true;
            if (webBrowser1.Url.OriginalString.ToUpper().Contains("https://isdna1.yzu.edu.tw/CnStdSel/index.aspx".ToUpper()))
            {
                if (webBrowser1.Document.Body.OuterHtml.Contains("帳號或密碼錯誤"))
                {
                    ErrorLabel.Text = "帳號或密碼錯誤";
                    wrongAccountOrPassword = true;
                }

                #region Get CheckPicture
                if (CheckPicture.Image == null || refreshWeb)
                {
                    HtmlElementCollection imgGet = webBrowser1.Document.GetElementsByTagName("img");

                    foreach (HtmlElement imgItem in imgGet)
                    {
                        if (imgItem.GetAttribute("src").Contains("SelRandomImage.aspx")) { CheckPicture.Image = GetCheckImage(imgItem); break; }
                    }
                    refreshWeb = CheckPicture.Image == null;
                }
                #endregion

                #region Get Class System
                if (ClassSystem.Items.Count == 0 && init) GetClassSystem();
                else if (!init) notGetClassNumYet = true;
                #endregion

                if (CheckPicture.Image != null)
                {
                    Check.Text = parseCaptchaStr(convert2GrayScale(RemoveNoiseLine(RemoveNoisePoint(ClearPictureBorder(CheckPicture.Image)))));
                    cracked = true;
                    if (Check.Text.Length != 4) { ReloadWeb(); cracked = false; }
                    if (validUser() && cracked && !wrongAccountOrPassword && init) timer1.Start();
                }
                else if (Check.Text.Length != 4) ReloadWeb();
                else ReloadWeb();
            }
            else if (webBrowser1.Url.ToString().ToUpper() == "https://www.yzu.edu.tw/".ToUpper())
            {
                timer1.Stop();
                MessageBox.Show("選課系統尚未開放!");
                Environment.Exit(Environment.ExitCode);
            }
            else if (!f2)
            {
                this.Hide();
                timer1.Stop();
                //通知欄顯示Icon
                notifyIcon.Visible = false;//
                //NewForm2 = new Form2();
                f2 = true;
                NewForm2.f1 = this;
                //webBrowser1.Dock = DockStyle.Fill;
                // this.Refresh();
                /**/
                NewForm2.makeSure = true;
                NewForm2.Show();
                NewForm2.WindowState = this.WindowState;
                /**/
                if (this.WindowState == FormWindowState.Minimized)
                {
                    NewForm2.WindowState = FormWindowState.Minimized;
                    NewForm2.ShowInTaskbar = false;
                    NewForm2.Hide();
                    //通知欄顯示Icon
                    NewForm2.notifyIcon.Visible = true;
                }
            }
        }

        private void ClassSystem_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClassSystemID.SelectedIndex = ClassSystem.SelectedIndex;
            Regex reg = new Regex(@"[0-9]+");
            Match match = reg.Match(ClassSystem.Text);
            if (match.Success)
                systemNum = match.Value;
        }

        private void submit(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return && validUser())
            {
                Login_Click(sender, e);
                e.Handled = true;
            }
        }

        private void stop_count(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                timer1.Stop();
                t = 5;
                Count.Text = "";
                ErrorLabel.Text = "";
                cracked = false;
                wrongAccountOrPassword = false;
            }
        }

        private void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            IHTMLDocument2 vDocument = (IHTMLDocument2)webBrowser1.Document.DomDocument;
            vDocument.parentWindow.execScript(@"window.confirm = function (){return true;} ", "javascript");
            vDocument.parentWindow.execScript(@"window.alert = function (){return true;} ", "javaScript");
            webBrowser1.ScriptErrorsSuppressed = true;
        }

        public void close()
        {
            if (NewForm2 != null)
            {
                try { NewForm2.Close(); }
                catch { }
            }
            try { Environment.Exit(Environment.ExitCode); }
            catch { };
            Application.Exit();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            #region mute webbrowser
            // save the current volume
            uint _savedVolume;
            waveOutGetVolume(IntPtr.Zero, out _savedVolume);

            this.FormClosing += delegate
            {
                close();
                // restore the volume upon exit
                waveOutSetVolume(IntPtr.Zero, _savedVolume);
            };

            // mute
            waveOutSetVolume(IntPtr.Zero, 0);
            CoInternetSetFeatureEnabled(FEATURE_DISABLE_NAVIGATION_SOUNDS, SET_FEATURE_ON_PROCESS, true);
            #endregion
        }

    }
}
