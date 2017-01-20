using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

namespace 魅力
{
    class Save
    {
        public static int close = 2;
        public static string SongName;//最后播放歌曲
        public static string FrmBack;//背景设置
        public static string FrmBackColor = ColorTranslator.ToHtml(Color.Transparent);//背景设置
        public static string lrcFont="华文新魏";//歌词字体
        public static string lrcColor=ColorTranslator.ToHtml(Color.MediumPurple);//歌词颜色
        public static string LryColor;//保存上次歌词颜色
        public static SongMS SaveSongMS;//保存上次歌词颜色
        public static int Vioce;//保存音量大小

        public static void GetSave()
        {
            //保存本地列表
            StreamWriter w = File.CreateText("本地列表.txt");
            w.WriteLine(Play.songNum);
            for (int i = 0; i < Play.songNum; i++)
            {
                string str = string.Format("{0},{1},{2},{3},{4},{5},{6}",
                    SongList.songList[i].SongID, SongList.songList[i].Title, SongList.songList[i].Singer
                    , SongList.songList[i].Type, SongList.songList[i].Time, SongList.songList[i].Url, SongList.songList[i].Love);
                w.WriteLine(str);
            }
            w.Close();
            //保存设置
            StreamWriter wss = File.CreateText("保存设置.txt");
            wss.WriteLine(Save.SongName);
            wss.WriteLine(Save.Vioce);
            wss.WriteLine(Save.LryColor);
            wss.WriteLine(Save.FrmBack);
            wss.WriteLine(SongList.playIndex);
            wss.WriteLine(Play.SongMS);
            wss.WriteLine(Save.FrmBackColor);
            wss.WriteLine(Save.lrcFont);
            wss.WriteLine(Save.lrcColor);
            wss.WriteLine(Save.close);
            wss.Close();
        }
        public static bool LaodSongList()
        {
            try
            {
                //写入本地列表
                StreamReader r = File.OpenText("本地列表.txt");
                Play.songNum = int.Parse(r.ReadLine());

                for (int i = 0; i < Play.songNum; i++)
                {
                    Song s = new Song();
                    string curSongStr = r.ReadLine();
                    string[] curSongStrList = curSongStr.Split(',');
                    s.SongID = curSongStrList[0];
                    s.Title = curSongStrList[1];
                    s.Singer = curSongStrList[2];
                    s.Type = curSongStrList[3];
                    s.Time = curSongStrList[4];
                    s.Url = curSongStrList[5];
                    s.Love =int.Parse(curSongStrList[6]);
                    SongList.songList[i] = s;
                }
                r.Close();
                return true;
            }
            catch (Exception)
            {
                return false;

                //throw;
            }
        }

    }
}
