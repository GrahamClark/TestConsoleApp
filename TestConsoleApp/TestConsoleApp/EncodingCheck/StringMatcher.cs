using System;
using System.Collections.Generic;
using System.Text;

namespace TestConsoleApp.EncodingCheck
{
    /// <summary>
    /// Fast string matching class based on Boyer-Moore
    /// </summary>
    /// <remarks>
    /// Stolen shamelessly from "Handbook of exact string-matching algorithms"
    ///   by Christian Charras and Thierry Lecroq
    ///   chapter 15
    /// http://www-igm.univ-mlv.fr/~lecroq/string/node15.html#SECTION00150
    /// </remarks>
    public class StringMatcher
    {
        private readonly int[] badCharacterShift;
        private readonly int[] goodSuffixShift;
        private readonly byte[] pattern;
        private readonly int[] suffixes;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pattern">Pattern for search</param>
        public StringMatcher(string pattern)
        {
            /* Preprocessing */
            this.pattern = Encoding.ASCII.GetBytes(pattern);
            this.badCharacterShift = BuildBadCharacterShift(this.pattern);
            this.suffixes = FindSuffixes(this.pattern);
            this.goodSuffixShift = BuildGoodSuffixShift(this.pattern, this.suffixes);
        }

        #region Preparation routines

        private static int[] BuildBadCharacterShift(byte[] pattern)
        {
            #region Algorithm in C, from http://www-igm.univ-mlv.fr/~lecroq/string/node14.html#SECTION00140

            // void preBmBc(char *x, int m, int bmBc[]) 
            // {
            //    int i;
            //   
            //    for (i = 0; i < ASIZE; ++i)
            //       bmBc[i] = m;
            //    for (i = 0; i < m - 1; ++i)
            //       bmBc[x[i]] = m - i - 1;
            // }    

            #endregion

            int[] badCharacterShift = new int[256];

            for (int c = 0; c < badCharacterShift.Length; ++c)
            {
                badCharacterShift[c] = pattern.Length;
            }
            for (int i = 0; i < pattern.Length - 1; ++i)
            {
                badCharacterShift[pattern[i]] = pattern.Length - i - 1;
            }

            return badCharacterShift;
        }

        private static int[] FindSuffixes(byte[] pattern)
        {
            #region Algorithm in C, from http://www-igm.univ-mlv.fr/~lecroq/string/node14.html#SECTION00140

            // void suffixes(char* x, int m, int* suff)
            // {
            //     int f, g, i;
            //
            //     suff[m - 1] = m;
            //     g = m - 1;
            //     for (i = m - 2; i >= 0; --i) {
            //         if (i > g && suff[i + m - 1 - f] < i - g)
            //             suff[i] = suff[i + m - 1 - f];
            //         else {
            //             if (i < g)
            //                 g = i;
            //             f = i;
            //             while (g >= 0 && x[g] == x[g + m - 1 - f])
            //                 --g;
            //             suff[i] = f - g;
            //         }
            //     }
            // }

            #endregion

            int f = 0, g;

            int patternLength = pattern.Length;
            int[] suffixes = new int[pattern.Length + 1];

            suffixes[patternLength - 1] = patternLength;
            g = patternLength - 1;
            for (int i = patternLength - 2; i >= 0; --i)
            {
                if (i > g && suffixes[i + patternLength - 1 - f] < i - g)
                {
                    suffixes[i] = suffixes[i + patternLength - 1 - f];
                }
                else
                {
                    if (i < g)
                    {
                        g = i;
                    }
                    f = i;
                    while (g >= 0 && (pattern[g] == pattern[g + patternLength - 1 - f]))
                    {
                        --g;
                    }
                    suffixes[i] = f - g;
                }
            }

            return suffixes;
        }

