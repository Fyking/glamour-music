using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 魅力
{
    public partial class FrmLry : Form
    {
        public FrmLry()
        {
            InitializeComponent();
        }
        Point start, end;
        private void panel3_MouseDown(object sender, MouseEventArgs e)
        {
            start = new Point(e.X, e.Y);//记录起点
        }

        private void panel3_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)//如果鼠标左键按下
            {
                //计算移动间距并移动窗体
                end = new Point(e.X, e.Y);//获取当前鼠标位置
                this.Top += end.Y - start.Y;//设置窗体与窗口上边距距离
                this.Left += end.X - start.X;//设置窗体与窗口左边距距离
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            gechi.showLrc = false;
            this.Close();
        }

        private void panel3_MouseHover(object sender, EventArgs e)
        {
            this.panel3.BackColor = Color.SkyBlue;
        }

        private void panel3_MouseLeave(object sender, EventArgs e)
        {
            this.panel3.BackColor = Color.Transparent;

        }

        private void FrmLry_Load(object sender, EventArgs e)
        {
            this.Top = 644;//设置窗体与窗口上边距距离
            this.Left = 306;//设置窗体与窗口左边距距离
            cmbColor.Items.Add("Green");
            cmbColor.Items.Add("LightPink");
            cmbColor.Items.Add("HotPink");
            cmbColor.Items.Add("YellowGreen");
            cmbColor.Items.Add("PowderBlue");
            cmbColor.Items.Add("OrangeRed");
            cmbColor.Items.Add("Yellow");
            cmbColor.Items.Add("Orange");
            cmbColor.Items.Add("MintCream");
            cmbColor.Items.Add("SkyBlue");
            lblLry.ForeColor = System.Drawing.Color.FromName(Save.LryColor);
            lblLry2.ForeColor = System.Drawing.Color.FromName(Save.LryColor);
        }
        bool flags = true;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (gechi.isLrc == true)
            {
                var lrc =
                    from n in gechi.lrcDictionary
                    where n.Key.Contains(gechi.TIME)
                    select n;
                if (lrc.Count() > 0)
                {
                    string currlrc = lrc.First().Value.currlrc;
                    if (flags)
                    {
                        lblLry.Text = currlrc;
                        flags = false;
                    }
                    else
                    {
                        lblLry2.Text = currlrc;
                        flags = true;

                    }
                }
                if (Play.play == false)
                {
                    lblLry.Text = "魅力音乐！！";
                    lblLry2.Text = "你的魅力.......";
                }
            }
            else
            {
                lblLry.Text = "魅力音乐！！";
                lblLry2.Text = "你的魅力.......";
            }
        }
        bool show = false;
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (show == false)
            {
                show = true;
                label1.Visible = true;
                cmbColor.Visible = true;
            }
            else
            {
                show = false;
                label1.Visible = false;
                cmbColor.Visible = false;
            }
        }

        private void cmbColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbColor.Text == "Green")
            {
                lblLry.ForeColor = Color.Green;
                lblLry2.ForeColor = Color.Green;
                Save.LryColor = ColorTranslator.ToHtml(lblLry.ForeColor);
            }
            if (cmbColor.Text == "LightPink")
            {
                lblLry.ForeColor = Color.LightPink;
                lblLry2.ForeColor = Color.LightPink;
            }
            if (cmbColor.Text == "HotPink")
            {
                lblLry.ForeColor = Color.HotPink;
                lblLry2.ForeColor = Color.HotPink;
                Save.LryColor = ColorTranslator.ToHtml(lblLry.ForeColor);

            }
                if (cmbColor.Text == "LightPink")
                {
                    lblLry.ForeColor = Color.LightPink;
                    lblLry2.ForeColor = Color.LightPink;
                    Save.LryColor = ColorTranslator.ToHtml(lblLry.ForeColor);
                }
                if (cmbColor.Text == "YellowGreen")
                {
                    lblLry.ForeColor = Color.YellowGreen;
                    lblLry2.ForeColor = Color.YellowGreen;
                    Save.LryColor = ColorTranslator.ToHtml(lblLry.ForeColor);
                }
                if (cmbColor.Text == "PowderBlue")
                {
                    lblLry.ForeColor = Color.PowderBlue;
                    lblLry2.ForeColor = Color.PowderBlue;
                    Save.LryColor = ColorTranslator.ToHtml(lblLry.ForeColor);
                }
                if (cmbColor.Text == "OrangeRed")
                {
                    lblLry.ForeColor = Color.OrangeRed;
                    lblLry2.ForeColor = Color.OrangeRed;
                    Save.LryColor = ColorTranslator.ToHtml(lblLry.ForeColor);
                }
                if (cmbColor.Text == "Yellow")
                {
                    lblLry.ForeColor = Color.Yellow;
                    lblLry2.ForeColor = Color.Yellow;
                    Save.LryColor = ColorTranslator.ToHtml(lblLry.ForeColor);
                }
                if (cmbColor.Text == "Orange")
                {
                    lblLry.ForeColor = Color.Orange;
                    lblLry2.ForeColor = Color.Orange;
                    Save.LryColor = ColorTranslator.ToHtml(lblLry.ForeColor);
                }
                if (cmbColor.Text == "MintCream")
                {
                    lblLry.ForeColor = Color.MintCream;
                    lblLry2.ForeColor = Color.MintCream;
                    Save.LryColor = ColorTranslator.ToHtml(lblLry.ForeColor);
                }
                if (cmbColor.Text == "SkyBlue")
                {
                    lblLry.ForeColor = Color.SkyBlue;
                    lblLry2.ForeColor = Color.SkyBlue;
                    Save.LryColor = ColorTranslator.ToHtml(lblLry.ForeColor);
                }
                Save.GetSave();//保存
            }

        private void cmbColor_TextChanged(object sender, EventArgs e)
        {
            label1.Visible = false;
            cmbColor.Visible = false;
        }

        }
    }


