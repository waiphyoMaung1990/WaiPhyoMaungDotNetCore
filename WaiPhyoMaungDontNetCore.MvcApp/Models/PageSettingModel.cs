﻿namespace WaiPhyoMaungDontNetCore.MvcApp.Models
{
  

    #region for pagination
    public class PageSettingModel
    {
        public PageSettingModel(int pageNo, int pageSize, int pageCount, int pageRowCount,string pageUrl)
        {
            PageNo = pageNo;
            PageSize = pageSize;
            PageCount = pageCount;
            PageRowCount = pageRowCount;
            PageUrl = pageUrl;
        }

        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }//total pageCount
        public int PageRowCount { get; set; }
        public string PageUrl { get; set; }
    }

    #endregion
}