        private static int[] BuildGoodSuffixShift(byte[] pattern, int[] suff)
        {
            #region Algorithm in C, from http://www-igm.univ-mlv.fr/~lecroq/string/node14.html#SECTION00140

            // void preBmGs(char *x, int m, int bmGs[]) 
            // {
            //    int i, j, suff[XSIZE];
            // 
            //    suffixes(x, m, suff);
            // 
            //    for (i = 0; i < m; ++i)
            //       bmGs[i] = m;
            //    j = 0;
            //    for (i = m - 1; i >= 0; --i)
            //       if (suff[i] == i + 1)
            //          for (; j < m - 1 - i; ++j)
            //             if (bmGs[j] == m)
            //                bmGs[j] = m - 1 - i;
            //    for (i = 0; i <= m - 2; ++i)
            //       bmGs[m - 1 - suff[i]] = m - 1 - i;
            // }

            #endregion

            int patternLength = pattern.Length;
            int[] goodSuffixShift = new int[pattern.Length + 1];

            for (int i = 0; i < patternLength; ++i)
            {
                goodSuffixShift[i] = patternLength;
            }
            int j = 0;
            for (int i = patternLength - 1; i >= -1; --i)
            {
                if (i == -1 || suff[i] == i + 1)
                {
                    for (; j < patternLength - 1 - i; ++j)
                    {
                        if (goodSuffixShift[j] == patternLength)
                        {
                            goodSuffixShift[j] = patternLength - 1 - i;
                        }
                    }
                }
            }
            for (int i = 0; i <= patternLength - 2; ++i)
            {
                goodSuffixShift[patternLength - 1 - suff[i]] = patternLength - 1 - i;
            }

            return goodSuffixShift;
        }

        #endregion

        #region Horspool

        // match using Horspool
        public IEnumerable<int> HorspoolMatchAll(byte[] text, int startIndex)
        {
            #region Algorithm in C, from http://www-igm.univ-mlv.fr/~lecroq/string/node18.html#SECTION00180

            // void HORSPOOL(char *x, int m, char *y, int n) {
            //    int j, bmBc[ASIZE];
            //    char c;
            //
            //    /* Preprocessing */
            //    preBmBc(x, m, bmBc);
            //
            //    /* Searching */
            //    j = 0;
            //    while (j <= n - m) {
            //       c = y[j + m - 1];
            //       if (x[m - 1] == c && memcmp(x, y + j, m - 1) == 0)
            //          OUTPUT(j);
            //       j += bmBc[c];
            //    }
            // }

            #endregion

            int patternLength = this.pattern.Length;
            int textLength = text.Length;

            /* Searching */
            int index = startIndex;
            while (index <= textLength - patternLength)
            {
                int unmatched;
                for (unmatched = patternLength - 1;
                     unmatched >= 0 && this.pattern[unmatched] == text[unmatched + index];
                     --unmatched)
                {
                    ; // do nothing
                }

                if (unmatched < 0)
                {
                    yield return index;
                }

                index += this.badCharacterShift[text[unmatched + patternLength - 1]];
            }
        }

        // unsafe "match first"
        public int HorspoolMatchFirst(byte[] text, int startIndex)
        {
            int patternLength = this.pattern.Length;
            int textLength = text.Length;

            /* Searching */
            int index = startIndex;
            unsafe
            {
                fixed (byte* pPattern = this.pattern, pText = text)
                {
                    fixed (int* pBadCharacterShift = this.badCharacterShift)
                    {
                        while (index <= textLength - patternLength)
                        {
                            int unmatched;
                            for (unmatched = patternLength - 1;
                                 unmatched >= 0 && *(pPattern + unmatched) == *(pText + unmatched + index);
                                 --unmatched)
                            {
                                ; // do nothing
                            }

                            if (unmatched < 0)
                            {
                                return index;
                            }

                            index += *(pBadCharacterShift + *(pText + unmatched + patternLength - 1));
                        }
                    }
                }
            }
            return -1;
        }

        // trivial forwarders
        public IEnumerable<int> HorspoolMatchAll(byte[] text)
        {
            return this.HorspoolMatchAll(text, 0);
        }

        public IEnumerable<int> HorspoolMatchAll(string text)
        {
            return this.HorspoolMatchAll(Encoding.ASCII.GetBytes(text), 0);
        }

        public IEnumerable<int> HorspoolMatchAll(string text, int startIndex)
        {
            return this.HorspoolMatchAll(Encoding.ASCII.GetBytes(text), startIndex);
        }

        public int HorspoolMatchFirst(byte[] text)
        {
            return this.HorspoolMatchFirst(text, 0);
        }

        public int HorspoolMatchFirst(string text)
        {
            return this.HorspoolMatchFirst(Encoding.ASCII.GetBytes(text), 0);
        }

