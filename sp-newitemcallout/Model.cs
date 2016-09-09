using CommandLine;
using Microsoft.SharePoint.Client;
using SP.Cmd.Deploy;
using SPMeta2.BuiltInDefinitions;
using SPMeta2.CSOM.ModelHosts;
using SPMeta2.CSOM.Services;
using SPMeta2.Definitions;
using SPMeta2.Syntax.Default;
using SPMeta2.Syntax.Default.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace sp_newitemcallout
{
    public static class Model
    {
        public static string Assets = @"SiteAssets";
        public static string SystemPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

        public static SiteModelNode DeployModel()
        {
            return SPMeta2Model.NewSiteModel(site =>
            {
                site
                    .AddRootWeb(new RootWebDefinition(), RootWeb =>
                    {
                        RootWeb
                            .AddHostList(BuiltInListDefinitions.Catalogs.MasterPage, list =>
                            {
                                var FolderPath = Path.Combine(SystemPath, Assets);
                                if (Directory.Exists(FolderPath))
                                {
                                    ModuleFileUtils.LoadModuleFilesFromLocalFolder(list, FolderPath);
                                }

                            });

                    })
                    ;
            });
        }

        public static void Deploy(SPDeployOptions options)
        {
            SharePoint.Session(options.url, options.Credentials, ctx =>
            {
                var provisionService = new CSOMProvisionService();
                provisionService.DeployModel(SiteModelHost.FromClientContext(ctx), DeployModel());

            });
        }
    }
}
