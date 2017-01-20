using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 魅力
{
    class lrc
    {
        public string currlrc = "";
        public string nextlrc = "";
        public string nextlrc2 = "";
        public string nextlrc3 = "";
    }
    class gechi
    {
        public static Dictionary<string, lrc> lrcDictionary = null;//歌词集合
        public static bool isLrc = false;//是否有歌词
        public static string TIME;//时间
        public static bool showLrc = false;//歌词是否显示
    }
}
