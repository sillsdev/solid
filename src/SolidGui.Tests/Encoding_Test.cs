using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace Solid.EngineTests {
	[TestFixture]
	public class Encoding_Test
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
			byte[] iso8859Bytes = 
				{
					0xef, 0xbf, 0xbd, 0x6c, 0xef, 0xbf, 0xbd, 0x2e
				};
			byte[] utf8Correct = 
				{
					0xc3, 0xaf, 0xc2, 0xbf, 0xc2, 0xbd, 0x6c, 0xc3, 0xaf, 0xc2, 0xbf, 0xc2, 0xbd, 0x2e 
				};
			Encoding iso8859_1 = Encoding.GetEncoding("iso-8859-1");
			Encoding stringEncoding = Encoding.UTF8;
			byte[] utf8Bytes = Encoding.Convert(iso8859_1, Encoding.UTF8, iso8859Bytes);
			Console.Write("iso: ");
			foreach (byte b in iso8859Bytes)
			{
				Console.Write("0x{0:x} ", b);
			}
			Console.Write("\nutf8: ");
			foreach (byte b in utf8Bytes)
			{
				Console.Write("0x{0:x} ", b);
			}
			Console.Write("\n");
			Assert.AreEqual(utf8Correct, utf8Bytes);
		}

		[Test]
		public void ExampleAsUTF8_Correct()
		{
			byte[] utf8Bytes = 
				{
					0xef, 0xbf, 0xbd, 0x6c, 0xef, 0xbf, 0xbd, 0x2e
				};
			byte[] utf8Correct = 
				{
					0xc3, 0xaf, 0xc2, 0xbf, 0xc2, 0xbd, 0x6c, 0xc3, 0xaf, 0xc2, 0xbf, 0xc2, 0xbd, 0x2e 
				};

			Encoding stringEncoding = Encoding.UTF8;
			string s = stringEncoding.GetString(utf8Bytes);

			Console.Write("Raw values: ");
			foreach (char c in s)
			{
				int i = Convert.ToInt32(c);
				Console.Write("0x{0:x} ", i);
			}
			Console.Write("\n");
            
		}

    
	}
}