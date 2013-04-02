using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace TextToXml
{
	internal class XmlParse
	{
		private XmlDocument document;
		private XmlElement root;
		private uint wordCount;
		private uint paragraphCount;

		public XmlParse()
		{
			Inicialize();
		}

		private void Inicialize()
		{
			document = new XmlDocument();
			root = document.CreateElement("Text");
			wordCount = 0;
			paragraphCount = 0;
			root.SetAttribute("WordCount", wordCount.ToString());
			root.SetAttribute("ParagraphCount", paragraphCount.ToString());
			document.AppendChild(root);
		}

		public void SaveToFile(string pathFile)
		{
			XmlTextWriter writer = new XmlTextWriter(pathFile, null);
			try
			{
				writer.Formatting = Formatting.Indented;
				document.Save(writer);
			}
			catch (Exception e)
			{
				throw new Exception("SaveToFile", e);
			}
			finally
			{
				writer.Close();
			}
		}

		public void Load(string pathFile)
		{
			document.Load(pathFile);
			root = document.DocumentElement;
		}

		public override string ToString()
		{
			string returnStr = "";
			SaveToFile(Path.GetTempPath() + @"temp.xml");
			//returnStr = root.OuterXml;
			FileStream stream = new FileStream(Path.GetTempPath() + @"temp.xml", FileMode.Open);
			try
			{
				int sizeBuffer = 1024;
				byte[] buffer = new byte[sizeBuffer];
				int readCount = 0;
				while (readCount < stream.Length)
				{
					int k = stream.Read(buffer, 0, sizeBuffer);
					readCount += k;
					returnStr += Encoding.UTF8.GetString(buffer, 0, k);
				}
				return returnStr;
			}
			catch (Exception e)
			{
				throw new Exception("ToString", e);
			}
			finally
			{
				stream.Close();
			}

		}

		public string XmlToText()
		{
			string str = "";
			foreach (XmlElement paragraph in root.ChildNodes)
			{
				foreach (XmlElement propos in paragraph.ChildNodes)
				{
					foreach (XmlElement words in propos.ChildNodes)
					{
						str += words.GetAttribute("Value") + " ";
					}
				}
				str += "\n";
			}
			return str;
		}

	public XmlDocument TextToXml(string text, bool reset)
		{
			if (reset)
			{
				Inicialize();	
			}
			//Paragraph
			Regex paragraphКegex = new Regex(@".*[^\r\n]");
			foreach (Match matchParagraph in paragraphКegex.Matches(text))
			{
				XmlElement paragrap = document.CreateElement("Paragrap");
				//Propos
				Regex proposeRegex = new Regex(@"[^.?!\n\r]+[.?!]*");
				var allPropose = proposeRegex.Matches(matchParagraph.Value);
				foreach (Match matchPropose in allPropose)
				{
					XmlElement propos = document.CreateElement("Propos");
					uint wordCounts = 0;
					// Word or symbol
					Regex wordAndSymbol = new Regex(@"\b(?:(?!exclude)\w)+\b|[,.!?]");
					foreach (Match match in wordAndSymbol.Matches(matchPropose.Value))
					{
						XmlElement word;
						if (match.Value.Length == 1 && char.IsLetterOrDigit(match.Value[0]) || match.Length > 1)
						{
							word = document.CreateElement("Word");
							wordCounts++;
						}
						else
						{
							word = document.CreateElement("Symbol");
						}
						word.SetAttribute("Value", match.Value);
						propos.AppendChild(word);
					}
					propos.SetAttribute("WordCount", wordCounts.ToString());
					wordCount += wordCounts;
					if (!propos.IsEmpty)
						propos.SetAttribute("LastSymbol", ((XmlElement) propos.LastChild).GetAttribute("Value"));
					paragrap.AppendChild(propos);
				}
				paragrap.SetAttribute("ProposeCount", allPropose.Count.ToString());
				paragraphCount++;
				root.AppendChild(paragrap);
			}
			root.SetAttribute("WordCount", wordCount.ToString());
			root.SetAttribute("ParagraphCount", paragraphCount.ToString());
			return document;
		}
	}
}
