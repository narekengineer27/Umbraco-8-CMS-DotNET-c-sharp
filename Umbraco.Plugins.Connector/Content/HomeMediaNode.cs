namespace Umbraco.Plugins.Connector.Content
{
    using System.Linq;
    using Umbraco.Core;
    using Umbraco.Core.Logging;
    using Umbraco.Core.Models;
    using Umbraco.Core.Models.Membership;
    using Umbraco.Core.Persistence;
    using Umbraco.Core.Persistence.Querying;
    using Umbraco.Core.Persistence.DatabaseModelDefinitions;
    using Umbraco.Core.Services;
    using Umbraco.Plugins.Connector.Exceptions;
    using Umbraco.Plugins.Connector.Helpers;
    using Umbraco.Plugins.Connector.Models;
    using Newtonsoft.Json;
    using System.IO;

    public class HomeMediaNode
    {
        private readonly IMediaService mediaService;
        private readonly ILogger logger;
        private readonly IContentTypeBaseServiceProvider contentTypeBaseServiceProvider;

        public HomeMediaNode(IMediaService mediaService, ILogger logger, IContentTypeBaseServiceProvider contentTypeBaseServiceProvider)
        {
            this.mediaService = mediaService;
            this.logger = logger;
            this.contentTypeBaseServiceProvider = contentTypeBaseServiceProvider;
        }

        public void Validate(Tenant tenant)
        {
            var exists = mediaService.GetByLevel(1).SingleOrDefault(x => x.Name.Equals(tenant.Name)) != null;
            //var exists = mediaService.GetByLevel(1).SingleOrDefault(x => x.Name.Equals(tenant.BrandName)) != null;
            if (exists)
            {
                throw new TenantException(ExceptionCode.MediaNodeAlreadyExists.CodeToString(), ExceptionCode.MediaNodeAlreadyExists, tenant.TenantUId);
            }
        }

        public int CreateMediaHome(Tenant tenant)
        {
            Validate(tenant);
            try
            {
                var nodeName = tenant.Name;
                var nodeAlias = tenant.Name.Trim(' ').ToLower();
                //var nodeName = tenant.BrandName;
                //var nodeAlias = tenant.BrandName.Trim(' ').ToLower();

                var folder = mediaService.CreateMedia(nodeName, Constants.System.Root, "Folder");
                mediaService.Save(folder);
                ConnectorContext.AuditService.Add(AuditType.New, -1, folder.Id, "Media Folder", $"Media Home for {tenant.TenantUId} has been created");

                return folder.Id;
            }
            catch (System.Exception ex)
            {
                logger.Error(typeof(HomeMediaNode), ex.Message);
                logger.Error(typeof(HomeMediaNode), ex.StackTrace);
                throw;
            }
        }

        public int CreateMediaSliderFolder(int parentId)
        {
            try
            {
                var rootId = mediaService.GetById(parentId);
                var folder = mediaService.CreateMedia("Slider Banners", rootId, "Folder");
                mediaService.Save(folder);
                ConnectorContext.AuditService.Add(AuditType.New, -1, folder.Id, "Media Folder", $"Folder for Slider Banners has been created");

                return folder.Id;
            }
            catch (System.Exception ex)
            {
                logger.Error(typeof(HomeMediaNode), ex.Message);
                logger.Error(typeof(HomeMediaNode), ex.StackTrace);
                throw;
            }
        }

        public int GetMediaSliderFolder(int tenantRootMediaId)
        {
            int mediaId = 0;
            
            long total;
            var slider = mediaService.GetPagedChildren(tenantRootMediaId, 0, 100, out total).SingleOrDefault(x => x.Name == "Slider Banners");
            if (slider != null)
            {
                mediaId = slider.Id;
            }
            return mediaId;
        }

        public int RenameMediaHome(TenantData tenant, TenantUser tenantUser)
        {
            var home = TenantHelper.GetCurrentTenantHome(ConnectorContext.ContentService, tenant.TenantUId.ToString());
            if (home == null)
            {
                throw new TenantException(ExceptionCode.TenantNotFound.CodeToString(), ExceptionCode.TenantNotFound, tenant.TenantUId);
            }

            IMedia mediaHome = null;
            IReadOnlyUserGroup group = null;
            if (tenantUser != null)
            {
                var uService = ConnectorContext.UserService;
                var user = uService.GetByUsername(tenantUser.Username);
                group = user.Groups.ToList()[0];
                mediaHome = mediaService.GetById(group.StartMediaId.Value);
            }
            else
            {
                mediaHome = mediaService.GetByLevel(1).SingleOrDefault(x => x.Name.Contains(tenant.Name));
            }

            if (mediaHome == null)
            {
                string mediaId = group?.StartMediaId.Value.ToString() ?? tenant.Name;
                //string mediaId = group?.StartMediaId.Value.ToString() ?? tenant.BrandName;
                throw new TenantException(ExceptionCode.MediaNodeDoesNotExist.CodeToString(), ExceptionCode.MediaNodeDoesNotExist, tenant.TenantUId, mediaId);
            }

            try
            {
                if (!mediaHome.Name.Equals(tenant.Name))
                {
                    mediaHome.Name = tenant.Name;
                    //mediaHome.Name = tenant.BrandName;
                    mediaService.Save(mediaHome);
                    ConnectorContext.AuditService.Add(AuditType.Save, -1, mediaHome.Id, "Media Folder", $"Media home for {tenant.TenantUId} has been renamed");
                }

                return mediaHome.Id;
            }
            catch (System.Exception ex)
            {
                logger.Error(typeof(HomeMediaNode), ex.Message);
                logger.Error(typeof(HomeMediaNode), ex.StackTrace);
                throw;
            }
        }

        public int CopyTenantMediaFolder(string sourceTenantName, Tenant tenant)
        {
            //get the root node
            int newMediaId = CreateMediaHome(tenant);

            //get source home media 
            IMedia sourceMediaHome = mediaService.GetByLevel(1).SingleOrDefault(x => x.Name.Equals(sourceTenantName));
            
            if (sourceMediaHome != null)
            {
                CopyMediaFolder(sourceMediaHome.Id, newMediaId);
            }

            return newMediaId;
        }

        private void CopyMediaFolder(int sourceMediaFolderId, int destinationMediaFolderId)
        {
            try
            {
                long totalchildren;
                var lstChildMedia = mediaService.GetPagedChildren(sourceMediaFolderId, 0, 1000, out totalchildren);
                foreach (IMedia itemToCopy in lstChildMedia)
                {
                    if (itemToCopy.ContentType.Name == "Folder")
                    {
                        var newfolder = mediaService.CreateMedia(itemToCopy.Name, destinationMediaFolderId, "Folder");
                        mediaService.Save(newfolder);
                        CopyMediaFolder(itemToCopy.Id, newfolder.Id); //recursive
                    }
                    else
                    {
                        //string sourceUmbFile = itemToCopy.GetValue<Image>("umbracoFile").ToString();
                        var imgObj = JsonConvert.DeserializeObject<ImageCropDataSet>(itemToCopy.GetValue<string>("umbracoFile"));
                        if (imgObj != null)
                        {
                            string path = System.Web.Hosting.HostingEnvironment.MapPath(imgObj.src);
                            string filename = Path.GetFileName(path);
                            using (var imageStream = new FileStream(path, FileMode.Open, FileAccess.Read))
                            {
                                IMedia newMedia = mediaService.CreateMedia(itemToCopy.Name, destinationMediaFolderId, itemToCopy.ContentType.Name);
                                newMedia.SetValue(contentTypeBaseServiceProvider, "umbracoFile", filename, imageStream);
                                mediaService.Save(newMedia);
                            }
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                logger.Error(typeof(HomeMediaNode), ex.Message);
                logger.Error(typeof(HomeMediaNode), ex.StackTrace);
                throw;
            }
        }
    }
}

