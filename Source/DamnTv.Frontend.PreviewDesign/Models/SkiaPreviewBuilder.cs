using SkiaSharp;

namespace DamnTv.Frontend.PreviewDesign.Models;

public class SkiaPreviewBuilder(SkiaFonts fonts) : IPreviewBuilder
{
    private float _scaling = 1.0f;
    private Stream? _templateImage = null;

    private string? _channelName = null;
    private Stream? _channelImage = null;

    private string? _showName = null;
    private string? _showDescription = null;
    private float? _showAssessment = null;
    private float _progress = 0;

    private int ToScale(int value) => (int)(value * _scaling);
    private float ToScale(float value) => value * _scaling;

    public Stream Build()
    {
        int height = ToScale(600);
        int width = ToScale(1200);
        int padding = ToScale(50);

        using SKBitmap bitmap = new(width, height);
        using SKCanvas canvas = new(bitmap);
        using SKPaint paint = new();
        using SKFont openSansBoldFont = new(fonts.OpenSansBold);

        canvas.DrawColor(new(0xff_ff_ff_ff));

        #region DrawChannelImage
        int channelImageSize = ToScale(125);
        SKRect channelImagePosition = new(
            padding,
            padding,
            padding + channelImageSize,
            padding + channelImageSize
        );

        if (_channelImage != null)
        {
            using SKImage channelImage = SKImage.FromEncodedData(_channelImage);
            canvas.DrawImage(channelImage, channelImagePosition);
        }
        #endregion

        #region DrawChannelName
        openSansBoldFont.Size = ToScale(80.0f);
        SKPoint channelNamePosition = new(
            channelImagePosition.Right + ToScale(25),
            padding + openSansBoldFont.Size / 2 + (int)(channelImageSize / 2.5)
        );
        float channelNameWidth = width - channelNamePosition.X - padding;
        if (_channelName != null)
        {
            paint.Color = new SKColor(0, 0, 0, 255);
            DrawTextLine(canvas, _channelName, channelNamePosition, channelNameWidth, openSansBoldFont, paint);
        }
        #endregion

        #region DrawShowInfoContainer
        SKRect showInfoContainerPosition = new(
            padding,
            channelImagePosition.Bottom + ToScale(50),
            width - padding,
            height - padding
        );


        using SKRoundRect showInfoContainer = new(
            showInfoContainerPosition,
            10
        );

        using (SKPaint blackShadowPaint = new())
        {
            blackShadowPaint.Style = SKPaintStyle.StrokeAndFill;
            blackShadowPaint.StrokeWidth = 10;
            blackShadowPaint.ImageFilter = SKImageFilter.CreateDropShadowOnly(
                dx: 0, dy: 0,
                sigmaX: ToScale(5.0f),
                sigmaY: ToScale(5.0f),
                color: new(0, 0, 0, 70)
            );

            canvas.DrawRoundRect(showInfoContainer, blackShadowPaint);
        }

        paint.Color = new(0xFF_FF_FF_FF);
        canvas.DrawRoundRect(showInfoContainer, paint);
        #endregion

        #region DrawShowAssessment
        openSansBoldFont.Size = ToScale(60.0f);
        SKPoint showAssessmentPosition = new SKPoint(
            showInfoContainerPosition.Left + ToScale(965),
            showInfoContainerPosition.Top + openSansBoldFont.Size + ToScale(18)
        );

        if (_showAssessment.HasValue)
        {
            paint.Color = new(0x00, 0x82, 0x1D, 0xff);
            string assessmentText = _showAssessment.Value.ToString();
            canvas.DrawText(assessmentText, showAssessmentPosition, SKTextAlign.Left, openSansBoldFont, paint);
        }
        #endregion

        #region DrawShowName
        openSansBoldFont.Size = ToScale(40.0f);
        SKPoint showNamePosition = new SKPoint(
            showInfoContainerPosition.Left + ToScale(35.0f),
            showInfoContainerPosition.Top + ToScale(32.0f) + openSansBoldFont.Size
        );
        float showNameWidth = showAssessmentPosition.X - showNamePosition.X;
        if (_showName != null)
        {
            paint.Color = new(0xFF_00_00_00);
            DrawTextLine(canvas, _showName, showNamePosition, showNameWidth, openSansBoldFont, paint);
        }
        #endregion

        #region DrawShowDescription

        using SKFont openSansLightFont = new(fonts.OpenSansLight);
        openSansLightFont.Size = ToScale(40.0f);

        SKRect descriptionPosition = new(
            showInfoContainerPosition.Left + ToScale(35.0f),
            showNamePosition.Y + openSansLightFont.Size + ToScale(12.0f),
            showInfoContainerPosition.Right - ToScale(35.0f),
            showInfoContainerPosition.Bottom - ToScale(35.0f)
        );

        if (_showDescription != null)
        {
            paint.Color = new(0xff_00_00_00);
            DrawDescription(canvas, _showDescription, descriptionPosition, openSansLightFont, paint);
        }
        #endregion

        #region DrawProgress
        paint.Color = new SKColor(0xff_9E_CE_6E);
        paint.Style = SKPaintStyle.Stroke;
        paint.StrokeWidth = ToScale(30);
        int progressWidth = width;///(int)(showInfoContainerPosition.Right - showInfoContainerPosition.Left);

        SKPoint progressStartPoint = new(
            0,
            height - ToScale(15)
        );
        SKPoint progressEndPoint = new(
            progressStartPoint.X + progressWidth,
            progressStartPoint.Y
        );
        SKPoint progressActiveEndPoint = new(
            progressStartPoint.X + (int)(progressWidth * _progress),
            progressStartPoint.Y
        );

        canvas.DrawLine(progressStartPoint, progressEndPoint, paint);
        paint.Color = new(0xff_00_87_1E);
        canvas.DrawLine(progressStartPoint, progressActiveEndPoint, paint);
        #endregion

        return bitmap.Encode(SKEncodedImageFormat.Png, 1).AsStream();
    }

