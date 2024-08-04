using SkiaSharp;

namespace DamnTv.Frontend.PreviewDesign.Models
{
    public record SkiaFonts(
        SKTypeface OpenSansBold,
        SKTypeface OpenSansLight
    ) : IDisposable {
        
        internal static SkiaFonts LoadFromFiles(string pathToBold, string pathToLight) 
        {
            //using SKFontStyle boldFontStyle = new(SKFontStyleWeight.Bold, SKFontStyleWidth.Normal, SKFontStyleSlant.Upright);
            //SKTypeface openSansBoldTypeface = SKTypeface.FromFamilyName("Open Sans", boldFontStyle);    

            SKTypeface? openSansBoldTypeface = SKTypeface.FromFile(pathToBold);

            //using SKFontStyle lightFontStyle = new(SKFontStyleWeight.Light, SKFontStyleWidth.Normal, SKFontStyleSlant.Upright);
            //SKTypeface openSansLightTypeface = SKTypeface.FromFamilyName("Open Sans", lightFontStyle);

            SKTypeface? openSansLightTypeface = SKTypeface.FromFile(pathToLight);

            ArgumentNullException.ThrowIfNull(openSansBoldTypeface);
            ArgumentNullException.ThrowIfNull(openSansLightTypeface);

            return new SkiaFonts(openSansBoldTypeface, openSansLightTypeface);
        }

        public void Dispose()
        {
            OpenSansBold.Dispose();
            OpenSansLight.Dispose();
        }

        ~SkiaFonts() {
            Dispose();
        }
    }
}
