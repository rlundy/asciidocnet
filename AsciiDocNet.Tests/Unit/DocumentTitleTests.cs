using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace AsciidocNet.Tests.Unit
{
	[TestFixture]
	public class DocumentTitleTests
	{
		private const string Title = "Document Title";
		private const string Subtitle = "Sub title";

		private const string DocumentTitle = "= " + Title;
		private const string DocumentWithSubtitle = DocumentTitle + ":" + Subtitle;

		public IEnumerable<string> Titles
		{
			get
			{
				yield return DocumentTitle;

				var allowedPrecedingEntries = new List<string>
				{
					"\n",
					"// This is a single comment",
					":exampleattribute: example attribute"
				};

				foreach (var entry in allowedPrecedingEntries)
				{
					yield return $"{entry}\n{DocumentTitle}";
				}

				foreach (var entry in allowedPrecedingEntries.Zip(allowedPrecedingEntries, (first, second) => first + "\n" + second))
				{
					yield return $"{entry}\n{DocumentTitle}";
				}
			}
		}

		public IEnumerable<string> TitlesWithSubtitle
		{
			get
			{
				yield return DocumentWithSubtitle;

				var allowedPrecedingEntries = new List<string>
				{
					"\n",
					"// This is a single comment",
					":exampleattribute: example attribute"
				};

				foreach (var entry in allowedPrecedingEntries)
				{
					yield return $"{entry}\n{DocumentWithSubtitle}";
				}

				foreach (var entry in allowedPrecedingEntries.Zip(allowedPrecedingEntries, (first, second) => first + "\n" + second))
				{
					yield return $"{entry}\n{DocumentWithSubtitle}";
				}
			}
		}

		[Test]
		[TestCaseSource(nameof(Titles))]
		public void ShouldParseDocumentTitle(string input)
		{
			var document = Document.Parse(input);

			Assert.IsNotNull(document.Title);
			Assert.AreEqual(Title, document.Title.Title);
		}

		[Test]
		[TestCaseSource(nameof(TitlesWithSubtitle))]
		public void ShouldParseDocumentTitleWithSubtitle(string input)
		{
			var document = Document.Parse(input);

			Assert.IsNotNull(document.Title);
			Assert.AreEqual(Title, document.Title.Title);
			Assert.AreEqual(Subtitle, document.Title.Subtitle);
		}
	}
}