        public int HorspoolMatchFirst(string text, int startIndex)
        {
            return this.HorspoolMatchFirst(Encoding.ASCII.GetBytes(text), startIndex);
        }

        #endregion

        #region Boyer-Moore

        // match using Boyer-Moore
        public IEnumerable<int> BoyerMooreMatchAll(byte[] text, int startIndex)
        {
            #region Algorithm in C, from http://www-igm.univ-mlv.fr/~lecroq/string/node14.html#SECTION00140

            // void BM(char *x, int m, char *y, int n)
            // {
            //    int i, j, bmGs[XSIZE], bmBc[ASIZE];
            // 
            //    /* Preprocessing */
            //    preBmGs(x, m, bmGs);
            //    preBmBc(x, m, bmBc);
            // 
            //    /* Searching */
            //    j = 0;
            //    while (j <= n - m) {
            //       for (i = m - 1; i >= 0 && x[i] == y[i + j]; --i);
            //       if (i < 0) {
            //          OUTPUT(j);
            //          j += bmGs[0];
            //       }
            //       else
            //          j += MAX(bmGs[i], bmBc[y[i + j]] - m + 1 + i);
            //    }
            // }

            #endregion

            int patternLength = this.pattern.Length;
            int textLength = text.Length;

            /* Searching */
            int index = startIndex;
            while (index <= textLength - patternLength)
            {
                int unmatched;
                for (unmatched = patternLength - 1;
                     unmatched >= 0 && (this.pattern[unmatched] == text[unmatched + index]);
                     --unmatched)
                {
                    ; // do nothing
                }

                if (unmatched < 0)
                {
                    yield return index;
                    index += this.goodSuffixShift[0];
                }
                else
                {
                    index +=
                        Math.Max(this.goodSuffixShift[unmatched],
                                 this.badCharacterShift[text[unmatched + index]] - patternLength + 1 + unmatched);
                }
            }
        }

        // unsafe "match first"
        public int BoyerMooreMatchFirst(byte[] text, int startIndex)
        {
            int patternLength = this.pattern.Length;
            int textLength = text.Length;

            /* Searching */
            int index = startIndex;
            unsafe
            {
                fixed (byte* pPattern = this.pattern, pText = text)
                {
                    fixed (int* pBadCharacterShift = this.badCharacterShift, pGoodSuffixShift = this.goodSuffixShift)
                    {
                        while (index <= textLength - patternLength)
                        {
                            int unmatched;
                            for (unmatched = patternLength - 1;
                                 unmatched >= 0 && (*(pPattern + unmatched) == *(pText + unmatched + index));
                                 --unmatched)
                            {
                                ; // do nothing
                            }

                            if (unmatched < 0)
                            {
                                return index;
                            }
                            else
                            {
                                index +=
                                    Math.Max(*(pGoodSuffixShift + unmatched),
                                             *
                                             (pBadCharacterShift + (*(pText + unmatched + index)) - patternLength + 1 +
                                              unmatched));
                            }
                        }
                    }
                }
            }
            return -1;
        }

        // trivial forwarders
        public IEnumerable<int> BoyerMooreMatchAll(byte[] text)
        {
            return this.BoyerMooreMatchAll(text, 0);
        }

        public IEnumerable<int> BoyerMooreMatchAll(string text)
        {
            return this.BoyerMooreMatchAll(Encoding.ASCII.GetBytes(text), 0);
        }

        public IEnumerable<int> BoyerMooreMatchAll(string text, int startIndex)
        {
            return this.BoyerMooreMatchAll(Encoding.ASCII.GetBytes(text), startIndex);
        }

        public int BoyerMooreMatchFirst(byte[] text)
        {
            return this.BoyerMooreMatchFirst(text, 0);
        }

        public int BoyerMooreMatchFirst(string text)
        {
            return this.BoyerMooreMatchFirst(Encoding.ASCII.GetBytes(text), 0);
        }

        public int BoyerMooreMatchFirst(string text, int startIndex)
        {
            return this.BoyerMooreMatchFirst(Encoding.ASCII.GetBytes(text), startIndex);
        }

        #endregion

        #region Turbo Boyer-Moore

