using Marketplace.Framework;
using System;
using System.Text.RegularExpressions;

namespace Marketplace.Domain
{
    public class ClassifiedAdTitle : Value<ClassifiedAdTitle>
    {
        public static ClassifiedAdTitle FromString(string title)
        {
            return new ClassifiedAdTitle(title);
        }

        public static ClassifiedAdTitle FromHtml(string html)
        {
            var supportedTagsReplaced = html
                .Replace("<i>", "*")
                .Replace("</i>", "*")
                .Replace("<b>", "**")
                .Replace("</b>", "**");

            return new ClassifiedAdTitle(Regex.Replace(
                supportedTagsReplaced, "<.*?>", string.Empty));
        }

        private string _title;

        private ClassifiedAdTitle(string title)
        {
            if (title.Length > 100)
            {
                throw new ArgumentOutOfRangeException("Title cannot be longer than 100 characters", nameof(title));
            }

            _title = title;
        }
    }
}
