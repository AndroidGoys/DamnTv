using System.Text;

using TvApi.Entities;

namespace TvShowsFrontend.Client.Widgets.ViewModels
{
    public class ReleaseWidgetViewModel
    {
        private readonly TvChannelRelease _release;

        public ReleaseWidgetViewModel(TvChannelRelease release)
        {
            _release = release;
     
            TimeStart = release.TimeStart;
            TimeStop = release.TimeStop;

            DateTimeOffset now = DateTimeOffset.Now;
            TimeToEnd = TimeStop - now;
            TimeToStart = TimeStart - now;
            Duration = TimeStop - TimeStart;

            Title = $"{_release.ShowName} " +
                $"({TimeStart.Hour:d2}:{TimeStart.Minute:d2} - " +
                $"{TimeStop.Hour:d2}:{TimeStop.Minute:d2})";
            ProgressTimeLabel = GetProgressLabel();
            Description = _release.Description ?? String.Empty;
            IsStarted = TimeToStart < TimeSpan.Zero;
        }

        public TimeSpan TimeToStart { get; }
        public TimeSpan TimeToEnd { get; }
        public TimeSpan Duration { get; }
        public DateTimeOffset TimeStart { get; }
        public DateTimeOffset TimeStop { get; }

        public string Title { get; }
        public string ProgressTimeLabel { get; }
        public string Description { get; }
        public bool IsStarted { get; }


        public float CalculatieProgress() 
        {
            DateTimeOffset now = DateTimeOffset.Now;
            if (TimeStart - now > TimeSpan.Zero)
                return 0;

            if (TimeToEnd < TimeSpan.Zero)
                return 1;

            return 1 - (float)(TimeToEnd / Duration);
        }

        private string GetProgressLabel() 
        {
            if (TimeToStart > TimeSpan.Zero)
                return $"До начала: {ToRuString(TimeToStart)}";
            else if (TimeToEnd > TimeSpan.Zero)
                return $"До конца: {ToRuString(TimeToEnd)}";
            else
                return $"Завершилось";
        }

        public static string ToRuString(TimeSpan timeSpan)
        {
            StringBuilder builder = new();

            if (timeSpan.Days > 0)
                builder.Append($"{timeSpan.Days} д ");


            if (timeSpan.Hours > 0)
                builder.Append($"{timeSpan.Hours} ч ");

            if (timeSpan.Minutes > 0)
                builder.Append($"{timeSpan.Minutes} мин");

            else if (timeSpan.TotalMinutes < 1)
                builder.Append($"{timeSpan.Minutes} мин");

            return builder.ToString();
        }
    }
}
