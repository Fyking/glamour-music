using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;
//方法一
using System.Runtime;
using System.Runtime.InteropServices;

//
using Shell32;
namespace 魅力
{
    public partial class FrmMain : Form
    {


        public FrmMain()
        {
            InitializeComponent();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            if (Save.close==0)
            {
                this.Hide();
            }
            else if (Save.close==1)
            {
                this.Close();
            }
            else if (Save.close == 2)
            {
                FrmApp A = new FrmApp();
                A.ShowDialog();
            }

        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            //提示信息
            toolShow.SetToolTip(pcNext, "下一曲");
            toolShow.SetToolTip(pcBack, "上一曲");
            toolShow.SetToolTip(pcPause, "播放/暂停");
            toolShow.SetToolTip(pcLove, "喜爱歌曲");
            toolShow.SetToolTip(pcBendi, "本地音乐");
            toolShow.SetToolTip(pcStar, "明星头像");
            toolShow.SetToolTip(pictureBox9, "设置");
            toolShow.SetToolTip(pictureBox10, "最小化");
            toolShow.SetToolTip(pictureBox8, "关闭");
            toolShow.SetToolTip(pictureBox12, "设置");
            toolShow.SetToolTip(pictureBox11, "最小化");
            toolShow.SetToolTip(pictureBox7, "关闭");
            toolShow.SetToolTip(pictureBox6, "音量");
            toolShow.SetToolTip(pictureBox5, "歌曲循环");
            toolShow.SetToolTip(pictureBox2, "添加歌曲");
            toolShow.SetToolTip(pictureBox3, "迷你化");
            toolShow.SetToolTip(pictureBox13, "显示正常窗体");
            toolShow.SetToolTip(pictureBox4, "定时关闭");
            toolShow.SetToolTip(lblMusic, "音乐窗口");
            toolShow.SetToolTip(lblLrcs, "歌词");
            //初始加载
            label1.Text = "";
            label2.Text = "";
            label3.Text = "";
            label4.Text = "魅力音乐，你的魅力";
            label5.Text = "";
            label6.Text = "";
            label7.Text = "";
            //加载设置,读取
            StreamReader r = File.OpenText("保存设置.txt");
            Save.SongName = r.ReadLine();
            Save.Vioce = int.Parse(r.ReadLine());
            Save.LryColor = r.ReadLine();
            Save.FrmBack = r.ReadLine();
            SongList.playIndex = int.Parse(r.ReadLine());
            string str = r.ReadLine();
            if (str == "随机播放")
            {
                Play.SongMS = SongMS.随机播放;
            }
            else if (str == "单曲播放")
            {
                Play.SongMS = SongMS.单曲播放;
            }
            else if (str == "单曲循环")
            {
                Play.SongMS = SongMS.单曲循环;
            }
            else if (str == "列表循环")
            {
                Play.SongMS = SongMS.列表循环;
            }
            else if (str == "顺序播放")
            {
                Play.SongMS = SongMS.顺序播放;
            }
            Save.FrmBackColor = r.ReadLine();
            //歌词颜色
            Save.lrcFont = r.ReadLine();
            //歌词字体
            Save.lrcColor = r.ReadLine();
            Save.close =int.Parse(r.ReadLine());
            r.Close();
            //设置上次设置
            //声音
            srocVoice.MyStartValue = Save.Vioce;
            //fram背景
            if (File.Exists(Save.FrmBack))
            {
                this.BackgroundImage = Image.FromFile(Save.FrmBack);
            }
            if (Save.FrmBackColor != "Transparent")
            {
                this.BackColor = System.Drawing.Color.FromName(Save.FrmBackColor);
            }

            //加载歌曲列表
            try
            {
                //加载数组
                Save.LaodSongList();
                for (int i = 0; i < Play.songNum; i++)
                {
                    ListViewItem item = new ListViewItem(SongList.songList[i].SongID);
                    item.Tag = SongList.songList[i].SongID;
                    item.SubItems.Add(SongList.songList[i].Title);
                    item.SubItems.Add(SongList.songList[i].Time);
                    lvSongList.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void 关闭ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            this.Close();
            Application.Exit();
        }

        private void 显示ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

            try
            {
                if (Play.play)
                {
                    this.pcPause.ImageLocation = "Images\\play_m.png";
                    player.Ctlcontrols.pause();
                    Play.play = false;
                }
                else
                {

                    this.pcPause.ImageLocation = "Images\\pause_m.png";
                    player.Ctlcontrols.play();
                    Play.play = true;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void pcNext_MouseHover(object sender, EventArgs e)
        {
            this.pcNext.ImageLocation = "Images\\next_m.png";

        }
        private void pcNext_MouseLeave(object sender, EventArgs e)
        {
            this.pcNext.ImageLocation = "Images\\next.png";
        }
        private void pcBack_MouseHover(object sender, EventArgs e)
        {
            this.pcBack.ImageLocation = "Images\\pre_m.png";
        }

        private void pcBack_MouseLeave(object sender, EventArgs e)
        {
            this.pcBack.ImageLocation = "Images\\pre.png";
        }
        Point start, end;
        private void FrmMain_MouseDown(object sender, MouseEventArgs e)
        {
            start = new Point(e.X, e.Y);//记录起点

        }

        private void FrmMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)//如果鼠标左键按下
            {
                //计算移动间距并移动窗体
                end = new Point(e.X, e.Y);//获取当前鼠标位置
                this.Top += end.Y - start.Y;//设置窗体与窗口上边距距离
                this.Left += end.X - start.X;//设置窗体与窗口左边距距离
            }
        }

        private void labelX2_Click(object sender, EventArgs e)
        {
            if (gechi.showLrc == false)
            {
                gechi.showLrc = true;
                FrmLry lry = new FrmLry();
                lry.Show();
                lblLrcs.ForeColor = Color.White;
            }
            else if (gechi.showLrc == true)
            {
                return;
            }

        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            //    添加歌曲
            //    打开文件夹(为debug下)
            this.openFileDialog1.InitialDirectory = Environment.CurrentDirectory + "\\song";
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //所有添加的歌曲
                string[] names = new string[500];
                names = this.openFileDialog1.FileNames;
                //每一首歌
                int num = this.openFileDialog1.FileNames.Count();
                for (int i = 0; i < num; i++)
                {
                    Song s = new Song();
                    //获取后缀名
                    string hou = Path.GetExtension(names[i]).ToLower();
                    //验证是否歌曲
                    if (hou != ".mp3")
                    {
                        num -= 1;
                        DialogResult result = MessageBox.Show("存在非歌曲格式，添加失败！！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        if (result == DialogResult.OK)
                        {
                            return;
                        }

                    }
                    string[] name = new string[2];
                    name = openFileDialog1.SafeFileNames[i].Split('.');
                    //从当前歌曲数后添加
                    string sFile = names[i];//获取文件名
                    ShellClass sh = new ShellClass();
                    Folder dir = sh.NameSpace(Path.GetDirectoryName(sFile));
                    FolderItem item = dir.ParseName(Path.GetFileName(sFile));
                    s.SongID = string.Format("{0}", Play.songNum + 1);
                    s.Title = name[0];
                    //this.lblSize.Text = dir.GetDetailsOf(item, 1);
                    s.Singer = dir.GetDetailsOf(item, 13);
                    //this.lblAlbum.Text = dir.GetDetailsOf(item, 12);
                    //this.lblTitle.Text = dir.GetDetailsOf(item, 21);
                    s.Type = "流行歌曲";
                    s.Time = dir.GetDetailsOf(item, 27).Substring(3); ;
                    s.Url = openFileDialog1.FileNames[i];
                    s.Love = 0;
                    SongList.songList[Play.songNum] = s;
                    Play.songNum += 1;
                }
                //更新列表
                ResetList();//从新加载                

            }
            Save.GetSave();//保存

        }
        public bool flag = false;
        private void pictureBox6_Click(object sender, EventArgs e)
        {
            if (flag)
            {
                pictureBox6.ImageLocation = "Images\\sound.png";
                player.settings.mute = false;
                flag = false;
            }
            else
            {
                pictureBox6.ImageLocation = "Images\\sound_m.png";
                player.settings.mute = true;
                flag = true;

            }
        }


        private void pictureBox5_Click(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                ctmBofang.Show();
            }

        }

        private void pictureBox9_Click(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                ctmBofang.Show();
            }
        }

