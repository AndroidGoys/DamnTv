using DamnTv.Frontend.PreviewDesign.Models;

using SkiaSharp;

namespace DamnTv.Frontend.PreviewDesign
{
    public static class BuilderExtension
    {
        public static WebApplicationBuilder UseSkiaPreviewDesigner(this WebApplicationBuilder builder)
        {
            string pathToFonts = Path.Combine(AppContext.BaseDirectory, @"Assets/Fonts/OpenSans/");
            string openSansBold = Path.Combine(pathToFonts, "OpenSans-Bold.ttf");
            string openSansLight = Path.Combine(pathToFonts, "OpenSans-Light.ttf");

            builder.Services.AddSingleton(SkiaFonts.LoadFromFiles(openSansBold, openSansLight));
            builder.Services.AddTransient<IPreviewBuilder, SkiaPreviewBuilder>();

            return builder;
        }
    }
}
