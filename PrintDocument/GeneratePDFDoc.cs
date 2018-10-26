using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Telerik.Windows.Documents.Fixed.FormatProviders;
using Telerik.Windows.Documents.Fixed.FormatProviders.Pdf;
using Telerik.Windows.Documents.Fixed.Model;
using Telerik.Windows.Documents.Fixed.Model.ColorSpaces;
using Telerik.Windows.Documents.Fixed.Model.Editing;
using Telerik.Windows.Documents.Fixed.Model.Editing.Flow;
using Telerik.Windows.Documents.Fixed.Model.Fonts;

namespace GenerateDocument
{
	public class GeneratePDFDoc : GenerateDocBase<RadFixedDocument>
	{
		private static readonly double DefaultLeftIndent = 40;
		private static readonly double PageWidth = 595;
		private static readonly double PageHeight = 842;

		private Size HeaderSize = Size.Empty;
		private Size FooterSize = Size.Empty;
		private Size ExpressionSize = Size.Empty;
		private double offset = 10;
		public override RadFixedDocument GenerateDoc(int pages)
		{
			RadFixedDocument doc = new RadFixedDocument();
			for (int i = 0; i < pages; i++)
			{
				RadFixedPage page = doc.Pages.AddPage();
				page.Size = new Size(PageWidth, PageHeight);

				FixedContentEditor editor = new FixedContentEditor(page);

				this.SetPageHeader(editor);
				this.SetPageFooter(editor);
				this.SetPageNumber(editor, i + 1);

				this.SetExpressions(editor);
			}

			PdfFormatProvider pdfProvider = new PdfFormatProvider();
			MemoryStream ms = new MemoryStream(pdfProvider.Export(doc));
			PdfFormatProvider pdfViewerProvider = new PdfFormatProvider(ms, FormatProviderSettings.ReadAllAtOnce);

			return pdfViewerProvider.Import();
		}

		private void SetPageNumber(FixedContentEditor editor, int pageNumber)
		{
			Block block = new Block();

			this.SetTextProperties(block, new RgbColor(0, 0, 0), 15, FontsRepository.TimesRoman);
			block.InsertText("- " + pageNumber + " -");
			Size pageNumberSize = block.Measure();

			editor.Position.Translate((PageWidth - pageNumberSize.Width) / 2, PageHeight - pageNumberSize.Height - offset);
			editor.DrawBlock(block, new Size(PageWidth, PageHeight));
		}

		private void SetExpressions(FixedContentEditor editor)
		{
			double expressionTotalHeight = PageHeight - HeaderSize.Height - offset * 2 - FooterSize.Height - offset * 2;
			double expressionTotalWidth = PageWidth - DefaultLeftIndent * 2;

			if (ExpressionSize == Size.Empty) ExpressionSize = GetExpressionSize();

			double expressionWidth = ExpressionSize.Width * 1.2;
			double expressionHeight = ExpressionSize.Height + 10d;

			int howManyExpressionsPerLine = Convert.ToInt32(Math.Floor(expressionTotalWidth / expressionWidth));
			double calculatedExpressionWidth = expressionTotalWidth / howManyExpressionsPerLine;
			int howManyExpressionLines = Convert.ToInt32(Math.Floor(expressionTotalHeight / expressionHeight));

			for (int i = 0; i < howManyExpressionLines; i++)
			{
				for (int j = 0; j < howManyExpressionsPerLine; j++)
				{
					editor.Position.Translate(j * calculatedExpressionWidth + DefaultLeftIndent, i * expressionHeight + HeaderSize.Height + offset * 2);
					Block block = new Block();

					this.SetTextProperties(block, new RgbColor(0, 0, 0), 22, FontsRepository.Helvetica);
					block.InsertText(GetExpression());
					editor.DrawBlock(block, new Size(PageWidth, PageHeight));
				}
			}
		}

		private Size GetExpressionSize()
		{
			Block block = new Block();

			this.SetTextProperties(block, new RgbColor(0, 0, 0), 22, FontsRepository.Helvetica);
			block.InsertText(MaxExpression);

			return block.Measure();
		}

		private void SetPageFooter(FixedContentEditor editor)
		{
			Block block = new Block();

			this.SetTextProperties(block, new RgbColor(0, 0, 0), 22, FontsRepository.TimesItalic);
			block.InsertText("Powered by Jeffrey");
			if (FooterSize == Size.Empty) FooterSize = block.Measure();

			editor.Position.Translate(PageWidth - FooterSize.Width - offset, PageHeight - FooterSize.Height - offset);
			editor.DrawBlock(block, new Size(PageWidth, PageHeight));
		}

		private void SetPageHeader(FixedContentEditor editor)
		{
			Block block = new Block();

			this.SetTextProperties(block, new RgbColor(0, 0, 0), 30, FontsRepository.TimesBold);
			block.InsertText("四则运算习题册");
			if (HeaderSize == Size.Empty) HeaderSize = block.Measure();

			editor.Position.Translate((PageWidth - HeaderSize.Width) / 2, offset);
			editor.DrawBlock(block, new Size(PageWidth, PageHeight));
		}

		private void SetTextProperties(Block block, ColorBase color, double fontSize, FontBase font, bool isUnderlined = false)
		{
			block.GraphicProperties.FillColor = color;
			block.HorizontalAlignment = Telerik.Windows.Documents.Fixed.Model.Editing.Flow.HorizontalAlignment.Left;
			block.TextProperties.FontSize = fontSize;
			block.TextProperties.Font = font;
			block.TextProperties.UnderlinePattern = isUnderlined ? UnderlinePattern.Single : UnderlinePattern.None;
		}
	}
}
