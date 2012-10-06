using System;
using System.Collections.Generic;
using System.Text;

namespace TestConsoleApp.EncodingCheck
{
    /// <summary>
    /// Detects text encodings in binary byte arrays.
    /// </summary>
    public static class EncodingDetector
    {
        /// <summary>
        /// Defines the default encoding to "Detect" when an encoding can't be detected.
        /// </summary>
        public static string DefaultEncoding = "ISO-8859-1";

        private static Dictionary<StringMatcher, Encoding> Encodings = null;

        /// <summary>
        /// The number of bytes to sniff when looking for an encoding in an xml prolog
        /// </summary>
        private static int PreambleLength = 200;

        /// <summary>
        /// Initializes the <see cref="EncodingDetector"/> class.
        /// </summary>
        static EncodingDetector()
        {
            Encodings = new Dictionary<StringMatcher, Encoding>();

            // Single byte character set sniffers
            Encodings.Add(new StringMatcher("iso-8859-1"), Encoding.GetEncoding("ISO-8859-1"));
            Encodings.Add(new StringMatcher("utf-8"), Encoding.UTF8);
            Encodings.Add(new StringMatcher("ISO-8859-1"), Encoding.GetEncoding("ISO-8859-1"));
            Encodings.Add(new StringMatcher("UTF-8"), Encoding.UTF8);
            Encodings.Add(new StringMatcher("utf-16"), Encoding.Unicode);
            Encodings.Add(new StringMatcher("UTF-16"), Encoding.Unicode);
            Encodings.Add(new StringMatcher("UTF-7"), Encoding.UTF7);
            Encodings.Add(new StringMatcher("utf-7"), Encoding.UTF7);
            Encodings.Add(new StringMatcher("ASCII"), Encoding.ASCII);
            Encodings.Add(new StringMatcher("ascii"), Encoding.ASCII);
        }

        /// <summary>
        /// Gets the encoding for the buffer of bytes.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        /// <returns>
        /// The encoding that has been detected, or the default encoding if it could not be found.
        /// </returns>
        public static string GetEncoding(byte[] buffer)
        {
            return GetEncoding(buffer, null);
        }

        /// <summary>
        /// Gets the encoding for based on the encoding hint.
        /// </summary>
        /// <param name="encodingHint">The encoding hint.</param>
        /// <returns>
        /// The encoding that has been detected, or the default encoding if it could not be found.
        /// </returns>
        public static Encoding GetEncoding(string encodingHint)
        {
            // if a non-default encoding has been asked for
            if (!String.IsNullOrEmpty(encodingHint) && !encodingHint.Equals("default", StringComparison.OrdinalIgnoreCase))
            {
                return Encoding.GetEncoding(encodingHint);
            }

            //not found, so return the default encoding
            return Encoding.GetEncoding(DefaultEncoding);
        }

        /// <summary>
        /// Gets the encoding for the buffer of bytes.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        /// <param name="contentType">Http Content Type of the buffer.</param>
        /// <returns>
        /// The encoding that has been detected, or the default encoding if it could not be found.
        /// </returns>
        public static string GetEncoding(byte[] buffer, ContentType contentType)
        {
            // attempt to figure out encoding by the byte order mark
            Encoding encoding = GetEncodingByBOM(ref buffer);
            if (encoding != null)
            {
                return encoding.WebName;
            }

            // attempt to detect whether it's a unicode based encoding that has
            // fixed width characters
            encoding = AttempDetectUnicodeEncoding(ref buffer);
            if (encoding != null)
            {
                return encoding.WebName;
            }

            // attempt to figure it out by looking for the xml processing instruction
            //encoding = GetEncodingByXmlProcessingInstruction(ref buffer);
            //if (encoding != null)
            //{
            //    return encoding;
            //}

            if (contentType != null)
            {
                // finally go back to the HTTP content type and see what that tells us
                encoding = GetEncodingByContentType(contentType);
                if (encoding != null)
                {
                    return encoding.WebName;
                }
            }

            return "unknown";
        }

