namespace WaiPhyoMaungDontNetCore.MvcApp.Models
{
    
    #region for pagination
    public class BlogResponseModel
    {
        public PageSettingModel PageSetting { get; set; }
        public List<BlogDataModel> Data { get; set; }
    }

    #endregion
}
