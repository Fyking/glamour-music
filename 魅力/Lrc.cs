using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//导入
using System.IO;//文件
using System.Text.RegularExpressions;//正则表达式

namespace 魅力
{
    /// <summary>
    /// 歌词类
    /// </summary>
    public class Lrc
    {
        /// <summary>
        /// 歌词集合
        /// </summary>
        private Dictionary<string, string> lrcCollections;
        public Dictionary<string, string> LrcCollection
        {
            get { return lrcCollections; }
            set { }//只读
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="path">歌词文件名</param>
        public Lrc(string path)
        {
            if (File.Exists("Song//"+path) == false)
            {
                throw new System.ArgumentException("歌词文件不存在");
            }
            getLrcContext(path);
        }

        /// <summary>
        /// 加载歌词到歌词集合
        /// </summary>
        /// <param name="path"></param>
        private void getLrcContext(string path)
        {
            lrcCollections = new Dictionary<string, string>();
            //正则表达式 元字符
            string excTime = @"(?<=\[).*?(?=\])";
            string excText = @"(?<=\])(?!\[).*";
            //正则表达式匹配结果集合
            MatchCollection matchTime;
            MatchCollection matchText;
            //读取歌词内容存入
            string[] str = File.ReadAllLines("Song\\"+path, Encoding.Default);
            for (int i = 0; i < str.Length - 1; i++)
            {
                matchTime = Regex.Matches(str[i], excTime);
                matchText = Regex.Matches(str[i], excText);
                foreach (var s in matchTime)
                {
                    string strTemp = "";
                    foreach (var m in matchText)
                    {
                        strTemp += m;
                    }
                    try
                    {
                        lrcCollections.Add(s.ToString(), strTemp);
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

    }
}
