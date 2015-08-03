using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
namespace DownloadDataFromWebofScience
{
    public class Requester
    {
        static int Count = 2;
        static object obj = new object();
        static int GetCount()
        {
            lock (obj)
            {
                Count++;
            }
            return Count;
        }
        /// <summary>
        /// 返回OutBoundService需要Post的Content
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="queryid"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static string GetOutBoundServiceContent(string sid, string queryid, int from, int to)
        {
            return string.Format("selectedIds=&displayCitedRefs=true&displayTimesCited=true&viewType=summary&product=WOS&rurl=http%253A%252F%252Fapps.webofknowledge.com%252Fsummary.do%253FSID%253D{0}%2526product%253DWOS%2526doc%253D1%2526qid%253D{1}%2526search_mode%253DAdvancedSearch&mark_id=WOS&colName=WOS&search_mode=AdvancedSearch&locale=zh_CN&view_name=WOS-summary&sortBy=PY.D%3BLD.D%3BSO.A%3BVL.D%3BPG.A%3BAU.A&mode=OpenOutputService&qid={1}&SID={0}&format=saveToFile&filters=USAGEIND+AUTHORSIDENTIFIERS+ACCESSION_NUM+FUNDING+SUBJECT_CATEGORY+JCR_CATEGORY+LANG+IDS+PAGEC+SABBR+CITREFC+ISSN+PUBINFO+KEYWORDS+CITTIMES+ADDRS+CONFERENCE_SPONSORS+DOCTYPE+ABSTRACT+CONFERENCE_INFO+SOURCE+TITLE+AUTHORS++&mark_to={3}&mark_from={2}&count_new_items_marked=0&value%28record_select_type%29=range&markFrom={2}&markTo={3}&fields_selection=USAGEIND+AUTHORSIDENTIFIERS+ACCESSION_NUM+FUNDING+SUBJECT_CATEGORY+JCR_CATEGORY+LANG+IDS+PAGEC+SABBR+CITREFC+ISSN+PUBINFO+KEYWORDS+CITTIMES+ADDRS+CONFERENCE_SPONSORS+DOCTYPE+ABSTRACT+CONFERENCE_INFO+SOURCE+TITLE+AUTHORS++&save_options=othersoftware", sid, queryid, from, to);
        }
        public static string GetDownloadDataUrl(string sid, string queryid, int from, int to)
        {
            return string.Format("http://ets.webofknowledge.com/ETS/ets.do?rurl=http%253A%252F%252Fapps.webofknowledge.com%252Fsummary.do%253FSID%253D{0}%2526product%253DWOS%2526doc%253D1%2526qid%253D{1}%2526search_mode%253DAdvancedSearch&qid={5}&mark_to={3}&fileOpt=othersoftware&displayCitedRefs=true&totalMarked={4}&SID={0}&product=UA&mark_from={2}&parentQid={1}&displayTimesCited=true&sortBy=PY.D;LD.D;SO.A;VL.D;PG.A;AU.A&rid=null&UserIDForSaveToRID=null&action=saveToFile&colName=WOS&filters=USAGEIND%20AUTHORSIDENTIFIERS%20ACCESSION_NUM%20FUNDING%20SUBJECT_CATEGORY%20JCR_CATEGORY%20LANG%20IDS%20PAGEC%20SABBR%20CITREFC%20ISSN%20PUBINFO%20KEYWORDS%20CITTIMES%20ADDRS%20CONFERENCE_SPONSORS%20DOCTYPE%20ABSTRACT%20CONFERENCE_INFO%20SOURCE%20TITLE%20AUTHORS",sid,queryid,from,to,to-from+1,GetCount());
        }
    }
}
