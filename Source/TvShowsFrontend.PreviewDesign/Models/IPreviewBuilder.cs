using System.Runtime.CompilerServices;
using TvApi.Entities;

namespace TvShowsFrontend.PreviewDesign.Models
{
    public interface IPreviewBuilder
    {
        IPreviewBuilder WithScaling(float size);
        IPreviewBuilder WithChannelName(string name);
        IPreviewBuilder WithChannelImage(Stream image);
        IPreviewBuilder WithTemplateImage(Stream image);
        IPreviewBuilder WithShowName(string name);
        IPreviewBuilder WithShowDescription(string description);
        IPreviewBuilder WithShowAssessment(float assessment);
        IPreviewBuilder WithProgress(float progress);
        Stream Build();
    }
}
