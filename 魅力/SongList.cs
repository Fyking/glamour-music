using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace 魅力
{
    public class SongList
    {
        //初始化本地列表数组
        public static Song[] songList = new Song[500];
        //当前播放歌曲索引
        public static int playIndex = 0;
    }
}
