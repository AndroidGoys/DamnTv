

using SkiaSharp;

using TvShowsFrontend.PreviewDesign.Models;

namespace TvShowsFrontend.PreviewDesign.Models;

public class SkiaPreviewBuilder : IPreviewBuilder
{
    private float _scaling = 1.0f;
    private Stream? _templateImage = null;

    private string? _channelName = null;
    private Stream? _channelImage = null;
    
    private string? _showName = null;
    private string? _showDescription = null;
    private float? _showAssessment = null;
    private float? _progress = null;

    public Stream Build()
    {
        int height = (int)(600 * _scaling);
        int width = (int)(1200 * _scaling);
        int padding = (int)(50 * _scaling);

        using SKBitmap bitmap = new(width, height);
        using SKCanvas canvas = new(bitmap);

        int channelImageSize = (int)(125 * _scaling);
        SKRect channelImagePosition = new(
            padding,
            padding,
            padding + channelImageSize,
            padding + channelImageSize
        );

        SKPoint channelNamePosition = new(
            channelImagePosition.Right + 25,
            padding
        );

        int channelNameMargin = (int)(25 * _scaling);

        if (_channelImage != null)
        { 
            using SKImage channelImage = SKImage.FromEncodedData(_channelImage);
            canvas.DrawImage(channelImage, channelImagePosition);
        }


        if (_channelName != null)
        {
            SKFontStyle style = new(700, 80, SKFontStyleSlant.Upright);
            SKTypeface typeface = SKTypeface.FromFamilyName("Open Sans", style);
            SKFont font = new(typeface);
            SKTextBlob text = SKTextBlob.Create(_channelName, font) ?? throw new NullReferenceException();
            canvas.DrawText(text.ToString(), channelNamePosition, SKTextAlign.Left, font, new SKPaint());
        }

        return bitmap.Encode(SKEncodedImageFormat.Png, 1).AsStream();
    }

    public IPreviewBuilder WithChannelName(string name)
    {
        _channelName = name;
        return this;
    }

    public IPreviewBuilder WithProgress(float progress)
    {
        _progress = progress;
        return this;
    }

    public IPreviewBuilder WithScaling(float size)
    {
        _scaling = size;
        return this;
    }

    public IPreviewBuilder WithShowAssessment(float assessment)
    {
        _showAssessment = assessment;
        return this;
    }

    public IPreviewBuilder WithShowDescription(string description)
    {
        _showDescription = description;
        return this;
    }

    public IPreviewBuilder WithShowName(string title)
    {
        _showName = title;
        return this;
    }

    public IPreviewBuilder WithTemplateImage(Stream image)
    {
        _templateImage = image;
        return this;
    }

    public IPreviewBuilder WithChannelImage(Stream imageStream)
    {
        _channelImage = imageStream;
        return this;
    }
}