using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Net;
using System.Net.NetworkInformation;
using System.IO;
namespace DownloadDataFromWebofScience
{
    public class Downloader
    {
        public readonly string DownloadDataUrl = "http://apps.webofknowledge.com/OutboundService.do?action=go&&";
        /// <summary>
        /// SID,根据浏览器生成，可以持续使用，跨浏览器、Ip
        /// </summary>
        public string SID { set; get; }
        /// <summary>
        /// 查询ID
        /// </summary>
        public string QueryId { set; get; }
        /// <summary>
        /// 记录数
        /// </summary>
        public int TotalCount { set; get; }
        /// <summary>
        /// Post Content from={0},to={1} 内部已经包含了qid与sid
        /// </summary>
        public string PostContent { set; get; }
        /// <summary>
        /// http标头
        /// </summary>
        public WebHeaderCollection Header { set; get; }
        /// <summary>
        /// Cookies
        /// </summary>
        public CookieCollection Cookies { set; get; }

        public Downloader(string sid, string queryid)
        {
            QueryId = queryid;
            SID = sid;
        }
        public Downloader(string sid, string queryid, CookieCollection cookies, WebHeaderCollection header = null)
        {
            QueryId = queryid;
            SID = sid;
            Cookies = cookies;
            if (header == null)
            {
                Header = new WebHeaderCollection();
                Header.Add(HttpRequestHeader.CacheControl, "max-age=0");

                Header.Add(HttpRequestHeader.AcceptEncoding, "gzip,deflate,sdch");
                Header.Add(HttpRequestHeader.AcceptLanguage, "zh-CN,zh;q=0.8");
            }
            else
            {
                Header = header;
            }
        }
        public Downloader()
        {





        }
        /// <summary>
        /// 【Post】绑定数据项
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="queryid"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public Stream DownloadDataStream(HttpWebRequest request, string sid, string queryid, int from, int to)
        {
            string content = GetDownloadDataPostContent(sid, queryid, from, to);
            byte[] contents = Encoding.UTF8.GetBytes(content);
            request.AllowAutoRedirect = true;
            request.ContentLength = contents.Length;
            Stream stream = request.GetRequestStream();
            stream.Write(contents, 0, contents.Length);
            stream.Close();
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            HttpStatusCode statusCode = response.StatusCode;
            if (statusCode == HttpStatusCode.OK)
            {
                return response.GetResponseStream();
            }
            else
            {
                return null;
            }
        }
        public Stream DownloadDataStream(string sid, string queryid, int from, int to)
        {
            HttpWebRequest request = GetRequest();
            request.Referer = string.Format("http://apps.webofknowledge.com/summary.do?product=WOS&doc=1&qid={1}&SID={0}&search_mode=AdvancedSearch", sid, queryid);

            return DownloadDataStream(request, sid, queryid, from, to);
        }
        public Stream DownloadDataStream(int from, int to)
        {
            return DownloadDataStream(SID, QueryId, from, to);
        }
        public string DownloadData(HttpWebRequest request, string sid, string queryid, int from, int to)
        {
            using (Stream stream = DownloadDataStream(request, sid, queryid, from, to))
            {
                if (stream == null)
                {
                    return string.Empty;
                }
                else
                {
                    StreamReader reader = new StreamReader(stream);
                    return reader.ReadToEnd();
                }
            }
        }
        public string DownloadData(string sid, string queryid, int from, int to)
        {
            HttpWebRequest request = GetRequest();
            request.Referer = string.Format("http://apps.webofknowledge.com/summary.do?product=WOS&doc=1&qid={1}&SID={0}&search_mode=AdvancedSearch", sid, queryid);

            return DownloadData(request, sid, queryid, from, to);
        }
        public string DownloadData(int from, int to)
        {
            return DownloadData(SID, QueryId, from, to);
        }
        public HttpWebRequest GetRequest()
        {
            HttpWebRequest request = WebRequest.Create(DownloadDataUrl) as HttpWebRequest;
            request.CookieContainer = new CookieContainer();
            if (Cookies != null)
            {
                request.CookieContainer.Add(new Uri("http://apps.webofknowledge.com"), Cookies);
            }
            request.Headers = Header;
            request.Method = "Post";
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Host = "apps.webofknowledge.com";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/28.0.1500.95 Safari/537.36 SE 2.X MetaSr 1.0";
            return request;
        }
        /// <summary>
        /// 返回OutBoundService需要Post的Content
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="queryid"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public string GetDownloadDataPostContent(string sid, string queryid, int from, int to)
        {
            if (PostContent.Length == 0)
            {
                return string.Format("selectedIds=&displayCitedRefs=true&displayTimesCited=true&viewType=summary&product=WOS&rurl=http%253A%252F%252Fapps.webofknowledge.com%252Fsummary.do%253FSID%253D{0}%2526product%253DWOS%2526doc%253D1%2526qid%253D{1}%2526search_mode%253DAdvancedSearch&mark_id=WOS&colName=WOS&search_mode=AdvancedSearch&locale=zh_CN&view_name=WOS-summary&sortBy=PY.D%3BLD.D%3BSO.A%3BVL.D%3BPG.A%3BAU.A&mode=OpenOutputService&qid={1}&SID={0}&format=saveToFile&filters=USAGEIND+AUTHORSIDENTIFIERS+ACCESSION_NUM+FUNDING+SUBJECT_CATEGORY+JCR_CATEGORY+LANG+IDS+PAGEC+SABBR+CITREFC+ISSN+PUBINFO+KEYWORDS+CITTIMES+ADDRS+CONFERENCE_SPONSORS+DOCTYPE+ABSTRACT+CONFERENCE_INFO+SOURCE+TITLE+AUTHORS++&mark_to={3}&mark_from={2}&count_new_items_marked=0&value%28record_select_type%29=range&markFrom={2}&markTo={3}&fields_selection=USAGEIND+AUTHORSIDENTIFIERS+ACCESSION_NUM+FUNDING+SUBJECT_CATEGORY+JCR_CATEGORY+LANG+IDS+PAGEC+SABBR+CITREFC+ISSN+PUBINFO+KEYWORDS+CITTIMES+ADDRS+CONFERENCE_SPONSORS+DOCTYPE+ABSTRACT+CONFERENCE_INFO+SOURCE+TITLE+AUTHORS++&save_options=othersoftware", sid, queryid, from, to);
            }
            else {
                return string.Format(PostContent, from, to);
            }
        }


    }
}