        private void tmSilider_Tick(object sender, EventArgs e)
        {

            //加载项
            try
            {
                if (Play.play)
                {

                    //加载最大进度
                    this.scroJingdu.MyMaxValue = (int)this.player.currentMedia.duration;
                    lblTime2.Text = this.player.currentMedia.durationString;//显示总时间
                    string time = this.player.Ctlcontrols.currentPositionString;//显示时间
                    string timen = (this.player.Ctlcontrols.currentPosition + 1).ToString("F0");
                    this.lblTime.Text = time;//显示当前时间
                    gechi.TIME = time;
                   this.scroJingdu.MyStartValue = (int)this.player.Ctlcontrols.currentPosition;//进度 
                }
            }
            catch (Exception)
            {
                Play.play = false;
            }


        }

        private void Reback()
        {
            //单曲循环
            Play.play = true;
            player.URL = "Song\\" + lvSongList.Items[SongList.playIndex - 1].SubItems[1].Text + ".mp3";
            lblName.Text = lvSongList.Items[SongList.playIndex - 1].SubItems[1].Text;
            SongList.playIndex = int.Parse(lvSongList.Items[SongList.playIndex - 1].SubItems[0].Text);
            //歌词
            Save.SongName = lblName.Text;
            Play.SongLry = lblName.Text + ".lrc";
            try
            {
                Lrc lrc = new Lrc(Play.SongLry);
                gechi.lrcDictionary = new Dictionary<string, lrc>();
                for (int i = 0; i < lrc.LrcCollection.Count - 1; i++)
                {
                    gechi.lrcDictionary.Add(lrc.LrcCollection.ElementAt(i).Key, new lrc() { currlrc = lrc.LrcCollection.ElementAt(i).Value, nextlrc = lrc.LrcCollection.ElementAt(i + 1).Value });
                }
                gechi.lrcDictionary.Add(lrc.LrcCollection.ElementAt(lrc.LrcCollection.Count - 1).Key, new lrc() { currlrc = lrc.LrcCollection.ElementAt(lrc.LrcCollection.Count - 1).Value, nextlrc = "" });
                gechi.isLrc = true;
            }
            catch (Exception)
            {
                gechi.isLrc = false;
                gechi.lrcDictionary = null;
            }
        }

        private void srocVoice_ValueChange(object sender, EventArgs e)
        {
            player.settings.volume = srocVoice.MyValue;
            Save.Vioce = srocVoice.MyValue;
            if (srocVoice.MyValue == 0)
            {
                pictureBox6.ImageLocation = "Images\\sound_m.png";
            }
            else
            {
                pictureBox6.ImageLocation = "Images\\sound.png";
            }
            Save.GetSave();
        }

        private void pcBack_Click(object sender, EventArgs e)
        {
            Play.play = true;
            Back();//上一曲
        }

        private void Back()
        {
            SongList.playIndex -= 1;
            if (SongList.playIndex < 0)
            {
                SongList.playIndex = 0;
            }
            //播放，加载
            player.URL = SongList.songList[SongList.playIndex].Url;
            lblName.Text = lvSongList.Items[SongList.playIndex].SubItems[1].Text;
            Save.SongName = lblName.Text;
            //选中状态颜色
            for (int i = 0; i < lvSongList.Items.Count; i++)
            {
                lvSongList.Items[i].BackColor = Color.Azure;
            }
            lvSongList.Items[SongList.playIndex].BackColor = Color.MediumPurple;
            //加载歌手头像
            if (File.Exists("Singer\\" + SongList.songList[SongList.playIndex].Singer + ".jpg") == true)
            {
                pcStar.ImageLocation = "Singer\\" + SongList.songList[SongList.playIndex].Singer + ".jpg";
            }
            else
            {
                pcStar.ImageLocation = "Singer\\" + "logo.png";
            }
            //重置歌词
            label1.Text = "";
            label2.Text = "";
            label3.Text = "";
            label4.Text = "";
            label5.Text = "";
            label6.Text = "";
            label7.Text = "";
            LoadLrc();//加载歌词
        }

