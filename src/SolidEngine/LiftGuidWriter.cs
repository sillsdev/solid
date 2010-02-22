using System;

namespace SolidEngine
{

	public class LiftGuidWriter : System.Xml.XmlTextWriter
	{
		public LiftGuidWriter (string outputFilePath, System.Text.Encoding encoding)
			: base (outputFilePath, encoding)
		{

		}

		public LiftGuidWriter (System.IO.Stream stream, System.Text.Encoding encoding)
			: base (stream, encoding)
		{

		}

		public override void WriteStartElement (string prefix, string localName, string ns)
		{
			base.WriteStartElement (prefix, localName, ns);
			if (localName == "entry")
			{
				base.WriteAttributeString ("guid", Guid.NewGuid().ToString());
			}
		}

	}

}
