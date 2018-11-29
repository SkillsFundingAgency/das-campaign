using System;
using System.Text;

namespace SFA.DAS.Campaign.Web.Models
{
    public class ButtonViewModel
    {
        private ButtonStyle _buttonStyle;
        private string _href;
        private string _text;
        private string _styleClass;

        public ButtonViewModel(ButtonStyle buttonStyle = ButtonStyle.Primary)
        {
            Element = "input";
            _buttonStyle = buttonStyle;

            switch (buttonStyle)
            {
                case ButtonStyle.Primary:
                    break;
                case ButtonStyle.PrimaryInverted:
                    _styleClass = "button-inverted";
                    break;
                case ButtonStyle.Apprentice:
                    _styleClass = "button-apprentice";
                    break;
                case ButtonStyle.Employer:
                    _styleClass = "button-employer";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(buttonStyle), buttonStyle, null);
            }
        }

        public ButtonType Type { get; set; }

        public bool Shadow { get; set; }
        public bool Sparks { get; set; }

        public string Text
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Html))
                {
                    return _text;
                }
                else
                {
                    return Html;
                }
            }
            set => _text = value;
        }

        public string Href
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_href))
                {
                    return "#";
                }
                else
                {
                    return _href;
                }
            }
            set => _href = value;
        }

        public string Element { get; set; }
        public string Html { get; set; }
        public string Name { get; set; }

        public string Class { get; set; }

        
        public string Value { get; set; }

        public string GetClasses()
        {
            var sb = new StringBuilder();

            sb.Append($"button ");

            if (! string.IsNullOrWhiteSpace(_styleClass))
            {
                sb.Append($"{_styleClass} ");
            }
            if (!string.IsNullOrWhiteSpace(Class))
            {
                sb.Append($"button--{Class} ");
            }


            if (Sparks)
            {
                sb.Append($"button--sparks ");
            }

            if (Shadow)
            {
                sb.Append($"button--shadow ");
            }

            return sb.ToString();
        }

    }
}