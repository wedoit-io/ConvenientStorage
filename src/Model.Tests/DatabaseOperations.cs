namespace Model.Tests
{
    using System;
    using System.IO;
    using System.Reflection;
    using Model.DataAccessLayer.Contexts;
    using Model.Seeding;
    using Model.Tests.Properties;
    using Xunit;

    public class DatabaseOperations
    {
        /// <summary>
        /// You can manually run this task to manually seed database.
        /// </summary>
        [Fact]
        [Trait("Category", "NoAutoRun")]
        public void Seed()
        {
            SetDataDirectory(Settings.Default.ProjectDirectoryNameContainingDatabase);

            var generator = new SeedGenerator(CreateDatabaseContext());
            generator.Generate();
        }

        #region /// internal ///////////////////////////////////////////////////

        // This allows us using the |DataDirectory| Substitution String
        // (Ref.: https://msdn.microsoft.com/en-us/library/ms254504(v=vs.110).aspx)
        private static void SetDataDirectory(string projectName)
        {
            var executingAssembly = Assembly.GetExecutingAssembly();
            var assemblyDirectory = new DirectoryInfo((new Uri(executingAssembly.CodeBase)).AbsolutePath);

            // ReSharper disable PossibleNullReferenceException
            var baseDirectory = assemblyDirectory.Parent.Parent.Parent.Parent;

            AppDomain.CurrentDomain.SetData(
                "DataDirectory",
                Path.Combine(baseDirectory.FullName, projectName, "App_Data"));

            // ReSharper restore PossibleNullReferenceException
        }

        private static SchoolContext CreateDatabaseContext()
        {
            var context = new SchoolContext();
            context.Configuration.ValidateOnSaveEnabled = false;

            return context;
        }

        #endregion
    }
}