        // match using turbo Boyer-Moore; requires an extra int
        public IEnumerable<int> TurboBoyerMooreMatchAll(byte[] text, int startIndex)
        {
            #region Algorithm in C, from http://www-igm.univ-mlv.fr/~lecroq/string/node15.html#SECTION00150

            // void TBM(char *x, int m, char *y, int n)
            // {
            //    int bcShift, i, j, shift, u, v, turboShift, bmGs[XSIZE], bmBc[ASIZE];
            //
            //    /* Preprocessing */
            //    preBmGs(x, m, bmGs);
            //    preBmBc(x, m, bmBc);
            //
            //    /* Searching */
            //    j = u = 0;
            //    shift = m;
            //    while (j <= n - m) {
            //       i = m - 1;
            //       while (i >= 0 && x[i] == y[i + j]) {
            //          --i;
            //          if (u != 0 && i == m - 1 - shift)
            //             i -= u;
            //       }
            //       if (i < 0) {
            //          OUTPUT(j);
            //          shift = bmGs[0];
            //          u = m - shift;
            //       }
            //       else {
            //          v = m - 1 - i;
            //          turboShift = u - v;
            //          bcShift = bmBc[y[i + j]] - m + 1 + i;
            //          shift = MAX(turboShift, bcShift);
            //          shift = MAX(shift, bmGs[i]);
            //          if (shift == bmGs[i])
            //             u = MIN(m - shift, v);
            //          else {
            //            if (turboShift < bcShift)
            //               shift = MAX(shift, u + 1);
            //            u = 0;
            //          }
            //       }
            //       j += shift;
            //    }
            // }

            #endregion

            int patternLength = this.pattern.Length;
            int textLength = text.Length;

            /* Searching */
            int index = startIndex;
            int overlap = 0;
            int shift = patternLength;
            while (index <= textLength - patternLength)
            {
                int unmatched = patternLength - 1;

                while (unmatched >= 0 && (this.pattern[unmatched] == text[unmatched + index]))
                {
                    --unmatched;
                    if (overlap != 0 && unmatched == patternLength - 1 - shift)
                    {
                        unmatched -= overlap;
                    }
                }

                if (unmatched < 0)
                {
                    yield return index;
                    shift = this.goodSuffixShift[0];
                    overlap = patternLength - shift;
                }
                else
                {
                    int matched = patternLength - 1 - unmatched;
                    int turboShift = overlap - matched;
                    int bcShift = this.badCharacterShift[text[unmatched + index]] - patternLength + 1 + unmatched;
                    shift = Math.Max(Math.Max(turboShift, bcShift), this.goodSuffixShift[unmatched]);
                    if (shift == this.goodSuffixShift[unmatched])
                    {
                        overlap = Math.Min(patternLength - shift, matched);
                    }
                    else
                    {
                        if (turboShift < bcShift)
                        {
                            shift = Math.Max(shift, overlap + 1);
                        }
                        overlap = 0;
                    }
                }

                index += shift;
            }
        }

        // unsafe "match first"
        public int TurboBoyerMooreMatchFirst(byte[] text, int startIndex)
        {
            int patternLength = this.pattern.Length;
            int textLength = text.Length;

            /* Searching */
            int index = startIndex;
            int overlap = 0;
            int shift = patternLength;
            unsafe
            {
                fixed (byte* pPattern = this.pattern, pText = text)
                {
                    fixed (int* pBadCharacterShift = this.badCharacterShift, pGoodSuffixShift = this.goodSuffixShift)
                    {
                        while (index <= textLength - patternLength)
                        {
                            int unmatched = patternLength - 1;

                            while (unmatched >= 0 && *(pPattern + unmatched) == *(pText + unmatched + index))
                            {
                                --unmatched;
                                if (overlap != 0 && unmatched == patternLength - 1 - shift)
                                {
                                    unmatched -= overlap;
                                }
                            }

                            if (unmatched < 0)
                            {
                                return index;
                            }
                            else
                            {
                                int matched = patternLength - 1 - unmatched;
                                int turboShift = overlap - matched;
                                int bcShift = *(pBadCharacterShift + *(pText + unmatched + index)) - patternLength + 1 +
                                              unmatched;
                                shift = Math.Max(Math.Max(turboShift, bcShift), *(pGoodSuffixShift + unmatched));
                                if (shift == *(pGoodSuffixShift + unmatched))
                                {
                                    overlap = Math.Min(patternLength - shift, matched);
                                }
                                else
                                {
                                    if (turboShift < bcShift)
                                    {
                                        shift = Math.Max(shift, overlap + 1);
                                    }
                                    overlap = 0;
                                }
                            }

                            index += shift;
                        }
                    }
                }
            }
            return -1;
        }