        private void pcNext_Click(object sender, EventArgs e)
        {
            Play.play = true;
            Next();//下一曲
        }

        private void Next()
        {

            try
            {
                SongList.playIndex += 1;
                if (SongList.playIndex >= lvSongList.Items.Count)
                {
                    SongList.playIndex = 0;
                }
                //播放
                player.URL = SongList.songList[SongList.playIndex].Url;
                //名字
                lblName.Text = lvSongList.Items[SongList.playIndex].SubItems[1].Text;
                Save.SongName = lblName.Text;
                //加载歌手头像
                if (File.Exists("Singer\\" + SongList.songList[SongList.playIndex].Singer + ".jpg") == true)
                {
                    pcStar.ImageLocation = "Singer\\" + SongList.songList[SongList.playIndex].Singer + ".jpg";
                }
                else
                {
                    pcStar.ImageLocation = "Singer\\" + "logo.png";
                }
                //选中状态颜色
                for (int i = 0; i < lvSongList.Items.Count; i++)
                {
                    lvSongList.Items[i].BackColor = Color.Azure;
                }
                lvSongList.Items[SongList.playIndex].BackColor = Color.MediumPurple;
                //歌词
                //初始加载进度
                scroJingdu.MyStartValue = scroJingdu.MyMinValue;
                //重置歌词
                label1.Text = "";
                label2.Text = "";
                label3.Text = "";
                label4.Text = "";
                label5.Text = "";
                label6.Text = "";
                label7.Text = "";
                LoadLrc();//加载歌词
            }
            catch (Exception)
            {
                throw;
            }

        }

        private void scroJingdu_ValueChange(object sender, EventArgs e)
        {
            player.Ctlcontrols.currentPosition = scroJingdu.MyValue;
            Play.play = true;


        }
        private void 清空列表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("确认清空列表？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
            if (result == DialogResult.Yes)
            {
                lvSongList.Items.Clear();

                for (int i = 0; i < Play.songNum; i++)
                {
                    Song s = new Song();
                    s = null;
                    SongList.songList[i] = s;
                }
                Play.songNum = 0;
                lblName.Text = "魅力音乐，美丽生活";
                player.Ctlcontrols.pause();
                Save.GetSave();
            }
            else
            {
                return;
            }
        }

        private void 打开文件夹ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //非空,获取选中
            if (lvSongList.Items.Count > 0)
            {
                ListViewItem select = lvSongList.SelectedItems[0];
                int num = int.Parse(select.SubItems[0].Text) - 1;
            }
        }

