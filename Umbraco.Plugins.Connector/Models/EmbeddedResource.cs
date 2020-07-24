using System;

namespace Umbraco.Plugins.Connector.Models
{
    public class EmbeddedResource
    {
        /// <summary>
        /// The Name of the file with the extension
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// The physical Path for the file (Consider ~/)
        /// </summary>
        public string OutputDirectory { get; set; }

        /// <summary>
        /// The physical location for the file in the assembly
        /// </summary>
        public string ResourceLocation { get; set; }

        /// <summary>
        /// Name for the File (Required only for Templates, in Umbraco, not required for other files)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Alias for the file (Required only for Templates, in Umbraco, not required for other files)
        /// </summary>
        public string Alias { get; set; }

        /// <summary>
        /// The type of resource i.e. file, view, css, etc
        /// </summary>
        public ResourceType ResourceType { get; set; }

        /// <summary>
        /// Whether to replace an existing file
        /// </summary>
        public bool Replace { get; set; }

        /// <summary>
        /// Adds resource to Visual Studio Project
        /// </summary>
        public bool AddToVisualStudioProject { get; set; }

        public bool DependentUpon { get; set; }
        public string DependentUponFile { get; set; }

        public bool CreateBackup { get; set; }

        public EmbeddedResource() { }

        /// <summary>
        /// Creates a new Embedded Resource
        /// </summary>
        /// <param name="fileName">The Name of the file with the extension</param>
        /// <param name="outputDirectory">The physical Path for the file (Consider ~/)</param>
        /// <param name="resourceLocation">The physical location for the file in the assembly</param>
        /// <param name="resourceType">The type of resource i.e. file, view, css, etc</param>
        /// <param name="name">Name for the File (Required only for Templates, in Umbraco, not required for other files)</param>
        /// <param name="alias">Alias for the file (Required only for Templates, in Umbraco, not required for other files)</param>
        public EmbeddedResource(string fileName, string outputDirectory, string resourceLocation, ResourceType resourceType, string name = "", string alias = "", bool createBackup = false)
        {
            if (string.IsNullOrEmpty(name) && string.IsNullOrEmpty(alias) && resourceType == ResourceType.Template)
                throw new ArgumentNullException(name);
            FileName = fileName;
            OutputDirectory = outputDirectory;
            ResourceLocation = resourceLocation;
            ResourceType = resourceType;
            Name = name;
            Alias = alias;
            CreateBackup = createBackup;
        }
    }

    public enum ResourceType
    {
        Template,
        NonTemplateView,
        Layout,
        Script,
        Partial,
        Macro,
        Style,
        Image,
        Other,
        Directory,
        CreateDirectoryOnly,
        All
    }
}
