using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NLog;
using System.Linq;


namespace SpiderDocsModule
{

    public class Cache
    {
        static Logger logger = LogManager.GetCurrentClassLogger();

        static LazyCache.IAppCache cache = new LazyCache.CachingService();

        int _id_user = 0;
        static List<int> _users = new List<int>();

        System.Threading.Thread[] t;

        public enum en_UKeys
        {
            DB_GetAssignedFolderToUser = 1,
            DB_cl_WorkspaceCustomize_Load,
            DB_cl_WorkspaceGridsize_Load,
            DB_UserGlobalSetting_Load,
            DB_SyncMgr
        }
        public enum en_GKeys
        {
            DB_getSystemVersion = 1000,
            BD_getIntSystemVersion,
            DB_MailSettingss_Load,
            DB_PublicSettings_Load,
            DB_User,
            DB_GetAttributes,
            DB_GetComboItems,
            DB_HasComboChild,
			DB_RptComparators,
			DB_RptDropDownFields,
			DB_RptReports,
			DB_RptFieldsByCategory,
			DB_RptFieldComparators
		}


        public Cache(int id_user)
        {
            try
            {
                _id_user = id_user;

                if (! _users.Exists(x => x == id_user) ) _users.Add(id_user);

            }
            catch (Exception ex)
            {
                logger.Error(ex);

                logger.Debug("id_user:{0}, _users.Count:{1}", id_user, _users?.Count);

                cache = new LazyCache.CachingService();
                _users = new List<int>(id_user);
            }
        }

        public void Create()
        {
            t = new System.Threading.Thread[4];

            //t[0]?.Abort();
            //t[0] = new System.Threading.Thread(() => GetFoldersPermissions( en_Actions.CheckIn_Out)) { Priority = System.Threading.ThreadPriority.Lowest};
            //t[0].Start();

            //t[1]?.Abort();
            //t[1] = new System.Threading.Thread(() => GetAssignedFolderToUser()) { Priority = System.Threading.ThreadPriority.Lowest};
            //t[1].Start();

            t[2]?.Abort();
            t[2] = new System.Threading.Thread(() => { getSystemVersion(); GetAttributes(); HasComboChild(); }) { Priority = System.Threading.ThreadPriority.Lowest};
            t[2].Start();

            t[3]?.Abort();
            t[3] = new System.Threading.Thread(() =>
            {
                if( _id_user > 0)
                {
                    DocumentAttributeController.GetAttributesCache(true);
                    Cache.GetComboItems(0);
                }

            }) { Priority = System.Threading.ThreadPriority.Lowest};
            t[3].Start();


        }

        public string UserKey(en_UKeys key)
        {
            return UserKey(key,_id_user);
        }

        static public string UserKey(en_UKeys key, int id_user)
        {
            return string.Format("{0}_{1}", key.ToString(), id_user);
        }


        static public void Remove(en_GKeys key)
        {
            cache.Remove(key.ToString());
        }

        public void Remove(en_UKeys key)
        {
            cache.Remove(UserKey(key));
        }

        static public void RemoveUsersCache(en_UKeys key)
        {
            foreach (int id in _users.ToList())
            {
                cache.Remove(UserKey(key, id));
            }
        }
        public void RemoveMyCache(en_UKeys key)
        {
            foreach (int id in _users.ToList())
            {
                cache.Remove(UserKey(key, _id_user));
            }
        }
        static public void RemoveAll()
        {
            foreach(en_GKeys foo in Enum.GetValues(typeof(en_GKeys)))
            {
                Remove(foo);
            }

            foreach (en_UKeys key in Enum.GetValues(typeof(en_UKeys)))
            {
                RemoveUsersCache(key);
            }

            _users = new List<int>();

            cache = new LazyCache.CachingService();
        }


        static public string getSystemVersion()
        {
            Func<string> fnc = () => SpiderDocsCoer.getSystemVersion();

            return cache.GetOrAdd(en_GKeys.DB_getSystemVersion.ToString(), fnc);
        }
        static public int getIntSystemVersion()
        {
            Func<int> fnc = () => int.Parse(getSystemVersion().Replace(".", ""));

            return cache.GetOrAdd(en_GKeys.BD_getIntSystemVersion.ToString(), fnc);
        }

        static public MailSettingss MailSettingss_Load()
        {
            Func<MailSettingss> fnc = () => MailSettingController.Load();

            return cache.GetOrAdd(en_GKeys.DB_MailSettingss_Load.ToString(), fnc);
        }
        static public PublicSettings PublicSetting_Load()
        {
            Func<PublicSettings> fnc = () => PublicSettingController.Load();

            return cache.GetOrAdd(en_GKeys.DB_PublicSettings_Load.ToString(), fnc);
        }
        public cl_WorkspaceCustomize cl_WorkspaceCustomize_Load()
        {
            Func<cl_WorkspaceCustomize> fnc = () => cl_WorkspaceCustomizeController.Load(new cl_WorkspaceCustomize(_id_user));

            return cache.GetOrAdd(UserKey(en_UKeys.DB_cl_WorkspaceCustomize_Load), fnc);
        }

