using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;

namespace ExercisePrj.Text
{
    public class TextEdit
    {
        //反转字符串
        public string Changeover(string input)
        {
            StringBuilder sb = new StringBuilder();
            for(int i= input.Length-1; i>=0;i--)
            {
                sb.Append(input[i]);
            }
            return sb.ToString();
        }
        //拉丁猪游戏
        public string PigLatin(string input)
        {
            char[] vowels = { 'a', 'e','i','o','u' };
            string inputlower = input.ToLower();
            for(int i=0;i< inputlower.Length;i++)
            {
                var cur = inputlower[i];
                if (cur < 'A' && cur > 'z') continue;
                if (vowels.Contains(cur)) continue;
                return input.Remove(i, 1) + '-' + cur + "ay";
            }
            return input;
        }
        //统计字符
        public Dictionary<char, int> VowelCount(string input)
        {
            char[] vowels = { 'a', 'e', 'i', 'o', 'u' };
            string lower = input.ToLower();
            Dictionary<char, int> res = new Dictionary<char, int>();
            foreach(var c in vowels)
            {
                res.Add(c, 0);
            }
            for(int i=0;i<lower.Length;i++)
            {
                if(vowels.Contains(lower[i]))
                {
                    res[lower[i]]++;
                }
            }
            return res;
        }
        //是否是回文
        public bool IsPlalindrome(string input)
        {
            int end = input.Length - 1;
            for(int i=0;i<((end+1)/2);i++,end--)
            {
                if (input[i] != input[end]) return false;
            }
            return true;
        }
        //统计单词
        public Dictionary<string,int>WordCount(string input)
        {
            string[] res = input.Split(' ', ',', '.', '\n');
            Dictionary<string,int> resdic = new Dictionary<string, int>();
            foreach(var s in res)
            {
                if(resdic.Keys.Contains(s.ToLower()))
                {
                    resdic[s.ToLower()]++;
                }
                else
                {
                    resdic.Add(s.ToLower(), 1);
                }
            }
            return resdic;
        }
        //获取rss订阅
        public Channel GetRss(string rssLink)
        {
            //http://feed.cnblogs.com/blog/u/22886/rss
            //http://www.nhzy.org/feed
            Channel chl = new Channel();
            
            try
            {
                XmlDocument xml = new XmlDocument();
                xml.Load(rssLink);
                var root = xml.GetElementsByTagName("feed")?[0];
                if(root==null)root= xml.GetElementsByTagName("rss")?[0];
                XmlElement element;
                if (root.Name == "feed") 
                {
                    var channel = root as XmlElement;
                    element = channel.GetElementsByTagName("title")[0] as XmlElement;
                    chl.Title=element.InnerText;
                    var items = channel.GetElementsByTagName("entry");
                    foreach (var item in items)
                    {
                        RssItem itm = new RssItem();
                         element = item as XmlElement;
                        itm.Title = element.GetElementsByTagName("title")[0].InnerText;
                        itm.Link = element.GetElementsByTagName("id")[0].InnerText;
                        itm.Description = element.GetElementsByTagName("summary")[0].InnerText;
                        itm.pubdate = element.GetElementsByTagName("published")[0].InnerText;
                        chl.Items.Add(itm.Title, itm);
                    }
                }
                else if (root.Name=="rss")
                {
                    var channel = (root as XmlElement).GetElementsByTagName("channel")[0] as XmlElement;
                    chl.Title = channel.GetElementsByTagName("title")[0].InnerText;
                    var items = channel.GetElementsByTagName("item");
                    foreach(var item in items)
                    {
                        RssItem itm = new RssItem();
                        element = item as XmlElement;
                        itm.Title = element.GetElementsByTagName("title")[0].InnerText;
                        itm.Link = element.GetElementsByTagName("link")[0].InnerText;
                        itm.Description = element.GetElementsByTagName("description")[0].InnerText;
                        itm.pubdate = element.GetElementsByTagName("pubDate")[0].InnerText;
                        chl.Items.Add(itm.Title, itm);
                    }
                }

                return chl;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        //获取股票
        public string GetStock(params string[] codes)
        {
            //sh601006
            //0：”大秦铁路”，股票名字；
            //1：”27.55″，今日开盘价；
            //2：”27.25″，昨日收盘价；
            //3：”26.91″，当前价格；
            //4：”27.55″，今日最高价；
            //5：”26.20″，今日最低价；
            //6：”26.91″，竞买价，即“买一”报价；
            //7：”26.92″，竞卖价，即“卖一”报价；
            //8：”22114263″，成交的股票数，由于股票交易以一百股为基本单位，所以在使用时，通常把该值除以一百；
            //9：”589824680″，成交金额，单位为“元”，为了一目了然，通常以“万元”为成交金额的单位，所以通常把该值除以一万；
            var code = "";
            foreach (string c in codes)
            {
                code += code + c + ",";
            }
            code.Remove(code.Length - 1);
            string Url = @"http://hq.sinajs.cn/list="+code;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("gb2312"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            return retString;
            
        }
        //获取当日股票K值线
        public void GetStockgif(string code)
        {
            string Url = string.Format(@"http://image.sinajs.cn/newchart/daily/n/{0}.gif", code);
            System.Net.WebClient web = new System.Net.WebClient();
            byte[] buffer = web.DownloadData(Url);
            web.Dispose();
            System.IO.MemoryStream ms = new System.IO.MemoryStream(buffer);
            Form frm = new Form();
            frm.Size = new Size(800, 600);
            frm.Text = "当天K线图";
            PictureBox pic = new PictureBox();
            pic.Dock = DockStyle.Fill;
            frm.Controls.Add(pic);
            pic.Image = Image.FromStream(ms);
            Application.Run(frm);
        }

    }

    public class Channel
    {
        public string Title;
        public Dictionary<string, RssItem> Items = new Dictionary<string, RssItem>();
    }
    public class RssItem
    {
        public string Title;
        public string Description;
        public string Link;
        public string author;
        public string pubdate;
    }
}