        // trivial forwarders
        public IEnumerable<int> TurboBoyerMooreMatchAll(byte[] text)
        {
            return this.TurboBoyerMooreMatchAll(text, 0);
        }

        public IEnumerable<int> TurboBoyerMooreMatchAll(string text)
        {
            return this.TurboBoyerMooreMatchAll(Encoding.ASCII.GetBytes(text), 0);
        }

        public IEnumerable<int> TurboBoyerMooreMatchAll(string text, int startIndex)
        {
            return this.TurboBoyerMooreMatchAll(Encoding.ASCII.GetBytes(text), startIndex);
        }

        public int TurboBoyerMooreMatchFirst(byte[] text)
        {
            return this.TurboBoyerMooreMatchFirst(text, 0);
        }

        public int TurboBoyerMooreMatchFirst(string text)
        {
            return this.TurboBoyerMooreMatchFirst(Encoding.ASCII.GetBytes(text), 0);
        }

        public int TurboBoyerMooreMatchFirst(string text, int startIndex)
        {
            return this.TurboBoyerMooreMatchFirst(Encoding.ASCII.GetBytes(text), startIndex);
        }

        #endregion

        #region Apostolico-Giancarlo

        // match using Apostolico-Giancarlo
        public IEnumerable<int> ApostolicoGiancarloMatchAll(byte[] text, int startIndex)
        {
            #region Algorithm in C, from http://www-igm.univ-mlv.fr/~lecroq/string/node16.html#SECTION00160

            // void AG(char *x, int m, char *y, int n) 
            // {
            //    int i, j, k, s, shift, bmGs[XSIZE], skip[XSIZE], suff[XSIZE], bmBc[ASIZE];
            //  
            //    /* Preprocessing */
            //    preBmGs(x, m, bmGs, suff);
            //    preBmBc(x, m, bmBc);
            //    memset(skip, 0, m*sizeof(int));
            //  
            //    /* Searching */
            //    j = 0;
            //    while (j <= n - m) {
            //       i = m - 1;
            //       while (i >= 0) {
            //          k = skip[i];
            //          s = suff[i];
            //          if (k > 0)
            //             if (k > s) {
            //                if (i + 1 == s)
            //                   i = (-1);
            //                else
            //                   i -= s;
            //                break;
            //             }
            //             else {
            //                i -= k;
            //                if (k < s)
            //                   break;
            //             }
            //          else {
            //             if (x[i] == y[i + j])
            //                --i;
            //             else
            //                break;
            //          }
            //       }
            //       if (i < 0) {
            //          OUTPUT(j);
            //          skip[m - 1] = m;
            //          shift = bmGs[0];
            //       }
            //       else {
            //          skip[m - 1] = m - 1 - i;
            //          shift = MAX(bmGs[i], bmBc[y[i + j]] - m + 1 + i);
            //       }
            //       j += shift;
            //       memcpy(skip, skip + shift, (m - shift)*sizeof(int));
            //       memset(skip + m - shift, 0, shift*sizeof(int));
            //    }
            // }

            #endregion

            int patternLength = this.pattern.Length;
            int textLength = text.Length;
            int[] skip = new int[patternLength];

            /* Searching */
            int index = startIndex;
            while (index <= textLength - patternLength)
            {
                int unmatched = patternLength - 1;
                while (unmatched >= 0)
                {
                    int skipLength = skip[unmatched];
                    int suffixLength = this.suffixes[unmatched];
                    if (skipLength > 0)
                    {
                        if (skipLength > suffixLength)
                        {
                            if (unmatched + 1 == suffixLength)
                            {
                                unmatched = -1;
                            }
                            else
                            {
                                unmatched -= suffixLength;
                            }
                            break;
                        }
                        else
                        {
                            unmatched -= skipLength;
                            if (skipLength < suffixLength)
                            {
                                break;
                            }
                        }
                    }
                    else
                    {
                        if (this.pattern[unmatched] == text[unmatched + index])
                        {
                            --unmatched;
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                int shift;
                if (unmatched < 0)
                {
                    yield return index;
                    skip[patternLength - 1] = patternLength;
                    shift = this.goodSuffixShift[0];
                }
                else
                {
                    skip[patternLength - 1] = patternLength - 1 - unmatched;
                    shift = Math.Max(this.goodSuffixShift[unmatched],
                                     this.badCharacterShift[text[unmatched + index]] - patternLength + 1 + unmatched);
                }
                index += shift;

                for (int copy = 0; copy < patternLength - shift; ++copy)
                {
                    skip[copy] = skip[copy + shift];
                }

                for (int clear = 0; clear < shift; ++clear)
                {
                    skip[patternLength - shift + clear] = 0;
                }
            }
        }

        // unsafe "match first"
        public int ApostolicoGiancarloMatchFirst(byte[] text, int startIndex)
        {
            int patternLength = this.pattern.Length;
            int textLength = text.Length;

            /* Searching */
            int index = startIndex;
            unsafe
            {
                fixed (byte* pPattern = this.pattern, pText = text)
                {
                    fixed (int* pBadCharacterShift = this.badCharacterShift, pGoodSuffixShift = this.goodSuffixShift,
                        pSuffixes = this.suffixes, pSkip = new int[patternLength])
                    {
                        while (index <= textLength - patternLength)
                        {
                            int unmatched = patternLength - 1;
                            while (unmatched >= 0)
                            {
                                int skipLength = *(pSkip + unmatched);
                                int suffixLength = *(pSuffixes + unmatched);
                                if (skipLength > 0)
                                {
                                    if (skipLength > suffixLength)
                                    {
                                        if (unmatched + 1 == suffixLength)
                                        {
                                            unmatched = -1;
                                        }
                                        else
                                        {
                                            unmatched -= suffixLength;
                                        }
                                        break;
                                    }
                                    else
                                    {
                                        unmatched -= skipLength;
                                        if (skipLength < suffixLength)
                                        {
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    if (*(pPattern + unmatched) == *(pText + unmatched + index))
                                    {
                                        --unmatched;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }

                            int shift;
                            if (unmatched < 0)
                            {
                                return index;
                            }
                            else
                            {
                                *(pSkip + patternLength - 1) = patternLength - 1 - unmatched;
                                shift = Math.Max(*(pGoodSuffixShift + unmatched),
                                                 *(pBadCharacterShift + *(pText + unmatched + index)) - patternLength + 1 + unmatched);
                            }
                            index += shift;

                            for (int copy = 0; copy < patternLength - shift; ++copy)
                            {
                                *(pSkip + copy) = *(pSkip + copy + shift);
                            }

                            for (int clear = 0; clear < shift; ++clear)
                            {
                                *(pSkip + patternLength - shift + clear) = 0;
                            }
                        }
                    }
                }
            }
            return -1;
        }

        // trivial forwarders
        public IEnumerable<int> ApostolicoGiancarloMatchAll(byte[] text)
        {
            return this.ApostolicoGiancarloMatchAll(text, 0);
        }

        public IEnumerable<int> ApostolicoGiancarloMatchAll(string text)
        {
            return this.ApostolicoGiancarloMatchAll(Encoding.ASCII.GetBytes(text), 0);
        }

        public IEnumerable<int> ApostolicoGiancarloMatchAll(string text, int startIndex)
        {
            return this.ApostolicoGiancarloMatchAll(Encoding.ASCII.GetBytes(text), startIndex);
        }

        public int ApostolicoGiancarloMatchFirst(string text)
        {
            return this.ApostolicoGiancarloMatchFirst(Encoding.ASCII.GetBytes(text), 0);
        }

        public int ApostolicoGiancarloMatchFirst(string text, int startIndex)
        {
            return this.ApostolicoGiancarloMatchFirst(Encoding.ASCII.GetBytes(text), startIndex);
        }

        public int ApostolicoGiancarloMatchFirst(byte[] text)
        {
            return this.ApostolicoGiancarloMatchFirst(text, 0);
        }

        #endregion
    }
}
