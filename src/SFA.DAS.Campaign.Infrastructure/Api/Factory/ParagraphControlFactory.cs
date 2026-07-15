using System;
using System.Collections.Generic;
using System.Linq;
using SFA.DAS.Campaign.Domain.Content;
using SFA.DAS.Campaign.Domain.Content.HtmlControl;
using SFA.DAS.Campaign.Infrastructure.Api.Converters;
using DomainVideoTranscript = SFA.DAS.Campaign.Domain.Content.HtmlControl.VideoTranscript;

namespace SFA.DAS.Campaign.Infrastructure.Api.Factory
{
    public class ParagraphControlFactory : IHtmlControlFactory
    {
        public IHtmlControl Create(Item control)
        {
            var para = new Paragraph
            {
                Content = control.Values,
                VideoTranscripts = control.VideoTranscripts?
                    .Select(transcript => new DomainVideoTranscript
                    {
                        VideoName = transcript.VideoName,
                        Text = transcript.Text
                    }).ToList() ?? new List<DomainVideoTranscript>()
            };

            return para;
        }

        public bool IsValid(Item control)
        {
            var firstValue = control.Values?.FirstOrDefault() ?? string.Empty;

            if (string.Compare(control.Type, "paragraph", StringComparison.OrdinalIgnoreCase) == 0 && !control.TableValue.Any() && !firstValue.StartsWith("https://www.youtube.com"))
            {
                return true;
            }

            return false;
        }
    }
}