        private void 删除列表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //非空,获取选中
                if (lvSongList.SelectedItems.Count > 0)
                {
                    ListViewItem select = lvSongList.SelectedItems[0];

                    int num = int.Parse(select.Tag.ToString()) - 1;
                    DialogResult result = MessageBox.Show("确认删除？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        for (int i = 0; i < Play.songNum - 1; i++)
                        {
                            SongList.songList[num] = SongList.songList[num + 1];
                            num += 1;
                        }
                        Play.songNum -= 1;
                        for (int i = 0; i < Play.songNum; i++)
                        {
                            SongList.songList[i].SongID = (i + 1).ToString();
                        }
                        ResetList();//从新加载
                        Save.GetSave();
                    }
                    else
                    {
                        return;
                    }

                }
            }
            catch (Exception)
            {
                if (lvSongList.SelectedItems.Count > 0)
                {
                    //更新数组   
                    ListViewItem select = lvSongList.SelectedItems[0];
                    select.Remove();
                    for (int i = 0; i < Play.songNum - 1; i++)
                    {
                        lvSongList.Items[i].SubItems[0].Text = (i + 1).ToString();
                    }
                }
            }
        }

        private void ResetList()
        {
            //从新加载
            lvSongList.Items.Clear();
            for (int i = 0; i < Play.songNum; i++)
            {
                ListViewItem item = new ListViewItem(SongList.songList[i].SongID);
                item.Tag = SongList.songList[i].SongID;
                item.SubItems.Add(SongList.songList[i].Title);
                item.SubItems.Add(SongList.songList[i].Time);
                lvSongList.Items.Add(item);
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog2.InitialDirectory = Environment.CurrentDirectory + "\\背景";
                if (this.openFileDialog2.ShowDialog() == DialogResult.OK)
                {
                    this.BackgroundImage = Image.FromFile(openFileDialog2.FileName);
                    Save.FrmBackColor = ColorTranslator.ToHtml(Color.Transparent);
                    Save.FrmBack = openFileDialog2.FileName;
                }
                Save.GetSave();//保存
            }
            catch (Exception ex)
            {

                MessageBox.Show("文件不符合规范" + ex.Message);
            }
        }

        private void lvSongList_MouseDoubleClick_1(object sender, MouseEventArgs e)
        {
            //选项
            try
            {
                if (lvSongList.SelectedItems.Count > 0)
                {
                    Play.play = true;
                    ListViewItem select = lvSongList.SelectedItems[0];
                    SongList.playIndex = int.Parse(select.Tag.ToString()) - 1;
                    //播放歌曲
                    //获取歌曲名
                    PlayNow();//播放
                }
            }
            catch (Exception)
            {

            }

        }

        private void PlayNow()
        {
            lblName.Text = SongList.songList[SongList.playIndex].Title;
            player.URL = SongList.songList[SongList.playIndex].Url;
            Save.SongName = lblName.Text;
            //进度
            //加载最大进度
            this.scroJingdu.MyStartValue = 0;//进度
            //选中状态颜色
            for (int i = 0; i < lvSongList.Items.Count; i++)
            {
                lvSongList.Items[i].BackColor = Color.Azure;
            }
            for (int i = 0; i < lvSongList.Items.Count; i++)
            {
                int index = int.Parse(lvSongList.Items[i].Tag.ToString());
                if (index == SongList.playIndex)
                {
                    lvSongList.Items[i + 1].BackColor = Color.MediumPurple;
                }
            }
            //重置歌词
            label1.Text = "";
            label2.Text = "";
            label3.Text = "";
            label4.Text = "";
            label5.Text = "";
            label6.Text = "";
            label7.Text = "";
            //加载歌手头像
            if (File.Exists("Singer\\" + SongList.songList[SongList.playIndex].Singer + ".jpg") == true)
            {
                pcStar.ImageLocation = "Singer\\" + SongList.songList[SongList.playIndex].Singer + ".jpg";
            }
            else
            {
                pcStar.ImageLocation = "Singer\\" + "logo.png";
            }
            //播放状态
            if (Play.play)
            {
                this.pcPause.ImageLocation = "Images\\pause_m.png";
            }
            else
            {
                this.pcPause.ImageLocation = "Images\\play.png";
            }
            LoadLrc();//加载歌词
        }

        private static void LoadLrc()
        {
            //加载歌词
            //初始歌词
            Play.SongLry = SongList.songList[SongList.playIndex].Title + ".lrc";
            if (File.Exists("Song\\" + Play.SongLry) == true)
            {
                try
                {
                    Lrc lrc = new Lrc(Play.SongLry);
                    gechi.lrcDictionary = new Dictionary<string, lrc>();
                    for (int i = 0; i < lrc.LrcCollection.Count - 3; i++)
                    {
                        //当前
                        gechi.lrcDictionary.Add(lrc.LrcCollection.ElementAt(i).Key, new lrc() { currlrc = lrc.LrcCollection.ElementAt(i).Value, nextlrc = lrc.LrcCollection.ElementAt(i + 1).Value, nextlrc2 = lrc.LrcCollection.ElementAt(i + 2).Value, nextlrc3 = lrc.LrcCollection.ElementAt(i + 3).Value });
                    }
                    //下一句
                    gechi.lrcDictionary.Add(lrc.LrcCollection.ElementAt(lrc.LrcCollection.Count - 1).Key, new lrc() { currlrc = lrc.LrcCollection.ElementAt(lrc.LrcCollection.Count - 1).Value, nextlrc = "" });
                    //下下一句
                    gechi.lrcDictionary.Add(lrc.LrcCollection.ElementAt(lrc.LrcCollection.Count - 2).Key, new lrc() { currlrc = lrc.LrcCollection.ElementAt(lrc.LrcCollection.Count - 2).Value, nextlrc2 = "" });
                    //下下下一句
                    gechi.lrcDictionary.Add(lrc.LrcCollection.ElementAt(lrc.LrcCollection.Count - 3).Key, new lrc() { currlrc = lrc.LrcCollection.ElementAt(lrc.LrcCollection.Count - 3).Value, nextlrc3 = "" });
                    gechi.isLrc = true;
                }
                catch (Exception)
                {
                    gechi.isLrc = false;
                    gechi.lrcDictionary = null;
                }
            }
            else
            {
                gechi.isLrc = false;
            }
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                this.Visible = true;
            }
        }

        private void pictureBox8_MouseHover(object sender, EventArgs e)
        {
            pictureBox8.ImageLocation = "Images//close_m.png";
        }

        private void pictureBox8_MouseLeave(object sender, EventArgs e)
        {
            pictureBox8.ImageLocation = "Images//close.png";
        }
        bool show = false;
        int time = 0;
        private void pictureBox6_MouseHover(object sender, EventArgs e)
        {
            time = 0;
            tmVioce.Start();
            srocVoice.Visible = true;
            show = true;

        }

        private void srocVoice_MouseLeave(object sender, EventArgs e)
        {
            srocVoice.Visible = false;
            show = false;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (show == true)
            {
                time += 1;
                if (time == 3)
                {
                    srocVoice.Visible = false;
                    show = false;
                    tmVioce.Stop();
                }
            }
        }

        private void srocVoice_MouseHover(object sender, EventArgs e)
        {
            tmVioce.Stop();
            srocVoice.Visible = true;
            show = true;
        }
        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Save.GetSave();//保存方法
        }
        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(int Description, int ReservedValue);

        #region 方法一

        /// <summary>
        /// 用于检查网络是否可以连接互联网,true表示连接成功,false表示连接失败 
        /// </summary>
        /// <returns></returns>
        /// 
        public static bool IsConnectInternet()
        {
            int Description = 0;
            return InternetGetConnectedState(Description, 0);
        }
        #endregion

        private void labelX4_Click(object sender, EventArgs e)
        {
            //选中状态
            lblDChi.BackgroundImage = Image.FromFile("Images\\背景选择.png");
            lblChi.BackgroundImage = Image.FromFile("Images\\space.png");
            lblDMusic.BackgroundImage = Image.FromFile("Images\\space.png");
            lblDMV.BackgroundImage = Image.FromFile("Images\\space.png");
            //加载界面
            panWeb.Visible = true;
            panLry.Visible = false;
            if (IsConnectInternet())
            {
                webBrow.Navigate("http://music.baidu.com/");
            }
            else
            {
                webBrow.Navigate("file:///D:/MeiLiMusic/MeiliMusic.html");
            }

        }


        private void lblChi_Click(object sender, EventArgs e)
        {
            //选中状态
            lblChi.BackgroundImage = Image.FromFile("Images\\背景选择.png");
            lblDChi.BackgroundImage = Image.FromFile("Images\\space.png");
            lblDMusic.BackgroundImage = Image.FromFile("Images\\space.png");
            lblDMV.BackgroundImage = Image.FromFile("Images\\space.png");
            //加载歌词界面
            panLry.Visible = true;
            panWeb.Visible = false;

        }

        private void lblDMusic_Click(object sender, EventArgs e)
        {
            //选中状态
            lblChi.BackgroundImage = Image.FromFile("Images\\space.png");
            lblDChi.BackgroundImage = Image.FromFile("Images\\space.png");
            lblDMusic.BackgroundImage = Image.FromFile("Images\\背景选择.png");
            lblDMV.BackgroundImage = Image.FromFile("Images\\space.png");
            //加载界面
            panWeb.Visible = true;
            panLry.Visible = false;
            //加载网址
            if (IsConnectInternet())
            {
                webBrow.Navigate("http://music.baidu.com/");
            }
            else
            {
                webBrow.Navigate("file:///D:/MeiLiMusic/MeiliMusic.html");
            }


        }

        private void lblDMV_Click(object sender, EventArgs e)
        {
            //选中状态
            lblChi.BackgroundImage = Image.FromFile("Images\\space.png");
            lblDChi.BackgroundImage = Image.FromFile("Images\\space.png");
            lblDMusic.BackgroundImage = Image.FromFile("Images\\space.png");
            lblDMV.BackgroundImage = Image.FromFile("Images\\背景选择.png");
            //加载界面
            panWeb.Visible = true;
            panLry.Visible = false;
            //加载网址
            if (IsConnectInternet())
            {
                webBrow.Navigate("http://music.baidu.com/");
            }
            else
            {
                webBrow.Navigate("file:///D:/MeiLiMusic/MeiliMusic.html");
            }
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            //选中状态
            pcLove.BackColor = Color.Transparent;
            pcBendi.BackColor = Color.LightBlue;
            //加载
            ResetList();//从新加载 

        }

        private void pcLove_Click(object sender, EventArgs e)
        {
            //选中状态
            pcBendi.BackColor = Color.Transparent;
            pcLove.BackColor = Color.LightBlue;
            //加载列表
            lvSongList.Items.Clear();
            for (int i = 0; i < Play.songNum; i++)
            {
                if (SongList.songList[i].Love == 1)
                {
                    ListViewItem item = new ListViewItem(SongList.songList[i].SongID);
                    item.Tag = SongList.songList[i].SongID;
                    item.SubItems.Add(SongList.songList[i].Title);
                    item.SubItems.Add(SongList.songList[i].Time);
                    lvSongList.Items.Add(item);
                }
            }
            //下标初始化
            for (int i = 0; i < lvSongList.Items.Count; i++)
            {
                lvSongList.Items[i].SubItems[0].Text = (i + 1).ToString();
            }
        }

        private void lblMusic_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.Width == 330)
            {
                this.Width = 880;
                pictureBox10.Visible = false;
                pictureBox8.Visible = false;
                pictureBox9.Visible = false;
            }
            else if (this.Width == 880)
            {
                this.Width = 330;
                pictureBox10.Visible = true;
                pictureBox8.Visible = true;
                pictureBox9.Visible = true;
            }
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {

            try
            {
                if (player.playState == WMPLib.WMPPlayState.wmppsStopped && Play.play == true)
                {
                    this.lblName.Text = lvSongList.Items[SongList.playIndex].SubItems[1].Text;
                    if (Play.SongMS == SongMS.单曲播放)
                    {
                        player.Ctlcontrols.stop();
                        Play.play = false;
                    }
                    else if (Play.SongMS == SongMS.单曲循环)
                    {
                        PlayNow();//播放;
                    }
                    else if (Play.SongMS == SongMS.列表循环)
                    {
                        Next();
                    }
                    else if (Play.SongMS == SongMS.随机播放)
                    {
                        SongList.playIndex = new Random().Next(Play.songNum);
                        PlayNow();
                    }
                    else if (Play.SongMS == SongMS.顺序播放)
                    {
                        if (SongList.playIndex > lvSongList.Items.Count - 1)
                        {
                            player.Ctlcontrols.stop();
                            Play.play = false;
                            return;
                        }
                        Next();

                    }
                }
            }
            catch (Exception)
            {
                for (int i = 0; i < Play.songNum; i++)
                {
                    if (SongList.songList[i].Love==1)
                    {
                        SongList.playIndex = int.Parse(lvSongList.Items[i].Tag.ToString());
                    }
                }
                
            }
        }
        private void 列表循环ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Play.SongMS = SongMS.列表循环;
            Save.SaveSongMS = SongMS.列表循环;
            Play.play = true;
            Save.GetSave();
        }

        private void 单曲循环ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Play.SongMS = SongMS.单曲循环;
            Save.SaveSongMS = SongMS.单曲循环;
            Play.play = true;
            Save.GetSave();
        }

        private void 随机播放ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Play.SongMS = SongMS.随机播放;
            Save.SaveSongMS = SongMS.随机播放;
            Play.play = true;
            Save.GetSave();
        }

        private void 顺序播放ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Play.SongMS = SongMS.顺序播放;
            Save.SaveSongMS = SongMS.顺序播放;
            Play.play = true;
            Save.GetSave();
        }

        private void 单曲播放ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Play.SongMS = SongMS.单曲播放;
            Save.SaveSongMS = SongMS.单曲播放;
            Play.play = true;
            Save.GetSave();
        }

        private void 添加到喜爱ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvSongList.SelectedItems.Count > 0)
            {
                ListViewItem select = lvSongList.SelectedItems[0];
                int index = int.Parse(select.Tag.ToString()) - 1;
                SongList.songList[index].Love = 1;
            }
            Save.GetSave();
        }

        private void pcSearch_Click(object sender, EventArgs e)
        {
            try
            {
                lvSongList.Items.Clear();
                string str = txtSearch.Text;
                for (int i = 0; i < Play.songNum; i++)
                {
                    if (SongList.songList[i].Title.IndexOf(str) >= 0)
                    {
                        ListViewItem item = new ListViewItem(SongList.songList[i].SongID);
                        item.Tag = SongList.songList[i].SongID;
                        item.SubItems.Add(SongList.songList[i].Title);
                        item.SubItems.Add(SongList.songList[i].Time);
                        lvSongList.Items.Add(item);
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void lvSongList_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.All;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void lvSongList_DragDrop(object sender, DragEventArgs e)
        {
            string[] names = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            //所有添加的歌曲
            //每一首歌
            int num = names.Length;
            for (int i = 0; i < names.Length; i++)
            {
                Song song = new Song();
                //获取后缀名
                string hou = Path.GetExtension(names[i]).ToLower();
                //验证是否歌曲
                if (hou != ".mp3")
                {
                    num -= 1;
                    DialogResult result = MessageBox.Show("存在非歌曲格式，添加失败！！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    if (result == DialogResult.OK)
                    {
                        return;
                    }

                }
                string[] name = new string[10];
                name = names[i].Split('\\');
                string title = "";
                for (int j = 0; j < name.Length; j++)
                {
                    if (name[j].IndexOf("mp3") > 0)
                    {
                        string[] stt = new string[2];
                        stt = name[j].Split('.');
                        title = stt[0];

                    }
                }
                //从当前歌曲数后添加
                string sFile = names[i];//获取文件名
                ShellClass sh = new ShellClass();
                Folder dir = sh.NameSpace(Path.GetDirectoryName(sFile));
                FolderItem item = dir.ParseName(Path.GetFileName(sFile));
                song.SongID = string.Format("{0}", Play.songNum + 1);
                song.Title = title;
                //this.lblSize.Text = dir.GetDetailsOf(item, 1);
                song.Singer = dir.GetDetailsOf(item, 13);
                //this.lblAlbum.Text = dir.GetDetailsOf(item, 12);
                //this.lblTitle.Text = dir.GetDetailsOf(item, 21);
                song.Type = "流行歌曲";
                song.Time = dir.GetDetailsOf(item, 27).Substring(3); ;
                song.Url = names[i];
                song.Love = 0;
                SongList.songList[Play.songNum] = song;
                Play.songNum += 1;
            }
            ResetList();//从新加载                
            Save.GetSave();//保存    
        }
        int xx = 445;
        int[] y = { 178, 236, 294, 352, 410, 468, 526 };
        private void tmchi_Tick(object sender, EventArgs e)
        {
            try
            {

                //歌词
                if (gechi.isLrc == true)
                {

                    var lrc =
                    from n in gechi.lrcDictionary
                    where n.Key.Contains(gechi.TIME)
                    select n;
                    if (lrc.Count() > 0)
                    {
                        for (int i = 0; i < 7; i++)
                        {
                            y[i] -= 58;
                            //7排
                            if (y[i] == 120)
                            {
                                #region 526
                                y[i] = 526;
                                string nextlrc3 = lrc.First().Value.nextlrc3;
                                if (i == 0)
                                {
                                    label1.Text = nextlrc3;
                                }
                                else if (i == 1)
                                {
                                    label2.Text = nextlrc3;
                                }
                                else if (i == 2)
                                {
                                    label3.Text = nextlrc3;
                                }
                                else if (i == 3)
                                {
                                    label4.Text = nextlrc3;
                                }
                                else if (i == 4)
                                {

                                    label5.Text = nextlrc3;
                                }
                                else if (i == 5)
                                {
                                    label6.Text = nextlrc3;
                                }
                                else if (i == 6)
                                {
                                    label7.Text = nextlrc3;
                                }
                                #endregion 526
                            }
                            //4排
                            else if (y[i] == 352)
                            {
                                #region 352
                                string currlrc = lrc.First().Value.currlrc;
                                if (i == 0)
                                {

                                    label1.ForeColor = System.Drawing.Color.FromName(Save.lrcColor);
                                    label1.Text = currlrc;
                                    label1.Font = new Font(Save.lrcFont, 15, FontStyle.Bold);
                                    label2.Font = new Font(Save.lrcFont, 12, FontStyle.Regular);
                                    label3.Font = new Font(Save.lrcFont, 12, FontStyle.Regular);
                                    label4.Font = new Font(Save.lrcFont, 12, FontStyle.Regular);
                                    label5.Font = new Font(Save.lrcFont, 12, FontStyle.Regular);
                                    label6.Font = new Font(Save.lrcFont, 12, FontStyle.Regular);
                                    label7.Font = new Font(Save.lrcFont, 12, FontStyle.Regular);
                                    label2.ForeColor = Color.WhiteSmoke;
                                    label3.ForeColor = Color.WhiteSmoke;
                                    label4.ForeColor = Color.WhiteSmoke;
                                    label5.ForeColor = Color.WhiteSmoke;
                                    label6.ForeColor = Color.WhiteSmoke;
                                    label7.ForeColor = Color.WhiteSmoke;
                                }
                                else if (i == 1)
                                {
                                    label1.ForeColor = Color.WhiteSmoke;
                                    label2.ForeColor = System.Drawing.Color.FromName(Save.lrcColor);
                                    label2.Text = currlrc;
                                    label2.Font = new Font(Save.lrcFont, 15, FontStyle.Bold);
                                    label1.Font = new Font(Save.lrcFont, 12, FontStyle.Regular);
                                    label3.Font = new Font(Save.lrcFont, 12, FontStyle.Regular);
                                    label4.Font = new Font(Save.lrcFont, 12, FontStyle.Regular);
                                    label5.Font = new Font(Save.lrcFont, 12, FontStyle.Regular);
                                    label6.Font = new Font(Save.lrcFont, 12, FontStyle.Regular);
                                    label7.Font = new Font(Save.lrcFont, 12, FontStyle.Regular);
                                    label3.ForeColor = Color.WhiteSmoke;
                                    label4.ForeColor = Color.WhiteSmoke;
                                    label5.ForeColor = Color.WhiteSmoke;
                                    label6.ForeColor = Color.WhiteSmoke;
                                    label7.ForeColor = Color.WhiteSmoke;
                                }
                                else if (i == 2)
                                {
                                    label1.ForeColor = Color.WhiteSmoke;
                                    label2.ForeColor = Color.WhiteSmoke;
                                    label3.ForeColor = System.Drawing.Color.FromName(Save.lrcColor);
                                    label3.Text = currlrc;
                                    label3.Font = new Font(Save.lrcFont, 15, FontStyle.Bold);
                                    label1.Font = new Font(Save.lrcFont, 12, FontStyle.Regular);
                                    label2.Font = new Font(Save.lrcFont, 12, FontStyle.Regular);
                                    label4.Font = new Font(Save.lrcFont, 12, FontStyle.Regular);
                                    label5.Font = new Font(Save.lrcFont, 12, FontStyle.Regular);
                                    label6.Font = new Font(Save.lrcFont, 12, FontStyle.Regular);
                                    label7.Font = new Font(Save.lrcFont, 12, FontStyle.Regular);
                                    label4.ForeColor = Color.WhiteSmoke;
                                    label5.ForeColor = Color.WhiteSmoke;
                                    label6.ForeColor = Color.WhiteSmoke;
                                    label7.ForeColor = Color.WhiteSmoke;
                                }
                                else if (i == 3)
                                {

                                    label1.ForeColor = Color.WhiteSmoke;
                                    label2.ForeColor = Color.WhiteSmoke;
                                    label3.ForeColor = Color.WhiteSmoke;
                                    label4.ForeColor = System.Drawing.Color.FromName(Save.lrcColor);
                                    label4.Text = currlrc;
                                    label4.Font = new Font(Save.lrcFont, 15, FontStyle.Bold);
                                    label1.Font = new Font(Save.lrcFont, 12, FontStyle.Regular);
                                    label2.Font = new Font(Save.lrcFont, 12, FontStyle.Regular);
                                    label3.Font = new Font(Save.lrcFont, 12, FontStyle.Regular);
                                    label5.Font = new Font(Save.lrcFont, 12, FontStyle.Regular);
                                    label6.Font = new Font(Save.lrcFont, 12, FontStyle.Regular);
                                    label7.Font = new Font(Save.lrcFont, 12, FontStyle.Regular);
                                    label5.ForeColor = Color.WhiteSmoke;
                                    label6.ForeColor = Color.WhiteSmoke;
                                    label7.ForeColor = Color.WhiteSmoke;
                                }
                                else if (i == 4)
                                {
                                    label1.ForeColor = Color.WhiteSmoke;
                                    label2.ForeColor = Color.WhiteSmoke;
                                    label3.ForeColor = Color.WhiteSmoke;
                                    label4.ForeColor = Color.WhiteSmoke;
                                    label5.ForeColor = System.Drawing.Color.FromName(Save.lrcColor);
                                    label5.Text = currlrc;
                                    label5.Font = new Font(Save.lrcFont, 15, FontStyle.Bold);
                                    label1.Font = new Font(Save.lrcFont, 12, FontStyle.Regular);
                                    label2.Font = new Font(Save.lrcFont, 12, FontStyle.Regular);
                                    label3.Font = new Font(Save.lrcFont, 12, FontStyle.Regular);
                                    label4.Font = new Font(Save.lrcFont, 12, FontStyle.Regular);
                                    label6.Font = new Font(Save.lrcFont, 12, FontStyle.Regular);
                                    label7.Font = new Font(Save.lrcFont, 12, FontStyle.Regular);
                                    label6.ForeColor = Color.WhiteSmoke;
                                    label7.ForeColor = Color.WhiteSmoke;
                                }
                                else if (i == 5)
                                {
                                    label1.ForeColor = Color.WhiteSmoke;
                                    label2.ForeColor = Color.WhiteSmoke;
                                    label3.ForeColor = Color.WhiteSmoke;
                                    label4.ForeColor = Color.WhiteSmoke;
                                    label5.ForeColor = Color.WhiteSmoke;
                                    label6.ForeColor = System.Drawing.Color.FromName(Save.lrcColor);
                                    label6.Text = currlrc;
                                    label6.Font = new Font(Save.lrcFont, 15, FontStyle.Bold);
                                    label2.Font = new Font(Save.lrcFont, 12, FontStyle.Regular);
                                    label3.Font = new Font(Save.lrcFont, 12, FontStyle.Regular);
                                    label4.Font = new Font(Save.lrcFont, 12, FontStyle.Regular);
                                    label5.Font = new Font(Save.lrcFont, 12, FontStyle.Regular);
                                    label7.Font = new Font(Save.lrcFont, 12, FontStyle.Regular);
                                    label1.Font = new Font(Save.lrcFont, 12, FontStyle.Regular);
                                    label7.ForeColor = Color.WhiteSmoke;
                                }
                                else if (i == 6)
                                {
                                    label1.ForeColor = Color.WhiteSmoke;
                                    label2.ForeColor = Color.WhiteSmoke;
                                    label3.ForeColor = Color.WhiteSmoke;
                                    label4.ForeColor = Color.WhiteSmoke;
                                    label5.ForeColor = Color.WhiteSmoke;
                                    label6.ForeColor = Color.WhiteSmoke;
                                    label7.ForeColor = System.Drawing.Color.FromName(Save.lrcColor);
                                    label7.Text = currlrc;
                                    label7.Font = new Font(Save.lrcFont, 15, FontStyle.Bold);
                                    label2.Font = new Font(Save.lrcFont, 12, FontStyle.Regular);
                                    label3.Font = new Font(Save.lrcFont, 12, FontStyle.Regular);
                                    label4.Font = new Font(Save.lrcFont, 12, FontStyle.Regular);
                                    label5.Font = new Font(Save.lrcFont, 12, FontStyle.Regular);
                                    label6.Font = new Font(Save.lrcFont, 12, FontStyle.Regular);
                                    label1.Font = new Font(Save.lrcFont, 12, FontStyle.Regular);
                                }
                                #endregion 352
                            }
                            //6排
                            else if (y[i] == 468)
                            {
                                #region 468
                                string nextlrc2 = lrc.First().Value.nextlrc2;
                                if (i == 0)
                                {
                                    label1.Text = nextlrc2;
                                }
                                else if (i == 1)
                                {
                                    label2.Text = nextlrc2;
                                }
                                else if (i == 2)
                                {
                                    label3.Text = nextlrc2;
                                }
                                else if (i == 3)
                                {
                                    label4.Text = nextlrc2;
                                }
                                else if (i == 4)
                                {

                                    label5.Text = nextlrc2;
                                }
                                else if (i == 5)
                                {
                                    label6.Text = nextlrc2;
                                }
                                else if (i == 6)
                                {
                                    label7.Text = nextlrc2;
                                }
                                #endregion 526
                            }
                            //4排
                            else if (y[i] == 410)
                            {
                                #region 410
                                string nextlrc = lrc.First().Value.nextlrc;
                                if (i == 0)
                                {
                                    label1.Text = nextlrc;
                                }
                                else if (i == 1)
                                {
                                    label2.Text = nextlrc;
                                }
                                else if (i == 2)
                                {
                                    label3.Text = nextlrc;
                                }
                                else if (i == 3)
                                {
                                    label4.Text = nextlrc;
                                }
                                else if (i == 4)
                                {

                                    label5.Text = nextlrc;
                                }
                                else if (i == 5)
                                {
                                    label6.Text = nextlrc;
                                }
                                else if (i == 6)
                                {
                                    label7.Text = nextlrc;
                                }
                                #endregion 526
                            }
                        }
                        label1.Location = new Point(xx, y[0]);
                        label2.Location = new Point(xx, y[1]);
                        label3.Location = new Point(xx, y[2]);
                        label4.Location = new Point(xx, y[3]);
                        label5.Location = new Point(xx, y[4]);
                        label6.Location = new Point(xx, y[5]);
                        label7.Location = new Point(xx, y[6]);

                    }
                }
                else
                {
                    label1.Text = "";
                    label2.Text = "";
                    label3.Text = "";
                    label4.Text = "魅力音乐，你的魅力";
                    label5.Text = "";
                    label6.Text = "";
                    label7.Text = "";
                }
            }
            catch (Exception)
            {

                gechi.isLrc = false;
            }


        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Width = 330;
            this.Height = 160;
            txtSearch.Visible = false;
            pcSearch.Visible = false;
            pictureBox13.Visible = true;
            lblMusic.Visible = false;
            pictureBox10.Visible = false;
            pictureBox9.Visible = false;

        }

        private void pictureBox13_Click_1(object sender, EventArgs e)
        {
            this.Width = 330;
            this.Height = 642;
            txtSearch.Visible = true;
            pcSearch.Visible = true;
            pictureBox13.Visible = false;
            pictureBox10.Visible = true;
            pictureBox8.Visible = true;
            pictureBox9.Visible = true;
            lblMusic.Visible = true;
        }

        private void pictureBox4_Click_1(object sender, EventArgs e)
        {

            if (panShijian.Visible == false)
            {
                panShijian.Visible = true;
            }
            else
            {
                panShijian.Visible = false;
            }
            for (int i = 0; i < 24; i++)
            {
                if (i < 10)
                {
                    cmbHH.Items.Add("0" + i);
                }
                else
                {
                    cmbHH.Items.Add(i);
                }

            }

            for (int i = 0; i < 60; i++)
            {
                if (i < 10)
                {
                    cmbMM.Items.Add("0" + i);
                }
                else
                {
                    cmbMM.Items.Add(i);
                }

            }
            cmbHH.Text = DateTime.Now.ToString("HH");
            cmbMM.Text = DateTime.Now.ToString("mm");
        }
        bool timess = false;
        string HH = "", mm = "";
        private void btOK_Click(object sender, EventArgs e)
        {
            if (timess==false)
            {
                HH=cmbHH.Text;
                mm = cmbMM.Text;
                panShijian.Visible = false;
                btOK.Text = "已设定";
                timess = true;
            }
            else if (timess)
            {
                btOK.Text = "设定";
                timess = false;
            }
            
        }

        private void btNO_Click(object sender, EventArgs e)
        {
            panShijian.Visible = false;
            timess = false;
        }

        private void timer1_Tick_2(object sender, EventArgs e)
        {
            lblNow.Text = DateTime.Now.ToString("HH:mm:ss");
            if (DateTime.Now.ToString("HH") == HH && DateTime.Now.ToString("mm") == mm && timess)
            {
                Application.Exit();
            }
            if (close.min == true)
            {
                this.Hide();
                close.min = false;
            }
        }

        private void 天空蓝ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.DodgerBlue;
            this.BackgroundImage = Image.FromFile("Images\\space.png");
            Save.FrmBack = "Images\\space.png";
            Save.FrmBackColor = ColorTranslator.ToHtml(this.BackColor);
            Save.GetSave();//保存
        }

        private void 夕阳红ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.Tomato;
            this.BackgroundImage = Image.FromFile("Images\\space.png");
            Save.FrmBack = "Images\\space.png";
            Save.FrmBackColor = ColorTranslator.ToHtml(this.BackColor);
            Save.GetSave();//保存
        }

        private void 魅力紫ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.MediumPurple;
            this.BackgroundImage = Image.FromFile("Images\\space.png");
            Save.FrmBack = "Images\\space.png";
            Save.FrmBackColor = ColorTranslator.ToHtml(this.BackColor);
            Save.GetSave();//保存
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            Save.lrcColor = ColorTranslator.ToHtml(Color.OrangeRed);
            Save.GetSave();//保存
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            Save.lrcColor = ColorTranslator.ToHtml(Color.GreenYellow);
            Save.GetSave();//保存
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            Save.lrcColor = ColorTranslator.ToHtml(Color.MediumPurple);
            Save.GetSave();//保存
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            Save.lrcColor = ColorTranslator.ToHtml(Color.Yellow);
            Save.GetSave();//保存
        }

        private void toolStripMenuItem12_Click(object sender, EventArgs e)
        {
            Save.lrcColor = ColorTranslator.ToHtml(Color.DeepSkyBlue);
            Save.GetSave();//保存
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            Save.lrcFont = "微软雅黑";
            Save.GetSave();//保存
        }

        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {
            Save.lrcFont = "华文新魏";
            Save.GetSave();//保存
        }

        private void toolStripMenuItem11_Click(object sender, EventArgs e)
        {
            Save.lrcFont = "楷体";
            Save.GetSave();//保存
        }

        private void panel1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Width=880;
            this.Height = 642;
            lblMusic.Visible = true;
            pictureBox13.Visible = false;
        }

        private void toolStripMenuItem13_Click(object sender, EventArgs e)
        {
            Save.close =1;
        }

        private void toolStripMenuItem14_Click(object sender, EventArgs e)
        {
            Save.close = 0;
        }

        private void toolStripMenuItem15_Click(object sender, EventArgs e)
        {
            Save.close = 2;
        }
    }
}
