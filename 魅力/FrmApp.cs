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
    public partial class FrmApp : Form
    {
        public FrmApp()
        {
            InitializeComponent();
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        Point start, end;
        private void FrmTime_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)//如果鼠标左键按下
            {
                //计算移动间距并移动窗体
                end = new Point(e.X, e.Y);//获取当前鼠标位置
                this.Top += end.Y - start.Y;//设置窗体与窗口上边距距离
                this.Left += end.X - start.X;//设置窗体与窗口左边距距离
            }
        }

        private void FrmTime_MouseDown(object sender, MouseEventArgs e)
        {
            start = new Point(e.X, e.Y);//记录起点
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            start = new Point(e.X, e.Y);//记录起点
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)//如果鼠标左键按下
            {
                //计算移动间距并移动窗体
                end = new Point(e.X, e.Y);//获取当前鼠标位置
                this.Top += end.Y - start.Y;//设置窗体与窗口上边距距离
                this.Left += end.X - start.X;//设置窗体与窗口左边距距离
            }
        }
        private void buttonX1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            try
            {
                if (checkbox.Checked && radbunmin.Checked)
                {
                    Save.close = 0;
                }
                else if (checkbox.Checked && radbunapp.Checked)
                {
                    Save.close = 1;
                }
                if (radbunmin.Checked == true)
                {
                    close.min = true;
                    this.Close();
                }
                else
                {
                    Application.Exit();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.TargetSite);
            }
        }
    }
}