    private void DrawTextLine(
        SKCanvas canvas,
        string text,
        SKPoint position,
        float width,
        SKFont font,
        SKPaint paint)
    {
        int breakLength = font.BreakText(text, width, paint);

        if (breakLength < text.Length)
        {
            breakLength = Math.Max(0, breakLength - 3);
            text = text[0..breakLength] + "...";
        }

        canvas.DrawText(text, position, SKTextAlign.Left, font, paint);
    }

    private void DrawDescription(
        SKCanvas canvas,
        string text,
        SKRect position,
        SKFont font,
        SKPaint paint
    )
    {
        float spaceLen = font.MeasureText(" ", paint);
        float lineMaxWidth = position.Width;
        int linesCount = (int)(position.Height / font.Size);
        SKPoint linePosition = new(position.Left, position.Top);

        LinkedList<string> words = new(text.Split('\n', ' ', '\t'));
        List<string> lineWords = new(5);

        for (int i = 1; i <= linesCount; i++)
        {
            float lineWidth = 0;

            if (words.Count == 0)
                break;

            do
            {
                string word = words.First!.Value;
                lineWidth += spaceLen + font.MeasureText(word);

                if (lineWidth > lineMaxWidth)
                {
                    if (i == linesCount)
                    {
                        lineWords[^1] = "...";
                    }
                    break;
                }

                words.RemoveFirst();
                lineWords.Add(word);
            } while (words.Count != 0);


            string line = string.Join(" ", lineWords);
            canvas.DrawText(line, linePosition, SKTextAlign.Left, font, paint);

            lineWords.Clear();
            linePosition.Y += font.Size;
        }
    }

    public IPreviewBuilder WithChannelName(string name)
    {
        _channelName = name;
        return this;
    }

    public IPreviewBuilder WithProgress(float progress)
    {
        _progress = MathF.Min(MathF.Max(progress, 0), 1);
        return this;
    }

    public IPreviewBuilder WithScaling(float size)
    {
        _scaling = MathF.Max(MathF.Min(size, 3), 0);
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