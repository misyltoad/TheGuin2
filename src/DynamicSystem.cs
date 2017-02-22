using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using System.Threading;

namespace TheGuin2
{
    public class DynamicSystem
    {
		protected string GetSourceFolder()
		{
			return StaticConfig.Paths.ModulesPath + "/" + module.meta.name + "/hooks";
		}

		private Module module;

        public DynamicSystem(Module module)
        {
			try
			{
				this.module = module;
				watcher = new FileSystemWatcher();
				watcher.IncludeSubdirectories = true;
				watcher.Path = GetSourceFolder();
				watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
							 | NotifyFilters.FileName | NotifyFilters.DirectoryName;
				watcher.Filter = "*.cs";

				watcher.Changed += new FileSystemEventHandler(OnChanged);
				watcher.Created += new FileSystemEventHandler(OnChanged);
				watcher.Renamed += new RenamedEventHandler(OnRenamed);
				watcher.Deleted += new FileSystemEventHandler(OnDeleted);

				watcher.EnableRaisingEvents = true;

				assemblies = new Dictionary<string, Assembly>();

				RecompileAll();
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
        }

        public List<Assembly> GetAssemblies()
        {
            return assemblies.Values.ToList();
        }

        public void RecompileAll()
        {
            string[] allfiles = System.IO.Directory.GetFiles(GetSourceFolder(), "*.cs", System.IO.SearchOption.AllDirectories);
            foreach (var filePath in allfiles)
            {
                AddOrReplaceAssembly(filePath, CompileFile(filePath));
            }
        }

        public Assembly CompileFile(string path)
        {
            string code = "";
            for (int i = 0; i < 100; i++)
            {
                try
                {
                    code = System.IO.File.ReadAllText(path);
                    break;
                }
                catch
                {
                    Thread.Sleep(100);
                }
            }
            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(code);

            string assemblyName = Path.GetRandomFileName();
            MetadataReference[] references = new MetadataReference[]
            {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Enumerable).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(System.Drawing.Image).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(System.Net.WebClient).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(ImageProcessor.ImageFactory).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(ImageProcessor.Imaging.TextLayer).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(ImageProcessor.Plugins.Effects.Ascii).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(ImageProcessor.Plugins.Cair.ImageFactoryExtensions).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(ImageProcessor.Plugins.Cair.Processors.ContentAwareResize).Assembly.Location),
                MetadataReference.CreateFromFile(System.Reflection.Assembly.GetEntryAssembly().Location)
            };
            CSharpCompilation compilation = CSharpCompilation.Create(
            assemblyName,
            syntaxTrees: new[] { syntaxTree },
            references: references,
            options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

            using (var ms = new MemoryStream())
            {
                EmitResult result = compilation.Emit(ms);

                if (!result.Success)
                {
                    IEnumerable<Diagnostic> failures = result.Diagnostics.Where(diagnostic =>
                        diagnostic.IsWarningAsError ||
                        diagnostic.Severity == DiagnosticSeverity.Error);

                    Console.WriteLine("Compilation of: " + path + " failed! Errors: ");
                    foreach (Diagnostic diagnostic in failures)
                    {
                        Console.WriteLine("{0}: {1}", diagnostic.Id, diagnostic.GetMessage());
                    }
                }
                else
                {
                    ms.Seek(0, SeekOrigin.Begin);
                    Assembly assembly = Assembly.Load(ms.ToArray());
                    return assembly;
                }
            }

            return null;
        }

        public void AddOrReplaceAssembly(string name, Assembly assembly)
        {
            if (assembly != null)
            {
                if (assemblies.ContainsKey(name))
                    assemblies.Remove(name);

                assemblies.Add(name, assembly);
            }
        }

        private void OnChanged(object source, FileSystemEventArgs e)
        {
            AddOrReplaceAssembly(e.FullPath, CompileFile(e.FullPath));
        }

        private void OnDeleted(object source, FileSystemEventArgs e)
        {
            if (assemblies.ContainsKey(e.FullPath))
                assemblies.Remove(e.FullPath);
        }

        private void OnRenamed(object source, RenamedEventArgs e)
        {
            if (assemblies.ContainsKey(e.OldFullPath))
                assemblies.Remove(e.OldFullPath);

            AddOrReplaceAssembly(e.FullPath, CompileFile(e.FullPath));
        }

        private Dictionary<string, Assembly> assemblies;
        private FileSystemWatcher watcher;
    }
}
