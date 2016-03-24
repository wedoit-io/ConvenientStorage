namespace WebApp
{
    using Cassette;
    using Cassette.Stylesheets;

    /// <summary>
    /// Configures the Cassette asset bundles for the web application.
    /// </summary>
    public class CassetteBundleConfiguration : IConfiguration<BundleCollection>
    {
        public void Configure(BundleCollection bundles)
        {
            bundles.Add<StylesheetBundle>("content/application.css");

            bundles.AddUrlWithAlias<StylesheetBundle>(
                "https://cdnjs.cloudflare.com/ajax/libs/normalize/3.0.3/normalize.min.css",
                "~/normalize.css");
        }
    }
}
