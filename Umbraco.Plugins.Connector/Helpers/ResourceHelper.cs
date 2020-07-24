namespace Umbraco.Plugins.Connector.Helpers
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Collections.Generic;
    using Umbraco.Plugins.Connector.Models;

    /// <summary>
    /// Extracts and embedded resource and copies it to the specified location
    /// </summary>
    /// <see cref="https://stackoverflow.com/questions/13031778/how-can-i-extract-a-file-from-an-embedded-resource-and-save-it-to-disk"/>
    public static class ResourceHelper
    {
        public static void ExtractEmbeddedResource(this Assembly assembly, List<EmbeddedResource> files)
        {
            var appRoot = System.Web.Hosting.HostingEnvironment.MapPath("~/");
            var assemblyName = assembly.GetName().Name;

            foreach (var file in files)
            {
                if (file.ResourceType == ResourceType.Directory)
                {
                    foreach (var rec in assembly.GetManifestResourceNames().ToList().Where(x => x.Contains(file.ResourceLocation)))
                        CopyResource(assembly, rec, file.OutputDirectory, appRoot, assemblyName, file.ResourceLocation, true, file.Replace, file.AddToVisualStudioProject, file.DependentUpon, file.DependentUponFile);
                }
                else if(file.ResourceType == ResourceType.CreateDirectoryOnly)
                {
                    CreateDirectory(file.OutputDirectory);
                }
                else
                {
                    CopyResource(assembly, file.FileName, file.OutputDirectory, appRoot, assemblyName, file.ResourceLocation, false, file.Replace, file.AddToVisualStudioProject, file.DependentUpon, file.DependentUponFile, createBackup: file.CreateBackup);
                }
            }
        }

        public static bool CheckExists(string appRoot, string fileName, string outputDirectory)
        {
            var fileFullPath = Path.Combine(appRoot, outputDirectory, fileName);
            return File.Exists(fileFullPath);
        }

        private static void CopyResource(Assembly assembly, string fileName, string outputDirectory, string appRoot, string assemblyName, string resourceLocation, bool isDirectory, bool? replace = false, bool? addtoVSProj = false, bool? dependentUpon = false, string dependentUponFile = "", bool createBackup = false)
        {
            string path = string.Empty;
            string fileFullPath = string.Empty;
            if (isDirectory)
            {
                path = fileName;
                fileName = fileName.Replace(assemblyName + ".", "").Replace(resourceLocation + ".", "");
            }
            else
            {
                path = $"{assemblyName}.{resourceLocation}.{fileName}";
            }

            try
            {
                using (Stream stream = assembly.GetManifestResourceStream(path))
                {
                    var outputPath = Path.Combine(appRoot, outputDirectory);
                    CreateDirectory(outputPath);

                    fileFullPath = Path.Combine(appRoot, outputDirectory, fileName);
                    if (createBackup)
                    {
                        if (File.Exists(fileFullPath) && new FileInfo(fileFullPath).Length != stream.Length)
                            new FileInfo(fileFullPath).MoveTo($"{fileFullPath}-{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")}");
                    }

                    var fileMode = replace.Value ? FileMode.OpenOrCreate : FileMode.Create;
                    if (!File.Exists(fileFullPath) || replace.Value )
                    {
                        using (FileStream fileStream = new FileStream(fileFullPath, fileMode))
                        {
                            for (int i = 0; i < stream.Length; i++)
                            {
                                fileStream.WriteByte((byte)stream.ReadByte());
                            }
                        }
                        if (addtoVSProj.Value)
                        {
                            AddToVisualStudioProject(fileFullPath, dependentUpon, dependentUponFile);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ConnectorContext.Logger.Error(typeof(ResourceHelper), ex.Message);
                ConnectorContext.Logger.Error(typeof(ResourceHelper), ex.StackTrace);
                ConnectorContext.Logger.Error(typeof(ResourceHelper), $"Path:{path}; File: {fileName}; Target: {fileFullPath}");
            }
        }
        private static void CreateDirectory(string path)
        {
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        }
        /// <summary>
        /// Adds resource file to Visual Studio Project
        /// </summary>
        /// <param name="filename">File to add</param>
        /// <see cref="https://stackoverflow.com/a/18317150/791245"/>
        /// <seealso cref="https://stackoverflow.com/a/23537007/791245"/>
        private static void AddToVisualStudioProject(string filename, bool? dependentUpon = false, string dependentUponFile = "")
        {
            var files = Directory.EnumerateFiles(System.Web.HttpContext.Current.Server.MapPath("~/"))?.ToList();
            if (files != null)
            {
                string proj = string.Empty;
                foreach (var file in files)
                {
                    if (file.Contains(".csproj"))
                    {
                        proj = file;
                        break;
                    }
                }

                // Hack for Visual Studio 2017 in order to make it work
                var msbuild = Path.GetFullPath($"{Path.GetDirectoryName(proj)}\\..\\packages\\Microsoft.Build.Runtime.15.1.1012\\contentFiles\\any\\net46\\");
                Environment.SetEnvironmentVariable("MSBUILD_EXE_PATH", $"{msbuild}MSBuild.exe");
                Environment.SetEnvironmentVariable("VisualStudioVersion", "15.0");
                Environment.SetEnvironmentVariable("VSINSTALLDIR", "C:\\Program Files (x86)\\Microsoft Visual Studio\\2017\\Enterprise\\");
                // Update instance of project
                var p = Microsoft.Build.Evaluation.ProjectCollection.GlobalProjectCollection.LoadProject(proj);

                // Check file is not already in the project
                if (!p.Items.Any(i => i.EvaluatedInclude == Path.GetFileName(filename)))
                {
                    if (dependentUpon.Value)
                    {
                        p.AddItem("Compile", Path.GetFileName(filename), new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("DependentUpon", dependentUponFile) });
                    }
                    else
                    {
                        p.AddItem("Compile", Path.GetFileName(filename));
                    }
                }

                p.Save();
            }
        }
    }
}
