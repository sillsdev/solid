using System;
using System.Text;
using NUnit.Framework;
using SolidGui.Engine;

namespace SolidGui.Tests {
	[TestFixture]
	public class EncodingTests
	{
		[Test]
		public void Store0x00To0xFFInString_Correct()
		{
			StringBuilder sb = new StringBuilder(256);
			byte b = 0x00;
			for (int i = 0x00; i <= 0xff; i++)
			{
				sb.Append((char)b);
				Assert.AreEqual(b, (byte)sb[i]);
				b++;
			}
			string s = sb.ToString();
			Assert.AreEqual(256, s.Length);
			b = 0x00;
			for (int i = 0x00; i <= 0xff; i++)
			{
				Assert.AreEqual(b, (byte)s[i]);
				b++;
			}
		}

		[Test]
        public void Trac78_Correct()
		{
			byte[] legacyBytes = 
				{
					0xef, 0xbf, 0xbd, 0x6c, 0xef, 0xbf, 0xbd, 0x2e, 0x93
				};
			byte[] utf8Correct = 
				{
					0xc3, 0xaf, 0xc2, 0xbf, 0xc2, 0xbd, 0x6c, 0xc3, 0xaf, 0xc2, 0xbf, 0xc2, 0xbd, 0x2e, 0xE2,0x80,0x9C  
				};
		    Encoding legacy = SolidSettings.LegacyEncoding; //was: Encoding.GetEncoding("iso-8859-1"); which didn't handle curly quotes -JMC
		    string isoString = legacy.GetString(legacyBytes);
            Console.Write("legacy: " + isoString + "\n");

			Console.Write("legacy bytes: ");
			foreach (byte b in legacyBytes)
			{
			    //string s = String.Format("0x{0:x} ", b);
                //Console.Write(s);
				Console.Write("0x{0:x} ", b);
			}

            byte[] utf8Bytes = Encoding.Convert(legacy, Encoding.UTF8, legacyBytes);
            string utfCorrect = Encoding.UTF8.GetString(utf8Correct);
            string utfString = Encoding.UTF8.GetString(utf8Bytes);
            Console.Write("\nutf8: " + utfCorrect);
            Console.Write("\nutf8: " + utfString);

            Console.Write("\nutf8 bytes: ");
			foreach (byte b in utf8Bytes)
			{
				Console.Write("0x{0:x} ", b);
			}
			Console.Write("\n");
			Assert.AreEqual(utf8Correct, utf8Bytes);
		}

		[Test]
        // Tried to figure this out, then realized it was unfinished. 
        // Rewrote... hmm, it doesn't really test Solid. And these are all one-byte codepoints. -JMC Mar 2014
		public void ExampleAsUTF8_Correct()
		{
			byte[] unicodeCodepoints = 
				{
					0xef,  0xbf,  0xbd,  0x6c,  0xef,  0xbf,  0xbd, 0x2e
				};  
			byte[] utf8EncodedBytes = 
				{
					0xc3, 0xaf,  0xc2, 0xbf,  0xc2, 0xbd,  0x6c,  0xc3, 0xaf, 
                      0xc2, 0xbf,  0xc2, 0xbd,  0x2e,  0xF0,0x9F,0x98,0x90  //not 0xD8,0x3D,0xDE,0x10
				};
            // That is: i+diaersis C3, inverted ? BF, 1/2 BD, small L 6C, i+diaersis C3,
            //   inverted ? BF, 1/2 BD, period 2E, neutral face
            // U+00EF 0xc3 0xaf  U+00BF 0xc2 0xbf  U+00BD 0xc2 0xbd  U+006C 0x6c  U+00EF 0xc3 0xaf
            //   U+00BF 0xc2 0xbf  U+00BF 0xc2  U+00BD 0xbd  U+002E 0x2e  U+1F610 0xD8 0x3D 0xDE 0x10  -JMC

            // Should we also test "wide characters" that require surrogate pairs? -JMC
            // Then append U+1F610 0xD8,0x3D,0xDE,0x10
           
            Console.Write("Codepoints: ");
            foreach (byte b in unicodeCodepoints)
            {
                Console.Write("0x{0:x} ", b);
            }
            Console.Write("\n\n");
            
            int n = 0;
		    var chars = new char[unicodeCodepoints.Length+2];  //the extra two will be one wide character
            foreach(byte b in unicodeCodepoints)
            {
                chars[n++] = (char)b;
            }
		    chars[n++] = '\xD83D'; //start a surrogate pair for neutral face
		    chars[n] = '\xDE10';
		    string s = new string(chars);

			Console.Write("Raw values: ");
			foreach (char c in s)
			{
				int i = Convert.ToInt32(c);
				Console.Write("0x{0:x} ", i);
			}
			Console.Write("\n\n");

            Console.Write("char[] as string: [" + s + "]\n\n"); // will this fail on some non-unicode consoles?

            Encoding utf8 = Encoding.UTF8;
            string s2 = utf8.GetString(utf8EncodedBytes);

            Console.Write("Should match this target string: [" + s2 + "]\n\n");  //Hmm. Wide character doesn't even print a placeholder.

            Assert.AreEqual(s, s2);

		    var encodeUtf8 = utf8.GetEncoder();
            var ba = new byte[utf8EncodedBytes.Length];
		    int x = encodeUtf8.GetBytes(chars, 0, chars.Length, ba, 0, true);

            Assert.AreEqual(utf8EncodedBytes, ba);

		}

    
	}
}