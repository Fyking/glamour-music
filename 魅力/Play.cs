using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace 魅力
{
    public enum SongMS
    {
        单曲播放,
        单曲循环,
        顺序播放,
        列表循环,
        随机播放
    }
    class Play
    {
        public static  bool play = false;//播放状态
        public static int songNum = 0;//歌曲总数
        public static string SongLry = "";//正在播放的歌曲歌词
        public static SongMS SongMS = SongMS.顺序播放; //歌曲循环模式
        public static AxWMPLib.AxWindowsMediaPlayer player;//播放器
        //播放当前歌曲
    }


}
