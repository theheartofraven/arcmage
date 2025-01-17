// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using Microsoft.Toolkit.Parsers.Core;
using Microsoft.Toolkit.Parsers.Markdown.Helpers;

namespace Microsoft.Toolkit.Parsers.Markdown.Inlines
{
    /// <summary>
    /// Represents a span containing emoji symbol.
    /// </summary>
    public partial class EmojiInline : MarkdownInline, IInlineLeaf
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmojiInline"/> class.
        /// </summary>
        public EmojiInline()
            : base(MarkdownInlineType.Emoji)
        {
        }

        /// <summary>
        /// Returns the chars that if found means we might have a match.
        /// </summary>
        internal static void AddTripChars(List<InlineTripCharHelper> tripCharHelpers)
        {
            tripCharHelpers.Add(new InlineTripCharHelper() { FirstChar = ':', Method = InlineParseMethod.Emoji });
        }

        internal static InlineParseResult Parse(string markdown, int start, int maxEnd)
        {
            if (start >= maxEnd - 1)
            {
                return null;
            }

            // Check the start sequence.
            string startSequence = markdown.Substring(start, 1);
            if (startSequence != ":")
            {
                return null;
            }

            // Find the end of the span.
            var innerStart = start + 1;
            int innerEnd = Common.IndexOf(markdown, startSequence, innerStart, maxEnd);
            if (innerEnd == -1)
            {
                return null;
            }

            // The span must contain at least one character.
            if (innerStart == innerEnd)
            {
                return null;
            }

            // The first character inside the span must NOT be a space.
            if (ParseHelpers.IsMarkdownWhiteSpace(markdown[innerStart]))
            {
                return null;
            }

            // The last character inside the span must NOT be a space.
            if (ParseHelpers.IsMarkdownWhiteSpace(markdown[innerEnd - 1]))
            {
                return null;
            }

            var emojiName = markdown.Substring(innerStart, innerEnd - innerStart);

            if (_emojiCodesDictionary.TryGetValue(emojiName, out var emojiCode))
            {
                var result = new EmojiInline { Text = emojiName, Type = MarkdownInlineType.Emoji, Code = emojiCode };
                return new InlineParseResult(result, start, innerEnd + 1);
            }
            else
            {
                var result = new EmojiInline { Text = emojiName, Type = MarkdownInlineType.Emoji, Code = -1 };
                return new InlineParseResult(result, start, innerEnd + 1);
            }

            return null;
        }

        public int Code { get; set; }

        /// <inheritdoc/>
        public string Text { get; set; }
    }
}