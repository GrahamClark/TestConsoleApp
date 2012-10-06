using System;

namespace TestConsoleApp.EncodingCheck
{
    /// <summary>
    /// A class that represents a Http Content-Type header
    /// </summary>
    public class ContentType
    {
        private string mediaType = String.Empty;

        /// <summary>
        /// Gets or sets the type of the media represented by the content type.
        /// </summary>
        /// <value>The type of the media represented by the content type.</value>
        public string MediaType
        {
            get { return mediaType; }
            set { mediaType = value; }
        }

        private string charSet = String.Empty;

        /// <summary>
        /// Gets or sets the character set represented by the content type.
        /// </summary>
        /// <value>The character set represented by the content type.</value>
        public string CharSet
        {
            get { return charSet; }
            set { charSet = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentType"/> class.
        /// </summary>
        /// <param name="contentType">the HTTP ContentType header to parse.</param>
        /// <remarks>
        /// Content types are in one of the following formats
        /// content-type := media-type
        /// or
        /// content-type := media-type + '; charset=' + charset
        /// </remarks>
        public ContentType(string contentType)
        {
            if (String.IsNullOrEmpty(contentType))
            {
                throw new ArgumentNullException("contentType", "The content type supplied was null or empty");
            }

            contentType = contentType.Trim();
            if (contentType.Length == 0)
            {
                throw new ArgumentException("contentType", "The content type supplied had an effective trimmed zero length");
            }

            int sepIndex = contentType.IndexOf(';');

            // extract the media type
            if (sepIndex == -1)
            {
                mediaType = contentType;
            }
            else
            {
                mediaType = contentType.Substring(0, sepIndex).Trim();
            }

            // if we have more to process, try and figure out the content type
            if (sepIndex != contentType.Length)
            {

                int equalsIndex = contentType.IndexOf('=', sepIndex);

                // if there was no equals found, lets assume that everything after the ; is
                // the charset, it's not valid via the http spec, but possible
                if (equalsIndex == -1)
                {
                    charSet = contentType.Substring(sepIndex + 1);
                }
                else
                {
                    // otherwise lets parse the charset
                    charSet = contentType.Substring(equalsIndex + 1).Trim();
                }
            }
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            if (String.IsNullOrEmpty(charSet))
            {
                return mediaType;
            }
            else
            {
                return String.Format("{0}; charset={2}", mediaType, charSet);
            }
        }
    }
}
