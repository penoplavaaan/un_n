using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegates.Reports
{
	public class ReportMaker<TFormat, TStat>
		where TFormat : IData, new()
	{
		protected string Caption;
		protected Func<IEnumerable<double>, TStat> MakeStatistics;

		public ReportMaker(string caption, Func<IEnumerable<double>, TStat> makeStatistics)
        {
			Caption = new TFormat().MakeCaption(caption);
			MakeStatistics = makeStatistics;
		}

		//protected object MakeStatistics(IEnumerable<double> data);
		//protected abstract string Caption { get; }
		public string MakeReport(IEnumerable<Measurement> measurements)
		{
			TFormat formatHelper = new TFormat(); 

			var data = measurements.ToList();
			var result = new StringBuilder();
			result.Append(Caption);
			result.Append(formatHelper.BeginSymbol);
			result.Append(formatHelper.MakeItem("Temperature", MakeStatistics(data.Select(z => z.Temperature)).ToString()));
			result.Append(formatHelper.MakeItem("Humidity", MakeStatistics(data.Select(z => z.Humidity)).ToString()));
			result.Append(formatHelper.EndSymbol);
			return result.ToString();
		}
	}

	public  interface IData
    {
		string MakeCaption(string caption);
		string MakeItem(string valueType, string entry);
		string BeginSymbol { get; }
		string EndSymbol { get; }
    }

    public class Html : IData
    {
        public string BeginSymbol => "<ul>";

        public string EndSymbol => "</ul>";

        public string MakeCaption(string caption)
        {
			return $"<h1>{caption}</h1>";
		}

		public string MakeItem(string valueType, string entry)
		{
			return $"<li><b>{valueType}</b>: {entry}";
		}
	}

	public class Markdown : IData
	{
		public string BeginSymbol => "";

		public string EndSymbol => "";

		public string MakeCaption(string caption)
		{
			return $"## {caption}\n\n";
		}

		public string MakeItem(string valueType, string entry)
		{
			return $" * **{valueType}**: {entry}\n\n";
		}
	}

	public interface IStat<out TStat>
    { 
		TStat Process(IEnumerable<double> element);
    }

    public class MeanAndStdStat : IStat<MeanAndStd>
    {
        public MeanAndStd Process(IEnumerable<double> elements)
        {
			var data = elements.ToList();
			var mean = data.Average();
			var std = Math.Sqrt(data.Select(z => Math.Pow(z - mean, 2)).Sum() / (data.Count - 1));

			return new MeanAndStd
			{
				Mean = mean,
				Std = std
			};
		}
    }

	public class MedianStat : IStat<double>
	{
		public double Process(IEnumerable<double> elements)
		{
			var list = elements.OrderBy(z => z).ToList();
			if (list.Count % 2 == 0)
				return (list[list.Count / 2] + list[list.Count / 2 - 1]) / 2;

			return list[list.Count / 2];
		}
	}

	public static class ReportMakerHelper
	{
		public static string MeanAndStdHtmlReport(IEnumerable<Measurement> measurementData)
		{ 
			return new ReportMaker<Html, MeanAndStd>(
				"Mean and Std",
				(elements) =>  new MeanAndStdStat().Process(elements)
			).MakeReport(measurementData);
		}

		public static string MedianMarkdownReport(IEnumerable<Measurement> data)
		{ 
			return new ReportMaker<Markdown, double>(
				"Median",
				(elements) => new MedianStat().Process(elements)
			).MakeReport(data);
		}

		public static string MeanAndStdMarkdownReport(IEnumerable<Measurement> measurements)
		{ 
			return new ReportMaker<Markdown, MeanAndStd>(
				"Mean and Std",
				(elements) => new MeanAndStdStat().Process(elements)
			).MakeReport(measurements);
		}

		public static string MedianHtmlReport(IEnumerable<Measurement> measurements)
		{
			return new ReportMaker<Html, double>(
				"Median",
				(elements) => new MedianStat().Process(elements)
			).MakeReport(measurements);
		}
	}
}