        public cl_WorkspaceGridsize cl_WorkspaceGridsize_Load()
        {
            Func<cl_WorkspaceGridsize> fnc = () => cl_WorkspaceGridsizeController.Load(new cl_WorkspaceGridsize(_id_user));

            return cache.GetOrAdd(UserKey(en_UKeys.DB_cl_WorkspaceGridsize_Load), fnc);
        }

        public UserGlobalSettings UserGlobalSetting_Load()
        {
            Func<UserGlobalSettings> fnc = () => UserGlobalSettingController.Load(_id_user);

            return cache.GetOrAdd(UserKey(en_UKeys.DB_UserGlobalSetting_Load), fnc);
        }

        static public List<User> GetUser()
        {
            Func<List<User>> fnc = () => UserController.GetUsers();

            return cache.GetOrAdd(en_GKeys.DB_User.ToString(), fnc).Select(x => (User)x.Clone()).ToList(); ;
        }

        static public List<DocumentAttribute> GetAttributes()
        {
            Func<List<DocumentAttribute>> fnc = () => DocumentAttributeController.GetAttributes();

            return cache.GetOrAdd(en_GKeys.DB_GetAttributes.ToString(), fnc).Select(x => (DocumentAttribute)x.Clone()).ToList();
        }

        static public List<DocumentAttributeCombo> GetComboItems(int id)
        {
            Func<List<DocumentAttributeCombo>> fnc = () => DocumentAttributeController.GetComboItems();

            List< DocumentAttributeCombo> combos = cache.GetOrAdd(en_GKeys.DB_GetComboItems.ToString(), fnc).Select(x => (DocumentAttributeCombo)x.Clone()).ToList();

            return combos.Where(x => x.id_atb == id).ToList();
        }

        static public bool HasComboChild()
        {
            Func<bool> fnc = () => DocumentAttributeController.HasComboChild();

            return cache.GetOrAdd(en_GKeys.DB_HasComboChild.ToString() , fnc);
        }

        public WorkSpaceMgr GetSyncMgr(string userFolder)
        {
            Func<WorkSpaceMgr> fnc = () => new WorkSpaceMgr(userFolder);

            return cache.GetOrAdd(UserKey(en_UKeys.DB_SyncMgr) , fnc);
        }

        static public List<ReportBuilder.Models.Report.Reporting_Comparator> RptComparators()
        {
			Func<List<ReportBuilder.Models.Report.Reporting_Comparator>> fnc = () =>
			{
				using(var  rptORM = new ReportBuilder.Controllers.ReportController()){
					return rptORM.GetComparators();
				}
			};

            return cache.GetOrAdd(en_GKeys.DB_RptComparators.ToString() , fnc);
        }

        static public List<ReportBuilder.Models.Report.Reporting_Dropdown_Fields> RptDropDownFields()
        {
			Func<List<ReportBuilder.Models.Report.Reporting_Dropdown_Fields>> fnc = () =>
			{
				using(var  rptORM = new ReportBuilder.Controllers.ReportController())
				{
					return rptORM.GetReportDropdownFields();
				}
			};

            return cache.GetOrAdd(en_GKeys.DB_RptDropDownFields.ToString() , fnc);
        }

        static public List<ReportBuilder.Models.Report.Reporting_Report> RptReports()
        {
			Func<List<ReportBuilder.Models.Report.Reporting_Report>> fnc = () =>
			{
				using(var  rptORM = new ReportBuilder.Controllers.ReportController())
				{
					return rptORM.GetReports("");
				}
			};

            return cache.GetOrAdd(en_GKeys.DB_RptReports.ToString() , fnc);
        }

        static public List<ReportBuilder.Models.Report.Reporting_Fields> RptFieldsByCategory(int categoryId)
        {
			Func<List<ReportBuilder.Models.Report.Reporting_Fields>> fnc = () =>
			{
				using(var  rptORM = new ReportBuilder.Controllers.ReportController())
				{
					return rptORM.GetFieldsBy();
				}
			};

            var list = cache.GetOrAdd(en_GKeys.DB_RptFieldsByCategory.ToString() , fnc);

			return list.Where(x => x.Category_Id == categoryId).ToList();
        }

        static public List<ReportBuilder.Models.Report.Reporting_Comparator> RptFieldComparators(int fieldId)
        {
			Func<List<ReportBuilder.Models.Report.Reporting_Comparator>> fnc = () =>
			{
				using(var  rptORM = new ReportBuilder.Controllers.ReportController())
				{
					return rptORM.GetFieldComparators();
				}
			};

            var list = cache.GetOrAdd(en_GKeys.DB_RptFieldComparators.ToString() , fnc);

			return list.Where(x => x.field_Id == fieldId).ToList();
        }
    }

}