        /// <summary>
        /// Detects the unicode encoding.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        /// <remarks>
        /// Sniffs the byte array to detect whether the ordering of bytes matches something
        /// that we know about.<br />
        /// <br />
        /// This will only detect Big or Little Endian Unicode, or UTf-32 which all have
        /// fixed character widths of 2 or 4 bytes per character.
        /// </remarks>
        /// <returns>An encoding object representing the encoding detected, otherwise null.</returns>
        private static Encoding AttempDetectUnicodeEncoding(ref byte[] buffer)
        {
            Encoding encoding = null;

            if (buffer[0] == 0)
            {
                // if the first byte is zero, then we must be big endian unicode, because no other 
                // encoding is going to have a first byte of zero.
                encoding = Encoding.BigEndianUnicode;
            }
            else if (buffer[1] == 0 && buffer[2] == 0 && buffer[3] == 0)
            {
                // if the 2nd, 3rd and 4th bytes are zero, then we are most likely to be
                // a utf-32 message
                encoding = Encoding.UTF32;
            }
            else if (buffer[1] == 0)
            {
                // if only the second byte is zero, then in all likelyhood we are going to be
                // Little Endian unicode
                encoding = Encoding.Unicode;
            }

            return encoding;
        }

        /// <summary>
        /// Attempts to get the encoding of a buffer of bytes based on the content at the beginning
        /// of the message.
        /// </summary>
        /// <param name="buffer">The buffer to analyse.</param>
        /// <returns>
        /// An Encoding object representing the encoding, or null if encoding was not deteceted.
        /// </returns>
        private static Encoding GetEncodingByXmlProcessingInstruction(ref byte[] buffer)
        {
            // create a slice of our array which will be big enough to contain the preamble
            // (which must be first in the stream, according to the xml spec)
            byte[] preAmble = new byte[PreambleLength];
            for (int i = 0; i < PreambleLength; i++)
            {
                preAmble[i] = buffer[i];
            }

            // and search it for the various encodings.
            foreach (KeyValuePair<StringMatcher, Encoding> encoding in Encodings)
            {
                if (encoding.Key.BoyerMooreMatchFirst(buffer) > 0)
                {
                    return encoding.Value;
                }
            }

            return null;
        }

        /// <summary>
        /// Attempts to get the encoding of a buffer of bytes based on a byte order mark.
        /// </summary>
        /// <param name="buffer">The buffer to analyse.</param>
        /// <returns>
        /// An Encoding object representing the encoding, or null if encoding was not deteceted.
        /// </returns>
        private static Encoding GetEncodingByBOM(ref byte[] buffer)
        {
            if (buffer[0] == 0xEF && buffer[1] == 0xBB && buffer[2] == 0xBF)
            {
                return Encoding.UTF8;
            }
            else if (buffer[0] == 0xFE && buffer[1] == 0xFF)
            {
                return Encoding.BigEndianUnicode;
            }
            else if (buffer[0] == 0xFF && buffer[1] == 0xFE)
            {
                return Encoding.Unicode;
            }
            else if (buffer[0] == 0 && buffer[1] == 0 && buffer[2] == 0xFE && buffer[3] == 0xFF)
            {
                return Encoding.UTF32;
            }
            else if (buffer[0] == 0x2B && buffer[1] == 0x2F && buffer[2] == 0x76)
            {
                return Encoding.UTF7;
            }
            return null;
        }

        /// <summary>
        /// Gets encoding based on the content type.
        /// </summary>
        /// <returns>The encoding represented by the message.</returns>
        private static Encoding GetEncodingByContentType(ContentType contentType)
        {
            Encoding encoding = null;

            if (contentType != null && !String.IsNullOrEmpty(contentType.CharSet))
            {

                encoding = Encoding.GetEncoding(contentType.CharSet);
            }

            return encoding;
        }
    }
}
